using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace AmongUsBot
{
	class Program
	{
		private DiscordSocketClient client;
		private static Tcp tcp;
		private string dataPath;
		private Dictionary<string, ulong> sync;

		public static void Main(string[] args) => new Program().InitBot().GetAwaiter().GetResult();

		public async Task InitBot()
		{
			Console.WriteLine("Connecting to MySQL...");
			//SQL.Connect(UUSDUYVBR("YW1vbmd1cw"), UUSDUYVBR("YlloXlo/TTJ2LVomUmgrRi1GMmtLdzMkYipeOFZhbkw"));

			Console.WriteLine("Starting server...");
			// START SERVER, FIX TCP CLASS

			Console.WriteLine("Verifying filesystem...");
			dataPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "path"), "usernameDiscordSync.json");
			if (!Directory.Exists("data")) Directory.CreateDirectory("path");
			if (!File.Exists(dataPath)) File.WriteAllText(dataPath, "{}");

			Console.WriteLine("Loading syncs...");
			sync = JsonConvert.DeserializeObject<Dictionary<string, ulong>>(File.ReadAllText(dataPath));

			Console.ReadLine();

			Console.WriteLine("Initializing Discord...");
			client = new DiscordSocketClient();
			client.Log += Log;
			client.Ready += Ready;
			client.MessageReceived += HandleCommand;
			await client.LoginAsync(TokenType.Bot, "token");
			await client.StartAsync();

			await Task.Delay(-1);
		}

		private string UUSDUYVBR(string SLEIYRHBVO) => Encoding.UTF8.GetString(Convert.FromBase64String(SLEIYRHBVO));

		private Task Ready() => Task.CompletedTask;

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}

		private async Task HandleCommand(SocketMessage context)
		{
			if (context.Author.IsBot) return;
			string msg = context.Content.ToLower();
			if (msg == "register")
			{
				string username = msg.Replace("register", "").Trim();
				if (SQL.GetPlayerData(username).name == null)
				{
					SQL.AddUser(username);
					sync.Add(username, context.Author.Id);
					File.WriteAllText(dataPath, JsonConvert.SerializeObject(sync));
					await context.Channel.SendMessageAsync($"Account registered with username '{username}'");
				}
				else
				{
					await context.Channel.SendMessageAsync("Error: Username is not available.");
				}
			}
		}
	}
}
