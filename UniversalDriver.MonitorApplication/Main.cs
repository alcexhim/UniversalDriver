using System;
using UniversalDriver.Devices.ContourDesign.Shuttle;
using UniversalDriver.Drivers.FingerprintScanner.URU4000;

namespace UniversalDriver.MonitorApplication
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*
			Shuttle device = new Shuttle("/dev/input/by-id/usb-Contour_Design_ShuttlePRO_v2-event-if00");
			// Shuttle device = new Shuttle("/dev/input/by-id/usb-Contour_Design_ShuttleXpress-event-if00");
			
			device.ButtonPressed += device_ButtonPressed;
			device.ButtonReleased += device_ButtonReleased;
			device.ShuttleWheelPositionChanged += device_ShuttleWheelPositionChanged;
			device.JogWheelPositionChanged += device_JogWheelPositionChanged;
			device.Start();
				*/

			USBDevice[] devices = USBDevice.Get();

			URU4000Device device = new URU4000Device();
			device.Start();


		}

		static void device_JogWheelPositionChanged(object sender, WheelPositionChangedEventArgs e)
		{
			Console.WriteLine ("Jog wheel position: " + e.Position.ToString () + " (" + e.Direction.ToString () + ")");
		}

		private static void device_ButtonPressed (object sender, ButtonEventArgs e)
		{
			Console.WriteLine ("Button " + e.Button.ToString () + " pressed!");
		}
		private static void device_ButtonReleased (object sender, ButtonEventArgs e)
		{
			Console.WriteLine ("Button " + e.Button.ToString () + " released!");
		}
		
		private static void device_ShuttleWheelPositionChanged(object sender, WheelPositionChangedEventArgs e)
		{
			Console.WriteLine ("Shuttle wheel position: " + e.Position.ToString () + " (" + e.Direction.ToString () + ")");
		}
		
	}
}
