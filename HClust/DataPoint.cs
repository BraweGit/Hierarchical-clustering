using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public class DataPoint
    {
        public double[] Values { get; set; }
        public int Cluster { get; set; }
        public bool IsVisited { get; set; }

        public DataPoint(int cluster = (int) ClusterIds.Unclassified)
        {
            Values = new double[2];
            Cluster = cluster;
            IsVisited = false;

        }

        public double DistanceToOtherPoint(DataPoint otherPoint)
        {
            return DistanceBetweenPoints(this, otherPoint);
        }

        public static double DistanceBetweenPoints(DataPoint c1, DataPoint c2)
        {
            return Distance.EuclideanDistance(new double[] { c1.Values[0], c1.Values[1] }, new double[] { c2.Values[0], c2.Values[1] });
        }
    }
}
