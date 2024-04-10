using Calendar;
using Common.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;

namespace Presentation
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        //Constructor
        {
            InitializeComponent();
            //Estas lineas eliminan los parpadeos del formulario o controles en la interfaz grafica (Pero no en un 100%)
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            AbrirFormulario<FormCalendario>();

        }

        #region Funcionalidades del formulario
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        //LOAAAAAAAAAAAAD
        private void FormPrinciapl_Load(object sender, EventArgs e)
        {
            LoadUserData();
            //Manage Permissions
            if (UserLoginCache.Position == Positions.Ingeniero)
            {

            }
            if (UserLoginCache.Position == Positions.Interno)
            {
                btnIndicadores.Enabled = false;
            }

        }
        private void LoadUserData()
        {
            lblName.Text = UserLoginCache.FirstName+", "+UserLoginCache.LastName;
            lblPosition.Text = UserLoginCache.Position;
            lblEmail.Text = UserLoginCache.Email;


        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro de salir del SGC sin cerrar sesion?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    Application.Exit();
        }
        int lx, ly;
        int sw, sh;

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            pictureBox4.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            pictureBox3.Visible = false;
            pictureBox4.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //ARRASTRAR FORMULARIO
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Cerrar Sesion?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormMantenimiento>();
            btnMantenimiento.BackColor = Color.FromArgb(210, 10, 10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormCap>();
            btnCapacitaciones.BackColor = Color.FromArgb(210, 10, 10);

        }

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormInventario>();
            btnInventario.BackColor = Color.FromArgb(210, 10, 10);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormUsuarios>();
            btnUsuarios.BackColor = Color.FromArgb(210, 10, 10);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormIndicadores>();
            btnIndicadores.BackColor = Color.FromArgb(210, 10, 10);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormCalendario>();
        }
        #endregion

        //ABRIR FORMULARIOS DENTRO DEL PANEL
        private void AbrirFormulario<MiForm>()where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelFormularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la coleccion el formualrio
            //si no existe el formulario
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;

                panelFormularios.Controls.Add(formulario);
                panelFormularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms );
            }
            //si existe
            else
            {
                formulario.BringToFront();
            }

        }

        //RESTAURAR FORMATO AL CERRAR FORMULARIOS
        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["FormInventario"] == null)
                btnInventario.BackColor = Color.FromArgb(255, 55, 55);
            if (Application.OpenForms["FormMantenimiento"] == null)
                btnMantenimiento.BackColor = Color.FromArgb(255, 55, 55);
            if (Application.OpenForms["FormCap"] == null)
                btnCapacitaciones.BackColor = Color.FromArgb(255, 55, 55);
            if (Application.OpenForms["FormIndicadores"] == null)
                btnIndicadores.BackColor = Color.FromArgb(255, 55, 55);
            if (Application.OpenForms["FormUsuarios"] == null)
                btnUsuarios.BackColor = Color.FromArgb(255, 55, 55);
        }
    }
}
