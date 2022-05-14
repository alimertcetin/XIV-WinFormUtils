using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XIV.Utils
{
    /// <summary>
    /// Utilities for <see cref="FlowLayoutPanel"/> class
    /// </summary>
    public static class FlpUtils
    {
        /// <summary>
        /// Gets values of <typeparamref name="TEnum"/> and fills <paramref name="panel"/> 
        /// with <typeparamref name="TButtonBase"/>
        /// </summary>
        /// <typeparam name="TEnum">Enum to get texts of <typeparamref name="TButtonBase"/></typeparam>
        /// <typeparam name="TButtonBase">Type to fill <paramref name="panel"/></typeparam>
        /// <param name = "panel" ><see cref="FlowLayoutPanel"/> to fill</param>
        public static void FillWithEnum<TEnum, TButtonBase>(FlowLayoutPanel panel)
            where TEnum : Enum
            where TButtonBase : ButtonBase
        {
            panel.Controls.Clear();
            Array values = EnumUtils.GetValues<TEnum>();
            IList list = values;
            for (int i = 0; i < list.Count; i++)
            {
                object item = list[i];
                var control = (ButtonBase)Activator.CreateInstance(typeof(TButtonBase));
                control.Name = item.ToString();
                control.Text = item.ToString();
                panel.Controls.Add(control);
            }
        }

        /// <summary>
        /// Gets values from <paramref name="list"/> and fills <paramref name="panel"/> 
        /// with giving <typeparamref name="TButtonBase"/>
        /// </summary>
        /// <typeparam name="TButtonBase">Type to fill <paramref name="panel"/></typeparam>
        /// <param name="panel"><see cref="FlowLayoutPanel"/> to fill</param>
        /// <param name="list">List that contains values</param>
        public static void FillWithEnumList<TEnum, TButtonBase>(FlowLayoutPanel panel, IList<TEnum> list)
            where TEnum : Enum
            where TButtonBase : ButtonBase
        {
            panel.Controls.Clear();
            foreach (TEnum item in list)
            {
                var control = (ButtonBase)Activator.CreateInstance(typeof(TButtonBase));
                control.Name = item.ToString();
                control.Text = item.ToString();
                panel.Controls.Add(control);
            }
        }

        public static void FillWithObject<TButtonBase, T>(FlowLayoutPanel panel, IList<T> list)
            where TButtonBase : ButtonBase
        {
            panel.Controls.Clear();
            foreach (T item in list)
            {
                var control = (ButtonBase)Activator.CreateInstance(typeof(TButtonBase));
                control.Name = item.ToString();
                control.Text = item.ToString();
                panel.Controls.Add(control);
            }
        }

        /// <summary>
        /// Loopes through all <see cref="RadioButton"/>s of <paramref name="panel"/> and returns true if there is a checked one,
        /// otherwise returns false
        /// </summary>
        /// <param name="panel"><see cref="FlowLayoutPanel"/> to check</param>
        /// <param name="selected"><see cref="null"/> if there is no selected</param>
        /// <returns>True if there is a selected <see cref="RadioButton"/>, otherwise false</returns>
        public static bool GetSelectedRadio(FlowLayoutPanel panel, out RadioButton selected)
        {
            selected = null;
            foreach (RadioButton rb in panel.Controls)
            {
                if (rb.Checked)
                {
                    selected = rb;
                    return true;
                }
            }

            return false;
        }

        public static void SetSelectedRadio(FlowLayoutPanel panel, string radioText)
        {
            foreach (RadioButton rb in panel.Controls)
            {
                if(rb.Text == radioText)
                {
                    rb.Checked = true;
                }
            }
        }

        /// <summary>
        /// Returns true if there is at least one selected <see cref="CheckBox"/>
        /// otherwise returns false
        /// </summary>
        /// <param name="panel"><see cref="FlowLayoutPanel"/> to check</param>
        /// <param name="selectedCb"></param>
        /// <returns>True if there is a selected <see cref="CheckBox"/>, otherwise false</returns>
        public static bool GetSelectedCheckBoxes(FlowLayoutPanel panel, out List<CheckBox> selectedCb)
        {
            selectedCb = null;
            foreach (CheckBox cb in panel.Controls)
            {
                if (cb.Checked)
                {
                    if(selectedCb == null) selectedCb = new List<CheckBox>();

                    selectedCb.Add(cb);
                }
            }

            return selectedCb != null && selectedCb.Count > 0;
        }
    }

}