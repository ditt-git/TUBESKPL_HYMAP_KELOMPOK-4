using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace HYMAPSOPIR
{
    public partial class Form1 : Form
    {
        private Sopir sopirAktif;
        private List<Pelanggan> databasePelanggan;
        private DateTime currentDate;

        public Form1(Sopir sopirLogin)
        {
            InitializeComponent();

            // Inisialisasi data pelanggan
            databasePelanggan = new List<Pelanggan>
            {
                new Pelanggan("P001", "Tono", "Jl. Teuku Umar", Armada.Denpasar, new DateTime(2026, 4, 22)),
                new Pelanggan("P002", "Pak RT", "Amlapura", Armada.Karangasem, new DateTime(2026, 4, 20)),
                new Pelanggan("P003", "Budi", "Ubud", Armada.Gianyar, new DateTime(2026, 4, 21)),
                new Pelanggan("P004", "Siti", "Renon", Armada.Denpasar, new DateTime(2026, 4, 23)),
            };

            // Sopir (login)
            sopirAktif = sopirLogin;

            // Ambil tanggal dari DateTimePicker
            currentDate = dtpTanggal.Value;

            // Set tugas sopir berdasarkan tanggal tersebut
            sopirAktif.SetTugasBerdasarkanArmada(databasePelanggan, currentDate);

            // Tampilkan nama dan armada
            lblNamaSopir.Text = sopirAktif.Nama;
            lblArmada.Text = sopirAktif.ArmadaTugas.ToString();

            // Tampilkan daftar pengiriman
            BindDataPengiriman();

            // Pasang event handler untuk perubahan tanggal
            dtpTanggal.ValueChanged += DtpTanggal_ValueChanged;
        }

        // Event ketika tanggal berubah
        private void DtpTanggal_ValueChanged(object sender, EventArgs e)
        {
            currentDate = dtpTanggal.Value;
            // Refresh tugas berdasarkan tanggal baru
            sopirAktif.SetTugasBerdasarkanArmada(databasePelanggan, currentDate);
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
                var row = dgvPengiriman.Rows[e.RowIndex];
                var cellValue = row.Cells["NamaPelanggan"]?.Value;
                if (cellValue != null)
                {
                    string namaPelanggan = cellValue.ToString();
                    MessageBox.Show($"Halaman detail pengiriman.",
                                    "Informasi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data pelanggan tidak valid.");
                }
            }
        }

        private void dtpTanggal_ValueChanged_1(object sender, EventArgs e)
        {

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
    }
}