using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalDriver.Internal.Windows
{
    internal static class Methods
    {
        #region kernel32.dll

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateFile(string lpFileName, Constants.CreateFileDesiredAccess dwDesiredAccess, Constants.CreateFileShareMode dwShareMode, IntPtr lpSecurityAttributes, Constants.CreateFileCreationDisposition dwCreationDisposition, Constants.CreateFileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

        /// <summary>
        /// Reads data from the specified file or input/output (I/O) device. Reads occur at the position specified by the file pointer if supported by the device.
        /// </summary>
        /// <param name="hFile">A handle to the device (for example, a file, file stream, physical disk, volume, console buffer, tape drive, socket, communications resource, mailslot, or pipe). The hFile parameter must have been created with read access. For more information, see Generic Access Rights and File Security and Access Rights. For asynchronous read operations, hFile can be any handle that is opened with the FILE_FLAG_OVERLAPPED flag by the CreateFile function, or a socket handle returned by the socket or accept function.</param>
        /// <param name="lpBuffer">A pointer to the buffer that receives the data read from a file or device. This buffer must remain valid for the duration of the read operation. The caller must not use this buffer until the read operation is completed.</param>
        /// <param name="nNumberOfBytesToRead">The maximum number of bytes to be read.</param>
        /// <param name="lpNumberOfBytesRead">A pointer to the variable that receives the number of bytes read when using a synchronous hFile parameter. ReadFile sets this value to zero before doing any work or error checking. Use NULL for this parameter if this is an asynchronous operation to avoid potentially erroneous results. This parameter can be NULL only when the lpOverlapped parameter is not NULL.</param>
        /// <param name="lpOverlapped">A pointer to an OVERLAPPED structure is required if the hFile parameter was opened with FILE_FLAG_OVERLAPPED, otherwise it can be NULL. If hFile is opened with FILE_FLAG_OVERLAPPED, the lpOverlapped parameter must point to a valid and unique OVERLAPPED structure, otherwise the function can incorrectly report that the read operation is complete. For an hFile that supports byte offsets, if you use this parameter you must specify a byte offset at which to start reading from the file or device. This offset is specified by setting the Offset and OffsetHigh members of the OVERLAPPED structure. For an hFile that does not support byte offsets, Offset and OffsetHigh are ignored.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();
        #endregion
        #region setupapi.dll
        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, [MarshalAs(UnmanagedType.LPTStr)] string Enumerator, IntPtr hwndParent, Constants.SetupDiGetClassDevsFlags flags);
        /// <summary>
        /// Returns a <see cref="Structures.SP_DEVINFO_DATA"/> structure that specifies a
        /// device information element in a device information set.
        /// </summary>
        /// <param name="DeviceInfoSet">A handle to the device information set for which to return an <see cref="Structures.SP_DEVINFO_DATA "/> structure that represents a device information element.</param>
        /// <param name="MemberIndex">A zero-based index of the device information element to retrieve.</param>
        /// <param name="DeviceInfoData">A pointer to an <see cref="Structures.SP_DEVINFO_DATA"/> structure to receive information about an enumerated device information element. The caller must set DeviceInfoData.cbSize to sizeof(SP_DEVINFO_DATA).</param>
        /// <returns>Returns TRUE if it is successful; otherwise, it returns FALSE and the logged error can be retrieved with a call to GetLastError.</returns>
        [DllImport("setupapi.dll")]
        public static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, int MemberIndex, out Structures.SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll")]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, ref Structures.SP_DEVINFO_DATA DeviceInfoData, ref Guid InterfaceClassGuid, int MemberIndex, out Structures.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

        /// <summary>
        /// The SetupDiGetDeviceInterfaceDetail function returns details about a device interface.
        /// </summary>
        /// <param name="DeviceInfoSet">A pointer to the device information set that contains the interface for which to retrieve details. This handle is typically returned by <see cref="SetupDiGetClassDevs"/>.</param>
        /// <param name="DeviceInterfaceData">A pointer to an <see cref="Structures.SP_DEVICE_INTERFACE_DATA"/> structure that specifies the interface in DeviceInfoSet for which to retrieve details. A pointer of this type is typically returned by <see cref="SetupDiEnumDeviceInterfaces"/>.</param>
        /// <param name="DeviceInterfaceDetailData">A pointer to an <see cref="Structures.SP_DEVICE_INTERFACE_DETAIL_DATA"/> structure to receive information about the specified interface. This parameter is optional and can be NULL. This parameter must be NULL if DeviceInterfaceDetailSize is zero. If this parameter is specified, the caller must set DeviceInterfaceDetailData.cbSize to sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA) before calling this function. The cbSize member always contains the size of the fixed part of the data structure, not a size reflecting the variable-length string at the end.</param>
        /// <param name="DeviceInterfaceDetailDataSize"><para>The size of the <paramref name="DeviceInterfaceDetailData"/> buffer. The buffer must be at least (offsetof(SP_DEVICE_INTERFACE_DETAIL_DATA, DevicePath) + sizeof(TCHAR)) bytes, to contain the fixed part of the structure and a single NULL to terminate an empty MULTI_SZ string.</para><para>This parameter must be zero if <paramref name="DeviceInterfaceDetailData"/> is NULL.</para></param>
        /// <param name="RequiredSize">A pointer to a variable of type DWORD that receives the required size of the <paramref name="DeviceInterfaceDetailData"/> buffer. This size includes the size of the fixed part of the structure plus the number of bytes required for the variable-length device path string. This parameter is optional and can be NULL.</param>
        /// <param name="DeviceInfoData">A pointer to a buffer that receives information about the device that supports the requested interface. The caller must set <paramref name="DeviceInfoData"/>.cbSize to sizeof(SP_DEVINFO_DATA). This parameter is optional and can be NULL.</param>
        /// <returns>SetupDiGetDeviceInterfaceDetail returns TRUE if the function completed without error. If the function completed with an error, FALSE is returned and the error code for the failure can be retrieved by calling <see cref="GetLastError"/>.</returns>
        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern bool SetupDiGetDeviceInterfaceDetail
        (
            IntPtr DeviceInfoSet,
            ref Structures.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
            ref Structures.SP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData,
            int DeviceInterfaceDetailDataSize,
            out int RequiredSize,
            ref Structures.SP_DEVINFO_DATA DeviceInfoData
        );

        /// <summary>
        /// The SetupDiGetDeviceInterfaceDetail function returns details about a device interface.
        /// </summary>
        /// <param name="DeviceInfoSet">A pointer to the device information set that contains the interface for which to retrieve details. This handle is typically returned by <see cref="SetupDiGetClassDevs"/>.</param>
        /// <param name="DeviceInterfaceData">A pointer to an <see cref="Structures.SP_DEVICE_INTERFACE_DATA"/> structure that specifies the interface in DeviceInfoSet for which to retrieve details. A pointer of this type is typically returned by <see cref="SetupDiEnumDeviceInterfaces"/>.</param>
        /// <param name="DeviceInterfaceDetailData">A pointer to an <see cref="Structures.SP_DEVICE_INTERFACE_DETAIL_DATA"/> structure to receive information about the specified interface. This parameter is optional and can be NULL. This parameter must be NULL if DeviceInterfaceDetailSize is zero. If this parameter is specified, the caller must set DeviceInterfaceDetailData.cbSize to sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA) before calling this function. The cbSize member always contains the size of the fixed part of the data structure, not a size reflecting the variable-length string at the end.</param>
        /// <param name="DeviceInterfaceDetailDataSize"><para>The size of the <paramref name="DeviceInterfaceDetailData"/> buffer. The buffer must be at least (offsetof(SP_DEVICE_INTERFACE_DETAIL_DATA, DevicePath) + sizeof(TCHAR)) bytes, to contain the fixed part of the structure and a single NULL to terminate an empty MULTI_SZ string.</para><para>This parameter must be zero if <paramref name="DeviceInterfaceDetailData"/> is NULL.</para></param>
        /// <param name="RequiredSize">A pointer to a variable of type DWORD that receives the required size of the <paramref name="DeviceInterfaceDetailData"/> buffer. This size includes the size of the fixed part of the structure plus the number of bytes required for the variable-length device path string. This parameter is optional and can be NULL.</param>
        /// <param name="DeviceInfoData">A pointer to a buffer that receives information about the device that supports the requested interface. The caller must set <paramref name="DeviceInfoData"/>.cbSize to sizeof(SP_DEVINFO_DATA). This parameter is optional and can be NULL.</param>
        /// <returns>SetupDiGetDeviceInterfaceDetail returns TRUE if the function completed without error. If the function completed with an error, FALSE is returned and the error code for the failure can be retrieved by calling <see cref="GetLastError"/>.</returns>
        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern bool SetupDiGetDeviceInterfaceDetail
        (
            IntPtr DeviceInfoSet,
            ref Structures.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
            IntPtr NullDeviceInterfaceDetailData,
            int DeviceInterfaceDetailDataSize,
            out int RequiredSize,
            ref Structures.SP_DEVINFO_DATA DeviceInfoData
        );
        #endregion

    }
}
