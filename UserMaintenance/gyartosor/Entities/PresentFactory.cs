using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gyartosor.Abstracts;

namespace gyartosor.Entities
{
    class PresentFactory : IToyFactory
    {
        public Color boxColor { get; set; }
        public Color ribbonColor { get; set; }
        public Toy CreateNew()
        {
            return new Present(boxColor, ribbonColor);
        }
    }
}
