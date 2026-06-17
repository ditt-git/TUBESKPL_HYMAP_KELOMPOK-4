namespace Admin
{
    partial class DataPelanggan
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
            dataGridView1 = new DataGridView();
            buttonaddpelanggan = new Button();
            buttoneditpelanggan = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            namapelanggan = new TextBox();
            alamatpelanggan = new TextBox();
            notelppelanggan = new TextBox();
            galondipinjam = new TextBox();
            idarmadapelanggan = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 38);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(379, 343);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // buttonaddpelanggan
            // 
            buttonaddpelanggan.Location = new Point(61, 403);
            buttonaddpelanggan.Name = "buttonaddpelanggan";
            buttonaddpelanggan.Size = new Size(94, 29);
            buttonaddpelanggan.TabIndex = 2;
            buttonaddpelanggan.Text = "tambah";
            buttonaddpelanggan.UseVisualStyleBackColor = true;
            buttonaddpelanggan.Click += button1_Click;
            // 
            // buttoneditpelanggan
            // 
            buttoneditpelanggan.Location = new Point(201, 402);
            buttoneditpelanggan.Name = "buttoneditpelanggan";
            buttoneditpelanggan.Size = new Size(94, 29);
            buttoneditpelanggan.TabIndex = 3;
            buttoneditpelanggan.Text = "edit";
            buttoneditpelanggan.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 6);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 4;
            label1.Text = "Data Pelanggan";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(427, 14);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 5;
            label2.Text = "nama";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(427, 105);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 6;
            label3.Text = "alamat";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(427, 199);
            label4.Name = "label4";
            label4.Size = new Size(81, 20);
            label4.TabIndex = 7;
            label4.Text = "no telepon";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(426, 293);
            label5.Name = "label5";
            label5.RightToLeft = RightToLeft.No;
            label5.Size = new Size(114, 20);
            label5.TabIndex = 8;
            label5.Text = "galon di pinjam";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(631, 14);
            label6.Name = "label6";
            label6.Size = new Size(164, 20);
            label6.TabIndex = 9;
            label6.Text = "tanggal terakhir dikirim";
            // 
            // namapelanggan
            // 
            namapelanggan.Location = new Point(430, 52);
            namapelanggan.Name = "namapelanggan";
            namapelanggan.Size = new Size(125, 27);
            namapelanggan.TabIndex = 10;
            // 
            // alamatpelanggan
            // 
            alamatpelanggan.Location = new Point(429, 146);
            alamatpelanggan.Name = "alamatpelanggan";
            alamatpelanggan.Size = new Size(125, 27);
            alamatpelanggan.TabIndex = 11;
            alamatpelanggan.TextChanged += textBox2_TextChanged;
            // 
            // notelppelanggan
            // 
            notelppelanggan.Location = new Point(427, 241);
            notelppelanggan.Name = "notelppelanggan";
            notelppelanggan.Size = new Size(125, 27);
            notelppelanggan.TabIndex = 12;
            // 
            // galondipinjam
            // 
            galondipinjam.Location = new Point(428, 328);
            galondipinjam.Name = "galondipinjam";
            galondipinjam.Size = new Size(125, 27);
            galondipinjam.TabIndex = 13;
            // 
            // idarmadapelanggan
            // 
            idarmadapelanggan.Location = new Point(641, 146);
            idarmadapelanggan.Name = "idarmadapelanggan";
            idarmadapelanggan.Size = new Size(125, 27);
            idarmadapelanggan.TabIndex = 15;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(641, 50);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(131, 27);
            dateTimePicker1.TabIndex = 19;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(641, 105);
            label7.Name = "label7";
            label7.Size = new Size(77, 20);
            label7.TabIndex = 17;
            label7.Text = "id armada";
            // 
            // DataPelanggan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dateTimePicker1);
            Controls.Add(label7);
            Controls.Add(idarmadapelanggan);
            Controls.Add(galondipinjam);
            Controls.Add(notelppelanggan);
            Controls.Add(alamatpelanggan);
            Controls.Add(namapelanggan);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttoneditpelanggan);
            Controls.Add(buttonaddpelanggan);
            Controls.Add(dataGridView1);
            Name = "DataPelanggan";
            Text = "DataPelanggan";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private Button buttonaddpelanggan;
        private Button buttoneditpelanggan;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox namapelanggan;
        private TextBox alamatpelanggan;
        private TextBox notelppelanggan;
        private TextBox galondipinjam;
        private TextBox idarmadapelanggan;
        private DateTimePicker dateTimePicker1;
        private Label label7;
    }
}