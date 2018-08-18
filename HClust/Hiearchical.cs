using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public class Hiearchical
    {
        private const double ZERO = 0d;
        private int _clusters;
        private int _reqClusters;
        private int N;
        private double[,] _distanceMatrix;
        private LinkageCriterion _criterion;
        private Linkage _linkage;

        public List<Cluster> LinkageClusters;

        public List<DataPoint> points { get; set; }

        public Hiearchical(LinkageCriterion criterion, int reqClust, List<DataPoint> p)
        {
            _criterion = criterion;
            LinkageClusters = new List<Cluster>();
            points = new List<DataPoint>();
            _clusters = N = p.Count();
            _reqClusters = reqClust;
            points = p;

            switch (_criterion)
            {
                case LinkageCriterion.SINGLE:
                    this._linkage = new SingleLinkage();
                    break;
                case LinkageCriterion.COMPLETE:
                    this._linkage = new CompleteLinkage();
                    break;
                case LinkageCriterion.AVERAGE:
                    this._linkage = new AverageLinkage();
                    break;
                case LinkageCriterion.WARD:
                    this._linkage = new WardLinkage();
                    break;
            }
        }

        public Hiearchical(LinkageCriterion criterion, int reqClust, double[,] dMatrix, List<DataPoint> p)
        {
            _criterion = criterion;
            LinkageClusters = new List<Cluster>();
            points = new List<DataPoint>();
            _distanceMatrix = dMatrix;
            _clusters = N = _distanceMatrix.GetLength(0);
            _reqClusters = reqClust;
            points = p;

            switch (_criterion)
            {
                case LinkageCriterion.SINGLE:
                    this._linkage = new SingleLinkage();
                    break;
                case LinkageCriterion.COMPLETE:
                    this._linkage = new CompleteLinkage();
                    break;
                case LinkageCriterion.AVERAGE:
                    this._linkage = new AverageLinkage();
                    break;
                case LinkageCriterion.WARD:
                    this._linkage = new WardLinkage();
                    break;
            }
        }


        public void Clustering()
        {
            // start timer
            var start_time = DateTime.Now;

            // Start by placing each point in its own cluster
            foreach (DataPoint p in points)
            {
                LinkageClusters.Add(new Cluster(p));
            }

            double minDistance = 0;

            if (_linkage.Equals(LinkageCriterion.COMPLETE) || _linkage.Equals(LinkageCriterion.WARD))
                minDistance = Double.MaxValue;
            else
                minDistance = Double.MinValue;

            double currentDistance;

            // Array to hold pair of closest clusters
            Cluster[] twoClosestClusters = new Cluster[2];

            // while there are more than k clusters
            while (LinkageClusters.Count() > _reqClusters)
            {

                // Calculate and store the distance between each pair of clusters, finding the minimum distance between clusters
                var link = _linkage.Measure(LinkageClusters);
                twoClosestClusters = link.Item1;
                minDistance = link.Item2;
                currentDistance = link.Item3;
                    

                Cluster mergedCluster = Cluster.mergeClusters(twoClosestClusters[0], twoClosestClusters[1]);
                LinkageClusters.Add(mergedCluster);
                LinkageClusters.Remove(twoClosestClusters[0]);
                LinkageClusters.Remove(twoClosestClusters[1]);

                // reset min distance so Array is overwritten
                if(_linkage.Equals(LinkageCriterion.COMPLETE) || _linkage.Equals(LinkageCriterion.WARD))
                    minDistance = Double.MaxValue;
                else
                    minDistance = Double.MinValue;

                // just to be safe, nullify array as well
                twoClosestClusters[0] = null;
                twoClosestClusters[1] = null;
            }

            // end timer
            var end_time = DateTime.Now;

            // compute time for algorithm
            var time = end_time - start_time;
        }
    }
}
