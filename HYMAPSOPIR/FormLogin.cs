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

        private List<Sopir> databaseSopir = new List<Sopir>{
        new Sopir("Aditya", "aditya", "12345", Armada.Denpasar),
        new Sopir("Budi", "budi", "admin", Armada.Gianyar)
        };

        public FormLogin()
        {
            InitializeComponent();
        }

        private void LabelJudul_Click(object sender, EventArgs e)
        {
           
    
            // tombol "Lanjut" diklik
            button1.Click += button1_Click;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input Kosong Dulu
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Username tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return; // Berhenti di sini jika kosong
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            // 2. Jika input ada, baru cek ke "database"
            string inputUsername = textBox1.Text;
            string inputPassword = textBox2.Text;

            Sopir akunValid = databaseSopir.Find(s => s.Username == inputUsername && s.Password == inputPassword);

            if (akunValid != null)
            {
                MessageBox.Show($"Selamat datang, {akunValid.Nama}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Pastikan Form1 punya konstruktor yang menerima object Sopir
                Form1 formUtama = new Form1(akunValid);

                this.Hide();
                formUtama.FormClosed += (s, args) => this.Close();
                formUtama.Show();
            }
            else
            {
                MessageBox.Show("Username atau Password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
