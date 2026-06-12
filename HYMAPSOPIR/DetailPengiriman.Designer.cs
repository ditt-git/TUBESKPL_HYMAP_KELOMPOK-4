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
            SuspendLayout();
            // 
            // labelNamaPelanggan
            // 
            labelNamaPelanggan.AutoSize = true;
            labelNamaPelanggan.Location = new Point(226, 58);
            labelNamaPelanggan.Name = "labelNamaPelanggan";
            labelNamaPelanggan.Size = new Size(50, 20);
            labelNamaPelanggan.TabIndex = 0;
            labelNamaPelanggan.Text = "label1";
            labelNamaPelanggan.Click += labelNamaPelanggan_Click;
            // 
            // labelAlamatPelanggan
            // 
            labelAlamatPelanggan.AutoSize = true;
            labelAlamatPelanggan.Location = new Point(226, 96);
            labelAlamatPelanggan.Name = "labelAlamatPelanggan";
            labelAlamatPelanggan.Size = new Size(50, 20);
            labelAlamatPelanggan.TabIndex = 1;
            labelAlamatPelanggan.Text = "label1";
            // 
            // labelBuktiKirim
            // 
            labelBuktiKirim.AutoSize = true;
            labelBuktiKirim.Location = new Point(226, 134);
            labelBuktiKirim.Name = "labelBuktiKirim";
            labelBuktiKirim.Size = new Size(50, 20);
            labelBuktiKirim.TabIndex = 2;
            labelBuktiKirim.Text = "label1";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(31, 58);
            label.Name = "label";
            label.Size = new Size(56, 20);
            label.TabIndex = 3;
            label.Text = "Nama :";
            label.Click += label_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 96);
            label1.Name = "label1";
            label1.Size = new Size(68, 20);
            label1.TabIndex = 4;
            label1.Text = "Alamat : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 134);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 5;
            label2.Text = "Bukti kirim : ";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 222);
            label3.Name = "label3";
            label3.Size = new Size(127, 20);
            label3.TabIndex = 6;
            label3.Text = "Status Pengiriman";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(36, 263);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(127, 24);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "Belum terkirim";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(36, 293);
            radioButton2.Name = "radioButton2";
            radioButton2.RightToLeft = RightToLeft.No;
            radioButton2.Size = new Size(83, 24);
            radioButton2.TabIndex = 8;
            radioButton2.TabStop = true;
            radioButton2.Text = "Terkirim";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 396);
            label4.Name = "label4";
            label4.Size = new Size(134, 20);
            label4.TabIndex = 9;
            label4.Text = "Status Pembayaran";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "BelumBayar ", "Cash", "Transfer ", "Bon " });
            comboBox1.Location = new Point(31, 436);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(31, 168);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 11;
            label5.Text = "Armada :";
            // 
            // labelArmada
            // 
            labelArmada.AutoSize = true;
            labelArmada.Location = new Point(226, 168);
            labelArmada.Name = "labelArmada";
            labelArmada.Size = new Size(50, 20);
            labelArmada.TabIndex = 12;
            labelArmada.Text = "label6";
            // 
            // labelPrioritas
            // 
            labelPrioritas.AutoSize = true;
            labelPrioritas.Location = new Point(330, 15);
            labelPrioritas.Name = "labelPrioritas";
            labelPrioritas.Size = new Size(63, 20);
            labelPrioritas.TabIndex = 13;
            labelPrioritas.Text = "Prioritas";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(36, 323);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(117, 24);
            radioButton3.TabIndex = 14;
            radioButton3.TabStop = true;
            radioButton3.Text = "radioButton3";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(73, 27);
            button1.TabIndex = 15;
            button1.Text = "Kembali";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonLanjut
            // 
            buttonLanjut.Location = new Point(172, 528);
            buttonLanjut.Name = "buttonLanjut";
            buttonLanjut.Size = new Size(94, 29);
            buttonLanjut.TabIndex = 16;
            buttonLanjut.Text = "Lanjut";
            buttonLanjut.UseVisualStyleBackColor = true;
            buttonLanjut.Click += buttonLanjut_Click;
            // 
            // DetailPengiriman
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 600);
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
            Name = "DetailPengiriman";
            Text = "DetailPengiriman";
            Load += DetailPengiriman_Load;
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
    }
}