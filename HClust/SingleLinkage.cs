using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public class SingleLinkage : Linkage
    {
        public override Tuple<Cluster[], double, double> Measure(List<Cluster> CompleteLinkageClusters)
        {
            Cluster[] twoClosestClusters = new Cluster[2];
            double minDistance = Double.MaxValue;
            double currentDistance = 0;
            for (int i = 0; i < CompleteLinkageClusters.Count(); i++)
            {
                for (int j = i + 1; j < CompleteLinkageClusters.Count(); j++)
                {
                    Cluster c1 = CompleteLinkageClusters[i];
                    Cluster c2 = CompleteLinkageClusters[j];
                    currentDistance = Cluster.singleLinkDistance(c1, c2);
                    if (currentDistance < minDistance)
                    {
                        twoClosestClusters[0] = c1;
                        twoClosestClusters[1] = c2;
                        minDistance = currentDistance;
                    }
                }
            }

            return new Tuple<Cluster[], double, double>(twoClosestClusters, minDistance, currentDistance);
        }
    }
    //{
    //    private const double ZERO = 0d;
    //    private int _clusters;
    //    private int _reqClusters;
    //    private int N;
    //    private double[,] _distanceMatrix;

    //    public List<DataPoint> points { get; set; }

    //    public SingleLinkage(int reqClust, double[,] dMatrix, List<DataPoint> p)
    //    {
    //        points = new List<DataPoint>();
    //        _distanceMatrix = dMatrix;
    //        _clusters = N = _distanceMatrix.GetLength(0);
    //        _reqClusters = reqClust;
    //        points = p;
    //    }

    //    public void Clustering()
    //    {

    //        while (_clusters > _reqClusters)
    //        {
    //            // find lowest distance
    //            int rowIndex = 0, colIndex = 0;
    //            var value = double.MaxValue;

    //            for (int i = 0; i < N; i++)
    //            {
    //                for (int j = i; j < N; j++)
    //                {
    //                    if (value > _distanceMatrix[i, j] && i != j)
    //                    {
    //                        rowIndex = i;
    //                        colIndex = j;
    //                        value = _distanceMatrix[i, j];
    //                    }
    //                }
    //            }

    //            for (var i = 0; i < N; i++)
    //            {
    //                if (points[i].Cluster == points[colIndex].Cluster)
    //                {
    //                    points[i].Cluster = points[rowIndex].Cluster;
    //                }
    //            }

    //            _distanceMatrix[rowIndex, colIndex] = double.MaxValue;


    //            Dictionary<int, int> clustersCount = new Dictionary<int, int>();

    //            for (var i = 0; i < N; i++)
    //            {
    //                if (clustersCount.ContainsKey(points[i].Cluster))
    //                {
    //                    clustersCount[points[i].Cluster]++;
    //                }
    //                else
    //                {
    //                    clustersCount.Add(points[i].Cluster, 1);
    //                }

    //                //for (var j = i; j < N; j++)
    //                //{
    //                //    if (points[i].Cluster == points[j].Cluster)
    //                //    {
    //                //        _distanceMatrix[i, j] = double.MaxValue;
    //                //    }
    //                //}
    //            }

    //            _clusters = clustersCount.Count;
    //            //Console.WriteLine("Number of clusters: {0}", _clusters);
    //        }
    //    }
}
