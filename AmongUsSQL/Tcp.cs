using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace AmongUsSQL
{
	class Tcp
	{
		private string ip;
		private int port;
		internal static Socket socket;

		public Tcp(string ip, int port)
		{
			this.ip = ip;
			this.port = port;
		}

		public void Init()
		{
			try
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.Connect(ip, port);
				if (!socket.Connected) Environment.Exit(1);
			}
			catch { }
		}

		public void SendData(object data) => socket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));

		public void SendData(byte[] data) => socket.Send(data);
	}
}
