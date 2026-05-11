using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace HYMAPSOPIR
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }



        private void LabelJudul_Click(object sender, EventArgs e)
        {
         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Menggunakan konfigurasi runtime
            this.Text = ConfigManager.Instance.AppName;
            LabelJudul.Text = ConfigManager.Instance.AppName;

            this.Text = ConfigManager.Instance.Version;
            LabelVersi.Text = ConfigManager.Instance.Version;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validasi Input Kosong menggunakan Library ValidationHelper
            if (ValidationHelper.IsEmpty(textBox2.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }




            // Jika input ada, cek ke "database"
            string inputUsername = textBox1.Text;
            string inputPassword = textBox2.Text;

            Sopir akunDitemukan = DataHelper.CariBerdasarkanId(DatabaseSimulasi.SopirDB, inputUsername);





            //Validasi Password dan penetapan akunValid
            Sopir akunValid = null;
            if (akunDitemukan != null && akunDitemukan.Password == inputPassword)
            {
                akunValid = akunDitemukan;
            }



            // Logika Perpindahan Form
            if (akunValid != null)
            {
                MessageBox.Show($"Selamat datang, {akunValid.Nama}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Membuka Form1 dengan data sopir yang valid
                Form1 formUtama = new Form1(akunValid);

                this.Hide();


                formUtama.FormClosed += (s, args) =>
                {
                    this.Show();
                    textBox2.Clear();
                };

                formUtama.Show();
            }
            else
            {
                MessageBox.Show("Username atau Password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
