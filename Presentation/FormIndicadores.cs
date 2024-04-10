using PdfSharp.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentation
{
    public partial class FormIndicadores : Form
    {
        public FormIndicadores()
        {
            InitializeComponent();
        }

        SqlConnection conexion = new SqlConnection("server = (LocalDb)\\MSSQLLocalDB; database = MyCompany; integrated security = true");

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormIndicadores_Load(object sender, EventArgs e)
        {

            //TASA DE CORRECTIVOS
            conexion.Open();
            string total = "select * from eventos where TipoEvento like 'Correctivo'";
            SqlDataAdapter adaptador = new SqlDataAdapter(total, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);

            float correctivos = dt.Rows.Count;

            string comp = "select * from eventos where TipoEvento like 'Correctivo'+ '%' AND Completado like '1' + '%'";
            SqlDataAdapter adaptador1 = new SqlDataAdapter(comp, conexion);
            DataTable dt2 = new DataTable();
            adaptador1.Fill(dt2);

            float mtto_comp = dt2.Rows.Count;

            float porcentaje = (mtto_comp / correctivos) * 100;

            lblCorrComp.Text = porcentaje.ToString("0.00");

            //Disp registrados
            string tabla = "SELECT * FROM disp";
            SqlDataAdapter adaptadordispreg = new SqlDataAdapter(tabla, conexion);
            DataTable dtdispreg = new DataTable();
            adaptadordispreg.Fill(dtdispreg);

            int disps = dtdispreg.Rows.Count;
            lblNoDispReg.Text = disps.ToString();

            conexion.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("No hay area seleccionada");
            }
            else
            {
                conexion.Open();
                string tabla = "CREATE TABLE #TablaTemp (NoEvento int ,TipoEvento varchar(10),Fecha varchar(50),NumSerie int,Completado bit, Reporte varchar(100),Nombre varchar(50),Modelo varchar(50),Marca varchar(50),AreaHosp varchar(50))";
                string tablaf = "INSERT INTO #TablaTemp (NoEvento,TipoEvento, Fecha, NumSerie, Completado, Reporte, Nombre, Modelo, Marca, AreaHosp) SELECT e.NoEvento,e.TipoEvento, e.Fecha, e.NumSerie, e.Completado, e.Reporte, e.Nombre, e.Modelo, e.Marca,d.AreaHosp FROM eventos e INNER JOIN disp d ON e.NumSerie = d.NumSerie";
                string total = "SELECT * FROM #TablaTemp WHERE TipoEvento like 'Correctivo' + '%' AND AreaHosp like'" + (string)listBox1.Items[listBox1.SelectedIndex] + "' + '%'";
                SqlCommand command1 = new SqlCommand(tabla, conexion);
                SqlCommand command2 = new SqlCommand(tablaf, conexion);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                SqlDataAdapter adaptador = new SqlDataAdapter(total, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                float correctivos = dt.Rows.Count;

                string comp = "select * from #TablaTemp where TipoEvento like 'Correctivo'+ '%' AND Completado like '1' + '%' AND AreaHosp like'" + (string)listBox1.Items[listBox1.SelectedIndex] + "' + '%'";
                SqlDataAdapter adaptador1 = new SqlDataAdapter(comp, conexion);
                DataTable dt2 = new DataTable();
                adaptador1.Fill(dt2);

                float mtto_comp = dt2.Rows.Count;

                float porcentaje = (mtto_comp / correctivos) * 100;

                lblCorrArea.Text = porcentaje.ToString("0.00");
            }
            conexion.Close();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            //conexion.Open();

            //if (listBoxAño.SelectedIndex == -1 || listBoxMes.SelectedIndex == -1)
            //{
            //    MessageBox.Show("No hay año o mes seleccionado");
            //}
            //else
            //{
            //    string mes = "SELECT * FROM eventos WHERE Fecha like '" + listBoxAño.Items[listBoxAño.SelectedIndex].ToString() + "/" + listBoxMes.Items[listBoxMes.SelectedIndex].ToString() + "/' + '%'";
            //    SqlDataAdapter adaptador = new SqlDataAdapter(mes, conexion);
            //    DataTable dt = new DataTable();
            //    adaptador.Fill(dt);

            //    float preventivos = dt.Rows.Count;

            //    string mes2 = "SELECT * FROM eventos WHERE Fecha like '" + listBoxAño.Items[listBoxAño.SelectedIndex].ToString() + "/" + listBoxMes.Items[listBoxMes.SelectedIndex].ToString() + "/' + '%' AND Completado like '1' + '%'";
            //    SqlDataAdapter adaptador2 = new SqlDataAdapter(mes2, conexion);
            //    DataTable dt2 = new DataTable();
            //    adaptador2.Fill(dt2);

            //    float complete = dt2.Rows.Count;

            //    float porcentaje = (complete / preventivos) * 100;

            //    if (complete == 0)
            //    {
            //        labelMes.Text = "00.00";
            //    }
            //    else
            //    {
            //        labelMes.Text = porcentaje.ToString("0.00");
            //    }
            //}

            //conexion.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (listBoxAño.SelectedIndex == -1 || listBoxMes.SelectedIndex==-1)
            {
                MessageBox.Show("No hay año o mes seleccionado");
            }
            else
            {
                string mes = "SELECT * FROM eventos WHERE Fecha like '" + listBoxAño.Items[listBoxAño.SelectedIndex].ToString() + "/" + listBoxMes.Items[listBoxMes.SelectedIndex].ToString() + "/' + '%'";
                SqlDataAdapter adaptador = new SqlDataAdapter(mes, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                float preventivos = dt.Rows.Count;

                string mes2 = "SELECT * FROM eventos WHERE Fecha like '" + listBoxAño.Items[listBoxAño.SelectedIndex].ToString() + "/" + listBoxMes.Items[listBoxMes.SelectedIndex].ToString() + "/' + '%' AND Completado like '1' + '%'";
                SqlDataAdapter adaptador2 = new SqlDataAdapter(mes2, conexion);
                DataTable dt2 = new DataTable();
                adaptador2.Fill(dt2);

                float complete = dt2.Rows.Count;

                float porcentaje = (complete / preventivos) * 100;

                if (complete == 0)
                {
                    lblPrevMes.Text = "00.00";
                }
                else
                {
                    lblPrevMes.Text = porcentaje.ToString("0.00");
                }
            }
            conexion.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conexion.Open();

            if (listBoxAño.SelectedIndex == -1)
            {
                MessageBox.Show("No hay año seleccionado");
            }
            else
            {
                string year = "SELECT * FROM eventos WHERE Fecha like '" + listBoxAño.Items[listBoxAño.SelectedIndex].ToString() + "/' + '%' AND TipoEvento like 'Preventivo' + '%'";
                SqlDataAdapter adaptador = new SqlDataAdapter(year, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                float preventivos = dt.Rows.Count;

                string mes2 = "SELECT * FROM eventos WHERE Fecha like '" + listBoxAño.Items[listBoxAño.SelectedIndex].ToString() + "/' + '%' AND Completado like '1' + '%' AND TipoEvento like 'Preventivo' + '%'";
                SqlDataAdapter adaptador2 = new SqlDataAdapter(mes2, conexion);
                DataTable dt2 = new DataTable();
                adaptador2.Fill(dt2);

                float complete = dt2.Rows.Count;

                float porcentaje = (complete / preventivos) * 100;

                if (complete == 0)
                {
                    lblPrevAño.Text = "00.00";
                }
                else
                {
                    lblPrevAño.Text = porcentaje.ToString("0.00");
                }
            }
            conexion.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conexion.Open();


            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("No hay año seleccionado");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("No hay dispositivo");
            }
            else
            {
                string disp = "select * from eventos where TipoEvento like 'Correctivo' + '%' AND Nombre like '" + textBox1.Text + "' + '%' AND Fecha like '" + listBox1.Items[listBox1.SelectedIndex].ToString() + "/' + '%'";
                SqlDataAdapter adaptador = new SqlDataAdapter(disp, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                int correctivos = dt.Rows.Count;

                lblCorrPerDisp.Text = correctivos.ToString();


            }

            conexion.Close();
        }
    }
}
