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

namespace Calendar
{
    public partial class FormEvent : Form
    {
        SqlConnection conexion = new SqlConnection("server = (LocalDb)\\MSSQLLocalDB; database = MyCompany; integrated security = true");
        public FormEvent()
        {
            InitializeComponent();
        }

        private void EventForm_Load(object sender, EventArgs e)
        {
            lblFecha.Text = UserControlDays.static_day + "/" + FormCalendario.static_month + "/" + FormCalendario.static_year;
            llenarTabla();
        }

        private void llenarTabla()
        {
            conexion.Open();
            string consulta = "select TipoEvento,Fecha,Completado,Nombre,Modelo,Marca FROM eventos where Fecha='" + FormCalendario.static_year + "/" + FormCalendario.static_month + "/" + UserControlDays.static_day + "'";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            conexion.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
