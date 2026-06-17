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

            // Validasi Panjang Password minimal 5 karakter
            if (!ValidationHelper.IsPasswordLengthValid(textBox2.Text))
            {
                MessageBox.Show("Password tidak boleh kurang dari 5 huruf!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Dashboard formUtama = new Dashboard(akunValid);

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validasi Input Kosong
            if (ValidationHelper.IsEmpty(textBox2.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            // Validasi Panjang Password minimal 5 karakter
            if (!ValidationHelper.IsPasswordLengthValid(textBox2.Text))
            {
                MessageBox.Show("Password tidak boleh kurang dari 5 huruf!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }




            // Jika input ada, cek ke "database"
            string inputUsername = textBox1.Text;
            string inputPassword = textBox2.Text;

            List<Sopir> dataSopirDb = Library.Database.SopirDAO.GetAllSopir();
            Sopir akunDitemukan = DataHelper.CariBerdasarkanId(dataSopirDb, inputUsername);




           
            Sopir akunValid = null;

            //Securitycode
            string hashedInputPassword = SecurityHelper.HashSHA256(inputPassword);

            if (akunDitemukan != null && akunDitemukan.Password == hashedInputPassword)
            {
                akunValid = akunDitemukan;
            }



            // Logika Perpindahan Form
            if (akunValid != null)
            {
                int idLogTergenerate = Library.Database.LoginHistoryDAO.Instance.CatatLogin(akunValid.Username);

                MessageBox.Show($"Selamat datang, {akunValid.Nama}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Membuka Form dengan data sopir yang valid
                Dashboard formUtama = new Dashboard(akunValid);

                this.Hide();


                formUtama.FormClosed += (s, args) =>
                {
                    this.Show();
                    textBox2.Clear();
                    if (idLogTergenerate > 0)
                    {
                        try
                        {
                            Library.Database.LoginHistoryDAO.Instance.CatatLogout(idLogTergenerate);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Gagal Logout: " + ex.Message);
                        }
                    }
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
