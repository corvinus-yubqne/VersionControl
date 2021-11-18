using gyartosor.Abstracts;
using gyartosor.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyartosor
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }

        }
        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
            
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var mostToTheRight = 0;

            foreach (var t in _toys)
            {
                t.MoveToy();
                if (t.Left > mostToTheRight)
                {
                    mostToTheRight = t.Left;
                }
            }

            if (mostToTheRight > 1000)
            {
                var delete = _toys[0];
                _toys.Remove(delete);
                mainPanel.Controls.Remove(delete);
            }
        }
    }
}
