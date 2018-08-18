using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public static class Distance
    {
        public static double EuclideanDistance(double[] a, double[] b)
        {
            var sum = 0d;
            for (int i = 0; i < a.Count(); i++)
            {
                sum += Math.Pow(Math.Abs(a[i] - b[i]), 2);
            }

            return Math.Sqrt(sum);
        }
    }
}
