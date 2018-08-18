using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public class DBScan
    {
        public List<DataPoint> Points { get; set; }
        private double _epsilon;
        private int _minPoints;
        public List<Cluster> Clusters;

        public DBScan(List<DataPoint> pts, double e, int minPts)
        {
            Points = pts;
            _epsilon = e;
            _minPoints = minPts;
            Clusters = new List<Cluster>();
        }

        public void Clustering()
        {
            if (Points.Count == 0) return;

            int clusterId = 1;

            for (int i = 0; i < Points.Count; i++)
            {
                var p = Points[i];
                if (p.Cluster == (int)ClusterIds.Unclassified)
                    if (ExpandCluster(Points, p, clusterId)) clusterId++;
            }

            int total = Points.OrderBy(p => p.Cluster).Last().Cluster;

            if (total < 1) return;

            for (int i = 0; i < total; i++) Clusters.Add(new Cluster());

            var noise = new Cluster();
            
            foreach (var p in Points)
            {
                if (p.Cluster > (int)ClusterIds.Unclassified) Clusters[p.Cluster - 1].add(p);
                 if (p.Cluster == (int)ClusterIds.Noise) noise.add(p);
            }

            Clusters.Add(noise);
            return;
        }

        private bool ExpandCluster(List<DataPoint> neighborPts, DataPoint p, int clusterId)
        {
            var seeds = RegionQuery(neighborPts, p);
            if (seeds.Count < _minPoints)
            {
                p.Cluster = (int)ClusterIds.Noise;
                return false;
            }
            else
            {
                for (int i = 0; i < seeds.Count; i++) seeds[i].Cluster = clusterId;
                seeds.Remove(p);
                while (seeds.Count > 0)
                {
                    var currentP = seeds[0];
                    var result = RegionQuery(neighborPts, currentP);
                    if (result.Count >= _minPoints)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            var resultP = result[i];
                            if (resultP.Cluster == (int)ClusterIds.Unclassified || resultP.Cluster == (int)ClusterIds.Noise)
                            {
                                if (resultP.Cluster == (int)ClusterIds.Unclassified) seeds.Add(resultP);
                                resultP.Cluster = clusterId;
                            }
                        }
                    }
                    seeds.Remove(currentP);
                }
                return true;
            }
        }


        private List<DataPoint> RegionQuery(List<DataPoint> pts, DataPoint point)
        {
            return pts.Where(x => Distance.EuclideanDistance(point.Values, x.Values) <= _epsilon).ToList();
        }
    }
}
