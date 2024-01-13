using System;
using System.Collections.Generic;

using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;


namespace StudentRock
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(!CheckDepFiles())
            {
                MessageBox.Show("错误：依赖文件缺失！\n请运行 RockLoader 释放依赖。", "Error");
                return;
            }
            Application.Run(new FormMain());
            LibStHook.UnsetGlobalHook();
        }

        static bool CheckDepFiles()
        {
            string[] dep = { "LibStHook.dll", "SimpleUpdater.dll" };
            foreach(string i in dep)
            {
                if (!File.Exists(i)) return false;
            }
            return true;
        }
    }
    public class LibStHook
    {
        [DllImport("LibStHook.dll")]
        public static extern void SetEnableTerminate(int x);

        [DllImport("LibStHook.dll")]
        public static extern void SetUnhookKeyboard(int x);

        [DllImport("LibStHook.dll")]
        public static extern void SetExitStMain(int x);

        [DllImport("LibStHook.dll")]
        public static extern void SetNoTopMostWindow(int x);

        [DllImport("LibStHook.dll")]
        public static extern void SetShowConsole(int x);

        [DllImport("LibStHook.dll")]
        public static extern int SetGlobalHook();

        [DllImport("LibStHook.dll")]
        public static extern int UnsetGlobalHook();

        [DllImport("LibStHook.dll")]
        public static extern int IsAlive();

        [DllImport("LibStHook.dll", CharSet = CharSet.Unicode)]
        public static extern void SetFakeImagePath(string x);

        [DllImport("LibStHook.dll")]
        public static extern void SetNoBlackScreen(int x);
    }
}
