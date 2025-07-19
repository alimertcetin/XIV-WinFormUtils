using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PyramidReservationTool.XIV_WinFormUtils.XIV_WinformUtils.XIVControls
{
    [DefaultEvent(nameof(TextChanged))]
    public partial class XIVTextBox : UserControl
    {
        private Color colorBorder = Color.MediumSlateBlue;
        private Color colorBorderFocus = Color.HotPink;
        private Color colorBorderHover = Color.LightSlateGray;
        private Color colorBorderDisabled = Color.LightGray;
        private Color colorText = Color.White;
        private Color colorTextDisabled = Color.LightGray;
        private Color colorBackground = Color.FromArgb(30, 30, 30);
        private Color colorBackgroundDisabled = Color.FromArgb(50, 50, 50);
        private Color colorBackgroundHover = Color.FromArgb(40, 40, 40);
        private Color colorBackgroundFocus = Color.FromArgb(40, 40, 40);
        private int sizeBorder = 2;
        private int sizeUnderlined = 2;
        private bool isHovered = false;
        private bool isUnderlined = false;
        private int singleLineHeight = -1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font TextBoxFont
        {
            get => txt.Font;
            set
            {
                txt.Font = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBorder
        {
            get => colorBorder;
            set
            {
                colorBorder = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBorderFocus
        {
            get => colorBorderFocus;
            set
            {
                colorBorderFocus = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBorderHover
        {
            get => colorBorderHover;
            set
            {
                colorBorderHover = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBorderDisabled
        {
            get => colorBorderDisabled;
            set
            {
                colorBorderDisabled = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorText
        {
            get => colorText;
            set
            {
                colorText = value;
                txt.ForeColor = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorTextDisabled
        {
            get => colorTextDisabled;
            set
            {
                colorTextDisabled = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBackground
        {
            get => colorBackground;
            set
            {
                colorBackground = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBackgroundDisabled
        {
            get => colorBackgroundDisabled;
            set
            {
                colorBackgroundDisabled = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBackgroundHover
        {
            get => colorBackgroundHover;
            set
            {
                colorBackgroundHover = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBackgroundFocus
        {
            get => colorBackgroundFocus;
            set
            {
                colorBackgroundFocus = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int SizeBorder
        {
            get => sizeBorder;
            set
            {
                sizeBorder = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int SizeUnderlined
        {
            get => sizeUnderlined;
            set
            {
                sizeUnderlined = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsHovered
        {
            get => isHovered;
            set
            {
                isHovered = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsUnderlined
        {
            get => isUnderlined;
            set
            {
                isUnderlined = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsMultiline
        {
            get => txt.Multiline;
            set
            {
                txt.Multiline = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TextBoxText
        {
            get => txt.Text;
            set
            {
                txt.Text = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PlaceholderText
        {
            get => txt.PlaceholderText;
            set
            {
                txt.PlaceholderText = value;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                if (!txt.Multiline && singleLineHeight > 0)
                    return new Size(base.MinimumSize.Width, singleLineHeight);
                return base.MinimumSize;
            }
            set => base.MinimumSize = value;
        }

        public override Size MaximumSize
        {
            get
            {
                if (!txt.Multiline && singleLineHeight > 0)
                    return new Size(base.MaximumSize.Width, singleLineHeight);
                return base.MaximumSize;
            }
            set => base.MaximumSize = value;
        }

        public new event EventHandler TextChanged
        {
            add => txt.TextChanged += value;
            remove => txt.TextChanged -= value;
        }

        public XIVTextBox()
        {
            InitializeComponent();
            InitializeTextBox();
            txt.GotFocus += Txt_GotFocus;
            txt.LostFocus += Txt_LostFocus;
            txt.MouseHover += Txt_MouseHover;
            txt.MouseLeave += Txt_MouseLeave;
        }

        private void InitializeTextBox()
        {
            this.BackColor = colorBackground;
            txt.BackColor = colorBackground;
            txt.ForeColor = colorText;
            this.Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (txt.Multiline == false)
            {
                // Calculate and store the fixed height for single-line mode
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 2;
                singleLineHeight = txtHeight + (sizeBorder * 2) + this.Padding.Top + this.Padding.Bottom;
                this.Height = singleLineHeight;
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (txt.Multiline == false && singleLineHeight > 0)
            {
                // Always use the fixed height for single-line mode
                base.SetBoundsCore(x, y, width, singleLineHeight, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
        }

        private void Txt_GotFocus(object sender, EventArgs e)
        {
            this.BackColor = colorBackgroundFocus;
            txt.BackColor = colorBackgroundFocus;
            this.Invalidate();
        }

        private void Txt_LostFocus(object sender, EventArgs e)
        {
            this.BackColor = colorBackground;
            txt.BackColor = colorBackground;
            this.Invalidate();
        }

        private void Txt_MouseHover(object sender, EventArgs e)
        {
            isHovered = true;
            this.BackColor = colorBackgroundHover;
            txt.BackColor = colorBackgroundHover;
            this.Invalidate();
        }

        private void Txt_MouseLeave(object sender, EventArgs e)
        {
            isHovered = false;
            this.BackColor = colorBackground;
            txt.BackColor = colorBackground;
            this.Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (this.Enabled == false)
            {
                this.BackColor = colorBackgroundDisabled;
                txt.BackColor = colorBackgroundDisabled;
                txt.ForeColor = colorTextDisabled;
            }
            else
            {
                this.BackColor = colorBackground;
                txt.BackColor = colorBackground;
                txt.ForeColor = colorText;
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isUnderlined)
            {
                using (var underlinePen = new Pen(GetBorderColor(), sizeUnderlined))
                {
                    underlinePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    e.Graphics.DrawLine(underlinePen, 0, this.Height - sizeUnderlined + 1, this.Width, this.Height - sizeUnderlined + 1);
                }
            }
            else
            {
                using (var borderPen = new Pen(GetBorderColor(), sizeBorder))
                {
                    borderPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - sizeBorder + 1, this.Height - sizeBorder + 1);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AlignTextBox();
        }

        private void AlignTextBox()
        {
            if (txt.Multiline)
            {
                txt.Location = new Point(this.Padding.Left + sizeBorder, this.Padding.Top + sizeBorder);
                txt.Size = new Size(
                    this.Width - this.Padding.Left - this.Padding.Right - (sizeBorder * 2),
                    this.Height - this.Padding.Top - this.Padding.Bottom - (sizeBorder * 2)
                );
            }
            else
            {
                // Calculate the ideal height for a single line of text
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height;
                txt.Height = txtHeight + 2; // +2 for padding
                txt.Location = new Point(
                    this.Padding.Left + sizeBorder,
                    (this.Height - txt.Height) / 2 // Vertically center
                );
                txt.Width = this.Width - this.Padding.Left - this.Padding.Right - (sizeBorder * 2);
            }
        }

        private Color GetBorderColor()
        {
            return this.Enabled ? (txt.Focused ? colorBorderFocus : (isHovered ? colorBorderHover : colorBorder)) : colorBorderDisabled;
        }
    }
}
