using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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

        private PathGeometry _Gray = new PathGeometry();
        public PathGeometry Gray
        {
            get
            {
                return _Gray;
            }
            set
            {
                _Gray = value;
                RaisePropertyChanged("Gray");
            }
        }

        private PathGeometry _Red = new PathGeometry();
        public PathGeometry Red
        {
            get
            {
                return _Red;
            }
            set
            {
                _Red = value;
                RaisePropertyChanged("Red");
            }
        }

        private PathGeometry _Green = new PathGeometry();
        public PathGeometry Green
        {
            get
            {
                return _Green;
            }
            set
            {
                _Green = value;
                RaisePropertyChanged("Green");
            }
        }

        private PathGeometry _Blue = new PathGeometry();
        public PathGeometry Blue
        {
            get
            {
                return _Blue;
            }
            set
            {
                _Blue = value;
                RaisePropertyChanged("Blue");
            }
        }


        void Grayplot()
        {


            double Max = 0;
            double Tab = 0;

            // 取最大值
            for (int i = 0; i < Graphy.Model.Modify.GraySum.Length; i++)
            {
                Tab = (Max > Graphy.Model.Modify.GraySum[i]) ? Tab : i;
                Max = (Max > Graphy.Model.Modify.GraySum[i]) ? Max : Graphy.Model.Modify.GraySum[i];
            }


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
            //MessageBox.Show(pathFigure.StartPoint.X.ToString() + " " + pathFigure.StartPoint.Y.ToString());
            PolyLineSegment myPolyLineSegment = new PolyLineSegment();
            myPolyLineSegment.Points = new PointCollection(Sum);

            pathFigure.Segments.Add(myPolyLineSegment);


            Gray.Figures.Add(pathFigure);
        }

        /// <summary>
        /// Initializes a new instance of the SumViewModel class.
        /// </summary>
        public SumViewModel()
        {

            Grayplot();
        }
    }
}