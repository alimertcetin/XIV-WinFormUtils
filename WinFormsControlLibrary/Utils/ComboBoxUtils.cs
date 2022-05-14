using System;
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
        public static void FillComboBox_WithEnum<T>(ComboBox comboBox) where T : Enum
        {
            comboBox.Items.Clear();
            Array values = EnumUtils.GetValues<T>();
            foreach (object item in values)
            {
                comboBox.Items.Add(item.ToString());
            }
            if (comboBox.SelectedIndex == -1 && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Clears the <paramref name="comboBox"/> and fills with giving <paramref name="itemList"/>
        /// </summary>
        /// <param name="comboBox"><see cref="ComboBox"/> to refresh</param>
        /// <param name="itemList">Values for filling the <paramref name="comboBox"/></param>
        public static void RefreshComboBox(ComboBox comboBox, List<object> itemList)
        {
            comboBox.Items.Clear();
            foreach (object item in itemList)
            {
                comboBox.Items.Add(item);
            }
            if (comboBox.SelectedIndex == -1 && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
    }

}