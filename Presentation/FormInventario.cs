using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Common.Cache;

namespace Presentation
{
    public partial class FormInventario : Form
    {
        public FormInventario()
        {
            InitializeComponent();
        }

        SqlConnection conexion = new SqlConnection("server = (LocalDb)\\MSSQLLocalDB; database = MyCompany; integrated security = true");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numero = new int();
            if (int.TryParse(txtBoxAddNoSerie.Text, out numero) && txtBoxAddNoSerie.Text.Length==8)
            {
                conexion.Open();
                bool coincide = new bool();

                string verificar = "select NumSerie from disp";
                SqlCommand command = new SqlCommand(verificar, conexion);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int valorColumna = reader.GetInt32(0);
                    if (txtBoxAddNoSerie.Text == valorColumna.ToString())
                    {
                        MessageBox.Show("Error: Numero de Serie existente, verificalo");
                        coincide = true;
                        break;
                    }
                }
                reader.Close();
                if (coincide == false)
                {
                    string consulta1 = "insert into disp values ('" + txtBoxAddNoSerie.Text + "','" + txtBoxAddName.Text + "','" + listBox1.Items[listBox1.SelectedIndex].ToString() + "','" + txtBoxAddModelo.Text + "','" + txtBoxAddMarca.Text + "','" + txtBoxAddProveedor.Text + "','" + txtBoxAddVerSof.Text + "',0,'YYYY/MM/DD','" + DateTime.Now.AddDays(60).ToString("yyyy/MM/dd") + "')";
                    string consulta2 = "insert into eventos values ('Preventivo','" + DateTime.Now.AddDays(60).ToString("yyyy/MM/dd")+ "','"+txtBoxAddNoSerie.Text + "',0,"+ "'Preventivo_"+txtBoxAddNoSerie.Text +"_"+ txtBoxAddNoSerie.Text + DateTime.Now.AddDays(60).ToString("yyyy/MM/dd")+ "','" + txtBoxAddName.Text + "','" + txtBoxAddModelo.Text + "','" + txtBoxAddMarca.Text + "')";
                    SqlCommand comando1 = new SqlCommand(consulta1, conexion);
                    SqlCommand comando2 = new SqlCommand(consulta2, conexion);
                    comando1.ExecuteNonQuery();
                    comando2.ExecuteNonQuery();
                    MessageBox.Show("Nuevo Equipo agregado");
                    llenar_tabla();
                    limpiar_campos();
                    tabControl1.TabPages.Add(tabPage1);
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Remove(tabPage3);
                }
                conexion.Close();
            }
            else
            {
                MessageBox.Show("El No. de Serie no es un numero de 8 digitos");
            }
        }

        public void llenar_tabla()
        {
            string consulta = "select * from disp";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["MttoCorrectivo"].Visible = false;
            this.dataGridView1.Columns["ProxMttoCorr"].Visible = false;
            this.dataGridView1.Columns["ProxMttoPrev"].Visible = false;
        }

        public void limpiar_campos()
        {
            txtBoxAddNoSerie.Clear();
            txtBoxAddName.Clear();
            txtBoxAddModelo.Clear();
            txtBoxAddMarca.Clear();
            txtBoxAddProveedor.Clear();
            txtBoxAddVerSof.Clear();
        }

        public void volver_page1()
        {
            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DATAGRID
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            llenar_tabla();
            //Manage Permissions
            if (UserLoginCache.Position == Positions.Interno)
            {
                button8.Enabled = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            llenar_tabla();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
        }

        private void busqueda()
        {
            string consulta = "select * from disp where NumSerie like '" + textBox8.Text + "' + '%' AND Nombre like '" + textBox9.Text + "' + '%' AND AreaHosp like '" + textBox10.Text + "' + '%' ";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataReader lector;
            lector = comando.ExecuteReader();
            this.dataGridView1.Columns["MttoCorrectivo"].Visible = false;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (textBox8.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (textBox9.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (textBox10.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int numero = new int();
            if (int.TryParse(textBox19.Text, out numero) && textBox19.Text.Length == 8)
            {
                conexion.Open();
                string consulta = "update disp set NumSerie = " + textBox19.Text + ", Nombre = '" + textBox18.Text + "', AreaHosp = '" + (string)listBox2.Items[listBox2.SelectedIndex] + "', Modelo = '" + textBox16.Text + "', Marca = '" + textBox15.Text + "', Proveedor = '" + textBox14.Text + "', VerSoft = '" + textBox13.Text + "' where NumSerie = " + textBox19.Text + "";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                if (MessageBox.Show("¿Deseas modificar a " + textBox19.Text + "?",
                        "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                        System.Windows.Forms.DialogResult.Yes)
                {
                    comando.ExecuteNonQuery();
                    int cant;
                    cant = comando.ExecuteNonQuery();
                    if (cant > 0) { MessageBox.Show("Equipo modificado"); }
                    llenar_tabla();
                    volver_page1();

                }
                conexion.Close();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
            }
            else
            {
                MessageBox.Show("El No. de Serie no es un numero de 8 digitos");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "delete from disp where NumSerie = " + textBox19.Text + "";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            if (MessageBox.Show("¿Está seguro de que desea eliminar el Equipo " + textBox19.Text + "?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    System.Windows.Forms.DialogResult.Yes)
            {
                comando.ExecuteNonQuery();
                MessageBox.Show("Equipo eliminado");
                llenar_tabla();
                volver_page1();
            }          
            conexion.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            volver_page1();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (UserLoginCache.Position != Positions.Interno)
            {
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage3);
                textBox19.Text = dataGridView1.SelectedCells[0].Value.ToString();
                textBox18.Text = dataGridView1.SelectedCells[1].Value.ToString();
                lblDetallesArea.Text = dataGridView1.SelectedCells[2].Value.ToString();
                textBox16.Text = dataGridView1.SelectedCells[3].Value.ToString();
                textBox15.Text = dataGridView1.SelectedCells[4].Value.ToString();
                textBox14.Text = dataGridView1.SelectedCells[5].Value.ToString();
                textBox13.Text = dataGridView1.SelectedCells[6].Value.ToString();
                
            }
        }

    }
}
