namespace GL_M2.Forms
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.npTime_process = new System.Windows.Forms.NumericUpDown();
            this.npPercent_check = new System.Windows.Forms.NumericUpDown();
            this.npCircle_radius = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.npToggle_time = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.npTriangle_length = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.npMedianBlur = new System.Windows.Forms.NumericUpDown();
            this.cbColorNG = new GL_M2.Controls.ColorPicker();
            this.cbColorOK = new GL_M2.Controls.ColorPicker();
            this.cbIsMedianBlur = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.npTime_process)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npPercent_check)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npCircle_radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npToggle_time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npTriangle_length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npMedianBlur)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Parameter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Point OK";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Point NG";
            // 
            // npTime_process
            // 
            this.npTime_process.Location = new System.Drawing.Point(126, 241);
            this.npTime_process.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.npTime_process.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.npTime_process.Name = "npTime_process";
            this.npTime_process.Size = new System.Drawing.Size(216, 20);
            this.npTime_process.TabIndex = 2;
            this.npTime_process.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.npTime_process.ValueChanged += new System.EventHandler(this.nP_ValueChanged);
            // 
            // npPercent_check
            // 
            this.npPercent_check.Location = new System.Drawing.Point(126, 274);
            this.npPercent_check.Name = "npPercent_check";
            this.npPercent_check.Size = new System.Drawing.Size(216, 20);
            this.npPercent_check.TabIndex = 2;
            this.npPercent_check.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.npPercent_check.ValueChanged += new System.EventHandler(this.nP_ValueChanged);
            // 
            // npCircle_radius
            // 
            this.npCircle_radius.Location = new System.Drawing.Point(126, 97);
            this.npCircle_radius.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.npCircle_radius.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.npCircle_radius.Name = "npCircle_radius";
            this.npCircle_radius.Size = new System.Drawing.Size(216, 20);
            this.npCircle_radius.TabIndex = 2;
            this.npCircle_radius.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.npCircle_radius.ValueChanged += new System.EventHandler(this.nP_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Time Checker(ms)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 274);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Percent +- (%)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Circle Radius";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Toggle Time(ms)";
            // 
            // npToggle_time
            // 
            this.npToggle_time.Location = new System.Drawing.Point(127, 197);
            this.npToggle_time.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.npToggle_time.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.npToggle_time.Name = "npToggle_time";
            this.npToggle_time.Size = new System.Drawing.Size(216, 20);
            this.npToggle_time.TabIndex = 2;
            this.npToggle_time.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.npToggle_time.ValueChanged += new System.EventHandler(this.nP_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Triangle Length";
            // 
            // npTriangle_length
            // 
            this.npTriangle_length.Location = new System.Drawing.Point(126, 167);
            this.npTriangle_length.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.npTriangle_length.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.npTriangle_length.Name = "npTriangle_length";
            this.npTriangle_length.Size = new System.Drawing.Size(216, 20);
            this.npTriangle_length.TabIndex = 2;
            this.npTriangle_length.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.npTriangle_length.ValueChanged += new System.EventHandler(this.nP_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Median Blur";
            // 
            // npMedianBlur
            // 
            this.npMedianBlur.Location = new System.Drawing.Point(126, 300);
            this.npMedianBlur.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.npMedianBlur.Name = "npMedianBlur";
            this.npMedianBlur.Size = new System.Drawing.Size(216, 20);
            this.npMedianBlur.TabIndex = 2;
            this.npMedianBlur.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.npMedianBlur.Value = Properties.Settings.Default.medianBlur;
            this.npMedianBlur.ValueChanged += new System.EventHandler(this.nP_ValueChanged);
            // 
            // cbColorNG
            // 
            this.cbColorNG.BackColor = System.Drawing.Color.White;
            this.cbColorNG.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColorNG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorNG.FormattingEnabled = true;
            this.cbColorNG.Location = new System.Drawing.Point(126, 140);
            this.cbColorNG.Name = "cbColorNG";
            this.cbColorNG.SelectedValue = System.Drawing.Color.White;
            this.cbColorNG.Size = new System.Drawing.Size(216, 21);
            this.cbColorNG.TabIndex = 0;
            this.cbColorNG.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // cbColorOK
            // 
            this.cbColorOK.BackColor = System.Drawing.Color.White;
            this.cbColorOK.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColorOK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorOK.FormattingEnabled = true;
            this.cbColorOK.Location = new System.Drawing.Point(126, 70);
            this.cbColorOK.Name = "cbColorOK";
            this.cbColorOK.SelectedValue = System.Drawing.Color.White;
            this.cbColorOK.Size = new System.Drawing.Size(216, 21);
            this.cbColorOK.TabIndex = 0;
            this.cbColorOK.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // cbIsMedianBlur
            // 
            this.cbIsMedianBlur.AutoSize = true;
            this.cbIsMedianBlur.Location = new System.Drawing.Point(105, 302);
            this.cbIsMedianBlur.Name = "cbIsMedianBlur";
            this.cbIsMedianBlur.Size = new System.Drawing.Size(15, 14);
            this.cbIsMedianBlur.TabIndex = 3;
            this.cbIsMedianBlur.UseVisualStyleBackColor = true;
            this.cbIsMedianBlur.Checked = Properties.Settings.Default.isMedianBlur;
            this.cbIsMedianBlur.CheckedChanged += new System.EventHandler(this.cbIsMedianBlur_CheckedChanged);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 358);
            this.Controls.Add(this.cbIsMedianBlur);
            this.Controls.Add(this.npTriangle_length);
            this.Controls.Add(this.npCircle_radius);
            this.Controls.Add(this.npMedianBlur);
            this.Controls.Add(this.npPercent_check);
            this.Controls.Add(this.npToggle_time);
            this.Controls.Add(this.npTime_process);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbColorNG);
            this.Controls.Add(this.cbColorOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(370, 600);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            ((System.ComponentModel.ISupportInitialize)(this.npTime_process)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npPercent_check)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npCircle_radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npToggle_time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npTriangle_length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npMedianBlur)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ColorPicker cbColorOK;
        private Controls.ColorPicker cbColorNG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown npTime_process;
        private System.Windows.Forms.NumericUpDown npPercent_check;
        private System.Windows.Forms.NumericUpDown npCircle_radius;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown npToggle_time;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown npTriangle_length;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown npMedianBlur;
        private System.Windows.Forms.CheckBox cbIsMedianBlur;
    }
}