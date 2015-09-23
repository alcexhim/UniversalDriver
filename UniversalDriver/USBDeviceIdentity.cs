using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalDriver
{
	public class USBDeviceIdentity
	{
		public class USBDeviceIdentityCollection
			: System.Collections.ObjectModel.Collection<USBDeviceIdentity>
		{

		}

		private short mvarVendorID = 0;
		public short VendorID { get { return mvarVendorID; } set { mvarVendorID = value; } }

		private short mvarProductID = 0;
		public short ProductID { get { return mvarProductID; } set { mvarProductID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public USBDeviceIdentity()
		{

		}
		public USBDeviceIdentity(short vendorID, short productID, string title)
		{
			mvarVendorID = vendorID;
			mvarProductID = productID;
			mvarTitle = title;
		}
	}
}
