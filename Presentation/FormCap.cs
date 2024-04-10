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

namespace WindowsFormsApp3
{
    public partial class FormCap : Form
    {

        public FormCap()
        {
            InitializeComponent();
        }

        SqlConnection conexion = new SqlConnection("server = (LocalDb)\\MSSQLLocalDB; database = MyCompany; integrated security = true");

        public void llenar_tablac()
        {
            string consulta = "select eventos.TipoEvento, eventos.Fecha, eventos.Completado, eventos.Nombre, eventos.Modelo, eventos.Marca from eventos where TipoEvento like 'Capacitacion' ";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridViewS.DataSource = dt;
        }

        public void limpiarcampos()
        {
            textBoxNombre.Clear();
            textBoxModelo.Clear();
            textBoxMarca.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string consulta = "select eventos.TipoEvento, eventos.Fecha, eventos.Completado, eventos.Nombre, eventos.Modelo, eventos.Marca from eventos where TipoEvento like 'Capacitacion' ";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridViewS.DataSource = dt;
            tabControlCap.TabPages.Remove(tabPageProgCap);
            
            if (UserLoginCache.Position == Positions.Interno)
            {
                tabControlCap.TabPages.Remove(tabPageSolis);

            }
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "insert into eventos values('Capacitacion','', '', 'False', 'Capacitacion_" + textBoxNombre.Text + "_" + textBoxModelo.Text + "_" + textBoxMarca.Text + "', '" + textBoxNombre.Text + "', '" + textBoxModelo.Text + "', '" + textBoxMarca.Text + "')";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            if (MessageBox.Show("¿Confirmas tu solicitud de capacitación para el dispositivo " + textBoxNombre.Text + "?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    System.Windows.Forms.DialogResult.Yes)
            {
                comando.ExecuteNonQuery();
                MessageBox.Show("Solicitud enviada");
                llenar_tablac();
                limpiarcampos();
            }
            conexion.Close();
        }

        private void busqueda()
        {
            string consulta = "select eventos.TipoEvento, eventos.Fecha, eventos.Completado, eventos.Nombre, eventos.Modelo, eventos.Marca from eventos where Nombre like '" + textBoxNombreB.Text + "' + '%' AND Modelo like '" + textBoxModeloB.Text + "' + '%' AND Marca like '" + textBoxMarcaB.Text + "' + '%' ";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridViewS.DataSource = dt;
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataReader lector;
            lector = comando.ExecuteReader();
        }

        private void textBoxNombreB_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (textBoxNombreB.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void textBoxModeloB_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (textBoxModeloB.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void textBoxMarcaB_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (textBoxMarcaB.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            llenar_tablac();
            textBoxNombreB.Clear();
            textBoxModeloB.Clear();
            textBoxMarcaB.Clear();
        }

        private void dataGridViewS_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tabControlCap.TabPages.Remove(tabPageSolis);
            tabControlCap.TabPages.Remove(tabPageSoli);
            tabControlCap.TabPages.Add(tabPageProgCap);
            if (dataGridViewS.SelectedCells[1].Value.ToString() != "")
            {
                dateTimePicker1.Text = dataGridViewS.SelectedCells[1].Value.ToString();
            }
            else
                dateTimePicker1.Text = "";
            labelNombre.Text = dataGridViewS.SelectedCells[3].Value.ToString();
            labelModelo.Text = dataGridViewS.SelectedCells[4].Value.ToString();
            labelMarca.Text = dataGridViewS.SelectedCells[5].Value.ToString();
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "update eventos set Fecha = '"+ dateTimePicker1.Text +"' where Nombre like '" + labelNombre.Text + "' AND Modelo like '"+ labelModelo.Text +"' AND Marca like '"+ labelMarca.Text +"' ";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            if (MessageBox.Show("¿Deseas programar la capacitación de " + labelNombre.Text + "?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    System.Windows.Forms.DialogResult.Yes)
            {
                comando.ExecuteNonQuery();
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant > 0) { MessageBox.Show("Capacitación programada correctamente"); }
                llenar_tablac();
                tabControlCap.TabPages.Remove(tabPageProgCap);
                tabControlCap.TabPages.Remove(tabPageSoli);
                tabControlCap.TabPages.Add(tabPageSolis);
            }
            conexion.Close();
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            tabControlCap.TabPages.Remove(tabPageProgCap);
            tabControlCap.TabPages.Remove(tabPageSoli);
            tabControlCap.TabPages.Add(tabPageSolis);
        }

        private void buttonCompletado_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "update eventos set Completado = 'True' where Nombre like '" + labelNombre.Text + "' AND Modelo like '" + labelModelo.Text + "' AND Marca like '" + labelMarca.Text + "' ";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            if (MessageBox.Show("¿La capacitación de " + labelNombre.Text + " fue completada correctamente?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    System.Windows.Forms.DialogResult.Yes)
            {
                comando.ExecuteNonQuery();
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant > 0) { MessageBox.Show("Capacitación completada correctamente"); }
                llenar_tablac();
                tabControlCap.TabPages.Remove(tabPageProgCap);
                tabControlCap.TabPages.Remove(tabPageSoli);
                tabControlCap.TabPages.Add(tabPageSolis);
            }
            conexion.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
