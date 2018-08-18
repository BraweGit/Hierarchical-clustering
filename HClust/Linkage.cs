using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClust
{
    public abstract class Linkage
    {
        public abstract Tuple<Cluster[], double, double> Measure(List<Cluster> CompleteLinkageClusters);
    }
}
