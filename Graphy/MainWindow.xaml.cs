using System.Windows;

using Graphy.Model;
using Graphy.ViewModel;

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Graphy
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class MainWindow : Window
    {

        string Picname = string.Empty;
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();

            MessageBox.Show(dlg.FileName);
            Picname = dlg.FileName;

            Show(Picname);
        }

        private void Show(string fileName)
        {
            Modify gray = new Model.Modify();
            Uri fileUri = new Uri(fileName);
            BitmapFrame img = BitmapFrame.Create(fileUri);
            BitmapDecoder decoder = BitmapDecoder.Create(fileUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            modify.Source = gray.DitherFloydSteinberg(new BitmapImage(fileUri));

        }

    }
}