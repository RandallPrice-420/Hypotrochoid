using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiGuage
{
    public partial class RPSciFiGuage : UserControl
    {
        public float Minimum { get; set; } = 0f;
        public float Maximum { get; set; } = 100f;


        private float _value = 50f;
        public float Value
        {
            get => _value;
            set
            {
                float v = Math.Max(Minimum, Math.Min(Maximum, value));
                if (Math.Abs(_value - v) > 0.01f)
                {
                    _value = v;
                    Invalidate();
                }
            }
        }


        public string LabelText { get; set; } = "POWER";
        public Color GlowColor { get; set; } = Color.Cyan;


        public RPSciFiGuage()
        {
            DoubleBuffered = true;
            Size = new Size(160, 160);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect   = new(0, 0, Width - 1, Height - 1);
            Point     center = new(Width / 2, Height / 2 + 20);
            float     radius = Math.Min(Width, Height) / 2f - 10;

            using (PathGradientBrush pgb = new(
            [
            new PointF(rect.Left,  rect.Top),
            new PointF(rect.Right, rect.Top),
            new PointF(rect.Right, rect.Bottom),
            new PointF(rect.Left,  rect.Bottom)
        ]))
            {
                pgb.CenterColor = Color.FromArgb(40, GlowColor);
                pgb.SurroundColors = [Color.FromArgb(5, 5, 10)];
                e.Graphics.FillEllipse(pgb, rect);
            }

            using (Pen p = new(Color.FromArgb(200, GlowColor), 2))
                e.Graphics.DrawEllipse(p, rect);

            // ticks
            int tickCount = 11;
            for (int i = 0; i < tickCount; i++)
            {
                float  t     = i / (float)(tickCount - 1);
                float  angle = 210f + t * 120f;
                double rad   = angle * Math.PI / 180.0;

                PointF inner = new(
                center.X + (float)Math.Cos(rad) * (radius - 15),
                center.Y + (float)Math.Sin(rad) * (radius - 15));

                PointF outer = new(
                center.X + (float)Math.Cos(rad) * (radius - 2),
                center.Y + (float)Math.Sin(rad) * (radius - 2));

                using Pen p = new(Color.FromArgb(160, Color.White), 1);
                e.Graphics.DrawLine(p, inner, outer);
            }

            // needle
            float  nt     = (Value - Minimum) / Math.Max(1f, (Maximum - Minimum));
            float  nAngle = 210f + nt * 120f;
            double nRad   = nAngle * Math.PI / 180.0;

            PointF nEnd = new(
            center.X + (float)Math.Cos(nRad) * (radius - 20),
            center.Y + (float)Math.Sin(nRad) * (radius - 20));

            using (Pen p = new(Color.FromArgb(230, Color.Red), 3))
            {
                e.Graphics.DrawLine(p, center, nEnd);
            }

            using (SolidBrush b = new(Color.FromArgb(220, Color.Black)))
                e.Graphics.FillEllipse(b, center.X - 6, center.Y - 6, 12, 12);

            using (Pen p = new(Color.FromArgb(220, GlowColor), 2))
                e.Graphics.DrawEllipse(p, center.X - 6, center.Y - 6, 12, 12);

            // label
            using (StringFormat sf = new()
            { Alignment = StringAlignment.Center })
            using (SolidBrush b = new(Color.White))
            {
                e.Graphics.DrawString(LabelText, Font, b,
                    new RectangleF(0, 10, Width, 20), sf);
            }
        }
    }

}
