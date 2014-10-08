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

        private PathFigure temp = new PathFigure();

        public RelayCommand Plot
        {
            get;
            set;
        }

        void plot()
        {
            myPathGeometry.Figures.Add(temp);
        }

        /// <summary>
        /// Initializes a new instance of the SumViewModel class.
        /// </summary>
        public SumViewModel()
        {
            Messenger.Default.Register<PathFigure>(this, "Sum", n => temp = n);

            MessageBox.Show("StartPoint: " + temp.StartPoint.X.ToString() + " " + temp.StartPoint.Y.ToString());

            
            Plot = new RelayCommand(() => plot());
        }
    }
}