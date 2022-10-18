using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IZ
{
    public class FibonacciProps
    {
        public List<Line> Lines { get; set; } = new List<Line>();
        public double N { get; set; } = 0;
        public double Fn { get; set; } = 0;
        public double n { get; set; } = 0;
        public double m { get; set; } = 0;
        public double resPoint { get; set; } = 0;
        public double resValue { get; set; } = 0;
    }
}
