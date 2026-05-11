using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HYMAPSOPIR
{
    public partial class DetailPengiriman : Form
    {
        private Pengiriman tugasPengiriman;

        public DetailPengiriman(Pengiriman tugas)
        {
            InitializeComponent();
            tugasPengiriman = tugas;

            radioButton3.Text = "Gagal";

            labelNamaPelanggan.Text = tugas.DataPelanggan.NamaPelanggan;
            labelAlamatPelanggan.Text = tugas.DataPelanggan.Alamat;
            labelArmada.Text = tugas.DataPelanggan.Wilayah.ToString();
            labelPrioritas.Text = tugas.Prioritas.ToString();
            labelBuktiKirim.Text = string.IsNullOrEmpty(tugas.BuktiFoto) ? "-" : tugas.BuktiFoto;

            if (tugas.StatusKirim == StatusPengiriman.BelumTerkirim) radioButton1.Checked = true;
            else if (tugas.StatusKirim == StatusPengiriman.SudahTerkirim) radioButton2.Checked = true;
            else radioButton3.Checked = true;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "BelumBayar", "Cash", "Transfer", "Bon" });
            comboBox1.SelectedItem = tugas.StatusBayar.ToString();



        }

        private void DetailPengiriman_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void labelNamaPelanggan_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonKembali_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void buttonLanjut_Click(object sender, EventArgs e)
        {
            // Simpan perubahan Status Pengiriman
            if (radioButton1.Checked) tugasPengiriman.StatusKirim = StatusPengiriman.BelumTerkirim;
            else if (radioButton2.Checked) tugasPengiriman.StatusKirim = StatusPengiriman.SudahTerkirim;
            else if (radioButton3.Checked) tugasPengiriman.StatusKirim = StatusPengiriman.Gagal;

            // Simpan perubahan Status Pembayaran
            if (comboBox1.SelectedItem != null)
            {
                string bayar = comboBox1.SelectedItem.ToString();
                if (bayar == "BelumBayar") tugasPengiriman.StatusBayar = StatusPembayaran.BelumBayar;
                else if (bayar == "Cash") tugasPengiriman.StatusBayar = StatusPembayaran.Cash;
                else if (bayar == "Transfer") tugasPengiriman.StatusBayar = StatusPembayaran.Transfer;
                else if (bayar == "Bon") tugasPengiriman.StatusBayar = StatusPembayaran.Bon;
            }

            // Jika barang terkirim, update otomatis tanggal pengiriman pelanggan
            if (tugasPengiriman.StatusKirim == StatusPengiriman.SudahTerkirim)
            {
                tugasPengiriman.DataPelanggan.UpdateTanggalPengirimanBerhasil(DateTime.Now);
            }

            MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Tutup form, kembali ke Form1
            this.Close();
        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
