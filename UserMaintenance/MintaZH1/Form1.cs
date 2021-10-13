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
using System.IO;

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
            LoadData("Summer_olympic_Medals.csv");
            FillUp();
            LineOfResults();
        }

        private void LineOfResults()
        {
            foreach (var r in results)
            {
                r.Position = Position(r);
            }
        }

        private int Position(OlympicResult oR)
        {
            var betterCountry = 0;
            var filteredCountry = from x in results
                                  where x.Year == oR.Year && x.Country != oR.Country
                                  select x;
            foreach (var fc in filteredCountry)
            {
                if (fc.Medals[0] > oR.Medals[0])
                {
                    betterCountry++;
                }
                else if (fc.Medals[0] == oR.Medals[0] && fc.Medals[1] > oR.Medals[1])
                {
                    betterCountry++;
                }
                else if (fc.Medals[0] == oR.Medals[0] && fc.Medals[1] == oR.Medals[1] && fc.Medals[2] > oR.Medals[2])
                {
                    betterCountry++;
                }
            }
            return betterCountry + 1;
        }

        private void FillUp()
        {
            var years = (from x in results
                         orderby x.Year
                         select x.Year).Distinct();
            comboBox1.DataSource = years.ToList();
        }

        private void LoadData (string fileName)
        {
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(',');
                    var or = new OlympicResult()
                    {
                        Year = int.Parse(line[0]),
                        Country = line[3],
                        Medals = new int[]
                        {
                            int.Parse(line[5]),
                            int.Parse(line[6]),
                            int.Parse(line[7])
                        }
                    };

                    results.Add(or);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateExcel();

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

        private void CreateExcel()
        {
            string[] headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };
        }
    }
}
