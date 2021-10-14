﻿using System;
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
using MintaZH1.Entities;

namespace MintaZH1
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        public Form1()
        {
            InitializeComponent();
            FillInData("Summer_olympic_Medals.csv");
            PickYear();
        }

        public void FillInData(string FileName)
        {
            using (var sr = new StreamReader(FileName, Encoding.Default))
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
                            int.Parse(line[7]),
                        }
                    };
                    results.Add(or);
                }
            }
        }

        public void PickYear()
        {
            var filter = (from x in results
                         orderby x.Year
                         select x.Year).Distinct().ToList();

            comboBox1.DataSource = filter;
        }

        private int CalculatePosition(OlympicResult olympic)
        {
            var betterCountry = 0;
            var filteredCountry = from x in results
                                  where olympic.Year == x.Year && olympic.Country != x.Country
                                  select x;
            foreach (var r in results)
            {
                if (r.Medals[0] > olympic.Medals[0])
                {
                    betterCountry++;
                }
                else if (r.Medals[0] == olympic.Medals[0] && r.Medals[1] > olympic.Medals[1])
                {
                    betterCountry++;
                }
                else if (r.Medals[0] == olympic.Medals[0] && r.Medals[1] == olympic.Medals[1] && r.Medals[2] > olympic.Medals[2])
                {
                    betterCountry++;
                }
            }
            return betterCountry + 1;
        }

        private void CalculateOrder()
        {
            foreach (var r in results)
            {
                r.Position = CalculatePosition(r);
            }
        }
    }
}
