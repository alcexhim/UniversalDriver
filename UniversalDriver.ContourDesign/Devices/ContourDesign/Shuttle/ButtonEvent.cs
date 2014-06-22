using System;

namespace UniversalDriver.Devices.ContourDesign.Shuttle
{
	public delegate void ButtonEventHandler(object sender, ButtonEventArgs e);
	public class ButtonEventArgs
	{
		private int mvarButton = 0;
		public int Button { get { return mvarButton; } }
		
		private bool mvarPressed = false;
		public bool Pressed { get { return mvarPressed; } }
		
		public ButtonEventArgs(int button, bool pressed)
		{
			mvarButton = button;
			mvarPressed = pressed;
		}
	}
}

