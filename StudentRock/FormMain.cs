using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentRock
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void applySettings()
        {
            if (c_enableTerminate.Checked) LibStHook.SetEnableTerminate(1);
            else LibStHook.SetEnableTerminate(0);

            if (c_fakeScreenshot.Checked) LibStHook.SetFakeScreenshot(1);
            else LibStHook.SetFakeScreenshot(0);

            if (c_unhookKeyboard.Checked) LibStHook.SetUnhookKeyboard(1);
            else LibStHook.SetUnhookKeyboard(0);

            if (c_exitStMain.Checked) LibStHook.SetExitStMain(1);
            else LibStHook.SetExitStMain(0);

            if (c_noTopMostWindow.Checked) LibStHook.SetNoTopMostWindow(1);
            else LibStHook.SetNoTopMostWindow(0);

            if (c_showConsole.Checked) LibStHook.SetShowConsole(1);
            else LibStHook.SetShowConsole(0);
        }

        private void CheckBoxIsConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsConnected.Checked)
            {
                checkBoxIsConnected.ForeColor = Color.Green;
                checkBoxIsConnected.Text = "已经与学生端建立了神经连接";
            }
            else
            {
                checkBoxIsConnected.ForeColor = Color.Red;
                checkBoxIsConnected.Text = "与学生端的连接已断开";
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            LibStHook.SetGlobalHook();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            LibStHook.UnsetGlobalHook();
        }

        private void TimerCheck_Tick(object sender, EventArgs e)
        {
            textBoxStMainPath.Text = LibStHook.GetStMainPath();
            textBoxStMainPID.Text = LibStHook.GetStMainId().ToString();

            if (LibStHook.IsAlive() != 0) checkBoxIsConnected.Checked = true;
            else checkBoxIsConnected.Checked = false;

            applySettings();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            } else
            {
                this.Show();
            }
        }
    }
}
