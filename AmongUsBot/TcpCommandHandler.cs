using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmongUsBot
{
	internal static class TcpCommandHandler
	{
		internal static void HandleCommand(JObject o)
		{
			try
			{
				string type = (string)o["type"];
				string username = (string)o["username"];
				SQL.UpdatePlayerData(username, type, SQL.GetPlayerData(username, type) + 1);
			}
			catch (Exception x)
			{
				Console.WriteLine("AmongUsBot handle command error: " + x.Message);
			}
		}
	}
}
