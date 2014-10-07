using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Graphy.Model;

namespace Graphy.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public RelayCommand Open
        {
            get;
            set;
        }
        public RelayCommand GetGray
        {
            get;
            set;
        }
        public RelayCommand Bayer
        {
            get;
            set;
        }
        public RelayCommand Floyd
        {
            get;
            set;
        }
        public RelayCommand GraySum
        {
            get;
            set;
        }

        private string fileName = string.Empty;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        private string originpic = "";
        public string Originpic
        {
            get { return originpic; }
            set 
            { 
                originpic = value; 
                RaisePropertyChanged("Originpic"); 
            }
        }

        private WriteableBitmap modifyimg = null;
        public WriteableBitmap Modifyimg
        {
            get
            {
                return modifyimg;
            }
            set
            {
                modifyimg = value;
                RaisePropertyChanged("Modifyimg");
            }
        }

        void Openpic()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();

            fileName = dlg.FileName;
           
            Originpic = dlg.FileName;
        }

        Modify gray = new Modify();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            

            Open = new RelayCommand(() => Openpic());

            GetGray = new RelayCommand(() => Modifyimg = gray.GrayFilter(new BitmapImage(new Uri(fileName))));

            Bayer = new RelayCommand(() => Modifyimg = gray.LimbPatternBayer(new BitmapImage(new Uri(fileName))));

            Floyd = new RelayCommand(() => Modifyimg = gray.DitherFloydSteinberg(new BitmapImage(new Uri(fileName))));

            GraySum = new RelayCommand(() => gray.GraySum(new BitmapImage(new Uri(fileName))));

        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}