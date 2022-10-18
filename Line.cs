using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IZ
{
    public class Line
    {
        public double L1 { get; set; } = 0;
        public double L2 { get; set; } = 0;
        public Tuple<double, double> pointValue1 { get; set; }
        public Tuple<double, double> pointValue2 { get; set; }

        public Line(double l1, double l2, Tuple<double, double> pointValue1, Tuple<double, double> pointValue2)
        {
            L1 = l1;
            L2 = l2;
            this.pointValue1 = pointValue1;
            this.pointValue2 = pointValue2;
        }
    }
}
