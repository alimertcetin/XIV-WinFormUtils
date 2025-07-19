namespace PyramidReservationTool.XIV_WinFormUtils.XIV_WinformUtils.XIVControls
{
    partial class XIVTextBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txt = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // txt
            // 
            txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txt.Dock = System.Windows.Forms.DockStyle.Fill;
            txt.Location = new System.Drawing.Point(5, 5);
            txt.Name = "txt";
            txt.Size = new System.Drawing.Size(240, 20);
            txt.TabIndex = 0;
            // 
            // XIVTextBox
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.Window;
            Controls.Add(txt);
            ForeColor = System.Drawing.Color.DimGray;
            Name = "XIVTextBox";
            Padding = new System.Windows.Forms.Padding(5);
            Size = new System.Drawing.Size(250, 30);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txt;
    }
}
