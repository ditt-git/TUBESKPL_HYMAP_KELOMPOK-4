namespace Admin
{
    partial class DataSopir
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
            buttonaddsopir = new Button();
            buttoneditsopir = new Button();
            buttonToggleStatusSopir = new Button();
            buttonAktifkanSopir = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            nmsopir = new TextBox();
            notelpsopir = new TextBox();
            usernmsopir = new TextBox();
            pwsopir = new TextBox();
            idarmadasopir = new TextBox();
            buttonKembali = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 43);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(383, 325);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            // 
            // buttonaddsopir
            // 
            buttonaddsopir.Location = new Point(37, 391);
            buttonaddsopir.Name = "buttonaddsopir";
            buttonaddsopir.Size = new Size(94, 29);
            buttonaddsopir.TabIndex = 2;
            buttonaddsopir.Text = "Tambah";
            buttonaddsopir.UseVisualStyleBackColor = true;
            buttonaddsopir.Click += button1_Click;
            // 
            // buttoneditsopir
            // 
            buttoneditsopir.Location = new Point(189, 391);
            buttoneditsopir.Name = "buttoneditsopir";
            buttoneditsopir.Size = new Size(94, 29);
            buttoneditsopir.TabIndex = 3;
            buttoneditsopir.Text = "edit";
            buttoneditsopir.UseVisualStyleBackColor = true;
            buttoneditsopir.Click += buttoneditsopir_Click;
            // 
            // buttonToggleStatusSopir
            // 
            buttonToggleStatusSopir.Location = new Point(453, 391);
            buttonToggleStatusSopir.Name = "buttonToggleStatusSopir";
            buttonToggleStatusSopir.Size = new Size(110, 29);
            buttonToggleStatusSopir.TabIndex = 15;
            buttonToggleStatusSopir.Text = "Nonaktifkan";
            buttonToggleStatusSopir.UseVisualStyleBackColor = true;
            buttonToggleStatusSopir.Click += buttonNonaktifkanSopir_Click;
            // 
            // buttonAktifkanSopir
            // 
            buttonAktifkanSopir.Location = new Point(575, 391);
            buttonAktifkanSopir.Name = "buttonAktifkanSopir";
            buttonAktifkanSopir.Size = new Size(110, 29);
            buttonAktifkanSopir.TabIndex = 16;
            buttonAktifkanSopir.Text = "Aktifkan";
            buttonAktifkanSopir.UseVisualStyleBackColor = true;
            buttonAktifkanSopir.Click += buttonAktifkanSopir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(127, 9);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 4;
            label1.Text = "DATA SOPIR";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(463, 18);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 5;
            label2.Text = "Nama";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(462, 102);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 6;
            label3.Text = "No telepon";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(462, 193);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 7;
            label4.Text = "Username";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(462, 293);
            label5.Name = "label5";
            label5.Size = new Size(70, 20);
            label5.TabIndex = 8;
            label5.Text = "Password";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(649, 18);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.No;
            label6.Size = new Size(77, 20);
            label6.TabIndex = 9;
            label6.Text = "Id wilayah";
            label6.Click += label6_Click;
            // 
            // nmsopir
            // 
            nmsopir.Location = new Point(452, 53);
            nmsopir.Name = "nmsopir";
            nmsopir.Size = new Size(125, 27);
            nmsopir.TabIndex = 10;
            nmsopir.TextChanged += textBox1_TextChanged;
            // 
            // notelpsopir
            // 
            notelpsopir.Location = new Point(452, 139);
            notelpsopir.Name = "notelpsopir";
            notelpsopir.Size = new Size(125, 27);
            notelpsopir.TabIndex = 11;
            notelpsopir.TextChanged += textBox2_TextChanged;
            // 
            // usernmsopir
            // 
            usernmsopir.Location = new Point(453, 230);
            usernmsopir.Name = "usernmsopir";
            usernmsopir.Size = new Size(125, 27);
            usernmsopir.TabIndex = 12;
            // 
            // pwsopir
            // 
            pwsopir.Location = new Point(452, 341);
            pwsopir.Name = "pwsopir";
            pwsopir.Size = new Size(125, 27);
            pwsopir.TabIndex = 13;
            pwsopir.TextChanged += textBox4_TextChanged;
            // 
            // idarmadasopir
            // 
            idarmadasopir.Location = new Point(638, 53);
            idarmadasopir.Name = "idarmadasopir";
            idarmadasopir.Size = new Size(125, 27);
            idarmadasopir.TabIndex = 14;
            // 
            // buttonKembali
            // 
            buttonKembali.Location = new Point(12, 5);
            buttonKembali.Name = "buttonKembali";
            buttonKembali.Size = new Size(100, 30);
            buttonKembali.TabIndex = 17;
            buttonKembali.Text = "Kembali";
            buttonKembali.UseVisualStyleBackColor = true;
            buttonKembali.Click += buttonKembali_Click;
            // 
            // DataSopir
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonKembali);
            Controls.Add(idarmadasopir);
            Controls.Add(pwsopir);
            Controls.Add(usernmsopir);
            Controls.Add(notelpsopir);
            Controls.Add(nmsopir);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonAktifkanSopir);
            Controls.Add(buttonToggleStatusSopir);
            Controls.Add(buttoneditsopir);
            Controls.Add(buttonaddsopir);
            Controls.Add(dataGridView1);
            Name = "DataSopir";
            Text = "DataSopir";
            Load += DataSopir_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button buttonaddsopir;
        private Button buttoneditsopir;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox nmsopir;
        private TextBox notelpsopir;
        private TextBox usernmsopir;
        private TextBox pwsopir;
        private TextBox idarmadasopir;
        private Button buttonToggleStatusSopir;
        private Button buttonAktifkanSopir;
        private Button buttonKembali;
    }
}