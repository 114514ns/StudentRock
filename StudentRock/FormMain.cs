using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StudentRock
{
    public partial class FormMain : Form
    {
        System.Windows.Forms.Timer refreshTimer;
        public FormMain()
        {
            InitializeComponent();
            InitializePictureBox();
            InitializeTimer();
            applySettings();
            //Bitmap pic = CaptureScreen();
            picture.Image = CaptureScreen();
            foreach(CheckBox c in coreConfig.Controls)
            {
                c.CheckedChanged += new System.EventHandler(checkBoxesStateChange);
            }
        }
        private void InitializeTimer()
        {
            // 创建Timer组件
            refreshTimer = new System.Windows.Forms.Timer
            {
                Interval = 5000, // 设置刷新间隔，单位为毫秒（这里设置为5秒）
                Enabled = true
            };

            // 绑定Tick事件处理程序
            refreshTimer.Tick += RefreshTimer_Tick;
        }
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            // 定时器触发的事件处理程序
            RefreshImageAndNotify();
        }

        private void RefreshImageAndNotify()
        {
            // 刷新图像
            
            picture.Image = CaptureScreen();
            picture.Update();
            // 发出提示（这里使用MessageBox演示，你可以根据需要替换成其他方式）
            //MessageBox.Show("Image Refreshed!");

            // 可以在这里添加其他自定义的处理逻辑
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            picture = new PictureBox();
            picture.Size = new Size(300, 200);
            picture.Location = new Point((this.ClientSize.Width - picture.Width) / 2, (this.ClientSize.Height - picture.Height) / 2);
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // 设置图片的显示方式为拉伸
            picture.BorderStyle = BorderStyle.FixedSingle; // 设置边框样式
            this.Controls.Add(picture);
        }
    }
}
