using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Admin
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSopir formSopir = new DataSopir();
            formSopir.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Armada formArmada = new Armada();
            formArmada.ShowDialog();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void tb_datapelanggan_Click(object sender, EventArgs e)
        {
            DataPelanggan formPelanggan = new DataPelanggan();
            formPelanggan.ShowDialog();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Yakin ingin logout?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void tb_laporan_Click(object sender, EventArgs e)
        {
            LaporanPengiriman formLaporan = new LaporanPengiriman();
            formLaporan.ShowDialog();
        }

        private void tb_aturjadwal_Click(object sender, EventArgs e)
        {
            AturJadwal formAturJadwal = new AturJadwal();
            formAturJadwal.ShowDialog();
        }

        private void tb_logaktivitas_Click(object sender, EventArgs e)
        {
            RiwayatAktivitas formRiwayat = new RiwayatAktivitas();
            formRiwayat.ShowDialog();
        }
    }
}
