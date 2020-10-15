using Newtonsoft.Json.Linq;
using System;

namespace AmongUsBot
{
	class TcpCommandHandler
	{
		internal static void HandleCommand(JObject o)
		{
			try
			{
				string type = (string)o["type"];
				if (type == "win")
				{
					// add +1 win to player
				}
				else if (type == "kill")
				{
					// add +1 kill to player
				}
				else if (type == "death")
				{
					// add +1 death to player
				}
				else if (type == "task")
				{
					// add +1 task to player
				}
			}
			catch (Exception x)
			{
				Console.WriteLine("AmongUsBot handle command error: " + x.Message);
			}
		}
	}
}
