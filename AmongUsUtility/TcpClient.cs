using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AmongUsUtility
{
	class TcpClient
	{
		private static string ip;
		private static int port;

		internal static Socket socket;

		internal static void Init(string _ip, int _port)
		{
			try
			{
				ip = _ip;
				port = _port;

				new Thread(AttemptConnection).Start();
			}
			catch (Exception x)
			{
				System.Console.WriteLine("Connection attempt failed: " + x.Message);
			}
		}

		private static void AttemptConnection()
		{
			while (!IsConnected())
			{
				try
				{
					socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.Connect(ip, port);

					new Thread(Listen).Start();
				}
				catch (Exception x)
				{
					// Failed to connect
					System.Console.WriteLine(x.Message);
					//System.Console.WriteLine("Failed to connect to server, retrying in 10 seconds...");
				}
				Thread.Sleep(10000);
			}
		}

		private static void Listen()
		{
			while (IsConnected())
			{
				try
				{
					byte[] a = new byte[1000];
					socket.Receive(a);
					JObject o = (JObject)JToken.FromObject(JsonConvert.DeserializeObject(Encoding.UTF8.GetString(a)));
				}
				catch (Exception x)
				{
					System.Console.WriteLine("Listener error: " + x.Message);
				}
			}
			new Thread(AttemptConnection).Start();
		}

		internal static void SendData(object data)
		{
			socket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
		}

		internal static void SendData(byte[] data)
		{
			socket.Send(data);
		}

		internal static bool IsConnected()
		{
			if (socket == null)
			{
				return false;
			}
			try
			{
				return !((socket.Poll(1000, SelectMode.SelectRead) && (socket.Available == 0)) || !socket.Connected);
			}
			catch (Exception x)
			{
				return false;
			}
		}
	}
}
