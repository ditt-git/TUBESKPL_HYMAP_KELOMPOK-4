using Admin.Library.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Admin
{
    public partial class Armada : Form
    {
        private int idArmadaTerpilih = 0;

        public Armada()
        {
            InitializeComponent();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.buttonedit.Click += new System.EventHandler(this.buttonedit_Click);
        }

        private void Armada_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                dataGridView1.DataSource = AdminDAO.GetAllArmada();
                dataGridView1.ClearSelection();

                // Mereset form dan ID saat data dimuat ulang
                idArmadaTerpilih = 0;
                tb_nmwilayah.Clear();
                tb_hargakirim.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat referensi armada: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Simpan ID Armada sebagai syarat edit
                idArmadaTerpilih = Convert.ToInt32(row.Cells["id_armada"].Value);

                // Pindahkan isi tabel ke TextBox untuk diedit
                tb_nmwilayah.Text = row.Cells["nama_wilayah"].Value.ToString();

                // Konversi harga untuk menghilangkan angka nol di belakang koma
                decimal harga = Convert.ToDecimal(row.Cells["harga_pengiriman"].Value);
                tb_hargakirim.Text = Math.Round(harga, 0).ToString();
            }
        }

        private void buttonedit_Click(object sender, EventArgs e)
        {
            // SYARAT 1: Harus klik tabel dulu (Jika ID masih 0 berarti belum klik)
            if (idArmadaTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu sebelum mengedit!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Batalkan proses edit
            }

            // SYARAT 2: Input tidak boleh kosong
            if (string.IsNullOrWhiteSpace(tb_nmwilayah.Text) || string.IsNullOrWhiteSpace(tb_hargakirim.Text))
            {
                MessageBox.Show("Nama wilayah dan harga pengiriman tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SYARAT 3: Harga harus berupa angka
            if (!decimal.TryParse(tb_hargakirim.Text, out decimal hargaBaru))
            {
                MessageBox.Show("Harga pengiriman harus berupa angka yang valid tanpa titik/koma sembarangan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Eksekusi penyimpanan ke Database MySQL
                AdminDAO.EditArmada(idArmadaTerpilih, tb_nmwilayah.Text, hargaBaru);

                MessageBox.Show("Data Armada berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan perubahan ke database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
