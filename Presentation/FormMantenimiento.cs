using Common.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;





namespace Presentation
{
    public partial class FormMantenimiento : Form
    {
        SqlConnection conexion = new SqlConnection("server = (LocalDb)\\MSSQLLocalDB; database = MyCompany; integrated security = true");
        public FormMantenimiento()
        {
            InitializeComponent();
            llenar_tabla();
        }

        private void FormMantenimiento_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            llenar_tabla();

            //permisos
            if (UserLoginCache.Position == Positions.Interno)
            {
                btnOrdServ.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void llenar_tabla()
        {
            string consulta = "select disp.ProxMttoPrev,disp.MttoCorrectivo,disp.ProxMttoCorr,disp.NumSerie,disp.Nombre,disp.AreaHosp,disp.Modelo,disp.Marca from disp";
            //SqlConnection conexion = new SqlConnection(user.nombreConexion());
            //conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void volver_page1()
        {
            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            llenar_tabla();
        }
        private void btnOrdServ_Click(object sender, EventArgs e)
        {
            //FormOrdenServ frm = new FormOrdenServ();
            //frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            llenar_tabla();
            txtBoxNoSerie.Clear();
            txtBoxNombre.Clear();
            txtBoxAreaHosp.Clear();
        }
        private void busqueda()
        {
            string consulta = "select disp.ProxMttoPrev,disp.MttoCorrectivo,disp.ProxMttoCorr,disp.NumSerie,disp.Nombre,disp.AreaHosp,disp.Modelo,disp.Marca from disp where NumSerie like '" + txtBoxNoSerie.Text + "' + '%' AND Nombre like '" + txtBoxNombre.Text + "' + '%' AND AreaHosp like '" + txtBoxAreaHosp.Text + "' + '%' ";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataReader lector;
            lector = comando.ExecuteReader();
        }

        private void txtBoxNoSerie_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (txtBoxNoSerie.Text != "")
            {
                busqueda();
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

        private void txtBoxAreaHosp_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();

            if (txtBoxAreaHosp.Text != "")
            {
                busqueda();
            }

            conexion.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (UserLoginCache.Position != Positions.Interno)
            {
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage3);
                txtBoxFechaMttoPrev.Text = dataGridView1.SelectedCells[0].Value.ToString();
                lblModNumSerie.Text = dataGridView1.SelectedCells[3].Value.ToString();
                lblModName.Text = dataGridView1.SelectedCells[4].Value.ToString();
                lblModArea.Text = dataGridView1.SelectedCells[5].Value.ToString();
                lblModModelo.Text = dataGridView1.SelectedCells[6].Value.ToString();
                lblModMarca.Text = dataGridView1.SelectedCells[7].Value.ToString();
                if (dataGridView1.SelectedCells[1].Value.ToString() == "True")
                {
                    picBoxTrue.Visible = true;
                    picBoxFalse.Visible = false;
                    btnAsignarCorrec.Text = "Modificar Correctivo";
                    txtBoxFechaMttoCorrec.Text = dataGridView1.SelectedCells[2].Value.ToString();
                    lblTipoMttoOS.Text = "Correctivo";
                }
                else
                {
                    picBoxTrue.Visible = false;
                    picBoxFalse.Visible = true;
                    btnAsignarCorrec.Text = "Asignar Correctivo";
                    txtBoxFechaMttoCorrec.Text = "YYYY/MM/DD";
                    lblTipoMttoOS.Text = "Preventivo";
                }
            }

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            volver_page1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conexion.Open();
            bool coincide = new bool();

            string verificar = "select ProxMttoCorr from disp";
            SqlCommand command = new SqlCommand(verificar, conexion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string valorColumna = reader.GetString(0);
                if (txtBoxFechaMttoCorrec.Text == valorColumna)
                {
                    MessageBox.Show("Fecha ocupada, elegir alguna otra");
                    coincide = true;
                    break;
                }
            }
            reader.Close();
            if (coincide == false)
            {
                if (dataGridView1.SelectedCells[1].Value.ToString() == "False")
                {

                    if (MessageBox.Show("¿Asignar fecha para el mtto Correctivo?",
                            "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                            System.Windows.Forms.DialogResult.Yes)
                    {
                        string consulta1 = "insert into eventos values ('Correctivo','" + txtBoxFechaMttoCorrec.Text + "','" + lblModNumSerie.Text + "','False','Correctivo_" + lblModNumSerie.Text + "_" + txtBoxFechaMttoCorrec.Text + "','" + lblModName.Text + "','" + lblModModelo.Text + "','" + lblModMarca.Text + "')";
                        string consulta2 = "update disp set MttoCorrectivo = 'True', ProxMttoCorr='" + txtBoxFechaMttoCorrec.Text + "' where NumSerie=" + lblModNumSerie.Text;
                        //where UserID = " + lblUserId.Text;

                        SqlCommand comando1 = new SqlCommand(consulta1, conexion);
                        SqlCommand comando2 = new SqlCommand(consulta2, conexion);

                        comando1.ExecuteNonQuery();
                        comando2.ExecuteNonQuery();

                        MessageBox.Show("Fecha asignada");
                        picBoxTrue.Visible = true;
                        picBoxFalse.Visible = false;

                        DateTime fechaInicial;
                        if (DateTime.TryParseExact(txtBoxFechaMttoCorrec.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaInicial))
                        {
                            string consulta11 = "update eventos set Fecha='" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "',Reporte='Preventivo_" + lblModNumSerie.Text + "_" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "' where NumSerie like '" + lblModNumSerie.Text + "' + '%' AND TipoEvento like 'Preventivo' + '%' AND Completado like '0' + '%'";
                            string consulta22 = "update disp set ProxMttoPrev='" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "' where NumSerie=" + lblModNumSerie.Text;
                            txtBoxFechaMttoPrev.Text = fechaInicial.AddDays(60).ToString("yyyy/MM/dd");
                            SqlCommand comando11 = new SqlCommand(consulta11, conexion);
                            SqlCommand comando22 = new SqlCommand(consulta22, conexion);

                            comando11.ExecuteNonQuery();
                            comando22.ExecuteNonQuery();
                        }
                        volver_page1();
                        llenar_tabla();
                    }
                }
                if (dataGridView1.SelectedCells[1].Value.ToString() == "True")
                {
                    if (MessageBox.Show("¿Cambiar fecha para el mtto Correctivo?",
                            "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                            System.Windows.Forms.DialogResult.Yes)
                    {
                        string consulta1 = "update eventos set Fecha='" + txtBoxFechaMttoCorrec.Text + "',Reporte='Correctivo_" + lblModNumSerie.Text + "_" + txtBoxFechaMttoCorrec.Text + "' where NumSerie like '" + lblModNumSerie.Text + "' + '%' AND TipoEvento like 'Correctivo' + '%' ";
                        string consulta2 = "update disp set ProxMttoCorr='" + txtBoxFechaMttoCorrec.Text + "' where NumSerie=" + lblModNumSerie.Text;
                        //where UserID = " + lblUserId.Text;

                        SqlCommand comando1 = new SqlCommand(consulta1, conexion);
                        SqlCommand comando2 = new SqlCommand(consulta2, conexion);

                        comando1.ExecuteNonQuery();
                        comando2.ExecuteNonQuery();

                        MessageBox.Show("Fecha modificada");

                        DateTime fechaInicial;
                        if (DateTime.TryParseExact(txtBoxFechaMttoCorrec.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaInicial))
                        {
                            string consulta11 = "update eventos set Fecha='" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "',Reporte='Preventivo_" + lblModNumSerie.Text + "_" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "' where NumSerie like '" + lblModNumSerie.Text + "' + '%' AND TipoEvento like 'Preventivo' + '%' AND Completado like '0' + '%'";
                            string consulta22 = "update disp set ProxMttoPrev='" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "' where NumSerie=" + lblModNumSerie.Text;
                            txtBoxFechaMttoPrev.Text = fechaInicial.AddDays(60).ToString("yyyy/MM/dd");
                            SqlCommand comando11 = new SqlCommand(consulta11, conexion);
                            SqlCommand comando22 = new SqlCommand(consulta22, conexion);

                            comando11.ExecuteNonQuery();
                            comando22.ExecuteNonQuery();
                        }
                        volver_page1();
                        llenar_tabla();
                    }
                }
            }
            conexion.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (MessageBox.Show("¿Modificar fecha del mantenimiento preventivo?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                    System.Windows.Forms.DialogResult.Yes)
            {
                string consulta1 = "update eventos set Fecha='" + txtBoxFechaMttoPrev.Text + "',Reporte='Preventivo_" + lblModNumSerie.Text + "_" + txtBoxFechaMttoPrev.Text + "' where NumSerie like '" + lblModNumSerie.Text + "' + '%' AND TipoEvento like 'Preventivo' + '%' ";
                string consulta2 = "update disp set ProxMttoPrev='" + txtBoxFechaMttoPrev.Text + "' where NumSerie=" + lblModNumSerie.Text;
                //where UserID = " + lblUserId.Text;

                SqlCommand comando1 = new SqlCommand(consulta1, conexion);
                SqlCommand comando2 = new SqlCommand(consulta2, conexion);

                comando1.ExecuteNonQuery();
                comando2.ExecuteNonQuery();

                MessageBox.Show("Fecha asignada");
                llenar_tabla();
            }
           conexion.Close();
           volver_page1();
        }


        private void btnOrdServ_Click_1(object sender, EventArgs e)
        {
            conexion.Open();
            llenar_tabla();
            conexion.Close();

            if (dataGridView1.SelectedCells[1].Value.ToString() == "True")
            {
                lblTipoMttoOS.Text = "Correctivo";
            }
            else
            {
                lblTipoMttoOS.Text = "Preventivo";
            }
            //Orden de servicio
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Add(tabPage2);
            lblOSFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
            lblNameOS.Text=dataGridView1.SelectedCells[4].Value.ToString();
            lblAreaOS.Text=dataGridView1.SelectedCells[5].Value.ToString();
            lblModeloOS.Text = dataGridView1.SelectedCells[6].Value.ToString();
            lblMarcaOS.Text = dataGridView1.SelectedCells[7].Value.ToString();
            lblNoSerieOS.Text = dataGridView1.SelectedCells[3].Value.ToString();

            lblNoOrdServ.Text = IdEvento(lblTipoMttoOS.Text, lblNoSerieOS.Text).ToString();
            
        }

        private int IdEvento(string CorrectivoPreventivo,string NoSerie)
        {
            int numeroEvento = new int();
            string query = "SELECT NoEvento FROM eventos WHERE NumSerie like '" + NoSerie + "' + '%' AND Completado like '0' + '%' AND TipoEvento like '" + CorrectivoPreventivo+ "'+'%'";
            SqlCommand command = new SqlCommand(query, conexion);
            conexion.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                numeroEvento = reader.GetInt32(0); // Obtiene el valor de la columna NoEvento como entero
            }
            reader.Close();
            conexion.Close();
            return numeroEvento;
        }
        private void btnVolverMtto_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Add(tabPage3);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Finalizar Orden de Servicio?",
                                "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                                System.Windows.Forms.DialogResult.Yes)
            {
                conexion.Open();
                string consultan = "select Reporte from eventos where NoEvento=" + lblNoOrdServ.Text;
                SqlCommand comandon = new SqlCommand(consultan, conexion);
                string reporte = comandon.ExecuteScalar()?.ToString();
                conexion.Close();

                //crear direccion de guardado
                string folderPath = @"C:\Users\Jovan Lopez\Desktop\Ordenes de Servicio";
                string fileName = reporte.Replace("/", "_") + ".pdf";
                string filePath = folderPath + "\\" + fileName;
                // Capturar una imagen del formulario
                Bitmap formImage = new Bitmap(this.Width, this.Height);
                this.DrawToBitmap(formImage, new Rectangle(0, 0, this.Width, this.Height));

                // Crear un documento PDF
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Convertir la imagen a formato MemoryStream
                using (var memoryStream = new MemoryStream())
                {
                    formImage.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Position = 0;

                    // Crear un objeto XImage desde el MemoryStream
                    XImage image = XImage.FromStream(memoryStream);

                    // Dibujar la imagen en la página del PDF
                    gfx.DrawImage(image, 0, 0);

                    // Guardar el documento PDF en el archivo especificado
                    document.Save(filePath);
                }
                conexion.Open();
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
                llenar_tabla();
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);

                if (lblTipoMttoOS.Text == "Correctivo")
                {
                    string consulta1 = "update eventos set Completado=1 where NoEvento=" + lblNoOrdServ.Text;
                    string consulta2 = "update disp set MttoCorrectivo=0,ProxMttoCorr='YYYY/MM/DD' where NumSerie=" + lblModNumSerie.Text;
                    //where UserID = " + lblUserId.Text;

                    SqlCommand comando1 = new SqlCommand(consulta1, conexion);
                    SqlCommand comando2 = new SqlCommand(consulta2, conexion);

                    comando1.ExecuteNonQuery();
                    comando2.ExecuteNonQuery();

                    MessageBox.Show("Mtto correctivo completado");
                    llenar_tabla();
                }
                if (lblTipoMttoOS.Text == "Preventivo")
                {
                    DateTime fechaInicial;
                    if (DateTime.TryParseExact(txtBoxFechaMttoPrev.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaInicial))
                    {
                        string consulta11 = "update eventos set Completado=1 where NoEvento=" + lblNoOrdServ.Text;
                        string consulta22 = "insert into eventos values ('Preventivo', '" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "'," + lblModNumSerie.Text + ",0,'Preventivo_" + lblModNumSerie.Text + "_" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "','" + lblModName.Text + "','" + lblModModelo.Text + "','" + lblModMarca.Text + "')";
                        string consulta33 = "update disp set ProxMttoPrev='" + fechaInicial.AddDays(60).ToString("yyyy/MM/dd") + "' where NumSerie=" + lblModNumSerie.Text;
                        txtBoxFechaMttoPrev.Text = fechaInicial.AddDays(60).ToString("yyyy/MM/dd");
                        SqlCommand comando11 = new SqlCommand(consulta11, conexion);
                        SqlCommand comando22 = new SqlCommand(consulta22, conexion);
                        SqlCommand comando33 = new SqlCommand(consulta33, conexion);

                        comando11.ExecuteNonQuery();
                        comando22.ExecuteNonQuery();
                        comando33.ExecuteNonQuery();


                        MessageBox.Show("Mtto preventivo completado");
                        llenar_tabla();
                    }
                }
                conexion.Close();
                volver_page1();
            }            
        }
    }
}
