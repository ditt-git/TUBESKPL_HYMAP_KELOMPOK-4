using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace HYMAPSOPIR
{
    public partial class Dashboard : Form, IObserver
    {
        private Sopir sopirAktif;
        private List<Pelanggan> databasePelanggan;
        private DateTime currentDate;
        private JadwalService serviceJadwal;
        private AudioTerkirim _audioObserver = new AudioTerkirim();

        public Dashboard(Sopir sopir)
        {
            InitializeComponent();

            this.sopirAktif = sopir;

            this.serviceJadwal = new JadwalService();


            currentDate = dtpTanggal.Value;

            this.serviceJadwal.CekDanGenerateJadwalHariIni(this.sopirAktif, currentDate);


            // Tampilkan nama dan armada
            lblNamaSopir.Text = sopirAktif.Nama;
            lblArmada.Text = sopirAktif.ArmadaTugas.ToString();

            // Tampilkan daftar pengiriman
            BindDataPengiriman();

        }

        private void BindDataPengiriman()
        {
            databasePelanggan = Library.Database.PelangganDAO.GetAllPelanggan();

            this.serviceJadwal.SetTugasBerdasarkanArmada(this.sopirAktif, databasePelanggan, currentDate);

            var listTugas = sopirAktif.DaftarTugasHariIni;
            var displayList = new List<object>();


            foreach (var tugas in listTugas)
            {
                // Attach observer pattern 
                tugas.Attach(this);
                tugas.Attach(_audioObserver);

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

        // Notifikasi observer pattern untuk menerima notifikasi perubahan status pengiriman
        public void Update(string notifikasiTerkirim)
        {
            MessageBox.Show(notifikasiTerkirim, "Notifikasi Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void dgvPengiriman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Ambil data tugas dari List sesuai urutan baris yang diklik
                Pengiriman tugasTerpilih = sopirAktif.DaftarTugasHariIni[e.RowIndex];

                DetailPengiriman formDetail = new DetailPengiriman(tugasTerpilih);

                formDetail.DataPengirimanDiubah += FormDetail_DataPengirimanDiubah;
                formDetail.ShowDialog();
            }

        }

        private void FormDetail_DataPengirimanDiubah(object sender, EventArgs e)
        {
            BindDataPengiriman();
        }

        // Event ketika tanggal berubah
        private void dtpTanggal_ValueChanged_1(object sender, EventArgs e)
        {
            currentDate = dtpTanggal.Value;

            this.serviceJadwal.CekDanGenerateJadwalHariIni(this.sopirAktif, currentDate);

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

                this.Close();
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