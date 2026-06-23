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
    public partial class DataPelanggan : Form
    {
        private int idPelangganTerpilih = 0;
        private int lastClickedRowIndex = -1; // Untuk toggle row selection
        private CommandInvoker _invoker = new CommandInvoker();

        public DataPelanggan()
        {
            InitializeComponent();
        }

        private void DataPelanggan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView1.DataSource = AdminDAO.GetAllPelanggan();
                dataGridView1.ClearSelection();

                // Mereset memori ID dan mengosongkan semua TextBox
                idPelangganTerpilih = 0;
                lastClickedRowIndex = -1;
                namapelanggan.Clear();
                alamatpelanggan.Clear();
                notelppelanggan.Clear();
                galondipinjam.Clear();
                idarmadapelanggan.Clear();
                dateTimePicker1.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data pelanggan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleRowClick(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                if (rowIndex == lastClickedRowIndex)
                {
                    idPelangganTerpilih = 0;
                    lastClickedRowIndex = -1;
                    namapelanggan.Clear();
                    alamatpelanggan.Clear();
                    notelppelanggan.Clear();
                    galondipinjam.Clear();
                    idarmadapelanggan.Clear();
                    dateTimePicker1.Value = DateTime.Today;
                    dataGridView1.ClearSelection();
                    return;
                }

                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                // 1. Simpan ID Pelanggan sebagai syarat edit
                idPelangganTerpilih = Convert.ToInt32(row.Cells["id_pelanggan"].Value);
                lastClickedRowIndex = rowIndex;

                // 2. Pindahkan isi tabel ke TextBox untuk diedit
                namapelanggan.Text = row.Cells["nama_pelanggan"].Value?.ToString() ?? "";
                alamatpelanggan.Text = row.Cells["alamat"].Value?.ToString() ?? "";
                notelppelanggan.Text = row.Cells["no_telepon"].Value?.ToString() ?? "";
                galondipinjam.Text = row.Cells["galon_dipinjam"].Value?.ToString() ?? "0";
                idarmadapelanggan.Text = row.Cells["id_wilayah"].Value?.ToString() ?? "";
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

        // Tombol Tambah Pelanggan
        private void button1_Click(object sender, EventArgs e)
        {
            // Mencegah duplicate data jika user mengklik "Tambah" saat sebuah baris data sedang dipilih
            if (idPelangganTerpilih != 0)
            {
                MessageBox.Show("Data ini sudah ada di database! Jika ingin mengubah data ini, gunakan tombol 'Edit'.\n\nJika ingin menambah pelanggan baru, kosongkan form terlebih dahulu dengan mengklik ulang baris tabel yang sedang dipilih (warna biru).", "Peringatan (Data Terduplikasi)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(namapelanggan.Text) || string.IsNullOrWhiteSpace(idarmadapelanggan.Text))
            {
                MessageBox.Show("Nama dan Id Wilayah wajib diisi untuk menambah pelanggan baru!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(idarmadapelanggan.Text, out int idWilayah))
            {
                MessageBox.Show("Id Wilayah harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int galon = 0;
            if (!string.IsNullOrWhiteSpace(galondipinjam.Text) && !int.TryParse(galondipinjam.Text, out galon))
            {
                MessageBox.Show("Galon dipinjam harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi sebelum tambah
            DialogResult konfirmasi = MessageBox.Show("Yakin ingin menambah pelanggan baru?", "Konfirmasi Tambah", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                ICommand command = new TambahPelangganCommand(namapelanggan.Text, alamatpelanggan.Text, notelppelanggan.Text, idWilayah, galon);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Pelanggan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Gagal menambah data, periksa kembali input Anda.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah data pelanggan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tombol Edit Pelanggan
        private void buttoneditpelanggan_Click(object sender, EventArgs e)
        {
            if (idPelangganTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu sebelum mengedit!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(namapelanggan.Text) || string.IsNullOrWhiteSpace(idarmadapelanggan.Text))
            {
                MessageBox.Show("Nama dan Id Wilayah tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(idarmadapelanggan.Text, out int idWilayah))
            {
                MessageBox.Show("Id Wilayah harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int galon = 0;
            if (!string.IsNullOrWhiteSpace(galondipinjam.Text) && !int.TryParse(galondipinjam.Text, out galon))
            {
                MessageBox.Show("Galon dipinjam harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi sebelum edit
            DialogResult konfirmasi = MessageBox.Show("Yakin ingin mengubah data pelanggan ini?", "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            buttoneditpelanggan.Enabled = false;
            try
            {
                ICommand command = new EditPelangganCommand(idPelangganTerpilih, namapelanggan.Text, alamatpelanggan.Text, notelppelanggan.Text, idWilayah, galon);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Data Pelanggan berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
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
                buttoneditpelanggan.Enabled = true;
            }
        }

        // Tombol Nonaktifkan Pelanggan (Soft Delete)
        private void buttonNonaktifkanPelanggan_Click(object sender, EventArgs e)
        {
            if (idPelangganTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lastClickedRowIndex < 0 || lastClickedRowIndex >= dataGridView1.Rows.Count)
            {
                MessageBox.Show("Baris tidak valid, muat ulang data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cek apakah pelanggan sudah nonaktif
            DataGridViewRow row = dataGridView1.Rows[lastClickedRowIndex];
            int statusSekarang = Convert.ToInt32(row.Cells["is_active"].Value);
            if (statusSekarang == 0)
            {
                MessageBox.Show("Pelanggan ini sudah dalam status nonaktif!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show("Yakin ingin menonaktifkan pelanggan ini?\nPelanggan yang dinonaktifkan tidak akan muncul di jadwal pengiriman.", "Konfirmasi Nonaktifkan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                ICommand command = new ToggleStatusPelangganCommand(idPelangganTerpilih, false);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Pelanggan berhasil dinonaktifkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Pelanggan gagal dinonaktifkan, data mungkin sudah berubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menonaktifkan pelanggan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tombol Aktifkan Pelanggan Kembali
        private void buttonAktifkanPelanggan_Click(object sender, EventArgs e)
        {
            if (idPelangganTerpilih == 0)
            {
                MessageBox.Show("Silakan klik salah satu baris pada tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lastClickedRowIndex < 0 || lastClickedRowIndex >= dataGridView1.Rows.Count)
            {
                MessageBox.Show("Baris tidak valid, muat ulang data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cek apakah pelanggan sudah aktif
            DataGridViewRow row = dataGridView1.Rows[lastClickedRowIndex];
            int statusSekarang = Convert.ToInt32(row.Cells["is_active"].Value);
            if (statusSekarang == 1)
            {
                MessageBox.Show("Pelanggan ini sudah dalam status aktif!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show("Yakin ingin mengaktifkan kembali pelanggan ini?", "Konfirmasi Aktifkan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                ICommand command = new ToggleStatusPelangganCommand(idPelangganTerpilih, true);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Pelanggan berhasil diaktifkan kembali!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Pelanggan gagal diaktifkan, data mungkin sudah berubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengaktifkan pelanggan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void buttonKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}
