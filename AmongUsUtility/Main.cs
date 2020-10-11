using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AmongUsUtility
{
	class Main
	{
		public static void OnUpdate(IntPtr @this)
		{
			Hooks.o_OnUpdate(@this);

			if (Input.GetKeyDown(KeyCode.F1))
			{
				GameData.Instance.AllPlayers[0].HIHKBDLNLMN.CmdReportDeadBody(GameData.Instance.AllPlayers[5]);
			}
		}

		public static void Exit(IntPtr @this)
		{
			Hooks.o_Exit(@this);

			System.Console.WriteLine($"-- Game Ended ({(HEEEEBPANNA.MFIPHLKOFHG[0].LODLBBJNGKB ? "Imposter" : "Crewmate")} Win) --");
			System.Console.WriteLine($"Winners:");
			// For each player
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
