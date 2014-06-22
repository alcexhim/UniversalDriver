using System;

namespace UniversalDriver.Devices.ContourDesign.Shuttle
{
	public class Shuttle : USBDevice
	{
		private int mvarJogWheelPosition = 0;
		public int ScrollPosition { get { return mvarJogWheelPosition; } }
		
		private bool mvarJogWheelPositionChanged = false;
		
		public event WheelPositionChangedEventHandler ShuttleWheelPositionChanged;
		protected virtual void OnShuttleWheelPositionChanged(WheelPositionChangedEventArgs e)
		{
			if (ShuttleWheelPositionChanged != null) ShuttleWheelPositionChanged(this, e);
		}
		
		public event WheelPositionChangedEventHandler JogWheelPositionChanged;
		protected virtual void OnJogWheelPositionChanged(WheelPositionChangedEventArgs e)
		{
			if (JogWheelPositionChanged != null) JogWheelPositionChanged(this, e);
		}
		
		public event ButtonEventHandler ButtonPressed;
		protected virtual void OnButtonPressed(ButtonEventArgs e)
		{
			if (ButtonPressed != null) ButtonPressed(this, e);
		}
		public event ButtonEventHandler ButtonReleased;
		protected virtual void OnButtonReleased(ButtonEventArgs e)
		{
			if (ButtonReleased != null) ButtonReleased(this, e);
		}
		
		public Shuttle(string path) : base(path)
		{
		}
		
		protected override void ReadPacket(System.IO.BinaryReader br)
		{
			int packetTime = br.ReadInt32 ();
			int w02 = br.ReadInt32 ();
			
			int w03a = br.ReadInt32();
			
			int w04 = br.ReadInt32 ();
			
			short w05 = br.ReadInt16 ();
			short w06 = br.ReadInt16 ();
			short w07 = br.ReadInt16 ();
			short shuttleWheelDirection = br.ReadInt16 ();
			
			/*
			Console.WriteLine ("Packet time: " + packetTime.ToString ());
			Console.WriteLine ("W02: " + w02.ToString ());
			
			Console.WriteLine ("W03a: " + w03a.ToString ());
			
			Console.WriteLine ("W04: " + w04.ToString ());
			
			Console.Write ("Event type: " + w05.ToString ());
			switch (w05)
			{
				case 1:
				{
					Console.WriteLine (" (button)");
					break;
				}
				case 2:
				{
					Console.WriteLine (" (scroll)");
					break;
				}
				default:
				{
					Console.WriteLine (" (unknown)");
					break;
				}
			}
			
			switch (w05)
			{
				case 1:
				{
					Console.WriteLine ("Button ID: " + w06.ToString ());
					Console.Write ("Button state: " + w07.ToString ());
					switch (w07)
					{
						case 0:
						{
							Console.WriteLine (" (released)");
							break;
						}
						case 1:
						{
							Console.WriteLine (" (pressed)");
							break;
						}
					}
					break;
				}
				case 2:
				{
					Console.Write ("Source: " + w06.ToString ());
					switch (w06)
					{
						case 7:
						{
							Console.WriteLine (" (Scroll Wheel)");
							break;
						}
					}
					Console.WriteLine ("Scroll position: " + w07.ToString ());
					break;
				}
			}
			*/
			
			switch (w05)
			{
				case 1: // button
				{
					switch (w07)
					{
						case 0:
						{
							// released
							OnButtonReleased(new ButtonEventArgs(w06, false));
							break;
						}
						case 1:
						{
							OnButtonPressed(new ButtonEventArgs(w06, true));
							break;
						}
					}
					break;
				}
				case 2: // scroll or jog
				{
					switch (w06)
					{
						case 7:
						{
							// scroll
							if (!mvarJogWheelPositionChanged)
							{
								mvarJogWheelPositionChanged = true;
								mvarJogWheelPosition = w07;
							}
							else
							{
								if (mvarJogWheelPosition != w07)
								{
									int position = w07;
									WheelDirection direction = WheelDirection.Forward;
									if ((position < mvarJogWheelPosition && !(position == 1 && mvarJogWheelPosition == 255)) || (position == 255 && mvarJogWheelPosition == 1))
									{
										direction = WheelDirection.Backward;
									}
									OnJogWheelPositionChanged(new WheelPositionChangedEventArgs(position, direction));
									mvarJogWheelPosition = w07;
								}
							}
							break;
						}
						case 8:
						{
							// jog wheel
							int position = w07;
							WheelDirection direction = WheelDirection.Forward;
							switch (shuttleWheelDirection)
							{
								case -1:
								{
									direction = WheelDirection.Backward;
									break;
								}
								case 0:
								{
									direction = WheelDirection.Forward;
									break;
								}
							}
							OnShuttleWheelPositionChanged(new WheelPositionChangedEventArgs(position, direction));
							break;
						}
					}
					break;
				}
			}
		}
	}
}

