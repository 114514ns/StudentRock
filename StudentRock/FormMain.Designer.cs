namespace StudentRock
{
    partial class FormMain
    {
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
            this.textBoxStMainPID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxStMainPath = new System.Windows.Forms.TextBox();
            this.labelpStMainPath = new System.Windows.Forms.Label();
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
            this.labelVersion = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBoxStatus.SuspendLayout();
            this.coreConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxStMainPID
            // 
            this.textBoxStMainPID.Location = new System.Drawing.Point(76, 86);
            this.textBoxStMainPID.Name = "textBoxStMainPID";
            this.textBoxStMainPID.Size = new System.Drawing.Size(130, 21);
            this.textBoxStMainPID.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "学生端 PID：";
            // 
            // textBoxStMainPath
            // 
            this.textBoxStMainPath.Location = new System.Drawing.Point(76, 40);
            this.textBoxStMainPath.Name = "textBoxStMainPath";
            this.textBoxStMainPath.Size = new System.Drawing.Size(130, 21);
            this.textBoxStMainPath.TabIndex = 1;
            // 
            // labelpStMainPath
            // 
            this.labelpStMainPath.AutoSize = true;
            this.labelpStMainPath.Location = new System.Drawing.Point(6, 46);
            this.labelpStMainPath.Name = "labelpStMainPath";
            this.labelpStMainPath.Size = new System.Drawing.Size(77, 12);
            this.labelpStMainPath.TabIndex = 2;
            this.labelpStMainPath.Text = "学生端路径：";
            // 
            // checkBoxIsConnected
            // 
            this.checkBoxIsConnected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIsConnected.AutoCheck = false;
            this.checkBoxIsConnected.AutoSize = true;
            this.checkBoxIsConnected.ForeColor = System.Drawing.Color.Red;
            this.checkBoxIsConnected.Location = new System.Drawing.Point(42, 18);
            this.checkBoxIsConnected.Name = "checkBoxIsConnected";
            this.checkBoxIsConnected.Size = new System.Drawing.Size(144, 16);
            this.checkBoxIsConnected.TabIndex = 0;
            this.checkBoxIsConnected.Text = "与学生端的连接已断开";
            this.checkBoxIsConnected.UseVisualStyleBackColor = true;
            this.checkBoxIsConnected.CheckedChanged += new System.EventHandler(this.CheckBoxIsConnected_CheckedChanged);
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Controls.Add(this.textBoxStMainPID);
            this.groupBoxStatus.Controls.Add(this.label1);
            this.groupBoxStatus.Controls.Add(this.textBoxStMainPath);
            this.groupBoxStatus.Controls.Add(this.labelpStMainPath);
            this.groupBoxStatus.Controls.Add(this.checkBoxIsConnected);
            this.groupBoxStatus.Location = new System.Drawing.Point(11, 10);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(242, 121);
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
            this.buttonStop.Location = new System.Drawing.Point(178, 285);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "断开";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // c_showConsole
            // 
            this.c_showConsole.AutoSize = true;
            this.c_showConsole.Location = new System.Drawing.Point(6, 88);
            this.c_showConsole.Name = "c_showConsole";
            this.c_showConsole.Size = new System.Drawing.Size(96, 16);
            this.c_showConsole.TabIndex = 5;
            this.c_showConsole.Text = "显示调试窗口";
            this.c_showConsole.UseVisualStyleBackColor = true;
            // 
            // c_noTopMostWindow
            // 
            this.c_noTopMostWindow.AutoSize = true;
            this.c_noTopMostWindow.Location = new System.Drawing.Point(126, 20);
            this.c_noTopMostWindow.Name = "c_noTopMostWindow";
            this.c_noTopMostWindow.Size = new System.Drawing.Size(60, 16);
            this.c_noTopMostWindow.TabIndex = 4;
            this.c_noTopMostWindow.Text = "窗口化";
            this.c_noTopMostWindow.UseVisualStyleBackColor = true;
            // 
            // c_exitStMain
            // 
            this.c_exitStMain.AutoSize = true;
            this.c_exitStMain.Location = new System.Drawing.Point(6, 65);
            this.c_exitStMain.Name = "c_exitStMain";
            this.c_exitStMain.Size = new System.Drawing.Size(108, 16);
            this.c_exitStMain.TabIndex = 3;
            this.c_exitStMain.Text = "阻止学生端启动";
            this.c_exitStMain.UseVisualStyleBackColor = true;
            // 
            // c_enableTerminate
            // 
            this.c_enableTerminate.AutoSize = true;
            this.c_enableTerminate.Location = new System.Drawing.Point(126, 43);
            this.c_enableTerminate.Name = "c_enableTerminate";
            this.c_enableTerminate.Size = new System.Drawing.Size(96, 16);
            this.c_enableTerminate.TabIndex = 2;
            this.c_enableTerminate.Text = "解锁进程保护";
            this.c_enableTerminate.UseVisualStyleBackColor = true;
            // 
            // c_unhookKeyboard
            // 
            this.c_unhookKeyboard.AutoSize = true;
            this.c_unhookKeyboard.Location = new System.Drawing.Point(6, 43);
            this.c_unhookKeyboard.Name = "c_unhookKeyboard";
            this.c_unhookKeyboard.Size = new System.Drawing.Size(72, 16);
            this.c_unhookKeyboard.TabIndex = 1;
            this.c_unhookKeyboard.Text = "解锁键盘";
            this.c_unhookKeyboard.UseVisualStyleBackColor = true;
            // 
            // c_fakeScreenshot
            // 
            this.c_fakeScreenshot.AutoSize = true;
            this.c_fakeScreenshot.Location = new System.Drawing.Point(6, 20);
            this.c_fakeScreenshot.Name = "c_fakeScreenshot";
            this.c_fakeScreenshot.Size = new System.Drawing.Size(72, 16);
            this.c_fakeScreenshot.TabIndex = 0;
            this.c_fakeScreenshot.Text = "屏幕伪装";
            this.c_fakeScreenshot.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(11, 285);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "连接";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // coreConfig
            // 
            this.coreConfig.Controls.Add(this.c_showConsole);
            this.coreConfig.Controls.Add(this.c_noTopMostWindow);
            this.coreConfig.Controls.Add(this.c_exitStMain);
            this.coreConfig.Controls.Add(this.c_enableTerminate);
            this.coreConfig.Controls.Add(this.c_unhookKeyboard);
            this.coreConfig.Controls.Add(this.c_fakeScreenshot);
            this.coreConfig.Location = new System.Drawing.Point(11, 165);
            this.coreConfig.Name = "coreConfig";
            this.coreConfig.Size = new System.Drawing.Size(242, 114);
            this.coreConfig.TabIndex = 7;
            this.coreConfig.TabStop = false;
            this.coreConfig.Text = "配置";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.labelVersion.Location = new System.Drawing.Point(182, 318);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(70, 14);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "α Build 1";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 341);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.groupBoxStatus);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.coreConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "StudentRock";
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.coreConfig.ResumeLayout(false);
            this.coreConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxStMainPID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStMainPath;
        private System.Windows.Forms.Label labelpStMainPath;
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
    }
}

