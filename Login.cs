using InventoryControl.Auth;
using InventoryControl.Interfaces;
using System;
using System.Windows.Forms;

namespace InventoryControl
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginLogic();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginLogic();
                loginButton.PerformClick();
            }
        }

        private void LoginLogic()
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (AuthenticationManager.AuthenticateUser(username, password))
            {
                MessageBox.Show("Inicio de sesión exitoso");

                this.Hide();
                Index index = new Index();
                index.Show();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. Por favor, inténtalo de nuevo.");
            }
        }
    }
}
