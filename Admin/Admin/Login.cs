using Admin.Library.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Admin
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Passw.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Implementasi Securitycode SHA256 di form login
            string hashedInputPassword = Admin.Library.Security.SecurityHelper.HashSHA256(password);

            if (AdminDAO.LoginAdmin(username, hashedInputPassword))
            {
                MessageBox.Show("Login Berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Dashboard formDashboard = new Dashboard();
                this.Hide();
                formDashboard.FormClosed += (s, args) => this.Show(); 
                formDashboard.Show();
            }
            else
            {
                MessageBox.Show("Username atau Password salah/Anda bukan Admin!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
