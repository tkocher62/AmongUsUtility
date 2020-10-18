using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AmongUsUtility
{
	class Main
	{
		private static bool isGameStarted = false;

		private static GameData.IHEKEPMDGIJ GetRandomPlayer()
		{
			List<GameData.IHEKEPMDGIJ> list = new List<GameData.IHEKEPMDGIJ>();
			foreach (var a in GameData.Instance.AllPlayers)
			{
				PlayerInfoProxy me = new PlayerInfoProxy(a);
				if (me.PlayerName != "cy")
				{
					list.Add(a);
				}
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		private static GameData.IHEKEPMDGIJ GetMe()
		{
			foreach (var a in GameData.Instance.AllPlayers)
			{
				PlayerInfoProxy me = new PlayerInfoProxy(a);
				if (me.PlayerName == "cy")
				{
					return a;
				}
			}
			return null;
		}

		public static void OnUpdate(IntPtr @this)
		{
			Hooks.o_OnUpdate(@this);

			if (Input.GetKeyDown(KeyCode.F1))
			{
				GetRandomPlayer().HIHKBDLNLMN.CmdReportDeadBody(null);
			}
			else if (Input.GetKeyDown(KeyCode.F2))
			{
				GetRandomPlayer().HIHKBDLNLMN.CmdReportDeadBody(GetRandomPlayer());
			}
			else if (Input.GetKeyDown(KeyCode.F3))
			{
				foreach (var a in GameData.Instance.AllPlayers)
				{
					var player = new PlayerInfoProxy(a);
					if (player.IsImpostor)
					{
						player._object.SetKillTimer(0);
						player._object.MurderPlayer(GetRandomPlayer().HIHKBDLNLMN);

						break;
					}
				}
			}
			else if (Input.GetKeyDown(KeyCode.F4))
			{
				GetMe().HIHKBDLNLMN.Die(NPLMBOLMMLB.Kill);
			}
		}

		public static void Exit(IntPtr @this)
		{
			Hooks.o_Exit(@this);

			//Process.Start("AmongUsSQL.exe", $"endgame");
			System.Console.WriteLine($"-- Game Ended ({(HEEEEBPANNA.MFIPHLKOFHG[0].LODLBBJNGKB ? "Imposter" : "Crewmate")} Win) --");
			System.Console.WriteLine($"Winners:");
			// For each player in temp winners
			string b = string.Empty;
			for (int i = 0; i < HEEEEBPANNA.MFIPHLKOFHG.Count; i++)
			{
				// AMBMBLABCCO - name
				// LMLKMLAHPDO - isYou
				// LODLBBJNGKB - isImposter

				AMCELEOOFNB player = HEEEEBPANNA.MFIPHLKOFHG[i];
				b += player.AMBMBLABCCO;
				if (i != HEEEEBPANNA.MFIPHLKOFHG.Count - 1) b += "|";
				System.Console.WriteLine(" - " + player.AMBMBLABCCO);
			}
			System.Console.WriteLine(b);
			Process.Start("AmongUsSQL.exe", $"wins {b}");
			Process.Start("AmongUsSQL.exe", $"enabletalk");
			isGameStarted = false;
		}

		public static void CompleteTask(IntPtr @this)
		{
			Hooks.o_CompleteTask(@this);

			/*foreach (GameData.IHEKEPMDGIJ playerinfo in GameData.Instance.AllPlayers)
			{
				foreach (GameData.CBOMPDNBEIF task in playerinfo.IHACFCJPFCF)
				{
					System.Console.WriteLine(task.AKLEDCMKHMC);
				}
			}*/

			System.Console.WriteLine("completed task");
		}

		public static void CallMeeting(IntPtr @this)
		{
			Hooks.o_CallMeeting(@this);

			System.Console.WriteLine("starting meeting - 1");
			Process.Start("AmongUsSQL.exe", $"enabletalk");
		}

		public static void EndMeeting(IntPtr @this)
		{
			Hooks.o_EndMeeting(@this);

			System.Console.WriteLine("ending meeting");
			Process.Start("AmongUsSQL.exe", "disabletalk");
		}

		public static void StartGame(IntPtr @this)
		{
			Hooks.o_StartGame(@this);

			System.Console.WriteLine("starting game - " + isGameStarted);
			Process.Start("AmongUsSQL.exe", isGameStarted ? "enabletalk" : "disabletalk");
			isGameStarted = true;
		}
	}
}
