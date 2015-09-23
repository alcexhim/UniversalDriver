using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalDriver.Drivers.FingerprintScanner.URU4000
{
	public class URU4000Device : USBDevice
	{
		public URU4000Device()
		{
			this.Identities.Add(new URU4000USBDeviceIdentity(0x045E, 0x00BB, "Microsoft Keyboard with Fingerprint Reader", false, false));
			this.Identities.Add(new URU4000USBDeviceIdentity(0x045E, 0x00BC, "Microsoft Wireless IntelliMouse with Fingerprint Reader", false, false));
			this.Identities.Add(new URU4000USBDeviceIdentity(0x045E, 0x00BD, "Microsoft Fingerprint Reader", false, false));
			this.Identities.Add(new URU4000USBDeviceIdentity(0x045E, 0x00CA, "Microsoft Fingerprint Reader v2", true, false));
			this.Identities.Add(new URU4000USBDeviceIdentity(0x05BA, 0x0007, "Digital Persona U.are.U 4000 (Standalone)", false, false));
			this.Identities.Add(new URU4000USBDeviceIdentity(0x05BA, 0x0008, "Digital Persona U.are.U 4000 (Keyboard)", false, false));
			this.Identities.Add(new URU4000USBDeviceIdentity(0x05BA, 0x000A, "Digital Persona U.are.U 4000B (Standalone)", false, true));
		}
		protected override void ReadPacket(System.IO.BinaryReader br)
		{
			throw new NotImplementedException();
		}
	}
}
