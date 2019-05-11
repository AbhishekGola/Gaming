using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stealth
{
    public class Stealth
    {
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_MAXIMIZE = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_SHOW = 5;
        const int SW_MINIMIZE = 6;
        const int SW_SHOWMINNOACTIVE = 7;
        const int SW_SHOWNA = 8;
        const int SW_RESTORE = 9;
        const int SW_SHOWDEFAULT = 10;
        const int SW_FORCEMINIMIZE = 11;
        const int SW_MAX = 11;

        public static void Hide(string name)
        {
            int hWnd;
            Process[] processRunning = null;

            processRunning = Process.GetProcesses();
            if (processRunning != null)
            {
                foreach (Process pr in processRunning)
                {
                    if (pr.ProcessName.Contains(name))
                    {
                        hWnd = pr.MainWindowHandle.ToInt32();
                        ShowWindow(hWnd, SW_HIDE);
                    }
                }
            }
        }
    }

}
