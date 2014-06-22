using System;

namespace UniversalDriver.Devices.ContourDesign.Shuttle
{
	public delegate void WheelPositionChangedEventHandler(object sender, WheelPositionChangedEventArgs e);
	public class WheelPositionChangedEventArgs
	{
		private int mvarPosition = 0;
		public int Position { get { return mvarPosition; } }
		
		private WheelDirection mvarDirection = WheelDirection.Forward;
		public WheelDirection Direction { get { return mvarDirection; } }
		
		public WheelPositionChangedEventArgs (int position, WheelDirection direction)
		{
			mvarPosition = position;
			mvarDirection = direction;
		}
	}
}

