using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmongUsBot
{
	internal class SQL
	{
		private static MySqlConnection con;

		internal enum StatKey
		{
			wins = 0,
			kills = 1,
			deaths = 2,
			tasks_completed = 3
		}

		internal static bool Connect(string u, string p)
		{
			con = new MySqlConnection($@"server=192.168.1.204;userid={u};password={p};database=among_us");
			con.Open();

			return IsConnected();
		}

		internal static bool IsConnected() => con.State == System.Data.ConnectionState.Open;

		internal static string GetServerVersion() => con.ServerVersion;

		internal static PlayerData GetPlayerData(string name)
		{
			if (IsConnected())
			{
				MySqlDataReader rdr = new MySqlCommand($"SELECT * FROM player_data WHERE name = '{name}'", con).ExecuteReader();
				PlayerData p = new PlayerData();
				while (rdr.Read())
				{
					p.name = rdr.GetString(0);
					p.wins = rdr.GetInt32(1);
					p.kills = rdr.GetInt32(2);
					p.deaths = rdr.GetInt32(3);
					p.tasksCompleted = rdr.GetInt32(4);
				}
				rdr.Close();
				return p;
			}
			return null;
		}

		internal static void Query(string cmd)
		{
			if (IsConnected())
			{
				new MySqlCommand(cmd, con).ExecuteNonQuery();
			}
		}

		internal static void UpdatePlayerData(string name, StatKey key, int value)
		{
			Query($"UPDATE player_data SET {key} = {value} WHERE name = '{name}'");
		}

		internal static void AddUser(string name)
		{
			Query($"INSERT INTO player_data(name) VALUES('{name}')");
		}

		internal static void DeleteUser(string name)
		{
			Query($"DELETE FROM player_data WHERE name = '{name}'");
		}
	}
}
