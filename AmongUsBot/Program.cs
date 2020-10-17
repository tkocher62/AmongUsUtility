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
		private string dataPath;
		private Dictionary<string, ulong> sync;

		public static void Main(string[] args) => new Program().InitBot().GetAwaiter().GetResult();

		public async Task InitBot()
		{
			Console.Write("Connecting to MySQL...");
			SQL.Connect(UUSDUYVBR("YW1vbmd1cw=="), UUSDUYVBR("YlloXlo/TTJ2LVomUmgrRi1GMmtLdzMkYipeOFZhbkw="));
			Console.WriteLine(" Done!");

			Console.Write("Verifying filesystem...");
			dataPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "path"), "usernameDiscordSync.json");
			if (!Directory.Exists("data")) Directory.CreateDirectory("path");
			if (!File.Exists(dataPath)) File.WriteAllText(dataPath, "{}");
			Console.WriteLine(" Done!");

			Console.Write("Starting server...");
			TcpServer tcp = new TcpServer("127.0.0.1", 7878);
			tcp.Init();
			Console.WriteLine(" Done!");

			Console.Write("Loading syncs...");
			sync = JsonConvert.DeserializeObject<Dictionary<string, ulong>>(File.ReadAllText(dataPath));
			Console.WriteLine(" Done!\n");

			client = new DiscordSocketClient();
			client.Log += Log;
			client.Ready += Ready;
			client.MessageReceived += HandleCommand;
			await client.LoginAsync(TokenType.Bot, "NzY2NzE1NjM3NzI0MzQ4NDM3.X4nZlA.YWe7iYgaWDsval3Z_SNQTaN9RpQ");
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
			Console.WriteLine(msg);
			if (msg.StartsWith(";register"))
			{
				string username = msg.Replace(";register", "").Trim();
				if (SQL.GetPlayerData(username).name == null)
				{
					SQL.AddUser(username);
					sync.Add(username, context.Author.Id);
					File.WriteAllText(dataPath, JsonConvert.SerializeObject(sync, Formatting.Indented));
					await context.Channel.SendMessageAsync($"Account registered with username '{username}'.");
				}
				else
				{
					await context.Channel.SendMessageAsync("Error: Username is not available.");
				}
			}
			else if (msg.StartsWith(";unregister"))
			{
				string username = msg.Replace(";unregister", "").Trim();
				if (SQL.GetPlayerData(username).name != null)
				{
					SQL.DeleteUser(username);
					sync.Remove(username);
					File.WriteAllText(dataPath, JsonConvert.SerializeObject(sync));
					await context.Channel.SendMessageAsync($"Account '{username}' unregistered.");
				}
				else
				{
					await context.Channel.SendMessageAsync("Error: You are not registered.");
				}
			}
			else if (msg == "fart")
			{
				await context.Channel.SendMessageAsync("**pfffpfpfpfpfp**");
			}
			else if (msg == "poop")
			{
				await context.Channel.SendMessageAsync("**pppft**");
			}
		}
	}
}
