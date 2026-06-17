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

            if (AdminDAO.LoginAdmin(username, password))
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
