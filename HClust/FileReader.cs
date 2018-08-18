using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HClust
{
    public class FileReader
    {
        private string path;
        private int columns;
        public double[,] Data { get; set; }
        public double[,] DistanceMatrix { get; set; }
        public List<DataPoint> DataPoints { get; set; }
        public FileReader(string p, int cols)
        {
            path = p;
            columns = cols;
            DataPoints = new List<DataPoint>();
            ReadFile();
            
        }

        private void ReadFile()
        {
            var lines = File.ReadAllLines(path);
            Data = new double[lines.Length, columns];
            int counter = 0;
            foreach (var line in lines)
            {
                var dp = new DataPoint((int)ClusterIds.Unclassified);
                //var dp = new DataPoint(counter);
                Data[counter, 0] = dp.Values[0] = Double.Parse(line.Split(';')[0], CultureInfo.InvariantCulture);
                Data[counter, 1] = dp.Values[1] = Double.Parse(line.Split(';')[1], CultureInfo.InvariantCulture);
                DataPoints.Add(dp);
                counter++;
            }
        }

        public Double[,] GetData()
        {
            return Data;
        }

        public List<DataPoint> GetDataPoints()
        {
            return DataPoints;
        }

        public Double[,] GetDistanceMatrix()
        {
            var size = Data.GetLength(0);
            DistanceMatrix = new Double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    var dist = Distance.EuclideanDistance(new double[] { Data[i, 0], Data[i, 1] }, new double[] { Data[j, 0], Data[j, 1] });
                    DistanceMatrix[i, j] = DistanceMatrix[j, i] = dist;
                }
            }

            return DistanceMatrix;
        }

    }
}
