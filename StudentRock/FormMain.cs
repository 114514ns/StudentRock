using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StudentRock
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            applySettings();
            foreach(CheckBox c in coreConfig.Controls)
            {
                c.CheckedChanged += new System.EventHandler(checkBoxesStateChange);
            }
        }

        private void applySettings()
        {
            if (c_enableTerminate.Checked) LibStHook.SetEnableTerminate(1);
            else LibStHook.SetEnableTerminate(0);

            if (!c_fakeScreenshot.Checked)
                LibStHook.SetFakeImagePath("");

            if (c_unhookKeyboard.Checked) LibStHook.SetUnhookKeyboard(1);
            else LibStHook.SetUnhookKeyboard(0);

            if (c_exitStMain.Checked) LibStHook.SetExitStMain(1);
            else LibStHook.SetExitStMain(0);

            if (c_noTopMostWindow.Checked) LibStHook.SetNoTopMostWindow(1);
            else LibStHook.SetNoTopMostWindow(0);

            if (c_showConsole.Checked) LibStHook.SetShowConsole(1);
            else LibStHook.SetShowConsole(0);

            if (c_noBlackScreen.Checked) LibStHook.SetNoBlackScreen(1);
            else LibStHook.SetNoBlackScreen(0);
        }

        private Image getScreenImage()
        {
            Rectangle tScreenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap tSrcBmp = new Bitmap(tScreenRect.Width, tScreenRect.Height);
            Graphics g = Graphics.FromImage(tSrcBmp);
            g.CopyFromScreen(0, 0, 0, 0, tScreenRect.Size);
            g.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
            return tSrcBmp;
        }

        private void checkBoxesStateChange(object sender, EventArgs e)
        {
            applySettings();
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
            applySettings();
            LibStHook.SetGlobalHook();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            LibStHook.UnsetGlobalHook();
        }

        private void TimerCheck_Tick(object sender, EventArgs e)
        {
            if (LibStHook.IsAlive() != 0) checkBoxIsConnected.Checked = true;
            else checkBoxIsConnected.Checked = false;

            applySettings();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            } else
            {
                this.Show();
            }
        }

        private void c_fakeScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            if(!c_fakeScreenshot.Checked)
            {
                LibStHook.SetFakeImagePath("");
                return;
            }

            if(openImage.ShowDialog() == DialogResult.OK)
            {
                LibStHook.SetFakeImagePath(openImage.FileName);
            } else
            {
                c_fakeScreenshot.Checked = false;
            }
        }

        private void c_exitStMain_CheckedChanged(object sender, EventArgs e)
        {
            if (c_exitStMain.Checked) c_exitStMain.ForeColor = Color.Red;
            else c_exitStMain.ForeColor = Color.Black;
        }

        private void buttonSaveScreen_Click(object sender, EventArgs e)
        {
            this.Hide();
            Thread.Sleep(500);
            Image i = getScreenImage();
            this.Show();
            if (saveImage.ShowDialog() == DialogResult.OK)
            {
                i.Save(saveImage.FileName);
            }
        }
    }
}
