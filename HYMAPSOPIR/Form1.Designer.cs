namespace HYMAPSOPIR
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
            lblNamaSopir.Location = new Point(111, 41);
            lblNamaSopir.Name = "lblNamaSopir";
            lblNamaSopir.Size = new Size(0, 15);
            lblNamaSopir.TabIndex = 0;
            // 
            // lblArmada
            // 
            lblArmada.AutoSize = true;
            lblArmada.Location = new Point(111, 56);
            lblArmada.Name = "lblArmada";
            lblArmada.Size = new Size(0, 15);
            lblArmada.TabIndex = 1;
            // 
            // dgvPengiriman
            // 
            dgvPengiriman.AllowUserToAddRows = false;
            dgvPengiriman.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPengiriman.Location = new Point(12, 200);
            dgvPengiriman.Margin = new Padding(1, 3, 3, 3);
            dgvPengiriman.Name = "dgvPengiriman";
            dgvPengiriman.ReadOnly = true;
            dgvPengiriman.RowHeadersVisible = false;
            dgvPengiriman.RowHeadersWidth = 51;
            dgvPengiriman.Size = new Size(348, 237);
            dgvPengiriman.TabIndex = 2;
            dgvPengiriman.CellClick += dgvPengiriman_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 41);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 3;
            label1.Text = "Selamat Datang!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 56);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 4;
            label2.Text = "Armada";
            // 
            // dtpTanggal
            // 
            dtpTanggal.Location = new Point(81, 132);
            dtpTanggal.Name = "dtpTanggal";
            dtpTanggal.Size = new Size(200, 23);
            dtpTanggal.TabIndex = 5;
            dtpTanggal.ValueChanged += dtpTanggal_ValueChanged_1;
            // 
            // button1
            // 
            button1.Location = new Point(294, 43);
            button1.Name = "button1";
            button1.Size = new Size(64, 20);
            button1.TabIndex = 6;
            button1.Text = "Keluar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 450);
            Controls.Add(button1);
            Controls.Add(dtpTanggal);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPengiriman);
            Controls.Add(lblArmada);
            Controls.Add(lblNamaSopir);
            Name = "Form1";
            Text = "Form1";
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
