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

        // Mendeklarasikan event Observer
        public event EventHandler DataPengirimanDiubah;

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

            jumlahpinjamgalon.Text = tugas.DataPelanggan.GalonDipinjam.ToString() + " Galon";

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

                int galonKembaliInput = (int)numGalonKembali.Value;

                // Menggunakan Command Pattern
                Library.Commands.ICommand updateCommand = new Library.Commands.UpdatePengirimanCommand(
                    tugasPengiriman,
                    statusKirimBaru,
                    statusBayarBaru,
                    galonKembaliInput
                );

                updateCommand.Execute();


                MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Memicu event untuk memberi tahu Dashboard bahwa data sudah diubah
                DataPengirimanDiubah?.Invoke(this, EventArgs.Empty);

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
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}