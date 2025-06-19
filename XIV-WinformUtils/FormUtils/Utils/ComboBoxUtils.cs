using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XIV.Utils
{
    // TODO : Maybe we can create ListControlUtils class

    /// <summary>
    /// Utilities for <see cref="ComboBox"/> class
    /// </summary>
    public static class ComboBoxUtils
    {
        /// <summary>
        /// Clears the <paramref name="comboBox"/> and fills with giving <typeparamref name="T"/> values where T is an Enum
        /// </summary>
        /// <typeparam name="T">The enum to get values</typeparam>
        /// <param name="comboBox"><see cref="ComboBox"/> to fill</param>
        public static void FillComboBox_WithEnum<T>(ComboBox cmb) where T : Enum
        {
            cmb.Items.Clear();
            Array values = EnumUtils.GetValues<T>();
            foreach (object item in values)
            {
                cmb.Items.Add(item.ToString());
            }
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }

        /// <summary>
        /// Clears the <paramref name="comboBox"/> and fills with giving <paramref name="itemList"/>
        /// </summary>
        /// <param name="comboBox"><see cref="ComboBox"/> to refresh</param>
        /// <param name="itemList">Values for filling the <paramref name="comboBox"/></param>
        public static void RefreshComboBox(ComboBox cmb, IList itemList)
        {
            cmb.Items.Clear();
            foreach (var item in itemList)
            {
                cmb.Items.Add(item);
            }
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }

        public static void RefreshComboBox<T>(ComboBox cmb, IList<T> itemList)
        {
            cmb.Items.Clear();
            foreach (var item in itemList)
            {
                cmb.Items.Add(item);
            }
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }

        public static void BindData<T>(ComboBox cmb, IEnumerable<T> enumerable, Func<T, string> bindFunc)
        {
            cmb.Items.Clear();

            foreach (var item in enumerable)
            {
                cmb.Items.Add(bindFunc(item));
            }

            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }

        public static void BindIf<T>(ComboBox cmb, IEnumerable<T> enumerable, Func<T, bool> ifFunc, Func<T, string> bindFunc)
        {
            cmb.Items.Clear();

            foreach (var item in enumerable)
            {
                if (ifFunc(item)) cmb.Items.Add(bindFunc(item));
            }

            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }

        public static void SetSelectedIndex<T>(ComboBox cmb, T value, Func<T, string> bindFunc)
        {
            if (cmb.Items.Count == 0) return;
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (cmb.Items[i].ToString() == bindFunc(value))
                {
                    cmb.SelectedIndex = i;
                    return;
                }
            }
            cmb.SelectedIndex = -1; // Not found
        }
    }

    public static class ComboBoxExtensions
    {
        public static void BindData<T>(this ComboBox cmb, IEnumerable<T> enumerable, Func<T, string> bindFunc)
        {
            ComboBoxUtils.BindData(cmb, enumerable, bindFunc);
        }

        public static void BindEnum<T>(this ComboBox cmb) where T : Enum
        {
            ComboBoxUtils.FillComboBox_WithEnum<T>(cmb);
        }

        public static void BindIf<T>(this ComboBox cmb, IEnumerable<T> enumerable, Func<T, bool> ifFunc, Func<T, string> bindFunc)
        {
            ComboBoxUtils.BindIf(cmb, enumerable, ifFunc, bindFunc);
        }

        public static void SetSelectedIndex<T>(this ComboBox cmb, T value, Func<T, string> bindFunc)
        {
            ComboBoxUtils.SetSelectedIndex(cmb, value, bindFunc);
        }
    }

    public static class ListBoxExtensions
    {
        public static void BindData<T>(this ListBox lb, IEnumerable<T> enumerable, Func<T, string> bindFunc)
        {
            lb.Items.Clear();
            foreach (var item in enumerable)
            {
                lb.Items.Add(bindFunc(item));
            }
            if (lb.Items.Count > 0) lb.SelectedIndex = 0;
        }
    }

}