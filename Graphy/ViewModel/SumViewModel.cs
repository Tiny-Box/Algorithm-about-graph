using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using System.Windows;
using System.Windows.Media;

namespace Graphy.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SumViewModel : ViewModelBase
    {

        private PathGeometry _myPathGeometry = new PathGeometry();
        public PathGeometry myPathGeometry
        {
            get
            {
                return _myPathGeometry;
            }
            set
            {
                _myPathGeometry = value;
                RaisePropertyChanged("myPathGeometry");
            }
        }

        private PathFigure _temp = new PathFigure();
        public PathFigure temp
        {
            get
            {
                return _temp;
            }
            set
            {
                _temp = value;
                RaisePropertyChanged("temp");
            }
        }

        public RelayCommand Plot
        {
            get;
            set;
        }

        void plot()
        {
            

            double Max = 0;
            double Tab = 0;

            // 取最大值
            for (int i = 0; i < Graphy.Model.Modify.GraySum.Length; i++)
            {
                Tab = (Max > Graphy.Model.Modify.GraySum[i]) ? Tab : i;
                Max = (Max > Graphy.Model.Modify.GraySum[i]) ? Max : Graphy.Model.Modify.GraySum[i];
            }
            //MessageBox.Show(pixels.Length.ToString());
            //MessageBox.Show(Max.ToString() + " " + Tab.ToString());
            //MessageBox.Show("50 " + Graphy.Model.Modify.GraySum[50].ToString());
            //MessageBox.Show("100 " + Graphy.Model.Modify.GraySum[100].ToString());


            PathFigure pathFigure = new PathFigure();

            double Xr = (300 - 0) / (256 - 0);
            double Yr = (300 - 0) / (0 - Max);
            Matrix matrix1 = new Matrix(Xr, 0, 0, Yr, 0, 300);

            Point[] Sum = new Point[Graphy.Model.Modify.GraySum.Length];
            for (int i = 0; i < 256; i++)
            {
                Sum[i] = Point.Multiply(new Point(i, Graphy.Model.Modify.GraySum[i]), matrix1);
            }
            pathFigure.StartPoint = Point.Multiply(new Point(0, Graphy.Model.Modify.GraySum[0]), matrix1);
            MessageBox.Show(pathFigure.StartPoint.X.ToString() + " " + pathFigure.StartPoint.Y.ToString());
            PolyLineSegment myPolyLineSegment = new PolyLineSegment();
            myPolyLineSegment.Points = new PointCollection(Sum);

            pathFigure.Segments.Add(myPolyLineSegment);

            //Messenger.Default.Send<PathFigure>(pathFigure, "Sum");

            //MessageBox.Show("Point: " + Sum[25].X.ToString() + " " + Sum[25].Y.ToString());


            myPathGeometry.Figures.Add(pathFigure);
        }

        /// <summary>
        /// Initializes a new instance of the SumViewModel class.
        /// </summary>
        public SumViewModel()
        {
            //Messenger.Default.Register<PathFigure>(this, "Sum", n => temp = n);




            plot();
        }
    }
}