using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HClust
{
    public partial class Form1 : Form
    {
        private FileReader _fileReader;
        private int _columns;
        private double[,] _data;
        private double[,] _distanceMatrix;
        private Hiearchical _hierarchical;
        private DBScan _dbScan;
        private ClusterValidation _clusterValidation;
        private double _epsilon;
        private int _minPoints;

        public Form1()
        {
            InitializeComponent();
            _clusterValidation = new ClusterValidation();
            // Getting data
            _columns = 2;
            _fileReader = new FileReader("pass.csv", _columns);
            _data = _fileReader.GetData();
            //_data = null;
            //_distanceMatrix = _fileReader.GetDistanceMatrix();

            // Sample data
            //var sampleData = new List<DataPoint>();
            //sampleData.Add(new DataPoint() { Values = new double[] { 0, 0 } });
            //sampleData.Add(new DataPoint() { Values = new double[] { 10, 10 } });
            //sampleData.Add(new DataPoint() { Values = new double[] { 8, 8 } });
            //sampleData.Add(new DataPoint() { Values = new double[] { 6, 7 } });
            //sampleData.Add(new DataPoint() { Values = new double[] { 4, 3 } });
            //sampleData.Add(new DataPoint() { Values = new double[] { 3, 2 } });

            // Hierarchical Clustering
            _hierarchical = new Hiearchical(LinkageCriterion.WARD, 2, _fileReader.DataPoints);
            _hierarchical.Clustering();
            //var ssq_H = _clusterValidation.SSQ(_hierarchical.LinkageClusters);
            //var ii_H = _clusterValidation.IntraToInter(_hierarchical.LinkageClusters);
            //var sc_H = _clusterValidation.Silhouette(_hierarchical.LinkageClusters);

            //_hierarchical = new Hiearchical(LinkageCriterion.COMPLETE, 5, _fileReader.DataPoints);
            //_hierarchical.Clustering();
            //var ssq_HC = _clusterValidation.SSQ(_hierarchical.LinkageClusters);
            //var ii_HC = _clusterValidation.IntraToInter(_hierarchical.LinkageClusters);
            //var sc_HC = _clusterValidation.Silhouette(_hierarchical.LinkageClusters);

            //ShowClust();

            // DBScan
            //_epsilon = 3d;
            //_minPoints = 20;
            //_dbScan = new DBScan(_fileReader.DataPoints, _epsilon, _minPoints);
            //_dbScan.Clustering();

            //var ssq = _clusterValidation.SSQ(_dbScan.Clusters);
            //var ii = _clusterValidation.IntraToInter(_dbScan.Clusters);
            //var sc = _clusterValidation.Silhouette(_dbScan.Clusters);
            ShowClust();
        }

        private void DrawPoint(int i, DataPoint p, Graphics g, Bitmap image)
        {
            var scale = 10;
            var brush = Brushes.Transparent;
            if (i == 0)
                brush = Brushes.Red;
            else if (i == 1)
                brush = Brushes.Blue;
            else if (i == 2)
                brush = Brushes.Green;
            else if (i == 3)
                brush = Brushes.Orange;
            else if (i == 4)
                brush = Brushes.SaddleBrown;
            else brush = Brushes.Black;

            var X = (p.Values[0] * scale) + (image.Width / 2);//(float)Utils.ConvertRange(min, max, 0, image.Width, p.Values[0]);
            var Y = (p.Values[1] * scale) + (image.Height / 2);//(float)Utils.ConvertRange(min, max, 0, image.Height, p.Values[1]);
            g.FillEllipse(brush, (float)X, (float)Y, 4, 4);
        }

        private void ShowClust()
        {
            
            var image = new Bitmap(picClust.Width, picClust.Height);
            var g = Graphics.FromImage(image);

            int i = 0;
            foreach (var c in _hierarchical.LinkageClusters)
            {
                foreach (var p in c.Points)
                {
                    DrawPoint(i, p, g, image);
                }
                i++;
            }
            picClust.Image = image;
        }
    }
}
