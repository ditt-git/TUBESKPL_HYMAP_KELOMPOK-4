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
    public partial class Armada : Form
    {
        private int idArmadaTerpilih = 0;
        private int lastClickedRowIndex = -1; 
        private CommandInvoker _invoker = new CommandInvoker();

        public Armada()
        {
            InitializeComponent();
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

                idArmadaTerpilih = 0;
                lastClickedRowIndex = -1;
                tb_nmwilayah.Clear();
                tb_hargakirim.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat referensi wilayah: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleRowClick(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                if (rowIndex == lastClickedRowIndex)
                {
                    idArmadaTerpilih = 0;
                    lastClickedRowIndex = -1;
                    tb_nmwilayah.Clear();
                    tb_hargakirim.Clear();
                    dataGridView1.ClearSelection();
                    return;
                }

                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                idArmadaTerpilih = Convert.ToInt32(row.Cells["id_wilayah"].Value);
                lastClickedRowIndex = rowIndex;

                tb_nmwilayah.Text = row.Cells["nama_wilayah"].Value.ToString();

                decimal harga = Convert.ToDecimal(row.Cells["harga_pengiriman"].Value);
                tb_hargakirim.Text = Math.Round(harga, 0).ToString();
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

            // Konfirmasi sebelum edit
            DialogResult konfirmasi = MessageBox.Show("Yakin ingin mengubah data wilayah ini?", "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            buttonedit.Enabled = false;
            try
            {
                // Eksekusi penyimpanan menggunakan Command Pattern
                ICommand command = new EditArmadaCommand(idArmadaTerpilih, tb_nmwilayah.Text, hargaBaru);
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Data Wilayah berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan perubahan ke database.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan perubahan ke database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonedit.Enabled = true;
            }
        }

        private void buttonKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
