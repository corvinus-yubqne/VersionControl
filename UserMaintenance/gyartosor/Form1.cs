﻿using gyartosor.Entities;
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
        private List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;
        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }

        }
        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
            
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var mostToTheRight = 0;

            foreach (var b in _balls)
            {
                b.MoveToy();
                if (b.Left > mostToTheRight)
                {
                    mostToTheRight = b.Left;
                }
            }

            if (mostToTheRight > 1000)
            {
                var delete = _balls[0];
                _balls.Remove(delete);
                mainPanel.Controls.Remove(delete);
            }
        }
    }
}
