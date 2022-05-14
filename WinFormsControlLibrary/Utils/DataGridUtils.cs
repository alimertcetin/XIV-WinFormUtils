using System.Windows.Forms;

namespace XIV.Utils
{
    /// <summary>
    /// Utilities for <see cref="DataGridView"/> class
    /// </summary>
    public static class DataGridUtils
    {
        /// <summary>
        /// Creates a new column with giving <paramref name="DataPropertyName"/> and <paramref name="Header"/> 
        /// and returns it
        /// </summary>
        /// <param name="DataPropertyName"></param>
        /// <param name="Header"></param>
        /// <returns>A new <see cref="DataGridViewColumn"/></returns>
        public static DataGridViewColumn CreateColumn(string DataPropertyName, string Header)
        {
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.Name = DataPropertyName;
            column.DataPropertyName = DataPropertyName;
            column.HeaderText = Header;
            return column;
        }
    }

}