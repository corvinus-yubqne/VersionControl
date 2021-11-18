using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gyartosor.Abstracts;

namespace gyartosor.Entities
{
    public class Present : Toy
    {
        public SolidBrush ribbonColor { get; set; }
        public SolidBrush boxColor { get; set; }

        public Present(Color box, Color ribbon)
        {
            boxColor = new SolidBrush(box);
            ribbonColor = new SolidBrush(ribbon);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(boxColor, 0, 0, Width, Height);
            g.FillRectangle(ribbonColor, 20, 0, Width/5, Height);
            g.FillRectangle(ribbonColor, 0, 20, Width, Height/5);
        }
    }
}
