using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace MintaZH1
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void ExcelExport()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;



                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void LoadData (string filename)
        {

        }
    }
}
