using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IZ
{
    internal class Core
    {
        public double a { get; set; } = 0;
        public double b { get; set; } = 0;
        public double c { get; set; } = 0;
        public double L1 { get; set; } = 0;
        public double L2 { get; set; } = 0;
        public double epsilon { get; set; } = 0;
        public double l0 { get; set; } = 0;
        public bool isMin { get; set; } = true;
        public int indexEquation { get; set; } = 0;
    }
}
