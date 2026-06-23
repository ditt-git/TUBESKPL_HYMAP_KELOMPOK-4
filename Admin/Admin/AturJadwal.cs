using System;
using System.Data;
using System.Windows.Forms;
using Admin.Library.Database;
using Library.Commands;

namespace Admin
{
    public partial class AturJadwal : Form
    {
        private int idJadwalTerpilih = -1;
        private CommandInvoker _invoker = new CommandInvoker();

        public AturJadwal()
        {
            InitializeComponent();
        }

        private void AturJadwal_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadDataJadwal();
        }

        private void LoadComboBoxData()
        {
            try
            {
                DataTable dtPelanggan = AdminDAO.GetListPelangganAktif();
                comboBoxPelanggan.DataSource = dtPelanggan;
                comboBoxPelanggan.DisplayMember = "nama_pelanggan";
                comboBoxPelanggan.ValueMember = "id_pelanggan";
                comboBoxPelanggan.SelectedIndex = -1;

                DataTable dtSopir = AdminDAO.GetListSopirAktif();
                comboBoxSopir.DataSource = dtSopir;
                comboBoxSopir.DisplayMember = "nama";
                comboBoxSopir.ValueMember = "id_user";
                comboBoxSopir.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar Pelanggan atau Sopir: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataJadwal()
        {
            try
            {
                dataGridView1.DataSource = AdminDAO.GetAllJadwal();
                dataGridView1.ClearSelection();
                
                if (dataGridView1.Columns.Contains("id_pelanggan"))
                    dataGridView1.Columns["id_pelanggan"].Visible = false;
                
                if (dataGridView1.Columns.Contains("id_user"))
                    dataGridView1.Columns["id_user"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar Jadwal Pengiriman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            idJadwalTerpilih = -1;
            dateTimePickerTanggal.Value = DateTime.Now;
            comboBoxPelanggan.SelectedIndex = -1;
            comboBoxSopir.SelectedIndex = -1;
            textBoxJumlahPesanan.Clear();
            dataGridView1.ClearSelection();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                idJadwalTerpilih = Convert.ToInt32(row.Cells["ID Jadwal"].Value);
                dateTimePickerTanggal.Value = Convert.ToDateTime(row.Cells["Tanggal"].Value);
                
                comboBoxPelanggan.SelectedValue = row.Cells["id_pelanggan"].Value;
                comboBoxSopir.SelectedValue = row.Cells["id_user"].Value;
                
                textBoxJumlahPesanan.Text = row.Cells["Jumlah Pesanan"].Value.ToString();
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            if (comboBoxPelanggan.SelectedValue == null || comboBoxSopir.SelectedValue == null || string.IsNullOrWhiteSpace(textBoxJumlahPesanan.Text))
            {
                MessageBox.Show("Pastikan Pelanggan, Sopir, dan Jumlah Pesanan terisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxJumlahPesanan.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah Pesanan harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ICommand command = new TambahJadwalCommand(
                    dateTimePickerTanggal.Value,
                    Convert.ToInt32(comboBoxPelanggan.SelectedValue),
                    Convert.ToInt32(comboBoxSopir.SelectedValue),
                    jumlah
                );
                
                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Jadwal Pengiriman berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDataJadwal();
                }
                else
                {
                    MessageBox.Show("Gagal menambah jadwal.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (idJadwalTerpilih == -1)
            {
                MessageBox.Show("Pilih jadwal yang ingin diedit terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxPelanggan.SelectedValue == null || comboBoxSopir.SelectedValue == null || string.IsNullOrWhiteSpace(textBoxJumlahPesanan.Text))
            {
                MessageBox.Show("Pastikan Pelanggan, Sopir, dan Jumlah Pesanan terisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxJumlahPesanan.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah Pesanan harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ICommand command = new EditJadwalCommand(
                    idJadwalTerpilih,
                    dateTimePickerTanggal.Value,
                    Convert.ToInt32(comboBoxPelanggan.SelectedValue),
                    Convert.ToInt32(comboBoxSopir.SelectedValue),
                    jumlah
                );

                if (_invoker.ExecuteCommand(command))
                {
                    MessageBox.Show("Jadwal Pengiriman berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDataJadwal();
                }
                else
                {
                    MessageBox.Show("Gagal mengedit jadwal. Data mungkin sudah dihapus.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengedit jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            if (idJadwalTerpilih == -1)
            {
                MessageBox.Show("Pilih jadwal yang ingin dihapus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Yakin ingin menghapus jadwal ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    ICommand command = new HapusJadwalCommand(idJadwalTerpilih);
                    if (_invoker.ExecuteCommand(command))
                    {
                        MessageBox.Show("Jadwal berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadDataJadwal();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menghapus jadwal. Data mungkin sudah terhapus.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
