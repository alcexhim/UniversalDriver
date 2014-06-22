using System;

namespace UniversalDriver
{
	public abstract class USBDevice : Device
	{
		private string mvarPath = String.Empty;
		public string Path { get { return mvarPath; } }
		
		public USBDevice (string path)
		{
			mvarPath = path;
		}
		
		protected abstract void ReadPacket(System.IO.BinaryReader br);
		
		private System.Threading.Thread _thread = null;
		private void _thread_ThreadStart()
		{
			System.IO.FileStream file = System.IO.File.Open(mvarPath, System.IO.FileMode.Open);
			System.IO.BinaryReader br = new System.IO.BinaryReader(file);
			
			while (true)
			{
				ReadPacket(br);
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
	}
}

