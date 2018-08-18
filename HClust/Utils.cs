using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public static class Utils
    {
        public static double ConvertRange(
                                   double originalStart, double originalEnd, // original range
                                   double newStart, double newEnd, // desired range
                                   double value) // value to convert
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (double)(newStart + ((value - originalStart) * scale));
        }
    }
}
