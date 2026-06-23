using Admin.Library.Database;
using System;
using System.Data;
using System.Windows.Forms;

namespace Admin
{
    public partial class LaporanPengiriman : Form
    {
        public LaporanPengiriman()
        {
            InitializeComponent();
        }

        private void LaporanPengiriman_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView1.DataSource = AdminDAO.GetAllLaporan();
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data laporan pengiriman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
