using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using Common.Cache;




namespace Presentation
{
    public partial class FormUsuarios : Form
    {
        SqlConnection conexion = new SqlConnection("server = (LocalDb)\\MSSQLLocalDB; database = MyCompany; integrated security = true");
        public FormUsuarios()
        {
            InitializeComponent();
            llenar_tabla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void llenar_tabla()
        {
            string consulta = "select * from Users";
            //SqlConnection conexion = new SqlConnection(user.nombreConexion());
            //conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta,conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["UserID"].Visible = false;
            this.dataGridView1.Columns["Password"].Visible = false;
            this.dataGridView1.Columns["LoginName"].Visible = false;

        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            llenar_tabla();

            //permisos
            if (UserLoginCache.Position != Positions.Administrador)
            {
                btnAgregar.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            llenar_tabla();
            txtBoxNombre.Clear();
            txtBoxPuesto.Clear();
            txtBoxEmail.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void buttonModif_Click(object sender, EventArgs e)
        {
        }
        
        public void volver_page1()
        {
            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
        }
        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (UserLoginCache.Position == Positions.Administrador)
            {
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage3);
                //textBox18.Text = dataGridView1.SelectedCells[1].Value.ToString();
                lblUserId.Text = dataGridView1.SelectedCells[0].Value.ToString();
                txtBoxModificarNombre.Text = dataGridView1.SelectedCells[3].Value.ToString();
                txtBoxModificarLastName.Text = dataGridView1.SelectedCells[4].Value.ToString();
                txtBoxModificarEmail.Text = dataGridView1.SelectedCells[6].Value.ToString();
                txtBoxModificarArea.Text = dataGridView1.SelectedCells[5].Value.ToString();
                txtBoxModificarUserName.Text = dataGridView1.SelectedCells[1].Value.ToString();
                txtBoxModificarContraseña.Text = dataGridView1.SelectedCells[2].Value.ToString();

            }
        }
        public void limpiar_campos()
        {
            txtBoxAgregarName.Clear();
            txtBoxAgregarApellido.Clear();
            txtBoxAgregarEmail.Clear();
            txtBoxAgregarArea.Clear();
            txtBoxAgregarUserName.Clear();
            txtBoxAgregarPassword.Clear();
        }
        private void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            bool coincide = new bool();
            
            string verificar = "select LoginName from Users";
            SqlCommand command = new SqlCommand(verificar, conexion);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                string valorColumna = reader.GetString(0);
                if(txtBoxAgregarUserName.Text==valorColumna)
                {
                    MessageBox.Show("Nombre de Usuario existente, ingresa otro");
                    coincide = true;
                    break;
                }
            }
            reader.Close();
            if(coincide==false)
            {
                string consulta = "insert into Users values ('" + txtBoxAgregarUserName.Text + "','" + txtBoxAgregarPassword.Text + "','" + txtBoxAgregarName.Text + "','" + txtBoxAgregarApellido.Text + "','" + txtBoxAgregarArea.Text + "','" + txtBoxAgregarEmail.Text + "')";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Nuevo Usuario agregado");
                llenar_tabla();
                limpiar_campos();
                tabControl1.TabPages.Add(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);               
            }
            conexion.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar la operación?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    System.Windows.Forms.DialogResult.Yes)
            {
                volver_page1();
            }
        }
        private void busqueda()
        {
            string consulta = "select * from Users where FirstName like '" + txtBoxNombre.Text + "' + '%' AND Position like '" + txtBoxPuesto.Text + "' + '%' AND Email like '" + txtBoxEmail.Text + "' + '%' ";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataReader lector;
            lector = comando.ExecuteReader();
        }

        private void buttonModif_Click_1(object sender, EventArgs e)
        {
            conexion.Open();
            bool coincide = new bool();
            string verificar = "select LoginName from Users";
            SqlCommand command = new SqlCommand(verificar, conexion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string valorColumna = reader.GetString(0);
                if (txtBoxModificarUserName.Text == valorColumna)
                {
                    MessageBox.Show("Nombre de Usuario existente, ingresa otro");
                    coincide = true;
                    break;
                }
            }
            reader.Close();

            if (coincide == false)
            {
                string consulta = "update Users set FirstName = '" + txtBoxModificarNombre.Text + "', LastName = '" + txtBoxModificarLastName.Text + "', Email = '" + txtBoxModificarEmail.Text + "', Position = '" + txtBoxModificarArea.Text + "', LoginName = '" + txtBoxModificarUserName.Text + "', Password = '" + txtBoxModificarContraseña.Text + "' where UserID = " + lblUserId.Text;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                if (MessageBox.Show("¿Deseas modificar a " + txtBoxModificarNombre.Text + "?",
                        "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                        System.Windows.Forms.DialogResult.Yes)
                {
                    comando.ExecuteNonQuery();
                    int cant;
                    cant = comando.ExecuteNonQuery();
                    if (cant > 0) { MessageBox.Show("Usuario modificado"); }
                    llenar_tabla();
                    volver_page1();
                    txtBoxNombre.Clear();
                    txtBoxPuesto.Clear();
                    txtBoxEmail.Clear();
                }
            }
            conexion.Close();
        }

        private void txtBoxNombre_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (txtBoxNombre.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void txtBoxPuesto_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (txtBoxPuesto.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void txtBoxEmail_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (txtBoxEmail.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "delete from Users where LoginName = '" + txtBoxModificarUserName.Text + "'";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            if (MessageBox.Show("¿Está seguro de que desea eliminar el Usuario " + txtBoxModificarNombre.Text + "?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    System.Windows.Forms.DialogResult.Yes)
            {
                comando.ExecuteNonQuery();
                MessageBox.Show("Usuario eliminado");
                llenar_tabla();
                volver_page1();
            }
            conexion.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            volver_page1();
        }
    }
}
