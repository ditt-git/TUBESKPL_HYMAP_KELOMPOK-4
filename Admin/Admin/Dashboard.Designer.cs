namespace Admin
{
    partial class Dashboard
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
            tb_datasopir = new Button();
            tb_datapelanggan = new Button();
            tb_armada = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 44);
            label1.Name = "label1";
            label1.Size = new Size(187, 20);
            label1.TabIndex = 0;
            label1.Text = "SELAMAT DATANG ADMIN";
            // 
            // tb_datasopir
            // 
            tb_datasopir.Location = new Point(64, 109);
            tb_datasopir.Name = "tb_datasopir";
            tb_datasopir.Size = new Size(152, 78);
            tb_datasopir.TabIndex = 1;
            tb_datasopir.Text = "Data SOPIR";
            tb_datasopir.UseVisualStyleBackColor = true;
            tb_datasopir.Click += button1_Click;
            // 
            // tb_datapelanggan
            // 
            tb_datapelanggan.Location = new Point(64, 219);
            tb_datapelanggan.Name = "tb_datapelanggan";
            tb_datapelanggan.Size = new Size(152, 71);
            tb_datapelanggan.TabIndex = 2;
            tb_datapelanggan.Text = "Data Pelanggan";
            tb_datapelanggan.UseVisualStyleBackColor = true;
            tb_datapelanggan.Click += tb_datapelanggan_Click;
            // 
            // tb_armada
            // 
            tb_armada.Location = new Point(64, 314);
            tb_armada.Name = "tb_armada";
            tb_armada.Size = new Size(152, 66);
            tb_armada.TabIndex = 3;
            tb_armada.Text = "ARMADA";
            tb_armada.UseVisualStyleBackColor = true;
            tb_armada.Click += button3_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tb_armada);
            Controls.Add(tb_datapelanggan);
            Controls.Add(tb_datasopir);
            Controls.Add(label1);
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button tb_datasopir;
        private Button tb_datapelanggan;
        private Button tb_armada;
    }
}