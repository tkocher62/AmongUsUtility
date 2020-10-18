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
		internal static DiscordSocketClient client;
		private string dataPath;
		internal static Dictionary<ulong, string> sync;

		private string prefix = ">";
		private ulong cyanox = 111676166556917760;

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
			sync = JsonConvert.DeserializeObject<Dictionary<ulong, string>>(File.ReadAllText(dataPath));
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
			if (msg.StartsWith($"{prefix}register"))
			{
				string username = msg.Replace($"{prefix}register", "").Trim();
				if (username.Length != 0)
				{
					if (!sync.ContainsKey(context.Author.Id))
					{
						if (SQL.GetPlayerData(username).name == null)
						{
							SQL.AddUser(username);
							sync.Add(context.Author.Id, username);
							File.WriteAllText(dataPath, JsonConvert.SerializeObject(sync, Formatting.Indented));
							await context.Channel.SendMessageAsync($"Account registered with username '{username}'.");
						}
						else
						{
							await context.Channel.SendMessageAsync("Error: Username is not available.");
						}
					}
					else
					{
						await context.Channel.SendMessageAsync($"You have already registered the username '{sync[context.Author.Id]}'.");
					}
				}
				else
				{
					await context.Channel.SendMessageAsync($"You must provide a username.");
					return;
				}
			}
			else if (msg == $"{prefix}unregister")
			{
				string username = sync[context.Author.Id];
				if (username != string.Empty)
				{
					if (SQL.GetPlayerData(username).name != null)
					{
						SQL.DeleteUser(username);
						sync.Remove(context.Author.Id);
						File.WriteAllText(dataPath, JsonConvert.SerializeObject(sync, Formatting.Indented));
						await context.Channel.SendMessageAsync($"Account '{username}' unregistered.");
					}
				}
				else
				{
					await context.Channel.SendMessageAsync("Error: You are not registered.");
				}
			}
			else if (msg == $"{prefix}stats")
			{
				if (sync.ContainsKey(context.Author.Id))
				{
					string username = sync[context.Author.Id];
					PlayerData data = SQL.GetPlayerData(username);

					EmbedBuilder builder = new EmbedBuilder();

					builder.WithTitle($"{context.Author.Username} ({username})'s Stats");
					builder.AddField("Wins", data.wins, false);
					//builder.AddField("Kills", data.kills, false);
					//builder.AddField("Deaths", data.deaths, false);
					//builder.AddField("Tasks Completed", data.tasksCompleted, false);
					//builder.WithThumbnailUrl("https://i.dlpng.com/static/png/6481182_preview.png");
					builder.WithCurrentTimestamp();
					builder.WithFooter("Among Us Leaderboard by Cyanox");
					builder.WithColor(Color.Green);
					await context.Channel.SendMessageAsync("", false, builder.Build());
				}
				else
				{
					await context.Channel.SendMessageAsync("Error: You must register a username to view your stats.");
				}
			}
			else if (msg.StartsWith($"{prefix}forcedel") && context.Author.Id == cyanox)
			{
				string username = msg.Replace($"{prefix}forcedel", "").Trim();
				SQL.DeleteUser(username);
				await context.Channel.SendMessageAsync($"Account '{username}' deleted.");
			}
			else if (msg.StartsWith($"{prefix}setstat") && context.Author.Id == cyanox)
			{
				string[] split = msg.Replace($"{prefix}forcedel", "").Trim().Split(' ');
				// not done lol
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
