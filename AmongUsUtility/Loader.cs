using AmongUsUtility;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loader
{
	class Load
	{
		[DllImport("kernel32.dll",
			EntryPoint = "GetStdHandle",
			SetLastError = true,
			CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll",
			EntryPoint = "AllocConsole",
			SetLastError = true,
			CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall)]
		private static extern int AllocConsole();

		private const int STD_OUTPUT_HANDLE = -11;
		private const int MY_CODE_PAGE = 437;

		public static void Init()
		{
			new Thread(Begin).Start();
		}

		private static void Begin()
		{
			Hooks.DoHook();

			AllocConsole();
			IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
			SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
			FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
			Encoding encoding = Encoding.GetEncoding(MY_CODE_PAGE);
			StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
			standardOutput.AutoFlush = true;
			System.Console.SetOut(standardOutput);

			TcpClient.Init("127.0.0.1", 7878);
		}
	}
}
