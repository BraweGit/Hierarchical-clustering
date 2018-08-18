using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public class CompleteLinkage : Linkage
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
                    currentDistance = Cluster.completeLinkageDistance(c1, c2);
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
}
