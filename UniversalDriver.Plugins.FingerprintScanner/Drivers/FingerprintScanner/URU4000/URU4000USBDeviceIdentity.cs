using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalDriver.Drivers.FingerprintScanner.URU4000
{
	public class URU4000USBDeviceIdentity : USBDeviceIdentity
	{
		private bool mvarAuthCR = false;
		public bool AuthCR { get { return mvarAuthCR; } }

		private bool mvarEncryption = false;
		public bool Encryption { get { return mvarEncryption; } }

		public URU4000USBDeviceIdentity()
		{

		}

		public URU4000USBDeviceIdentity(short vendorID, short productID, string title, bool auth_cr, bool encryption)
			: base(vendorID, productID, title)
		{
			mvarAuthCR = auth_cr;
			mvarEncryption = encryption;
		}
	}
}
