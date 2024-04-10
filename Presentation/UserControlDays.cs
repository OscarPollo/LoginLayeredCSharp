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
using Presentation;

namespace Calendar
{
    public partial class UserControlDays : UserControl
    {
        SqlConnection conexion = new SqlConnection("server = (LocalDb)\\MSSQLLocalDB; database = MyCompany; integrated security = true");
        public static string static_day;

        public UserControlDays()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {
        }
        public void days(int numday)
        {
            lbdays.Text = numday + "";
            if (numday < 10)
            {
                lbdays.Text = "0" + numday;
            }
        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            static_day = lbdays.Text;
            FormEvent eventform = new FormEvent();
            eventform.ShowDialog();
        }

        public void displayEvent()
        {
            conexion.Open();
            String despEv = "SELECT TipoEvento FROM eventos where Fecha = '"+ FormCalendario.static_year + "/" + FormCalendario.static_month + "/" + lbdays.Text+"'";
            SqlCommand despEvCommand = new SqlCommand(despEv, conexion);
            SqlDataReader reader = despEvCommand.ExecuteReader();
            if (reader.Read())
            {
                lbevent.Text = reader["TipoEvento"].ToString();
            }
            reader.Close();
            conexion.Close();
            if (lbevent.Text == "Correctivo")
            {
                lbevent.BackColor = Color.IndianRed;
            }
            if (lbevent.Text == "Preventivo")
            {
                lbevent.BackColor = Color.LightSkyBlue;
            }
            if (lbevent.Text == "Capacitacion")
            {
                lbevent.BackColor = Color.Yellow;
            }
        }

            private void timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}
