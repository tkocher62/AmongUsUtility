using inject;
using System;
using System.Runtime.InteropServices;

namespace AmongUsUtility
{
	public static class Hooks
	{
		public static OnUpdate o_OnUpdate;
		public static Exit o_Exit;
		public static CompleteTask o_CompleteTask;
		public static CallMeeting o_CallMeeting;
		public static EndMeeting o_EndMeeting;
		public static StartGame o_StartGame;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void OnUpdate(IntPtr @this);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void Exit(IntPtr @this);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CompleteTask(IntPtr @this);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallMeeting(IntPtr @this);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void EndMeeting(IntPtr @this);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void StartGame(IntPtr @this);

		internal static void DoHook()
		{
			o_OnUpdate = HookUtils.HookCall<OnUpdate>(0xA70090, Main.OnUpdate);
			o_Exit = HookUtils.HookCall<Exit>(0xE79BF0, Main.Exit);
			o_CompleteTask = HookUtils.HookCall<CompleteTask>(0x88AB50, Main.CompleteTask);
			o_CallMeeting = HookUtils.HookCall<CallMeeting>(0x8DF240, Main.CallMeeting);
			o_EndMeeting = HookUtils.HookCall<EndMeeting>(0xE1B490, Main.EndMeeting);
			o_StartGame = HookUtils.HookCall<StartGame>(0xD59E40, Main.StartGame); // is called when game starts and when meeting starts
		}
	}
}
