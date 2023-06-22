namespace GL_M2.Forms
{
    partial class Rectangles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rectangles));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Id = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbPoint = new GL_M2.Controls.ColorPicker();
            this.cbCurrentPoint = new GL_M2.Controls.ColorPicker();
            this.cbNewPoint = new GL_M2.Controls.ColorPicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtG = new System.Windows.Forms.TextBox();
            this.txtR = new System.Windows.Forms.TextBox();
            this.lbB = new System.Windows.Forms.Label();
            this.lbG = new System.Windows.Forms.Label();
            this.lbR = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pgColor = new System.Windows.Forms.PictureBox();
            this.npHeight = new System.Windows.Forms.NumericUpDown();
            this.npWidth = new System.Windows.Forms.NumericUpDown();
            this.npX = new System.Windows.Forms.NumericUpDown();
            this.npY = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scrollablePictureBox = new System.Windows.Forms.PictureBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvRectangles = new System.Windows.Forms.DataGridView();
            this.timer_image = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npY)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollablePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRectangles)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Id});
            this.statusStrip1.Location = new System.Drawing.Point(0, 632);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1048, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Id
            // 
            this.toolStripStatusLabel_Id.Name = "toolStripStatusLabel_Id";
            this.toolStripStatusLabel_Id.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel_Id.Text = "-";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbPoint);
            this.groupBox1.Controls.Add(this.cbCurrentPoint);
            this.groupBox1.Controls.Add(this.cbNewPoint);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtB);
            this.groupBox1.Controls.Add(this.txtG);
            this.groupBox1.Controls.Add(this.txtR);
            this.groupBox1.Controls.Add(this.lbB);
            this.groupBox1.Controls.Add(this.lbG);
            this.groupBox1.Controls.Add(this.lbR);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pgColor);
            this.groupBox1.Controls.Add(this.npHeight);
            this.groupBox1.Controls.Add(this.npWidth);
            this.groupBox1.Controls.Add(this.npX);
            this.groupBox1.Controls.Add(this.npY);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.dgvRectangles);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1048, 632);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(756, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "New Point";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(744, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Current Point";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(781, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Point";
            // 
            // cbPoint
            // 
            this.cbPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPoint.BackColor = System.Drawing.Color.White;
            this.cbPoint.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPoint.FormattingEnabled = true;
            this.cbPoint.Location = new System.Drawing.Point(824, 46);
            this.cbPoint.Name = "cbPoint";
            this.cbPoint.SelectedValue = System.Drawing.Color.White;
            this.cbPoint.Size = new System.Drawing.Size(212, 21);
            this.cbPoint.TabIndex = 24;
            this.cbPoint.SelectedIndexChanged += new System.EventHandler(this.cbPoint_SelectedIndexChanged);
            // 
            // cbCurrentPoint
            // 
            this.cbCurrentPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCurrentPoint.BackColor = System.Drawing.Color.White;
            this.cbCurrentPoint.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCurrentPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentPoint.FormattingEnabled = true;
            this.cbCurrentPoint.Location = new System.Drawing.Point(824, 73);
            this.cbCurrentPoint.Name = "cbCurrentPoint";
            this.cbCurrentPoint.SelectedValue = System.Drawing.Color.White;
            this.cbCurrentPoint.Size = new System.Drawing.Size(212, 21);
            this.cbCurrentPoint.TabIndex = 23;
            this.cbCurrentPoint.SelectedIndexChanged += new System.EventHandler(this.cbPoint_SelectedIndexChanged);
            // 
            // cbNewPoint
            // 
            this.cbNewPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNewPoint.BackColor = System.Drawing.Color.White;
            this.cbNewPoint.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbNewPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewPoint.FormattingEnabled = true;
            this.cbNewPoint.Location = new System.Drawing.Point(824, 100);
            this.cbNewPoint.Name = "cbNewPoint";
            this.cbNewPoint.SelectedValue = System.Drawing.Color.White;
            this.cbNewPoint.Size = new System.Drawing.Size(212, 21);
            this.cbNewPoint.TabIndex = 22;
            this.cbNewPoint.SelectedIndexChanged += new System.EventHandler(this.cbPoint_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(716, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(320, 5);
            this.label5.TabIndex = 21;
            // 
            // txtB
            // 
            this.txtB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtB.Enabled = false;
            this.txtB.Location = new System.Drawing.Point(861, 201);
            this.txtB.Multiline = true;
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(47, 30);
            this.txtB.TabIndex = 20;
            // 
            // txtG
            // 
            this.txtG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtG.Enabled = false;
            this.txtG.Location = new System.Drawing.Point(861, 168);
            this.txtG.Multiline = true;
            this.txtG.Name = "txtG";
            this.txtG.Size = new System.Drawing.Size(47, 30);
            this.txtG.TabIndex = 20;
            // 
            // txtR
            // 
            this.txtR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtR.Enabled = false;
            this.txtR.Location = new System.Drawing.Point(861, 136);
            this.txtR.Multiline = true;
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(47, 30);
            this.txtR.TabIndex = 20;
            // 
            // lbB
            // 
            this.lbB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbB.Location = new System.Drawing.Point(820, 201);
            this.lbB.Name = "lbB";
            this.lbB.Size = new System.Drawing.Size(35, 30);
            this.lbB.TabIndex = 19;
            this.lbB.Text = "B";
            this.lbB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbG
            // 
            this.lbG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbG.Location = new System.Drawing.Point(820, 168);
            this.lbG.Name = "lbG";
            this.lbG.Size = new System.Drawing.Size(35, 30);
            this.lbG.TabIndex = 19;
            this.lbG.Text = "G";
            this.lbG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbR
            // 
            this.lbR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbR.Location = new System.Drawing.Point(820, 136);
            this.lbR.Name = "lbR";
            this.lbR.Size = new System.Drawing.Size(35, 30);
            this.lbR.TabIndex = 19;
            this.lbR.Text = "R";
            this.lbR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(761, 322);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Height :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(770, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Width :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(791, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Y :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(791, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "X :";
            // 
            // pgColor
            // 
            this.pgColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pgColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pgColor.Location = new System.Drawing.Point(717, 136);
            this.pgColor.Name = "pgColor";
            this.pgColor.Size = new System.Drawing.Size(100, 95);
            this.pgColor.TabIndex = 17;
            this.pgColor.TabStop = false;
            // 
            // npHeight
            // 
            this.npHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.npHeight.Enabled = false;
            this.npHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npHeight.Location = new System.Drawing.Point(823, 320);
            this.npHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.npHeight.Name = "npHeight";
            this.npHeight.Size = new System.Drawing.Size(209, 22);
            this.npHeight.TabIndex = 16;
            this.npHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.npHeight.ValueChanged += new System.EventHandler(this.np_ValueChanged);
            // 
            // npWidth
            // 
            this.npWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.npWidth.Enabled = false;
            this.npWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npWidth.Location = new System.Drawing.Point(823, 292);
            this.npWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.npWidth.Name = "npWidth";
            this.npWidth.Size = new System.Drawing.Size(209, 22);
            this.npWidth.TabIndex = 16;
            this.npWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.npWidth.ValueChanged += new System.EventHandler(this.np_ValueChanged);
            // 
            // npX
            // 
            this.npX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.npX.Enabled = false;
            this.npX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npX.Location = new System.Drawing.Point(823, 237);
            this.npX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.npX.Name = "npX";
            this.npX.Size = new System.Drawing.Size(209, 22);
            this.npX.TabIndex = 16;
            this.npX.ValueChanged += new System.EventHandler(this.np_ValueChanged);
            // 
            // npY
            // 
            this.npY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.npY.Enabled = false;
            this.npY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npY.Location = new System.Drawing.Point(823, 264);
            this.npY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.npY.Name = "npY";
            this.npY.Size = new System.Drawing.Size(209, 22);
            this.npY.TabIndex = 16;
            this.npY.ValueChanged += new System.EventHandler(this.np_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.scrollablePictureBox);
            this.panel1.Location = new System.Drawing.Point(12, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 573);
            this.panel1.TabIndex = 15;
            // 
            // scrollablePictureBox
            // 
            this.scrollablePictureBox.Location = new System.Drawing.Point(3, 3);
            this.scrollablePictureBox.Name = "scrollablePictureBox";
            this.scrollablePictureBox.Size = new System.Drawing.Size(437, 406);
            this.scrollablePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.scrollablePictureBox.TabIndex = 0;
            this.scrollablePictureBox.TabStop = false;
            this.scrollablePictureBox.Click += new System.EventHandler(this.scrollablePictureBox_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(717, 598);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 11;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(798, 598);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(961, 598);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(879, 598);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvRectangles
            // 
            this.dgvRectangles.AllowUserToAddRows = false;
            this.dgvRectangles.AllowUserToDeleteRows = false;
            this.dgvRectangles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRectangles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRectangles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRectangles.Location = new System.Drawing.Point(717, 346);
            this.dgvRectangles.Name = "dgvRectangles";
            this.dgvRectangles.ReadOnly = true;
            this.dgvRectangles.RowHeadersVisible = false;
            this.dgvRectangles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRectangles.Size = new System.Drawing.Size(319, 246);
            this.dgvRectangles.TabIndex = 6;
            this.dgvRectangles.SelectionChanged += new System.EventHandler(this.dgvRectangles_SelectionChanged);
            // 
            // timer_image
            // 
            this.timer_image.Interval = 1000;
            // 
            // Rectangles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 654);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Rectangles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rectangles";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Rectangles_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npY)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollablePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRectangles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvRectangles;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown npHeight;
        private System.Windows.Forms.NumericUpDown npWidth;
        private System.Windows.Forms.NumericUpDown npX;
        private System.Windows.Forms.NumericUpDown npY;
        private System.Windows.Forms.PictureBox pgColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer_image;
        private System.Windows.Forms.PictureBox scrollablePictureBox;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Id;
        private System.Windows.Forms.TextBox txtR;
        private System.Windows.Forms.Label lbB;
        private System.Windows.Forms.Label lbG;
        private System.Windows.Forms.Label lbR;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtG;
        private System.Windows.Forms.Label label5;
        private Controls.ColorPicker cbNewPoint;
        private Controls.ColorPicker cbPoint;
        private Controls.ColorPicker cbCurrentPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}