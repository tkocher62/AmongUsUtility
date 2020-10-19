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
				string args = (string)o["args"];
				Console.WriteLine(o);
				if (type == "enabletalk")
				{
					ChangeGameState(false);
				}
				else if (type == "disabletalk")
				{
					ChangeGameState(true);
				}
				else if (type == "wins")
				{
					Console.WriteLine(args);
					foreach (string user in args.Split('|'))
					{
						Console.WriteLine("giving user '" + user + "' a win");
						SQL.UpdatePlayerData(user, type, SQL.GetPlayerData(user, type) + 1);
					}
				}
				else
				{
					SQL.UpdatePlayerData(args, type, SQL.GetPlayerData(args, type) + 1);
				}
			}
			catch (Exception x)
			{
				Console.WriteLine("AmongUsBot handle command error: " + x.Message);
			}
		}

		private static async void ChangeGameState(bool mute)
		{
			foreach (var s in Program.sync)
			{
				var guild = Program.client.GetGuild(663897360673275935);
				var guildUser = guild.GetUser(111676166556917760);
				//var guildUser = guild.GetUser(s.Key);
				await guildUser.ModifyAsync(x => x.Mute = mute);
			}
		}
	}
}
