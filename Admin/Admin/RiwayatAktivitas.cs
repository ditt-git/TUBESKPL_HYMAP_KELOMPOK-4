using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Admin.Library.Database;

namespace Admin
{
    public class RiwayatAktivitas : Form
    {
        private DataGridView dgvRiwayat;
        private Label lblTitle;
        private Button btnRefresh;

        public RiwayatAktivitas()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            dgvRiwayat = new DataGridView();
            lblTitle = new Label();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRiwayat).BeginInit();
            SuspendLayout();
            // 
            // dgvRiwayat
            // 
            dgvRiwayat.AllowUserToAddRows = false;
            dgvRiwayat.AllowUserToDeleteRows = false;
            dgvRiwayat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRiwayat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRiwayat.Location = new Point(12, 60);
            dgvRiwayat.Name = "dgvRiwayat";
            dgvRiwayat.ReadOnly = true;
            dgvRiwayat.RowHeadersVisible = false;
            dgvRiwayat.RowHeadersWidth = 51;
            dgvRiwayat.Size = new Size(760, 480);
            dgvRiwayat.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(242, 37);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Riwayat Aktivitas";
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(670, 15);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 37);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // RiwayatAktivitas
            // 
            ClientSize = new Size(784, 561);
            Controls.Add(btnRefresh);
            Controls.Add(lblTitle);
            Controls.Add(dgvRiwayat);
            Name = "RiwayatAktivitas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Riwayat Aktivitas - Admin";
            Load += RiwayatAktivitas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRiwayat).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = AdminDAO.GetLogAktivitas();
                dgvRiwayat.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat log: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void RiwayatAktivitas_Load(object sender, EventArgs e)
        {

        }
    }
}
