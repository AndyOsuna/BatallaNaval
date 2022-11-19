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
using BatallaNaval.Views;

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
            bool response = pUser.authLogin(User, Password);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void UserTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            
          


        }

        private Form activoForm = null;
        public void abrirHijoForm(Form hijoForm)
        {
            if (activoForm != null)
                activoForm.Close();
             activoForm = hijoForm;
            hijoForm.TopLevel = false;
            hijoForm.FormBorderStyle = FormBorderStyle.None;
            hijoForm.Dock = DockStyle.Fill;
            panelHijoForm.Controls.Add(hijoForm);
            panelHijoForm.Tag = hijoForm;
            hijoForm.BringToFront();
            hijoForm.Show();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            abrirHijoForm(new Register());
        }
    }
}
