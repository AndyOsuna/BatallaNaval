using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatallaNaval.Persistence;

namespace BatallaNaval
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        

        private void Login_Load(object sender, EventArgs e)
        {


        }

        private void Registrarse_click(object sender, EventArgs e)
        {

        }

        

        private void IniciarSession_click(object sender, EventArgs e)
        {
            
            String User = UserTextBox.Text;
            String Password = ContraseñaTextBox.Text;
            bool response = pLogin.AuthLogin(User, Password);
        }
    }
}
