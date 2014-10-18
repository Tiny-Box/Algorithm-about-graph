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

namespace Graphy
{
    public class DragHelper
    {
        public DragHelper()
        {
        }

        public void BindMoveFunction(UIElement triggerPart, UIElement movePart)
        {
            this.triggerPart = triggerPart;
            this.movePart = movePart;

            this.triggerPart.MouseLeftButtonDown += new MouseButtonEventHandler(triggerPart_MouseLeftButtonDown);
            this.triggerPart.MouseLeftButtonUp += new MouseButtonEventHandler(triggerPart_MouseLeftButtonUp);
            this.triggerPart.MouseMove += new MouseEventHandler(triggerPart_MouseMove);
        }

        void triggerPart_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.startMoving)
            {
                Point curMousePos = e.GetPosition(null);
                this.movePart.SetValue(Canvas.LeftProperty,
                    (double)this.movePart.GetValue(Canvas.LeftProperty) + curMousePos.X - this.prevMousePos.X);
                this.movePart.SetValue(Canvas.TopProperty,
                    (double)this.movePart.GetValue(Canvas.TopProperty) + curMousePos.Y - this.prevMousePos.Y);
                this.prevMousePos = curMousePos;
            }
        }

        void triggerPart_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.startMoving)
            {
                this.triggerPart.ReleaseMouseCapture();
                this.startMoving = false;
            }
        }

        void triggerPart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.startMoving)
            {
                this.startMoving = true;
                this.triggerPart.CaptureMouse();
                prevMousePos = e.GetPosition(null);
            }
        }

        private UIElement triggerPart;
        private UIElement movePart;
        private bool startMoving = false;
        private Point prevMousePos;
    }
}
