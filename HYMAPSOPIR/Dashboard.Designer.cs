namespace HYMAPSOPIR
{
    partial class Dashboard
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
            lblNamaSopir = new Label();
            lblArmada = new Label();
            dgvPengiriman = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            dtpTanggal = new DateTimePicker();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPengiriman).BeginInit();
            SuspendLayout();
            // 
            // lblNamaSopir
            // 
            lblNamaSopir.AutoSize = true;
            lblNamaSopir.Location = new Point(127, 55);
            lblNamaSopir.Name = "lblNamaSopir";
            lblNamaSopir.Size = new Size(0, 20);
            lblNamaSopir.TabIndex = 0;
            // 
            // lblArmada
            // 
            lblArmada.AutoSize = true;
            lblArmada.Location = new Point(127, 75);
            lblArmada.Name = "lblArmada";
            lblArmada.Size = new Size(0, 20);
            lblArmada.TabIndex = 1;
            // 
            // dgvPengiriman
            // 
            dgvPengiriman.AllowUserToAddRows = false;
            dgvPengiriman.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPengiriman.Location = new Point(14, 267);
            dgvPengiriman.Margin = new Padding(1, 4, 3, 4);
            dgvPengiriman.Name = "dgvPengiriman";
            dgvPengiriman.ReadOnly = true;
            dgvPengiriman.RowHeadersVisible = false;
            dgvPengiriman.RowHeadersWidth = 51;
            dgvPengiriman.Size = new Size(398, 316);
            dgvPengiriman.TabIndex = 2;
            dgvPengiriman.CellClick += dgvPengiriman_CellClick;
            dgvPengiriman.CellContentClick += dgvPengiriman_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 55);
            label1.Name = "label1";
            label1.Size = new Size(120, 20);
            label1.TabIndex = 3;
            label1.Text = "Selamat Datang!";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 75);
            label2.Name = "label2";
            label2.Size = new Size(62, 20);
            label2.TabIndex = 4;
            label2.Text = "Armada";
            // 
            // dtpTanggal
            // 
            dtpTanggal.Location = new Point(93, 176);
            dtpTanggal.Margin = new Padding(3, 4, 3, 4);
            dtpTanggal.Name = "dtpTanggal";
            dtpTanggal.Size = new Size(228, 27);
            dtpTanggal.TabIndex = 5;
            dtpTanggal.ValueChanged += dtpTanggal_ValueChanged_1;
            // 
            // button1
            // 
            button1.Location = new Point(336, 57);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(73, 27);
            button1.TabIndex = 6;
            button1.Text = "Keluar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 600);
            Controls.Add(button1);
            Controls.Add(dtpTanggal);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPengiriman);
            Controls.Add(lblArmada);
            Controls.Add(lblNamaSopir);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPengiriman).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNamaSopir;
        private Label lblArmada;
        private DataGridView dgvPengiriman;
        private Label label1;
        private Label label2;
        private DateTimePicker dtpTanggal;
        private Button button1;
    }
}
