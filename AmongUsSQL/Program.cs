namespace AmongUsSQL
{
	class Program
	{
		static void Main(string[] args)
		{
			Tcp tcp = new Tcp("127.0.0.1", 7878);
			tcp.Init();

			tcp.SendData(new Data()
			{
				type = args[0],
				args = args.Length == 2 ? args[1] : null
			});
		}
	}
}
