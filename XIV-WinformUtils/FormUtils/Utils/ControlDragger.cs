using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace XIV.Utils
{
    //TODO : Add ability to filter controls
    //TODO : Create a generic structure
    public class ControlDragger
    {
        public bool IsDragging { get; private set; }

        private IList<Control> selection = new List<Control>();
        private Point previousLocation;
        private Dictionary<Control, EventSuppressor> controlSuppressor = new Dictionary<Control, EventSuppressor>();
        private Form form;

        public ControlDragger(Form form)
        {
            this.form = form;
            form.FormClosing += OnFormClosing;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Remove(selection);
        }

        public void Add(Control control)
        {
            if (selection.Contains(control)) return;

            if (controlSuppressor.TryGetValue(control, out var suppressor))
            {
                suppressor.Suppress();
            }
            else
            {
                EventSuppressor es = new EventSuppressor(control);
                es.Suppress();
                controlSuppressor.Add(control, es);
            }

            control.MouseDown += OnMouseDown;
            control.MouseMove += OnMouseMove;
            control.MouseUp += OnMouseUp;
            selection.Add(control);
        }

        public void Add(IList<Control> controls)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                Add(controls[i]);
            }
        }

        public void Remove(Control control)
        {
            if (!selection.Contains(control)) return;

            control.MouseDown -= OnMouseDown;
            control.MouseMove -= OnMouseMove;
            control.MouseUp -= OnMouseUp;

            if (controlSuppressor.TryGetValue(control, out var suppressor))
            {
                suppressor.Resume();
            }
            selection.Remove(control);
        }

        public void Remove(IList<Control> controls)
        {
            for (int i = controls.Count - 1; i >= 0; i--)
            {
                Remove(controls[i]);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.previousLocation = e.Location;
            IsDragging = true;
            ((Control)sender).Cursor = Cursors.Cross;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDragging) return;

            foreach (var selectedControl in selection)
            {
                var location = selectedControl.Location;
                location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
                selectedControl.Location = location;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
            ((Control)sender).Cursor = Cursors.Default;
            form.Focus();
        }

    }

}