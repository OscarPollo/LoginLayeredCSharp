using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Domain;

namespace Presentation
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg,int wparam,int lparam);


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textuser_Enter(object sender, EventArgs e)
        {
            if (textuser.Text == "USUARIO")
            {
                textuser.Text = "";
                textuser.ForeColor = Color.LightGray;
            }
        }

        private void textuser_Leave(object sender, EventArgs e)
        {
            if (textuser.Text=="")
            {
                textuser.Text = "USUARIO";
                textuser.ForeColor = Color.DimGray;
            }
        }

        private void textpass_Enter(object sender, EventArgs e)
        {
            if (textpass.Text == "CONTRASEÑA")
            {
                textpass.Text = "";
                textpass.ForeColor = Color.LightGray;
                textpass.UseSystemPasswordChar = true;

            }
        }

        private void textpass_Leave(object sender, EventArgs e)
        {
            if (textpass.Text == "")
            {
                textpass.Text = "CONTRASEÑA";
                textpass.ForeColor = Color.DimGray;
                textpass.UseSystemPasswordChar = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textuser.Text != "USUARIO") 
            {
                if (textpass.Text != "CONTRASEÑA") 
                {
                    UserModel user = new UserModel();
                    var validLogin = user.LoginUser(textuser.Text, textpass.Text);
                    if(validLogin==true)
                    {
                        this.Hide();
                        FormBienvenida bienvenida = new FormBienvenida();
                        bienvenida.ShowDialog();
                        FormPrincipal mainMenu = new FormPrincipal();
                        mainMenu.Show();
                        mainMenu.FormClosed += Logout;
                    }
                    else
                    {
                        msgError("Nombre de usuario o Contraseña incorrectas. \n Intenta de nuevo");
                        textpass.Text="CONTRASEÑA";
                        textpass.UseSystemPasswordChar = false;
                        textpass.ForeColor = Color.DimGray;
                        textuser.Focus();
                    }
                }
                else msgError("Ingresa tu contraseña");
            }
            else msgError("Ingresa tu nombre de usuario");
        }
        private void msgError(string msg)
        {
            lblErrorMessagge.Text = "     " + msg;
            lblErrorMessagge.Visible = true;
        }

        private void Logout(object sender, FormClosedEventArgs e)
        {
            textpass.ForeColor = Color.DimGray;
            textpass.Text="CONTRASEÑA";
            textpass.UseSystemPasswordChar = false;
            textuser.ForeColor = Color.DimGray;
            textuser.Text = "USUARIO";
            lblErrorMessagge.Visible = false;
            this.Show();
            //textuser.Focus();
        }
        private void textpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var recoverPassword = new FormRecuperarContraseña();
            recoverPassword.ShowDialog();
        }
    }
}
