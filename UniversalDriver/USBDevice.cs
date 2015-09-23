using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UniversalDriver
{
	public abstract class USBDevice : Device
	{
		public USBDevice ()
		{
		}

		private USBDeviceIdentity.USBDeviceIdentityCollection mvarIdentities = new USBDeviceIdentity.USBDeviceIdentityCollection();
		public USBDeviceIdentity.USBDeviceIdentityCollection Identities { get { return mvarIdentities; } }
		
		protected abstract void ReadPacket(System.IO.BinaryReader br);
		
		private System.Threading.Thread _thread = null;
		private void _thread_ThreadStart()
		{
            switch (System.Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
					/*
                    System.IO.FileStream file = System.IO.File.Open(mvarPath, System.IO.FileMode.Open);
                    System.IO.BinaryReader br = new System.IO.BinaryReader(file);

                    while (true)
                    {
                        ReadPacket(br);
                    }
					*/
					break;
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                {
					string pathName = null; // devicePaths[devicePaths.Count - 1];
                    IntPtr fileHandle = Internal.Windows.Methods.CreateFile(pathName, Internal.Windows.Constants.CreateFileDesiredAccess.GenericRead, Internal.Windows.Constants.CreateFileShareMode.Read, IntPtr.Zero, Internal.Windows.Constants.CreateFileCreationDisposition.OpenExisting, Internal.Windows.Constants.CreateFileAttributes.Normal, IntPtr.Zero);
                    int err = Internal.Windows.Methods.GetLastError();
                    if (err != 0) throw new System.ComponentModel.Win32Exception(err);

                    byte[] xbuffer = new byte[1024];
                    int readLength = 0;
                    bool retval1 = Internal.Windows.Methods.ReadFile(fileHandle, xbuffer, xbuffer.Length, ref readLength, IntPtr.Zero);
                    
                    err = Internal.Windows.Methods.GetLastError();
                    if (err != 0) throw new System.ComponentModel.Win32Exception(err);

                    break;
                }
                default:
                {
                    throw new PlatformNotSupportedException();
                }
            }
		}
		
		public void Start()
		{
			if (_thread == null) _thread = new System.Threading.Thread(_thread_ThreadStart);
			_thread.Start();
		}
		public void Stop()
		{
			if (_thread == null) return;
			_thread.Abort();
		}

		public static USBDevice[] Get()
		{
			List<USBDevice> list = new List<USBDevice>();

			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
				{
					IntPtr hDevInfo = Internal.Windows.Methods.SetupDiGetClassDevs(ref Internal.Windows.Constants.DeviceClassGUID.UsbDevice, null, IntPtr.Zero, Internal.Windows.Constants.SetupDiGetClassDevsFlags.DeviceInterfaces | Internal.Windows.Constants.SetupDiGetClassDevsFlags.Present);
					System.Collections.Generic.List<Internal.Windows.Structures.SP_DEVICE_INTERFACE_DATA> listDevs = new System.Collections.Generic.List<Internal.Windows.Structures.SP_DEVICE_INTERFACE_DATA>();

					System.Collections.Generic.List<string> devicePaths = new System.Collections.Generic.List<string>();

					int devinfo_memberIndex = 0;
					while (true)
					{
						int memberIndex = 0;

						Internal.Windows.Structures.SP_DEVINFO_DATA devinfo_data = new Internal.Windows.Structures.SP_DEVINFO_DATA();
						devinfo_data.cbSize = Marshal.SizeOf(devinfo_data);
						bool devinfo_retval = Internal.Windows.Methods.SetupDiEnumDeviceInfo(hDevInfo, devinfo_memberIndex, out devinfo_data);
						int devinfo_lastError = Internal.Windows.Methods.GetLastError();
						if (devinfo_lastError == 259)
						{
							// no more data is available
							break;
						}

						if (devinfo_lastError != 0) throw new System.ComponentModel.Win32Exception(devinfo_lastError);

						while (true)
						{
							Internal.Windows.Structures.SP_DEVICE_INTERFACE_DATA data = new Internal.Windows.Structures.SP_DEVICE_INTERFACE_DATA();
							data.cbSize = Marshal.SizeOf(data);

							bool retval = Internal.Windows.Methods.SetupDiEnumDeviceInterfaces(hDevInfo, ref devinfo_data, ref Internal.Windows.Constants.DeviceClassGUID.UsbDevice, memberIndex, out data);

							int lastError = Internal.Windows.Methods.GetLastError();
							if (lastError == 259)
							{
								// no more data is available
								break;
							}

							if (lastError != 0) throw new System.ComponentModel.Win32Exception(lastError);

							listDevs.Add(data);
							memberIndex++;
						}
						devinfo_memberIndex++;
					}

					for (int i = 0; i < listDevs.Count; i++)
					{
						Internal.Windows.Structures.SP_DEVICE_INTERFACE_DATA data = listDevs[i];

						Internal.Windows.Structures.SP_DEVINFO_DATA devinfo_data = new Internal.Windows.Structures.SP_DEVINFO_DATA();
						devinfo_data.cbSize = Marshal.SizeOf(devinfo_data);

						int requiredSize = 0;
						bool retval = Internal.Windows.Methods.SetupDiGetDeviceInterfaceDetail(hDevInfo, ref data, IntPtr.Zero, 0, out requiredSize, ref devinfo_data);
						if (!retval)
						{
							int lastError = Internal.Windows.Methods.GetLastError();
							if (lastError == 87) continue;
							if (lastError != 0 && lastError != 122) throw new System.ComponentModel.Win32Exception(lastError);
						}

						int dummy = 0;

						IntPtr buffer = Marshal.AllocHGlobal(requiredSize);
						Internal.Windows.Structures.SP_DEVICE_INTERFACE_DETAIL_DATA detail = new Internal.Windows.Structures.SP_DEVICE_INTERFACE_DETAIL_DATA();
						detail.cbSize = Marshal.SizeOf(typeof(Internal.Windows.Structures.SP_DEVICE_INTERFACE_DETAIL_DATA));
						Marshal.StructureToPtr(detail, buffer, false);

						retval = Internal.Windows.Methods.SetupDiGetDeviceInterfaceDetail(hDevInfo, ref data, buffer, requiredSize, out dummy, ref devinfo_data);

						if (!retval)
						{
							int lastError = Internal.Windows.Methods.GetLastError();
							if (lastError != 0 && lastError != 122) throw new System.ComponentModel.Win32Exception(lastError);
						}

						IntPtr pDevicePath = (IntPtr)((int)buffer + Marshal.SizeOf(typeof(int)));
						string devicePath = Marshal.PtrToStringAuto(pDevicePath);
						Marshal.FreeHGlobal(buffer);

						if (String.IsNullOrEmpty(devicePath)) continue;
						devicePaths.Add(devicePath);
					}

					foreach (string devicePath in devicePaths)
					{
						string[] parts = devicePath.Split(new char[] { '#' });
						if (parts.Length >= 4)
						{
							string pathPrefix = parts[0];
							string vendorAndProductId = parts[1];
							string[] vendorAndProductIdParts = vendorAndProductId.Split(new char[] { '&' });

							string strVendorID = vendorAndProductIdParts[0].Substring(4);
							string strProductID = vendorAndProductIdParts[1].Substring(4);
							
							ushort vendorID = UInt16.Parse(strVendorID, System.Globalization.NumberStyles.HexNumber);
							ushort productID = UInt16.Parse(strProductID, System.Globalization.NumberStyles.HexNumber);

							string unknown1 = parts[2];
							string deviceClassId = parts[3];

							// USBDeviceClass deviceClass = USBDeviceClass.GetByIDs(vendorID, productID);
						}
					}
					break;
				}
			}
			return list.ToArray();
		}
	}
}

