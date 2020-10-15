namespace AmongUsUtility.DataObjects
{
	internal enum Type
	{
		win = 0,
		kill = 1,
		death = 2,
		task = 3
	}

	class UpdateData
	{
		public string type;
		public string username;
	}
}
