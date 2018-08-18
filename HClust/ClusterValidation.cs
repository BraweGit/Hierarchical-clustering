using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HClust
{
    public class ClusterValidation
    {
        public double SSQ(List<Cluster> clusters)
        {
            return (from c in clusters let centroid = new DataPoint() { Values = c.GetCentroid(c) } select c.Points.Sum(p => Math.Pow(p.DistanceToOtherPoint(centroid), 2))).Sum();
        }

        public double RIntraToInter(List<Cluster> clusters)
        {
            var inners = new List<double>();
            var inters = new List<double>();

            foreach (var c in clusters)
            {
                var inn = (from a in c.Points from b in c.Points select a.DistanceToOtherPoint(b)).ToList();
                inners.Add(inn.Sum() / inn.Count);
                var inter = new List<double>();

                foreach (var outer in clusters)
                {
                    if (!outer.Equals(c))
                        inter = (from a in c.Points from b in outer.Points select a.DistanceToOtherPoint(b)).ToList();
                }

                inters.Add(inter.Sum() / inter.Count);
            }

            var sum = inners.Sum() / inters.Sum();
            return sum;
        }

        public double IntraToInter(List<Cluster> clusters)
        {
            var data = new List<DataPoint>();
            var cl = 0;
            foreach (var c in clusters)
            {
                foreach (var p in c.Points)
                {
                    p.Cluster = cl;
                    data.Add(p);
                }
                cl++;
            }

            var clusterCount = data.GroupBy(x => x.Cluster).Count();
            var intraAvg = 0.0;
            var interAvg = 0.0;

            for (int i = 0; i < clusterCount; i++)
            {
                var intra = 0.0;
                var inter = 0.0;
                foreach (var p1 in data.Where(x => x.Cluster == i))
                {
                    foreach (var p2 in data.Where(x => x.Cluster == i))
                        intra += Math.Sqrt(Math.Pow(p1.Values[0] - p2.Values[0], 2) + Math.Pow(p1.Values[1] - p2.Values[1], 2));
                    foreach (var p2 in data.Where(x => x.Cluster != i))
                        inter += Math.Sqrt(Math.Pow(p1.Values[0] - p2.Values[0], 2) + Math.Pow(p1.Values[1] - p2.Values[1], 2));
                }
                intraAvg += intra / data.Count(x => x.Cluster == i);
                interAvg += inter / data.Count(x => x.Cluster != i);
            }
            intraAvg /= clusterCount;
            interAvg /= clusterCount;
            Console.WriteLine("Intra: {0} Inter: {1} Intra/Inter {2}", intraAvg, interAvg, intraAvg / interAvg);
            return intraAvg / interAvg;
        }

        public double Silhouette(List<Cluster> clusters)
        {

            var davg = new List<double>();
            var dmin = new List<double>();

            var i = 0;
            var avg_list = new List<double>();
            var avgmin_list = new List<double>();
            var avgmin = double.MaxValue;
            foreach (var c in clusters)
            {
                foreach (var p in c.Points)
                {
                    var temp_avgmin_list = new List<double>();
                    var temp_avg_list = new List<double>();
                    foreach (var outer in clusters)
                    {
                        if (!outer.Equals(c))
                        {
                            foreach (var pp in outer.Points)
                            {
                                temp_avgmin_list.Add(p.DistanceToOtherPoint(pp));
                            }
                        }
                    }



                    
                    foreach (var pp in c.Points)
                    {
                        if (!p.Equals(pp))
                        {
                            temp_avg_list.Add(p.DistanceToOtherPoint(pp));
                        }
                    }

                    if(temp_avgmin_list.Count != 0)
                        avgmin_list.Add(temp_avgmin_list.Average());
                    else avgmin_list.Add(0);

                    if (temp_avg_list.Count != 0)
                        avg_list.Add(temp_avg_list.Average());
                    else avg_list.Add(0);
                }

                davg.Add(avg_list.Average());
                avgmin = avgmin_list.Average() < avgmin ? avgmin_list.Average() : avgmin;
                dmin.Add(avgmin);

                Console.WriteLine("Silhouette: {0}", (dmin[i] - davg[i]) / Math.Max(dmin[i], davg[i]));
                i++;
            }

            return 0;
        }
    }
}
