namespace HYMAPSOPIR
{
    partial class DetailPengiriman
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
            labelNamaPelanggan = new Label();
            labelAlamatPelanggan = new Label();
            labelBuktiKirim = new Label();
            label = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label5 = new Label();
            labelArmada = new Label();
            labelPrioritas = new Label();
            radioButton3 = new RadioButton();
            button1 = new Button();
            buttonLanjut = new Button();
            label6 = new Label();
            numGalonKembali = new NumericUpDown();
            label7 = new Label();
            label8 = new Label();
            jumlahpinjamgalon = new Label();
            ((System.ComponentModel.ISupportInitialize)numGalonKembali).BeginInit();
            SuspendLayout();
            // 
            // labelNamaPelanggan
            // 
            labelNamaPelanggan.AutoSize = true;
            labelNamaPelanggan.Location = new Point(198, 44);
            labelNamaPelanggan.Name = "labelNamaPelanggan";
            labelNamaPelanggan.Size = new Size(38, 15);
            labelNamaPelanggan.TabIndex = 0;
            labelNamaPelanggan.Text = "label1";
            labelNamaPelanggan.Click += labelNamaPelanggan_Click;
            // 
            // labelAlamatPelanggan
            // 
            labelAlamatPelanggan.AutoSize = true;
            labelAlamatPelanggan.Location = new Point(198, 72);
            labelAlamatPelanggan.Name = "labelAlamatPelanggan";
            labelAlamatPelanggan.Size = new Size(38, 15);
            labelAlamatPelanggan.TabIndex = 1;
            labelAlamatPelanggan.Text = "label1";
            // 
            // labelBuktiKirim
            // 
            labelBuktiKirim.AutoSize = true;
            labelBuktiKirim.Location = new Point(198, 100);
            labelBuktiKirim.Name = "labelBuktiKirim";
            labelBuktiKirim.Size = new Size(38, 15);
            labelBuktiKirim.TabIndex = 2;
            labelBuktiKirim.Text = "label1";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(27, 44);
            label.Name = "label";
            label.Size = new Size(45, 15);
            label.TabIndex = 3;
            label.Text = "Nama :";
            label.Click += label_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 72);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 4;
            label1.Text = "Alamat : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 100);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 5;
            label2.Text = "Bukti kirim : ";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 166);
            label3.Name = "label3";
            label3.Size = new Size(103, 15);
            label3.TabIndex = 6;
            label3.Text = "Status Pengiriman";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(32, 197);
            radioButton1.Margin = new Padding(3, 2, 3, 2);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(103, 19);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "Belum terkirim";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(32, 220);
            radioButton2.Margin = new Padding(3, 2, 3, 2);
            radioButton2.Name = "radioButton2";
            radioButton2.RightToLeft = RightToLeft.No;
            radioButton2.Size = new Size(67, 19);
            radioButton2.TabIndex = 8;
            radioButton2.TabStop = true;
            radioButton2.Text = "Terkirim";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 297);
            label4.Name = "label4";
            label4.Size = new Size(108, 15);
            label4.TabIndex = 9;
            label4.Text = "Status Pembayaran";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "BelumBayar ", "Cash", "Transfer ", "Bon " });
            comboBox1.Location = new Point(27, 327);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(133, 23);
            comboBox1.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(27, 126);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 11;
            label5.Text = "Armada :";
            // 
            // labelArmada
            // 
            labelArmada.AutoSize = true;
            labelArmada.Location = new Point(198, 126);
            labelArmada.Name = "labelArmada";
            labelArmada.Size = new Size(38, 15);
            labelArmada.TabIndex = 12;
            labelArmada.Text = "label6";
            // 
            // labelPrioritas
            // 
            labelPrioritas.AutoSize = true;
            labelPrioritas.Location = new Point(289, 11);
            labelPrioritas.Name = "labelPrioritas";
            labelPrioritas.Size = new Size(50, 15);
            labelPrioritas.TabIndex = 13;
            labelPrioritas.Text = "Prioritas";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(32, 242);
            radioButton3.Margin = new Padding(3, 2, 3, 2);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(94, 19);
            radioButton3.TabIndex = 14;
            radioButton3.TabStop = true;
            radioButton3.Text = "radioButton3";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(10, 9);
            button1.Name = "button1";
            button1.Size = new Size(64, 20);
            button1.TabIndex = 15;
            button1.Text = "Kembali";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonLanjut
            // 
            buttonLanjut.Location = new Point(150, 396);
            buttonLanjut.Margin = new Padding(3, 2, 3, 2);
            buttonLanjut.Name = "buttonLanjut";
            buttonLanjut.Size = new Size(82, 22);
            buttonLanjut.TabIndex = 16;
            buttonLanjut.Text = "Lanjut";
            buttonLanjut.UseVisualStyleBackColor = true;
            buttonLanjut.Click += buttonLanjut_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(217, 12);
            label6.Name = "label6";
            label6.Size = new Size(59, 15);
            label6.TabIndex = 17;
            label6.Text = "Prioritas : ";
            // 
            // numGalonKembali
            // 
            numGalonKembali.Location = new Point(202, 244);
            numGalonKembali.Margin = new Padding(3, 2, 3, 2);
            numGalonKembali.Name = "numGalonKembali";
            numGalonKembali.Size = new Size(131, 23);
            numGalonKembali.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(195, 220);
            label7.Name = "label7";
            label7.Size = new Size(124, 15);
            label7.TabIndex = 19;
            label7.Text = "Jumlah Galon kembali";
            label7.Click += label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(192, 166);
            label8.Name = "label8";
            label8.Size = new Size(140, 15);
            label8.TabIndex = 20;
            label8.Text = "Jumlah galon di pinjam : ";
            // 
            // jumlahpinjamgalon
            // 
            jumlahpinjamgalon.AutoSize = true;
            jumlahpinjamgalon.Location = new Point(198, 188);
            jumlahpinjamgalon.Name = "jumlahpinjamgalon";
            jumlahpinjamgalon.Size = new Size(13, 15);
            jumlahpinjamgalon.TabIndex = 21;
            jumlahpinjamgalon.Text = "0";
            jumlahpinjamgalon.Click += label9_Click;
            // 
            // DetailPengiriman
            // 
            AcceptButton = buttonLanjut;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 450);
            Controls.Add(jumlahpinjamgalon);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(numGalonKembali);
            Controls.Add(label6);
            Controls.Add(buttonLanjut);
            Controls.Add(button1);
            Controls.Add(radioButton3);
            Controls.Add(labelPrioritas);
            Controls.Add(labelArmada);
            Controls.Add(label5);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label);
            Controls.Add(labelBuktiKirim);
            Controls.Add(labelAlamatPelanggan);
            Controls.Add(labelNamaPelanggan);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DetailPengiriman";
            Text = "DetailPengiriman";
            Load += DetailPengiriman_Load;
            ((System.ComponentModel.ISupportInitialize)numGalonKembali).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNamaPelanggan;
        private Label labelAlamatPelanggan;
        private Label labelBuktiKirim;
        private Label label;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Label label4;
        private ComboBox comboBox1;
        private Label label5;
        private Label labelArmada;
        private Label labelPrioritas;
        private RadioButton radioButton3;
        private Button button1;
        private Button buttonLanjut;
        private Label label6;
        private NumericUpDown numGalonKembali;
        private Label label7;
        private Label label8;
        private Label jumlahpinjamgalon;
    }
}