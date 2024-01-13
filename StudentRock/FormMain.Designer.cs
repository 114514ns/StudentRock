using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace StudentRock

{


    partial class FormMain
    {

        const int SRCCOPY = 0x00CC0020;

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern int DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern int DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern int GetDIBits(IntPtr hdc, IntPtr hBitmap, uint uStartScan, uint cScanLines, IntPtr lpvBits, ref BITMAPINFO lpbi, uint uUsage);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

  
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.checkBoxIsConnected = new System.Windows.Forms.CheckBox();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this.c_showConsole = new System.Windows.Forms.CheckBox();
            this.c_noTopMostWindow = new System.Windows.Forms.CheckBox();
            this.c_exitStMain = new System.Windows.Forms.CheckBox();
            this.c_enableTerminate = new System.Windows.Forms.CheckBox();
            this.c_unhookKeyboard = new System.Windows.Forms.CheckBox();
            this.c_fakeScreenshot = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.coreConfig = new System.Windows.Forms.GroupBox();
            this.c_noBlackScreen = new System.Windows.Forms.CheckBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.openImage = new System.Windows.Forms.OpenFileDialog();
            this.readme = new System.Windows.Forms.GroupBox();
            this.l_readme = new System.Windows.Forms.Label();
            this.buttonSaveScreen = new System.Windows.Forms.Button();
            this.saveImage = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxStatus.SuspendLayout();
            this.coreConfig.SuspendLayout();
            this.readme.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxIsConnected
            // 
            this.checkBoxIsConnected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIsConnected.AutoCheck = false;
            this.checkBoxIsConnected.AutoSize = true;
            this.checkBoxIsConnected.ForeColor = System.Drawing.Color.Red;
            this.checkBoxIsConnected.Location = new System.Drawing.Point(42, 20);
            this.checkBoxIsConnected.Name = "checkBoxIsConnected";
            this.checkBoxIsConnected.Size = new System.Drawing.Size(146, 17);
            this.checkBoxIsConnected.TabIndex = 0;
            this.checkBoxIsConnected.Text = "与学生端的连接已断开";
            this.checkBoxIsConnected.UseVisualStyleBackColor = true;
            this.checkBoxIsConnected.CheckedChanged += new System.EventHandler(this.CheckBoxIsConnected_CheckedChanged);
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Controls.Add(this.checkBoxIsConnected);
            this.groupBoxStatus.Location = new System.Drawing.Point(11, 11);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(242, 46);
            this.groupBoxStatus.TabIndex = 10;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "状态";
            // 
            // timerCheck
            // 
            this.timerCheck.Enabled = true;
            this.timerCheck.Interval = 1000;
            this.timerCheck.Tick += new System.EventHandler(this.TimerCheck_Tick);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(178, 339);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 25);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "断开";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // c_showConsole
            // 
            this.c_showConsole.AutoSize = true;
            this.c_showConsole.Location = new System.Drawing.Point(6, 95);
            this.c_showConsole.Name = "c_showConsole";
            this.c_showConsole.Size = new System.Drawing.Size(56, 17);
            this.c_showConsole.TabIndex = 5;
            this.c_showConsole.Text = "debug";
            this.c_showConsole.UseVisualStyleBackColor = true;
            // 
            // c_noTopMostWindow
            // 
            this.c_noTopMostWindow.AutoSize = true;
            this.c_noTopMostWindow.Checked = true;
            this.c_noTopMostWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c_noTopMostWindow.Location = new System.Drawing.Point(126, 22);
            this.c_noTopMostWindow.Name = "c_noTopMostWindow";
            this.c_noTopMostWindow.Size = new System.Drawing.Size(62, 17);
            this.c_noTopMostWindow.TabIndex = 4;
            this.c_noTopMostWindow.Text = "窗口化";
            this.c_noTopMostWindow.UseVisualStyleBackColor = true;
            // 
            // c_exitStMain
            // 
            this.c_exitStMain.AutoSize = true;
            this.c_exitStMain.Location = new System.Drawing.Point(6, 70);
            this.c_exitStMain.Name = "c_exitStMain";
            this.c_exitStMain.Size = new System.Drawing.Size(110, 17);
            this.c_exitStMain.TabIndex = 3;
            this.c_exitStMain.Text = "阻止学生端启动";
            this.c_exitStMain.UseVisualStyleBackColor = true;
            this.c_exitStMain.CheckedChanged += new System.EventHandler(this.c_exitStMain_CheckedChanged);
            // 
            // c_enableTerminate
            // 
            this.c_enableTerminate.AutoSize = true;
            this.c_enableTerminate.Location = new System.Drawing.Point(126, 47);
            this.c_enableTerminate.Name = "c_enableTerminate";
            this.c_enableTerminate.Size = new System.Drawing.Size(98, 17);
            this.c_enableTerminate.TabIndex = 2;
            this.c_enableTerminate.Text = "解锁进程保护";
            this.c_enableTerminate.UseVisualStyleBackColor = true;
            // 
            // c_unhookKeyboard
            // 
            this.c_unhookKeyboard.AutoSize = true;
            this.c_unhookKeyboard.Checked = true;
            this.c_unhookKeyboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c_unhookKeyboard.Location = new System.Drawing.Point(6, 47);
            this.c_unhookKeyboard.Name = "c_unhookKeyboard";
            this.c_unhookKeyboard.Size = new System.Drawing.Size(74, 17);
            this.c_unhookKeyboard.TabIndex = 1;
            this.c_unhookKeyboard.Text = "解锁键盘";
            this.c_unhookKeyboard.UseVisualStyleBackColor = true;
            // 
            // c_fakeScreenshot
            // 
            this.c_fakeScreenshot.AutoSize = true;
            this.c_fakeScreenshot.Location = new System.Drawing.Point(6, 22);
            this.c_fakeScreenshot.Name = "c_fakeScreenshot";
            this.c_fakeScreenshot.Size = new System.Drawing.Size(74, 17);
            this.c_fakeScreenshot.TabIndex = 0;
            this.c_fakeScreenshot.Text = "屏幕伪装";
            this.c_fakeScreenshot.UseVisualStyleBackColor = true;
            this.c_fakeScreenshot.CheckedChanged += new System.EventHandler(this.c_fakeScreenshot_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 339);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 25);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "连接";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // coreConfig
            // 
            this.coreConfig.Controls.Add(this.c_noBlackScreen);
            this.coreConfig.Controls.Add(this.c_showConsole);
            this.coreConfig.Controls.Add(this.c_noTopMostWindow);
            this.coreConfig.Controls.Add(this.c_exitStMain);
            this.coreConfig.Controls.Add(this.c_enableTerminate);
            this.coreConfig.Controls.Add(this.c_unhookKeyboard);
            this.coreConfig.Controls.Add(this.c_fakeScreenshot);
            this.coreConfig.Location = new System.Drawing.Point(11, 63);
            this.coreConfig.Name = "coreConfig";
            this.coreConfig.Size = new System.Drawing.Size(242, 124);
            this.coreConfig.TabIndex = 7;
            this.coreConfig.TabStop = false;
            this.coreConfig.Text = "配置";
            // 
            // c_noBlackScreen
            // 
            this.c_noBlackScreen.AutoSize = true;
            this.c_noBlackScreen.Location = new System.Drawing.Point(126, 69);
            this.c_noBlackScreen.Name = "c_noBlackScreen";
            this.c_noBlackScreen.Size = new System.Drawing.Size(98, 17);
            this.c_noBlackScreen.TabIndex = 6;
            this.c_noBlackScreen.Text = "阻止黑屏肃静";
            this.c_noBlackScreen.UseVisualStyleBackColor = true;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.labelVersion.Location = new System.Drawing.Point(183, 367);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(70, 14);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "α Build 2";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "StudentRock";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // openImage
            // 
            this.openImage.DefaultExt = "bmp";
            this.openImage.Filter = "Bitmap 图像(*.bmp)|*.bmp|所有合适文件(*.bmp)|*.bmp";
            this.openImage.FilterIndex = 2;
            this.openImage.RestoreDirectory = true;
            this.openImage.Title = "选择伪装的图像";
            // 
            // readme
            // 
            this.readme.Controls.Add(this.l_readme);
            this.readme.Location = new System.Drawing.Point(11, 194);
            this.readme.Name = "readme";
            this.readme.Size = new System.Drawing.Size(241, 107);
            this.readme.TabIndex = 12;
            this.readme.TabStop = false;
            this.readme.Text = "说明";
            // 
            // l_readme
            // 
            this.l_readme.AutoSize = true;
            this.l_readme.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.l_readme.Location = new System.Drawing.Point(10, 18);
            this.l_readme.Name = "l_readme";
            this.l_readme.Size = new System.Drawing.Size(212, 68);
            this.l_readme.TabIndex = 0;
            this.l_readme.Text = "默认配置即可工作，只需点击“连接”\r\n单击任务栏中的图标可以隐藏\r\n保存屏幕后即可配置屏幕伪装\r\n保存屏幕时本软件界面不会出现";
            // 
            // buttonSaveScreen
            // 
            this.buttonSaveScreen.Location = new System.Drawing.Point(178, 308);
            this.buttonSaveScreen.Name = "buttonSaveScreen";
            this.buttonSaveScreen.Size = new System.Drawing.Size(75, 25);
            this.buttonSaveScreen.TabIndex = 13;
            this.buttonSaveScreen.Text = "保存屏幕";
            this.buttonSaveScreen.UseVisualStyleBackColor = true;
            this.buttonSaveScreen.Click += new System.EventHandler(this.buttonSaveScreen_Click);
            // 
            // saveImage
            // 
            this.saveImage.DefaultExt = "bmp";
            this.saveImage.FileName = "屏幕截图";
            this.saveImage.Filter = "Bitmap 图像(*.bmp)|*.bmp|所有合适文件(*.bmp)|*.bmp";
            this.saveImage.Title = "保存屏幕截图";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 497);
            this.Controls.Add(this.buttonSaveScreen);
            this.Controls.Add(this.readme);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.groupBoxStatus);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.coreConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            //this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.coreConfig.ResumeLayout(false);
            this.coreConfig.PerformLayout();
            this.readme.ResumeLayout(false);
            this.readme.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxIsConnected;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Timer timerCheck;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox c_showConsole;
        private System.Windows.Forms.CheckBox c_noTopMostWindow;
        private System.Windows.Forms.CheckBox c_exitStMain;
        private System.Windows.Forms.CheckBox c_enableTerminate;
        private System.Windows.Forms.CheckBox c_unhookKeyboard;
        private System.Windows.Forms.CheckBox c_fakeScreenshot;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox coreConfig;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.OpenFileDialog openImage;
        private System.Windows.Forms.GroupBox readme;
        private System.Windows.Forms.Label l_readme;
        private System.Windows.Forms.Button buttonSaveScreen;
        private System.Windows.Forms.SaveFileDialog saveImage;
        private System.Windows.Forms.CheckBox c_noBlackScreen;
        private System.Windows.Forms.PictureBox picture;
        private Bitmap CaptureScreen()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            IntPtr hdcScreen = GetDC(IntPtr.Zero);
            IntPtr hdcMem = CreateCompatibleDC(hdcScreen);
            IntPtr hBitmap = CreateCompatibleBitmap(hdcScreen, screenWidth, screenHeight);
            IntPtr hOld = SelectObject(hdcMem, hBitmap);

            BitBlt(hdcMem, 0, 0, screenWidth, screenHeight, hdcScreen, 0, 0, SRCCOPY);

            BITMAPINFO bmi = new BITMAPINFO();
            bmi.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFO));
            GetDIBits(hdcMem, hBitmap, 0, (uint)screenHeight, IntPtr.Zero, ref bmi, 0);

            IntPtr ptr = Marshal.AllocHGlobal((int)bmi.biSizeImage);
            GetDIBits(hdcMem, hBitmap, 0, (uint)screenHeight, ptr, ref bmi, 0);

            byte[] pixels = new byte[bmi.biSizeImage];
            Marshal.Copy(ptr, pixels, 0, pixels.Length);

            // 翻转图像
            byte[] flippedPixels = new byte[pixels.Length];
            int rowSize = screenWidth * 4;
            for (int y = 0; y < screenHeight; y++)
            {
                Array.Copy(pixels, (screenHeight - y - 1) * rowSize, flippedPixels, y * rowSize, rowSize);
            }

            Marshal.FreeHGlobal(ptr);

            Bitmap bitmap = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppRgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, screenWidth, screenHeight), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            Marshal.Copy(flippedPixels, 0, bmpData.Scan0, flippedPixels.Length);
            bitmap.UnlockBits(bmpData);

            SelectObject(hdcMem, hOld);
            DeleteObject(hBitmap);
            DeleteDC(hdcMem);
            DeleteDC(hdcScreen);

            return bitmap;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public uint[] cols;
        }
        private void InitializePictureBox()
        {
            // 创建PictureBox控件
            picture = new PictureBox();
            picture.Width = (int)(400 * 1.3);
            picture.Height = (int)(225 * 1.3);
            //picture.Size = new Size((int)(400 * 1.3), (int)(225 * 1.3));
            picture.Location = new Point((this.ClientSize.Width - picture.Width) / 2+120, (this.ClientSize.Height - picture.Height) / 2);
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // 设置图片的显示方式为拉伸
            picture.BorderStyle = BorderStyle.FixedSingle; // 设置边框样式
            this.Controls.Add(picture);
        }
    }


}

