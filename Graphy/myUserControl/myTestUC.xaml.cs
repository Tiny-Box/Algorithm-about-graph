using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphy.myUserControl
{
    /// <summary>
    /// myTestUC.xaml 的交互逻辑
    /// </summary>
    public partial class myTestUC : BaseUserControl
    {
        public myTestUC()
        {
            InitializeComponent();
        }

        void myDialog_OnClose(object sender, RoutedEventArgs e)
        {
            base.Close();
        }
        public myTestUC(string info) : this()
        {
            this.info.Text = info;
        }

        protected override void OnClickOutside()
        {
            //base.Close();
        }
    }


}
