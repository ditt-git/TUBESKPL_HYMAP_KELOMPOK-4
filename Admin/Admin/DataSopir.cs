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
    public partial class DataSopir : Form
    {

        private int idUserTerpilih = 0;
        public DataSopir()
        {
            InitializeComponent();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.buttoneditsopir.Click += new System.EventHandler(this.buttoneditsopir_Click);

        }

        private void DataSopir_Load(object sender, EventArgs e)
        {
            LoadData();

        }
        private void LoadData()
        {
            try
            {
                dataGridView1.DataSource = AdminDAO.GetAllSopir();
                dataGridView1.ClearSelection();

                // Mereset memori ID dan mengosongkan semua TextBox
                idUserTerpilih = 0;
                nmsopir.Clear();
                notelpsopir.Clear();
                usernmsopir.Clear();
                pwsopir.Clear();
                idarmadasopir.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data sopir: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nmsopir.Text) || string.IsNullOrWhiteSpace(usernmsopir.Text) ||
                string.IsNullOrWhiteSpace(pwsopir.Text) || string.IsNullOrWhiteSpace(idarmadasopir.Text))
            {
                MessageBox.Show("Nama, Username, Password, dan Id Armada wajib diisi untuk menambah sopir baru!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi format angka untuk Id Armada
            if (!int.TryParse(idarmadasopir.Text, out int idArmada))
            {
                MessageBox.Show("Id Armada harus berupa angka (1-4)!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                AdminDAO.TambahSopir(nmsopir.Text, notelpsopir.Text, usernmsopir.Text, pwsopir.Text, idArmada);
                MessageBox.Show("Sopir berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Segarkan tampilan
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Memastikan yang diklik adalah data, bukan header
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // 1. Simpan ID User sebagai syarat edit
                idUserTerpilih = Convert.ToInt32(row.Cells["id_user"].Value);

                // 2. Pindahkan isi tabel ke TextBox untuk diedit
                nmsopir.Text = row.Cells["nama"].Value.ToString();
                notelpsopir.Text = row.Cells["no_telepon"].Value.ToString();
                usernmsopir.Text = row.Cells["username"].Value.ToString();
                idarmadasopir.Text = row.Cells["id_armada"].Value.ToString();

                // Kosongkan password untuk keamanan (password lama tidak ditampilkan)
                pwsopir.Text = "";
            }
        }

        private void buttoneditsopir_Click(object sender, EventArgs e)
        {
            if (idUserTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu sebelum mengedit!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Batalkan proses edit
            }

            // SYARAT 2: Input penting tidak boleh kosong
            if (string.IsNullOrWhiteSpace(nmsopir.Text) || string.IsNullOrWhiteSpace(usernmsopir.Text) || string.IsNullOrWhiteSpace(idarmadasopir.Text))
            {
                MessageBox.Show("Nama, Username, dan Id Armada tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(idarmadasopir.Text, out int idArmada))
            {
                MessageBox.Show("Id Armada harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Eksekusi EditSopir
                AdminDAO.EditSopir(idUserTerpilih, nmsopir.Text, notelpsopir.Text, usernmsopir.Text, idArmada);
                MessageBox.Show("Data Sopir berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Segarkan tampilan
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan perubahan ke database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
