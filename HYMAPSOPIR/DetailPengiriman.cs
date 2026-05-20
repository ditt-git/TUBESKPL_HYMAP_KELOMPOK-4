using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            radioButton1.Text = "Belum Terkirim";
            radioButton2.Text = "Sudah Terkirim";
            radioButton3.Text = "Gagal";

            labelNamaPelanggan.Text = tugas.DataPelanggan.NamaPelanggan;
            labelAlamatPelanggan.Text = tugas.DataPelanggan.Alamat;
            labelArmada.Text = tugas.DataPelanggan.Wilayah.ToString();
            labelPrioritas.Text = tugas.Prioritas.ToString();
            labelBuktiKirim.Text = string.IsNullOrEmpty(tugas.BuktiFoto) ? "-" : tugas.BuktiFoto;

            if (tugas.StatusKirim == (StatusPengiriman)0) radioButton1.Checked = true;
            else if (tugas.StatusKirim == (StatusPengiriman)1) radioButton2.Checked = true;
            else radioButton3.Checked = true;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Bon", "Cash", "Transfer" });
            comboBox1.SelectedItem = tugas.StatusBayar.ToString();
        }

        private void DetailPengiriman_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLanjut_Click(object sender, EventArgs e)
        {
            try
            {
                StatusPengiriman statusKirimBaru;
                if (radioButton1.Checked) statusKirimBaru = (StatusPengiriman)0;
                else if (radioButton2.Checked) statusKirimBaru = (StatusPengiriman)1;
                else statusKirimBaru = (StatusPengiriman)2;

                StatusPembayaran statusBayarBaru = StatusPembayaran.Bon;
                if (comboBox1.SelectedItem != null)
                {
                    string bayar = comboBox1.SelectedItem.ToString();
                    if (bayar == "Cash") statusBayarBaru = StatusPembayaran.Cash;
                    else if (bayar == "Transfer") statusBayarBaru = StatusPembayaran.Transfer;
                    else statusBayarBaru = StatusPembayaran.Bon;
                }

                //DESIGN BY CONTRACT
                if (statusKirimBaru == (StatusPengiriman)0 || statusKirimBaru == (StatusPengiriman)2)
                {
                    //Memastikan status bayar harus Bon
                    Debug.Assert(statusBayarBaru == StatusPembayaran.Bon,
                        "KONTRAK: Belum terkirim atau gagal tidak dapat pilih pembayaran selain bon!");
                }

                tugasPengiriman.StatusKirim = statusKirimBaru;
                tugasPengiriman.StatusBayar = statusBayarBaru;

                if (tugasPengiriman.StatusKirim == StatusPengiriman.SudahTerkirim)
                {
                    tugasPengiriman.DataPelanggan.UpdateTanggalPengirimanBerhasil(DateTime.Now);
                }

                MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Peringatan Kontrak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void labelNamaPelanggan_Click(object sender, EventArgs e) { }
        private void ButtonKembali_Click(object sender, EventArgs e) { }
        private void label_Click(object sender, EventArgs e) { }
        private void DetailPengiriman_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}