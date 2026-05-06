using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiToggleSwitch
{
    public partial class RPSciFiToggleSwitch : UserControl
    {
        public bool IsOn { get; private set; }
        public event EventHandler Toggled;

        public Color OnColor { get; set; } = Color.Lime;
        public Color OffColor { get; set; } = Color.Red;

        public RPSciFiToggleSwitch()
        {
            DoubleBuffered = true;
            Size = new Size(60, 26);
            Cursor = Cursors.Hand;
        }

        protected override void OnClick(EventArgs e)
        {
            IsOn = !IsOn;
            Invalidate();
            Toggled?.Invoke(this, EventArgs.Empty);
            base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = rect.Height;

            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddArc(rect.Left, rect.Top, radius, radius, 90, 180);
                gp.AddArc(rect.Right - radius, rect.Top, radius, radius, 270, 180);
                gp.CloseFigure();

                Color c = IsOn ? OnColor : OffColor;
                using (LinearGradientBrush bg = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(40, c),
                    Color.FromArgb(5, 0, 0, 0),
                    0f))
                {
                    e.Graphics.FillPath(bg, gp);
                }

                using (Pen p = new Pen(Color.FromArgb(180, c), 2))
                    e.Graphics.DrawPath(p, gp);

                int knobSize = rect.Height - 4;
                int knobX = IsOn ? rect.Right - knobSize - 2 : rect.Left + 2;
                Rectangle knob = new Rectangle(knobX, rect.Top + 2, knobSize, knobSize);

                using (SolidBrush b = new SolidBrush(Color.FromArgb(220, Color.White)))
                    e.Graphics.FillEllipse(b, knob);

                using (Pen p = new Pen(Color.FromArgb(200, c), 2))
                    e.Graphics.DrawEllipse(p, knob);
            }
        }


    }

}   // namespace Spirograph_v1.Controls.RPSciFiToggleSwitch
