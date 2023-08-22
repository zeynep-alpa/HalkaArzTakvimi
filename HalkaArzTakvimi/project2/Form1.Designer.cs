namespace project2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dgvHalkaArz = new DataGridView();
            groupBox1 = new GroupBox();
            toolStrip1 = new ToolStrip();
            btnVeriCek = new ToolStripButton();
            btnExcel = new ToolStripButton();
            btnData = new ToolStripButton();
            btnKapat = new ToolStripButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox2 = new GroupBox();
            dgvTaslakArz = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvHalkaArz).BeginInit();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTaslakArz).BeginInit();
            SuspendLayout();
            // 
            // dgvHalkaArz
            // 
            dgvHalkaArz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHalkaArz.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvHalkaArz.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHalkaArz.Dock = DockStyle.Fill;
            dgvHalkaArz.Location = new Point(3, 27);
            dgvHalkaArz.Name = "dgvHalkaArz";
            dgvHalkaArz.RowHeadersWidth = 62;
            dgvHalkaArz.RowTemplate.Height = 33;
            dgvHalkaArz.Size = new Size(676, 543);
            dgvHalkaArz.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvHalkaArz);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(682, 573);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Halka Arz Olacaklar";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(48, 48);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnVeriCek, btnExcel, btnData, btnKapat });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Margin = new Padding(5);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1376, 57);
            toolStrip1.TabIndex = 4;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnVeriCek
            // 
            btnVeriCek.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnVeriCek.Image = Properties.Resources.icn48_refresh;
            btnVeriCek.ImageTransparentColor = Color.Magenta;
            btnVeriCek.Margin = new Padding(10, 2, 0, 3);
            btnVeriCek.Name = "btnVeriCek";
            btnVeriCek.Size = new Size(52, 52);
            btnVeriCek.Click += btnVeriCek_Click;
            // 
            // btnExcel
            // 
            btnExcel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnExcel.Image = (Image)resources.GetObject("btnExcel.Image");
            btnExcel.ImageTransparentColor = Color.Magenta;
            btnExcel.Margin = new Padding(10, 2, 0, 3);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(52, 52);
            btnExcel.Click += btnExcel_Click;
            // 
            // btnData
            // 
            btnData.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnData.Image = (Image)resources.GetObject("btnData.Image");
            btnData.ImageTransparentColor = Color.Magenta;
            btnData.Margin = new Padding(10, 2, 0, 3);
            btnData.Name = "btnData";
            btnData.Size = new Size(52, 52);
            btnData.Click += btnData_Click;
            // 
            // btnKapat
            // 
            btnKapat.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnKapat.Image = Properties.Resources.icn48_close;
            btnKapat.ImageTransparentColor = Color.Magenta;
            btnKapat.Margin = new Padding(10, 2, 0, 3);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new Size(52, 52);
            btnKapat.Click += btnKapat_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 57);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1376, 579);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvTaslakArz);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(691, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(682, 573);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Taslak Arz Olacaklar";
            // 
            // dgvTaslakArz
            // 
            dgvTaslakArz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaslakArz.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTaslakArz.Dock = DockStyle.Fill;
            dgvTaslakArz.Location = new Point(3, 27);
            dgvTaslakArz.Name = "dgvTaslakArz";
            dgvTaslakArz.RowHeadersWidth = 62;
            dgvTaslakArz.RowTemplate.Height = 33;
            dgvTaslakArz.Size = new Size(676, 543);
            dgvTaslakArz.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1376, 636);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(toolStrip1);
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvHalkaArz).EndInit();
            groupBox1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTaslakArz).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvHalkaArz;
        private GroupBox groupBox1;
        private ToolStrip toolStrip1;
        private ToolStripButton btnVeriCek;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripButton btnKapat;
        private GroupBox groupBox2;
        private DataGridView dgvTaslakArz;
        private ToolStripButton btnExcel;
        private ToolStripButton btnData;
    }
}