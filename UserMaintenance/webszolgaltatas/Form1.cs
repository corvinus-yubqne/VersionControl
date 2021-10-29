using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using webszolgaltatas.MnbServiceReference;
using webszolgaltatas.Entities;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace webszolgaltatas
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();

            comboBox1.DataSource = Currencies;

            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetCurrenciesRequestBody request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);
            string result = response.GetCurrenciesResult;
            XmlDocument vxml = new XmlDocument();
            vxml.LoadXml(result);
            foreach (XmlElement item in vxml.DocumentElement.FirstChild.ChildNodes)
            {
                Currencies.Add(item.InnerText);
            }


            RefreshData();
        }

        private string WebServiceCall()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = comboBox1.SelectedItem.ToString();
            request.startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            request.endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            File.WriteAllText("export.xml", result);

            return result;
        }

        private void XmlProcess(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                RateData rate = new RateData();

                rate.Date = DateTime.Parse(element.GetAttribute("date"));
               
                var childElement = (XmlElement)element.FirstChild;
                if (childElement == null) continue;

                rate.Currency = childElement.GetAttribute("curr");
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0) 
                    rate.Value = value / unit;

                Rates.Add(rate);
            }            
        }

        private void VisualizeData()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisX.IsStartedFromZero = false;
        }

        private void RefreshData()
        {
            if (comboBox1.SelectedItem == null) return;

            Rates.Clear();

            string result = WebServiceCall();
            XmlProcess(result);
            VisualizeData();

            dataGridView1.DataSource = Rates;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }




        //EZ VÉLETLEN VOLT
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
