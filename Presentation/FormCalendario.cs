using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    public partial class FormCalendario : Form
    {
        int month, year;
        public static int prot = 0;
        public static string static_month, static_year;
        public FormCalendario()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplaDays();
        }
        private void mesAñoaCadena()
        {
            if (month < 10)
            {
                static_month = "0" + month.ToString();

            }
            else { static_month = month.ToString(); }
            static_year = year.ToString();
        }
        public void DisplaDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string MonthName = textInfo.ToTitleCase(monthname);
            LBDATE.Text = MonthName + " " + year;


            mesAñoaCadena();

            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                contenedorDias.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                ucdays.displayEvent();
                contenedorDias.Controls.Add(ucdays);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            contenedorDias.Controls.Clear();
            DisplaDays();
        }

        private void botonAnterior_Click(object sender, EventArgs e)
        {
            contenedorDias.Controls.Clear();
            if (month == 1)
            {
                month = 12;
                year--;
            }
            else { month--; }

            mesAñoaCadena();

            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string MonthName = textInfo.ToTitleCase(monthname);
            LBDATE.Text = MonthName + " " + year;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                contenedorDias.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                ucdays.displayEvent();
                contenedorDias.Controls.Add(ucdays);
            }
        }

        private void botonSiguiente_Click(object sender, EventArgs e)
        {
            contenedorDias.Controls.Clear();
            if (month == 12)
            {
                month = 1;
                year++;
            }
            else { month++; }

            mesAñoaCadena();

            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string MonthName = textInfo.ToTitleCase(monthname);
            LBDATE.Text = MonthName + " " + year;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                contenedorDias.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                ucdays.displayEvent();
                contenedorDias.Controls.Add(ucdays);
            }
        }
    }
}

