using inject;
using System;
using System.Runtime.InteropServices;

namespace AmongUsUtility
{
	public static class Hooks
	{
		public static OnUpdate o_OnUpdate;
		public static Exit o_Exit;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void OnUpdate(IntPtr @this);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void Exit(IntPtr @this);

		internal static void DoHook()
		{
			o_OnUpdate = HookUtils.HookCall<OnUpdate>(0xA70090, Main.OnUpdate);
			o_Exit = HookUtils.HookCall<Exit>(0xE79BF0, Main.Exit);
		}
	}
}
