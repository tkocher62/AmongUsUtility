using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmongUsBot
{
	class TcpServer
	{
		private string ip;
		private int port;
		internal static TcpListener listener;
		private static TcpClient client;

		public TcpServer(string ip, int port)
		{
			this.ip = ip;
			this.port = port;
		}

		public void Init()
		{
			try
			{
				listener = new TcpListener(IPAddress.Parse(ip), port);
				listener.Start();

				new Thread(AcceptClient).Start();
			}
			catch (Exception x)
			{
				Console.WriteLine("Failed to create listener.");
			}
		}

		private void AcceptClient()
		{
			while (true)
			{
				try
				{
					client = listener.AcceptTcpClient();

					new Thread(Listen).Start();
				}
				catch (Exception x)
				{
					Console.WriteLine("Failed to create server, retrying in 10 seconds...");
				}
				Thread.Sleep(10000);
			}
		}

		private void Listen()
		{
			while (client.Connected)
			{
				try
				{
					byte[] data = new Byte[256];
					String responseData = String.Empty;
					Int32 bytes = client.GetStream().Read(data, 0, data.Length);
					responseData = Encoding.ASCII.GetString(data, 0, bytes);

					JObject o = (JObject)JToken.FromObject(JsonConvert.DeserializeObject(responseData));
					TcpCommandHandler.HandleCommand(o);
				}
				catch (Exception x)
				{
					Console.WriteLine("AmongUsBot listener error: " + x.Message);
				}
			}
			new Thread(AcceptClient).Start();
		}
	}
}
