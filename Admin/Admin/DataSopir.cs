using Admin.Library.Database;
using Library.Commands;
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
        private int lastClickedRowIndex = -1; // Untuk toggle row selection
        private CommandInvoker _invoker = new CommandInvoker();
        public DataSopir()
        {
            InitializeComponent();
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
                lastClickedRowIndex = -1;
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

        // Tombol Tambah Sopir
        private void button1_Click(object sender, EventArgs e)
        {
            // Mencegah duplicate data jika user mengklik "Tambah" saat sebuah baris data sedang dipilih
            if (idUserTerpilih != 0)
            {
                MessageBox.Show("Data ini sudah ada di database! Jika ingin mengubah data ini, gunakan tombol 'Edit'.\n\nJika ingin menambah sopir baru, kosongkan form terlebih dahulu dengan mengklik ulang baris tabel yang sedang dipilih (warna biru).", "Peringatan (Data Terduplikasi)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(nmsopir.Text) || string.IsNullOrWhiteSpace(usernmsopir.Text) ||
                string.IsNullOrWhiteSpace(pwsopir.Text) || string.IsNullOrWhiteSpace(idarmadasopir.Text))
            {
                MessageBox.Show("Nama, Username, Password, dan Id Armada wajib diisi untuk menambah sopir baru!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(idarmadasopir.Text, out int idArmada))
            {
                MessageBox.Show("Id Armada harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi sebelum tambah
            DialogResult konfirmasi = MessageBox.Show("Yakin ingin menambah sopir baru?", "Konfirmasi Tambah", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                ICommand command = new TambahSopirCommand(nmsopir.Text, notelpsopir.Text, usernmsopir.Text, pwsopir.Text, idArmada);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Sopir berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Segarkan tampilan
                }
                else
                {
                    MessageBox.Show("Gagal menambah data, periksa kembali input Anda.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void HandleRowClick(int rowIndex)
        {
            if (rowIndex >= 0) // Memastikan yang diklik adalah data, bukan header
            {
                // Toggle: jika klik row yang sama → kosongkan dan reset
                if (rowIndex == lastClickedRowIndex)
                {
                    idUserTerpilih = 0;
                    lastClickedRowIndex = -1;
                    nmsopir.Clear();
                    notelpsopir.Clear();
                    usernmsopir.Clear();
                    pwsopir.Clear();
                    idarmadasopir.Clear();
                    dataGridView1.ClearSelection();
                    return;
                }

                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                // 1. Simpan ID User sebagai syarat edit
                idUserTerpilih = Convert.ToInt32(row.Cells["id_user"].Value);
                lastClickedRowIndex = rowIndex;

                // 2. Pindahkan isi tabel ke TextBox untuk diedit
                nmsopir.Text = row.Cells["nama"].Value.ToString();
                notelpsopir.Text = row.Cells["no_telepon"].Value?.ToString() ?? "";
                usernmsopir.Text = row.Cells["username"].Value.ToString();
                idarmadasopir.Text = row.Cells["id_wilayah"].Value.ToString();

                // Kosongkan password untuk keamanan (password lama tidak ditampilkan)
                pwsopir.Text = "";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowClick(e.RowIndex);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            HandleRowClick(e.RowIndex);
        }

        // Tombol Edit Sopir
        private void buttoneditsopir_Click(object sender, EventArgs e)
        {
            if (idUserTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu sebelum mengedit!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Batalkan proses edit
            }

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

            // Konfirmasi sebelum edit
            DialogResult konfirmasi = MessageBox.Show("Yakin ingin mengubah data sopir ini?", "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            buttoneditsopir.Enabled = false;
            try
            {
                // Eksekusi EditSopir menggunakan Command Pattern
                ICommand command = new EditSopirCommand(idUserTerpilih, nmsopir.Text, notelpsopir.Text, usernmsopir.Text, idArmada, pwsopir.Text);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Data Sopir berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Segarkan tampilan
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan perubahan ke database. Data mungkin sudah dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan perubahan ke database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttoneditsopir.Enabled = true;
            }
        }

        // Tombol Nonaktifkan Sopir (Soft Delete)
        private void buttonNonaktifkanSopir_Click(object sender, EventArgs e)
        {
            if (idUserTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lastClickedRowIndex < 0 || lastClickedRowIndex >= dataGridView1.Rows.Count)
            {
                MessageBox.Show("Baris tidak valid, muat ulang data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cek apakah sopir sudah nonaktif
            DataGridViewRow row = dataGridView1.Rows[lastClickedRowIndex];
            int statusSekarang = Convert.ToInt32(row.Cells["is_active"].Value);
            if (statusSekarang == 0)
            {
                MessageBox.Show("Sopir ini sudah dalam status nonaktif!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show("Yakin ingin menonaktifkan sopir ini?\nSopir yang dinonaktifkan tidak akan bisa login.", "Konfirmasi Nonaktifkan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                ICommand command = new ToggleStatusSopirCommand(idUserTerpilih, false);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Sopir berhasil dinonaktifkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Sopir gagal dinonaktifkan, data mungkin sudah berubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menonaktifkan sopir: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tombol Aktifkan Sopir Kembali
        private void buttonAktifkanSopir_Click(object sender, EventArgs e)
        {
            if (idUserTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lastClickedRowIndex < 0 || lastClickedRowIndex >= dataGridView1.Rows.Count)
            {
                MessageBox.Show("Baris tidak valid, muat ulang data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cek apakah sopir sudah aktif
            DataGridViewRow row = dataGridView1.Rows[lastClickedRowIndex];
            int statusSekarang = Convert.ToInt32(row.Cells["is_active"].Value);
            if (statusSekarang == 1)
            {
                MessageBox.Show("Sopir ini sudah dalam status aktif!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show("Yakin ingin mengaktifkan kembali sopir ini?", "Konfirmasi Aktifkan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                ICommand command = new ToggleStatusSopirCommand(idUserTerpilih, true);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Sopir berhasil diaktifkan kembali!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Sopir gagal diaktifkan, data mungkin sudah berubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengaktifkan sopir: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
    }
}
