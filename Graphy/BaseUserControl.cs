using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Linq;
using System.Text;

namespace Graphy
{
    public class BaseUserControl : UserControl
    {
        #region 私有字段
        Popup popup;
        Grid grid;
        Canvas canvas;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseUserControl()
        {
            popup = new Popup();
            grid = new Grid();
            popup.Child = grid;

        }

        protected virtual void OnClickOutside() { }

        #region 公共方法

        public void Show()
        {
            UpdateSize();
            grid.Children.Add(this);
            popup.IsOpen = true;

        }
        public void ShowAsModal()
        {
            canvas = new Canvas();
            UpdateSize();
            canvas.Background = new SolidColorBrush(Colors.Red);
            canvas.Opacity = 0.2;
            canvas.MouseLeftButtonDown += (sender, args) => { OnClickOutside(); };
            grid.Children.Add(canvas);
            grid.Children.Add(this);
            popup.IsOpen = true;
        }

        public void Close()
        {
            if (popup != null)
            {
                popup.IsOpen = false;
                //popup = null;
                //grid = null;
                //canvas = null;
            }
        }
        #endregion
        #region 私有方法

        private void UpdateSize()
        {
            grid.Width = Application.Current.Windows[0].ActualWidth;
            grid.Height = Application.Current.Windows[0].ActualHeight;
            if (canvas != null)
            {
                canvas.Width = grid.Width;
                canvas.Height = grid.Height;
            }
        }
        #endregion
    }
}
