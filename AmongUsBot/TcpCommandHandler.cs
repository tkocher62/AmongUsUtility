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
					string username = (string)o["username"];
					Console.WriteLine(username);
					foreach (string user in username.Split('|'))
					{
						Console.WriteLine("giving user '" + user + "' a win");
						SQL.UpdatePlayerData(user, type, SQL.GetPlayerData(user, type) + 1);
					}
				}
				else
				{
					string username = (string)o["username"];
					SQL.UpdatePlayerData(username, type, SQL.GetPlayerData(username, type) + 1);
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
