using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;


class Program
{
    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int vKey);

    static void Main()
    {
        string Mult = "Multi Tool.exe";

        while (true)
        {
            bool ctrlPressed = (GetAsyncKeyState(0x11) & 0x8000) != 0;
            bool altPressed = (GetAsyncKeyState(0x12) & 0x8000) != 0;
            bool spacePressed = (GetAsyncKeyState(0x20) & 0x8000) != 0;

            if (ctrlPressed && altPressed && spacePressed)
            {
                Process.Start(Mult);
                Thread.Sleep(5000);
            }

            Thread.Sleep(50);
        }
    }
}
