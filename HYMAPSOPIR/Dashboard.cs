using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace HYMAPSOPIR
{
    public partial class Dashboard : Form
    {
        private Sopir sopirAktif;
        private List<Pelanggan> databasePelanggan;
        private DateTime currentDate;

        public Dashboard(Sopir sopirLogin)
        {
            InitializeComponent();

            sopirAktif = sopirLogin;

            // Ambil tanggal dari DateTimePicker
            currentDate = dtpTanggal.Value;

            // Set tugas sopir berdasarkan tanggal tersebut
            sopirAktif.SetTugasBerdasarkanArmada(DatabaseSimulasi.PelangganDB, currentDate);

            // Tampilkan nama dan armada
            lblNamaSopir.Text = sopirAktif.Nama;
            lblArmada.Text = sopirAktif.ArmadaTugas.ToString();

            // Tampilkan daftar pengiriman
            BindDataPengiriman();

        }


        private void BindDataPengiriman()
        {
            var listTugas = sopirAktif.DaftarTugasHariIni;
            var displayList = new List<object>();

            foreach (var tugas in listTugas)
            {
                displayList.Add(new
                {
                    NamaPelanggan = tugas.DataPelanggan.NamaPelanggan,
                    Alamat = tugas.DataPelanggan.Alamat,
                    StatusKirim = tugas.StatusKirim.ToString(),
                    StatusBayar = tugas.StatusBayar.ToString(),
                    Prioritas = tugas.Prioritas.ToString()
                });
            }

            dgvPengiriman.DataSource = null;
            dgvPengiriman.DataSource = displayList;
            dgvPengiriman.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }



        private void dgvPengiriman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Ambil data tugas dari List sesuai urutan baris yang diklik
                Pengiriman tugasTerpilih = sopirAktif.DaftarTugasHariIni[e.RowIndex];

                DetailPengiriman formDetail = new DetailPengiriman(tugasTerpilih);

                // Tampilkan form menggunakan ShowDialog
                formDetail.ShowDialog();

                //refresh tble
                BindDataPengiriman();
            }

        }


        // Event ketika tanggal berubah
        private void dtpTanggal_ValueChanged_1(object sender, EventArgs e)
        {
            currentDate = dtpTanggal.Value;
            // Refresh tugas berdasarkan tanggal baru
            sopirAktif.SetTugasBerdasarkanArmada(DatabaseSimulasi.PelangganDB, currentDate);
            BindDataPengiriman();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Menampilkan kotak pesan dengan ikon peringatan dan tombol Yes No
            DialogResult result = MessageBox.Show("yakin keluar?",
                                                  "Konfirmasi",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FormLogin halamanBaru = new FormLogin();
                halamanBaru.Show();
                this.Hide();
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvPengiriman_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}