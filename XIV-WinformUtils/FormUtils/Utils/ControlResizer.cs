using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace XIV.Utils
{
    //https://codeutility.org/c-how-can-user-resize-control-at-runtime-in-winforms-stack-overflow/
    public class ControlResizer
    {
        private bool mMouseDown = false;
        private EdgeEnum mEdge = EdgeEnum.None;
        private int selectionWidth = 4;
        private bool mOutlineDrawn = false;
        private Dictionary<Control, EventSuppressor> eventSuppressor = new Dictionary<Control, EventSuppressor>();
        private IList<Control> selectedControls = new List<Control>();

        private enum EdgeEnum
        {
            None,
            Right,
            Left,
            Top,
            Bottom,
            //TopLeft
        }

        public void Add(Control control)
        {
            if (selectedControls.Contains(control)) return;

            if(eventSuppressor.TryGetValue(control, out var suppressor))
            {
                suppressor.Suppress();
            }
            else
            {
                suppressor = new EventSuppressor(control);
                suppressor.Suppress();
                eventSuppressor.Add(control, suppressor);
            }

            control.MouseDown += mControl_MouseDown;
            control.MouseUp += mControl_MouseUp;
            control.MouseMove += mControl_MouseMove;
            control.MouseLeave += mControl_MouseLeave;

            selectedControls.Add(control);
        }

        public void Add(IList<Control> controls)
        {
            foreach (var item in controls)
            {
                Add(item);
            }
        }

        public void Remove(Control control)
        {
            if (!selectedControls.Contains(control)) return;

            control.MouseDown -= mControl_MouseDown;
            control.MouseUp -= mControl_MouseUp;
            control.MouseMove -= mControl_MouseMove;
            control.MouseLeave -= mControl_MouseLeave;
            selectedControls.Remove(control);
            eventSuppressor[control].Resume();
        }

        public void Remove(IList<Control> controls)
        {
            foreach (var item in controls)
            {
                Remove(item);
            }
        }

        private void mControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mMouseDown = true;
            }
        }

        private void mControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mMouseDown = false;
        }

        private void mControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Control control = (Control)sender;
            Graphics graphics = control.CreateGraphics();
            switch (mEdge)
            {
                //case EdgeEnum.TopLeft:
                //    graphics.FillRectangle(Brushes.Fuchsia, 0, 0, selectionWidth * 4, selectionWidth * 4);
                //    mOutlineDrawn = true;
                //    break;
                case EdgeEnum.Left:
                    graphics.FillRectangle(Brushes.Fuchsia, 0, 0, selectionWidth, control.Height);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Right:
                    graphics.FillRectangle(Brushes.Fuchsia, control.Width - selectionWidth, 0, control.Width, control.Height);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Top:
                    graphics.FillRectangle(Brushes.Fuchsia, 0, 0, control.Width, selectionWidth);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Bottom:
                    graphics.FillRectangle(Brushes.Fuchsia, 0, control.Height - selectionWidth, control.Width, selectionWidth);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.None:
                    if (mOutlineDrawn)
                    {
                        control.Refresh();
                        mOutlineDrawn = false;
                    }
                    break;
            }
            if (mMouseDown & mEdge != EdgeEnum.None)
            {
                control.SuspendLayout();
                switch (mEdge)
                {
                    //case EdgeEnum.TopLeft:
                    //    control.SetBounds(control.Left + e.X, control.Top + e.Y, control.Width, control.Height);
                    //    break;
                    case EdgeEnum.Left:
                        control.SetBounds(control.Left + e.X, control.Top, control.Width - e.X, control.Height);
                        break;
                    case EdgeEnum.Right:
                        control.SetBounds(control.Left, control.Top, control.Width - (control.Width - e.X), control.Height);
                        break;
                    case EdgeEnum.Top:
                        control.SetBounds(control.Left, control.Top + e.Y, control.Width, control.Height - e.Y);
                        break;
                    case EdgeEnum.Bottom:
                        control.SetBounds(control.Left, control.Top, control.Width, control.Height - (control.Height - e.Y));
                        break;
                }
                control.ResumeLayout();
            }
            else
            {
                //if(e.X <= (selectionWidth * 4) & e.Y <= (selectionWidth * 4))
                //{
                //    //top left corner
                //    control.Cursor = Cursors.SizeAll;
                //    mEdge = EdgeEnum.TopLeft;
                //}
                //else if (e.X <= selectionWidth)
                if (e.X <= selectionWidth)
                {
                    //left edge
                    Cursor.Current = Cursors.VSplit;
                    mEdge = EdgeEnum.Left;
                }
                else if (e.X > control.Width - (selectionWidth + 1))
                {
                    //right edge
                    Cursor.Current = Cursors.VSplit;
                    mEdge = EdgeEnum.Right;
                }
                else if(e.Y <= selectionWidth)
                {
                    //top edge
                    Cursor.Current = Cursors.HSplit;
                    mEdge = EdgeEnum.Top;
                }
                else if (e.Y > control.Height - (selectionWidth + 1))
                {
                    //bottom edge
                    Cursor.Current = Cursors.HSplit;
                    mEdge = EdgeEnum.Bottom;
                }
                else
                {
                    //no edge
                    Cursor.Current = Cursors.Default;
                    mEdge = EdgeEnum.None;
                }
            }
        }

        private void mControl_MouseLeave(object sender, System.EventArgs e)
        {
            Control c = (Control)sender;
            mEdge = EdgeEnum.None;
            c.Refresh();
        }
    }
}
