using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceClicker
{
    public class ClickAction
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx, dy;
            public uint mouseData, dwFlags, time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint n, INPUT[] inputs, int size);

        private const uint LEFTDOWN = 0x0002;
        private const uint LEFTUP = 0x0004;

        public static void LeftClick()
        {
            var inputs = new INPUT[2];
            inputs[0].mi.dwFlags = LEFTDOWN;
            inputs[1].mi.dwFlags = LEFTUP;
            SendInput(2, inputs, Marshal.SizeOf<INPUT>());
        }
    }
}
