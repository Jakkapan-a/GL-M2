namespace GL_M2
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Id = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Error = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusSentData = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusConnect_SerialPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbModels = new System.Windows.Forms.ComboBox();
            this.txtModels = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.btRefresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxCOMPort = new System.Windows.Forms.ComboBox();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCamera = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_Main = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pgCam = new System.Windows.Forms.PictureBox();
            this.timerTest = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabelrgbName = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel_Left.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgCam)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Image = global::GL_M2.Properties.Resources.save_32;
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Visible = false;
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.modelsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testToolStripMenuItem.Text = "Reset";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // modelsToolStripMenuItem
            // 
            this.modelsToolStripMenuItem.Image = global::GL_M2.Properties.Resources.edit_property_32;
            this.modelsToolStripMenuItem.Name = "modelsToolStripMenuItem";
            this.modelsToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.modelsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.modelsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.modelsToolStripMenuItem.Text = "Models";
            this.modelsToolStripMenuItem.Click += new System.EventHandler(this.modelsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::GL_M2.Properties.Resources.settings__32;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Id,
            this.toolStripStatusLabel_Error,
            this.toolStripStatusSentData,
            this.toolStripStatusConnect_SerialPort,
            this.toolStripStatusLabelrgbName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 657);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Id
            // 
            this.toolStripStatusLabel_Id.Name = "toolStripStatusLabel_Id";
            this.toolStripStatusLabel_Id.Size = new System.Drawing.Size(12, 19);
            this.toolStripStatusLabel_Id.Text = "-";
            // 
            // toolStripStatusLabel_Error
            // 
            this.toolStripStatusLabel_Error.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel_Error.Name = "toolStripStatusLabel_Error";
            this.toolStripStatusLabel_Error.Size = new System.Drawing.Size(16, 19);
            this.toolStripStatusLabel_Error.Text = "-";
            // 
            // toolStripStatusSentData
            // 
            this.toolStripStatusSentData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusSentData.Name = "toolStripStatusSentData";
            this.toolStripStatusSentData.Size = new System.Drawing.Size(16, 19);
            this.toolStripStatusSentData.Text = "-";
            // 
            // toolStripStatusConnect_SerialPort
            // 
            this.toolStripStatusConnect_SerialPort.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusConnect_SerialPort.Name = "toolStripStatusConnect_SerialPort";
            this.toolStripStatusConnect_SerialPort.Size = new System.Drawing.Size(16, 19);
            this.toolStripStatusConnect_SerialPort.Text = "-";
            // 
            // panel_Left
            // 
            this.panel_Left.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Left.Controls.Add(this.lbStatus);
            this.panel_Left.Controls.Add(this.groupBox2);
            this.panel_Left.Controls.Add(this.groupBox1);
            this.panel_Left.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Left.Location = new System.Drawing.Point(1035, 24);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(229, 633);
            this.panel_Left.TabIndex = 2;
            // 
            // lbStatus
            // 
            this.lbStatus.BackColor = System.Drawing.Color.Yellow;
            this.lbStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbStatus.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(6, 127);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(218, 111);
            this.lbStatus.TabIndex = 1;
            this.lbStatus.Text = "Wait..";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.cbModels);
            this.groupBox2.Controls.Add(this.txtModels);
            this.groupBox2.Location = new System.Drawing.Point(5, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 95);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select models";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::GL_M2.Properties.Resources.search_32;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Location = new System.Drawing.Point(183, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(28, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbModels
            // 
            this.cbModels.FormattingEnabled = true;
            this.cbModels.Location = new System.Drawing.Point(14, 57);
            this.cbModels.Name = "cbModels";
            this.cbModels.Size = new System.Drawing.Size(197, 21);
            this.cbModels.TabIndex = 1;
            this.cbModels.Visible = false;
            this.cbModels.SelectedIndexChanged += new System.EventHandler(this.cbModels_SelectedIndexChanged);
            // 
            // txtModels
            // 
            this.txtModels.Location = new System.Drawing.Point(14, 29);
            this.txtModels.Name = "txtModels";
            this.txtModels.Size = new System.Drawing.Size(163, 20);
            this.txtModels.TabIndex = 0;
            this.txtModels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModels_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btConnect);
            this.groupBox1.Controls.Add(this.btRefresh);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxCOMPort);
            this.groupBox1.Controls.Add(this.comboBoxBaud);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxCamera);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(5, 426);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 196);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // btConnect
            // 
            this.btConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btConnect.Location = new System.Drawing.Point(138, 167);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 29;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btRefresh.BackgroundImage = global::GL_M2.Properties.Resources._refresh_32;
            this.btRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btRefresh.Location = new System.Drawing.Point(6, 167);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(25, 23);
            this.btRefresh.TabIndex = 30;
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "COM Port :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Baud :";
            // 
            // comboBoxCOMPort
            // 
            this.comboBoxCOMPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCOMPort.FormattingEnabled = true;
            this.comboBoxCOMPort.Location = new System.Drawing.Point(100, 128);
            this.comboBoxCOMPort.Name = "comboBoxCOMPort";
            this.comboBoxCOMPort.Size = new System.Drawing.Size(111, 21);
            this.comboBoxCOMPort.TabIndex = 25;
            this.comboBoxCOMPort.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // comboBoxBaud
            // 
            this.comboBoxBaud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaud.FormattingEnabled = true;
            this.comboBoxBaud.Location = new System.Drawing.Point(100, 99);
            this.comboBoxBaud.Name = "comboBoxBaud";
            this.comboBoxBaud.Size = new System.Drawing.Size(111, 21);
            this.comboBoxBaud.TabIndex = 26;
            this.comboBoxBaud.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Drive Camera :";
            // 
            // comboBoxCamera
            // 
            this.comboBoxCamera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCamera.FormattingEnabled = true;
            this.comboBoxCamera.Location = new System.Drawing.Point(100, 68);
            this.comboBoxCamera.Name = "comboBoxCamera";
            this.comboBoxCamera.Size = new System.Drawing.Size(111, 21);
            this.comboBoxCamera.TabIndex = 23;
            this.comboBoxCamera.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GL_M2.Properties.Resources.camera_logo;
            this.pictureBox1.Location = new System.Drawing.Point(66, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel_Main
            // 
            this.panel_Main.Controls.Add(this.lbTitle);
            this.panel_Main.Controls.Add(this.pgCam);
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(0, 24);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Padding = new System.Windows.Forms.Padding(10);
            this.panel_Main.Size = new System.Drawing.Size(1035, 633);
            this.panel_Main.TabIndex = 3;
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitle.BackColor = System.Drawing.Color.Yellow;
            this.lbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(154, 10);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(726, 34);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "----------------------";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgCam
            // 
            this.pgCam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgCam.BackColor = System.Drawing.Color.Black;
            this.pgCam.Location = new System.Drawing.Point(10, 54);
            this.pgCam.Name = "pgCam";
            this.pgCam.Size = new System.Drawing.Size(1015, 569);
            this.pgCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pgCam.TabIndex = 0;
            this.pgCam.TabStop = false;
            // 
            // timerTest
            // 
            this.timerTest.Interval = 500;
            this.timerTest.Tick += new System.EventHandler(this.timerTest_Tick);
            // 
            // toolStripStatusLabelrgbName
            // 
            this.toolStripStatusLabelrgbName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelrgbName.Name = "toolStripStatusLabelrgbName";
            this.toolStripStatusLabelrgbName.Size = new System.Drawing.Size(16, 19);
            this.toolStripStatusLabelrgbName.Text = "-";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.panel_Left);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GL-M2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel_Left.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pgCam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Id;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.PictureBox pgCam;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxCOMPort;
        private System.Windows.Forms.ComboBox comboBoxBaud;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCamera;
        private System.Windows.Forms.ToolStripMenuItem modelsToolStripMenuItem;
        private System.Windows.Forms.TextBox txtModels;
        private System.Windows.Forms.ComboBox cbModels;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Timer timerTest;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Error;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSentData;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusConnect_SerialPort;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelrgbName;
    }
}

