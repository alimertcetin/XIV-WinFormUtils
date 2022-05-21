using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace XIV.Utils
{
    //TODO : Change paramaters
    /// <summary>
    /// Dont forget to write a good summary to here
    /// </summary>
    /// <param name="isSelected"></param>
    /// <param name="controlSelector"></param>
    public delegate void ControlSelectionHandler(bool isSelected, ControlSelector controlSelector);

    public class ControlSelector
    {
        //TODO : Add ability to filter selection
        /// <summary>
        /// Occurs when selection gets changed
        /// </summary>
        public event ControlSelectionHandler SelectionChanged
        {
            add => selectionHandler += value;
            remove => selectionHandler -= value;
        }
        private event ControlSelectionHandler selectionHandler;
        
        /// <summary>
        /// A readonly selection list
        /// </summary>
        public IList<Control> Selection => selectedControls.AsReadOnly();
        
        private Form form;
        private Point mouseStartPos;
        private Point mouseEndPos;
        private Rectangle r = new Rectangle();
        private List<Control> selectedControls = new List<Control>();
        private int selectedControlCount;
        private Pen outlinePen = new Pen(Brushes.Black, 5);
        private ToolStripItem clearSelectionItem;

        public ControlSelector(Form formToDraw)
        {
            form = formToDraw;
            var menuStrip = form.ContextMenuStrip;
            if (menuStrip == null)
            {
                menuStrip = new ContextMenuStrip();
                form.ContextMenuStrip = menuStrip;
            }
            menuStrip.Items.Add("Clear Selection");
            clearSelectionItem = menuStrip.Items[menuStrip.Items.Count - 1];
            clearSelectionItem.Click += OnClearSelectionClicked;

            Register();
        }

        private void OnClearSelectionClicked(object sender, EventArgs e)
        {
            selectionHandler?.Invoke(false, this);
            DeselectControls();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            clearSelectionItem.Click -= OnClearSelectionClicked;
            form.ContextMenuStrip.Items.Remove(clearSelectionItem);
            Unregister();
        }

        private void Register()
        {
            form.MouseDown += OnMouseDown;
            form.MouseMove += OnMouseMove;
            form.MouseUp += OnMouseUp;
            form.Paint += OnPaintForm;
            form.FormClosing += OnFormClosing;
        }

        private void Unregister()
        {
            form.MouseDown -= OnMouseDown;
            form.MouseMove -= OnMouseMove;
            form.MouseUp -= OnMouseUp;
            form.Paint -= OnPaintForm;
            form.FormClosing -= OnFormClosing;
        }

        private void OnPaintForm(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LightBlue, GetRect());
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            ((Control)sender).Cursor = Cursors.Cross;
            if(selectedControlCount > 0)
            {
                selectionHandler?.Invoke(false, this);
                DeselectControls();
                selectedControlCount = 0;
            }
            mouseStartPos = e.Location;
            mouseEndPos = e.Location;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            DrawOutlineOnSelection();
            if (e.Button != MouseButtons.Left) return;

            mouseEndPos = e.Location;
            form.Refresh();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            SelectControls();
            DrawOutlineOnSelection();
            if (selectedControls.Count != selectedControlCount)
            {
                selectionHandler?.Invoke(true, this);
            }
            selectedControlCount = selectedControls.Count;
            mouseStartPos = new Point(0, 0);
            mouseEndPos = new Point(0, 0);
            form.Refresh();
            ((Control)sender).Cursor = Cursors.Default;
        }

        private void DrawOutlineOnSelection()
        {
            foreach (Control item in selectedControls)
            {
                var graphichs = item.CreateGraphics();
                graphichs.DrawRectangle(outlinePen, item.ClientRectangle);
            }
        }

        private void SelectControls()
        {
            var rect = GetRect();
            for (int i = 0; i < form.Controls.Count; i++)
            {
                if (rect.IntersectsWith(form.Controls[i].Bounds))
                {
                    //form.Controls[i].BackColor = Color.Red;
                    var control = form.Controls[i];
                    control.MouseMove += OnMouseMovedOverControl;
                    selectedControls.Add(control);
                }
            }
            form.Focus();
        }

        private void DeselectControls()
        {
            for (int i = 0; i < selectedControls.Count; i++)
            {
                selectedControls[i].MouseMove -= OnMouseMovedOverControl;
                selectedControls[i].Invalidate();
            }
            form.Validate();
            selectedControls.Clear();
        }

        private void OnMouseMovedOverControl(object sender, MouseEventArgs e)
        {
            DrawOutlineOnSelection();
        }

        private Rectangle GetRect()
        {
            r.X = Math.Min(mouseStartPos.X, mouseEndPos.X);
            r.Y = Math.Min(mouseStartPos.Y, mouseEndPos.Y);
            r.Width = Math.Abs(mouseStartPos.X - mouseEndPos.X);
            r.Height = Math.Abs(mouseStartPos.Y - mouseEndPos.Y);

            return r;
        }

    }
}
