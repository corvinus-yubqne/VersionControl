﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using webszolgaltatas.MnbServiceReference;

namespace webszolgaltatas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WebServiceCall();
        }

        private void WebServiceCall()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
        }
    }
}
