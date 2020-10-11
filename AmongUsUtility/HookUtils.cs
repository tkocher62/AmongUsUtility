using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace inject
{
    public class HookUtils
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern IntPtr Hook(IntPtr hookTarget, IntPtr hook);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Unhook(IntPtr hookTarget);

        public static unsafe T HookCall<T>(int rva, T hook) where T : Delegate
        {
            IntPtr baseAddr = GetModuleHandle("GameAssembly.dll");

            IntPtr offset = baseAddr + rva;

            IntPtr hookaddr = Marshal.GetFunctionPointerForDelegate(hook);

            IntPtr original = Hook(offset, hookaddr);

            return Marshal.GetDelegateForFunctionPointer<T>(original);
        }

        public static void Unhook(int rva)
        {
            IntPtr baseAddr = GetModuleHandle("GameAssembly.dll");

            IntPtr offset = baseAddr + rva;

            Unhook(offset);
        }
    }
}
