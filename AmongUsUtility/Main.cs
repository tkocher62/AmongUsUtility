using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AmongUsUtility
{
	class Main
	{
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

		public static void OnUpdate(IntPtr @this)
		{
			Hooks.o_OnUpdate(@this);

			if (Input.GetKeyDown(KeyCode.F1))
			{
				//GetRandomPlayer().HIHKBDLNLMN.CmdReportDeadBody(null);

				Process.Start("AmongUsSQL.exe", "wins todd");
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
		}

		public static void Exit(IntPtr @this)
		{
			Hooks.o_Exit(@this);

			System.Console.WriteLine($"-- Game Ended ({(HEEEEBPANNA.MFIPHLKOFHG[0].LODLBBJNGKB ? "Imposter" : "Crewmate")} Win) --");
			System.Console.WriteLine($"Winners:");
			// For each player in temp winners
			foreach (var a in HEEEEBPANNA.MFIPHLKOFHG)
			{
				// AMBMBLABCCO - name
				// LMLKMLAHPDO - isYou
				// LODLBBJNGKB - isImposter

				System.Console.WriteLine(" - " + a.AMBMBLABCCO);
			}
		}
	}
}
