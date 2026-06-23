namespace Admin
{
    partial class Armada
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
            label1 = new Label();
            dataGridView1 = new DataGridView();
            buttonedit = new Button();
            tb_nmwilayah = new TextBox();
            tb_hargakirim = new TextBox();
            label2 = new Label();
            label3 = new Label();
            buttonKembali = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(152, 45);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 0;
            label1.Text = "Wilayah";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(23, 83);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(482, 246);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            // 
            // buttonedit
            // 
            buttonedit.Location = new Point(608, 315);
            buttonedit.Name = "buttonedit";
            buttonedit.Size = new Size(94, 29);
            buttonedit.TabIndex = 2;
            buttonedit.Text = "edit";
            buttonedit.UseVisualStyleBackColor = true;
            buttonedit.Click += buttonedit_Click;
            // 
            // tb_nmwilayah
            // 
            tb_nmwilayah.Location = new Point(592, 133);
            tb_nmwilayah.Name = "tb_nmwilayah";
            tb_nmwilayah.Size = new Size(125, 27);
            tb_nmwilayah.TabIndex = 3;
            // 
            // tb_hargakirim
            // 
            tb_hargakirim.Location = new Point(592, 220);
            tb_hargakirim.Name = "tb_hargakirim";
            tb_hargakirim.Size = new Size(125, 27);
            tb_hargakirim.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(592, 83);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 5;
            label2.Text = "nama wilayah";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(591, 181);
            label3.Name = "label3";
            label3.Size = new Size(127, 20);
            label3.TabIndex = 6;
            label3.Text = "harga pengiriman";
            // 
            // buttonKembali
            // 
            buttonKembali.Location = new Point(12, 12);
            buttonKembali.Name = "buttonKembali";
            buttonKembali.Size = new Size(100, 30);
            buttonKembali.TabIndex = 17;
            buttonKembali.Text = "Kembali";
            buttonKembali.UseVisualStyleBackColor = true;
            buttonKembali.Click += buttonKembali_Click;
            // 
            // Armada
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonKembali);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(tb_hargakirim);
            Controls.Add(tb_nmwilayah);
            Controls.Add(buttonedit);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "Armada";
            Text = "Armada";
            Load += Armada_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Button buttonedit;
        private TextBox tb_nmwilayah;
        private TextBox tb_hargakirim;
        private Label label2;
        private Label label3;
        private Button buttonKembali;
    }
}