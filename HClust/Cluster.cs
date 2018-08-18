using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public class Cluster
    {
        public List<DataPoint> Points { get; set; }

        public Cluster(List<DataPoint> points)
        {
            Points = points;
        }

        public Cluster()
        {
            Points = new List<DataPoint>();
        }

        public static Cluster mergeClusters(Cluster c1, Cluster c2)
        {
            Cluster mergedCluster = new Cluster(new List<DataPoint>());
            List<DataPoint> pointsC1 = c1.getPoints();
            for (int i = 0; i < pointsC1.Count(); i++)
            {
                mergedCluster.add(pointsC1[i]);
            }
            List<DataPoint> pointsC2 = c2.getPoints();
            for (int i = 0; i < pointsC2.Count(); i++)
            {
                mergedCluster.add(pointsC2[i]);
            }
            return mergedCluster;
        }

        public Cluster(DataPoint p)
        {
            Points = new List<DataPoint>();
            add(p);
        }

        public void add(DataPoint p)
        {
            Points.Add(p);
        }

        public double[] GetCentroid(Cluster c)
        {
            // Create arr to represent centroid values
            var centroid = new double[c.Points[0].Values.Length];

            for (int i = 0; i < centroid.Length; i++)
            {
                foreach (var p in c.Points)
                {
                    centroid[i] += p.Values[i];
                }

                centroid[i] = centroid[i] / c.Points.Count;
            }

            return centroid;
        }

        public double SumOfSquares(Cluster c)
        {
            var sum = 0d;
            var centroid = GetCentroid(c);

            for(int i = 0; i < c.Points.Count; i++)
            {
                for(int j = 0; j < centroid.Length; j++)
                {
                    sum += Math.Abs(c.Points[i].Values[j] - centroid[j]);
                }
            }

            return Math.Pow(sum,2);
        }

        public double wardLinkageDistance(Cluster c1, Cluster c2)
        {

            var points1 = c1.getPoints();
            var points2 = c2.getPoints();

            var allPoints = points1.Union(points2).ToList();
            var c3 = new Cluster() { Points = allPoints };

            var sseA = SumOfSquares(c1);
            var sseB = SumOfSquares(c2);
            var sseAB = SumOfSquares(c3);

            var dist = sseAB - (sseA - sseB);

            return dist;
        }

        public static double singleLinkDistance(Cluster c1, Cluster c2)
        {

            List<DataPoint> points1 = c1.getPoints();
            List<DataPoint> points2 = c2.getPoints();

            int numPointsInC1 = points1.Count();
            int numPointsInC2 = points2.Count();

            double maxDistance = Double.MaxValue;

            for (int i1 = 0; i1 < numPointsInC1; i1++)
            {
                for (int i2 = 0; i2 < numPointsInC2; i2++)
                {
                    maxDistance = Math.Min(points1[i1].DistanceToOtherPoint(points2[i2]), maxDistance);
                }
            }

            return maxDistance;
        }

        public static double completeLinkageDistance(Cluster c1, Cluster c2)
        {

            List<DataPoint> points1 = c1.getPoints();
            List<DataPoint> points2 = c2.getPoints();

            int numPointsInC1 = points1.Count();
            int numPointsInC2 = points2.Count();

            double maxDistance = Double.MinValue;

            for (int i1 = 0; i1 < numPointsInC1; i1++)
            {
                for (int i2 = 0; i2 < numPointsInC2; i2++)
                {
                    maxDistance = Math.Max(points1[i1].DistanceToOtherPoint(points2[i2]), maxDistance);
                }
            }

            return maxDistance;
        }

        public List<DataPoint> getPoints()
        {
            return Points;
        }
    }
}
