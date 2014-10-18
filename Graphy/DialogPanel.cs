using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace Graphy
{
    public class DialogPanel : ContentControl
    {
        public DialogPanel()
            : base()
        {
            this.DefaultStyleKey = typeof(DialogPanel);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.DragEnable)
            {
                UIElement header = this.GetTemplateChild("Header") as UIElement;
                UIElement root = this.GetTemplateChild("root") as UIElement;

                DragHelper dragHelper = new DragHelper();
                dragHelper.BindMoveFunction(header, root);
            }

            ButtonBase close = this.GetTemplateChild("close") as ButtonBase;
            close.Click += new RoutedEventHandler(close_Click);

            Storyboard storyboard = this.GetTemplateChild("storyboard") as Storyboard;
            storyboard.Begin();
        }

        void close_Click(object sender, RoutedEventArgs e)
        {
            if (this.OnClose != null)
                OnClose(sender, e);
        }



        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DialogPanel), null);
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public event RoutedEventHandler OnClose;

        public bool DragEnable { get; set; }



    }
}
