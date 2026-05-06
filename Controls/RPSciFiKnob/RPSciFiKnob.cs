using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiKnob
{
    public partial class RPSciFiKnob : UserControl
    {
        private float _angle = 225f; // 225–-45 range
        public float Minimum { get; set; } = 0f;
        public float Maximum { get; set; } = 100f;

        private float _value;
        public float Value
        {
            get => _value;
            set
            {
                float v = Math.Max(Minimum, Math.Min(Maximum, value));
                if (Math.Abs(_value - v) > 0.001f)
                {
                    _value = v;
                    _angle = 225f - (v - Minimum) / Math.Max(1f, (Maximum - Minimum)) * 270f;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ValueChanged;
        public Color GlowColor { get; set; } = Color.Cyan;

        private bool _dragging;

        public RPSciFiKnob()
        {
            DoubleBuffered = true;
            Size = new Size(60, 60);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging = true;
            Capture = true;
            UpdateFromMouse(e.Location);
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
                UpdateFromMouse(e.Location);
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;
            Capture = false;
            base.OnMouseUp(e);
        }

        private void UpdateFromMouse(Point p)
        {
            Point center = new(Width / 2, Height / 2);
            float dx = p.X - center.X;
            float dy = p.Y - center.Y;
            float ang = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);

            float mapped = ang + 180f; // 0–360
            if (mapped < 45 || mapped > 315)
                return; // dead zone

            float t = (mapped - 45f) / 270f;
            Value = Minimum + t * (Maximum - Minimum);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            using (PathGradientBrush pgb = new(
            [
            new PointF(rect.Left, rect.Top),
            new PointF(rect.Right, rect.Top),
            new PointF(rect.Right, rect.Bottom),
            new PointF(rect.Left, rect.Bottom)
        ]))
            {
                pgb.CenterColor = Color.FromArgb(40, GlowColor);
                pgb.SurroundColors = [Color.FromArgb(5, 5, 10)];
                e.Graphics.FillEllipse(pgb, rect);
            }

            using (Pen p = new(Color.FromArgb(200, GlowColor), 2))
                e.Graphics.DrawEllipse(p, rect);

            Point center = new(Width / 2, Height / 2);
            float radius = Width / 2f - 8;

            double rad = _angle * Math.PI / 180.0;
            PointF end = new(
            center.X + (float)Math.Cos(rad) * radius,
            center.Y + (float)Math.Sin(rad) * radius);

            using (Pen p = new(Color.FromArgb(230, Color.White), 3))
            {
                e.Graphics.DrawLine(p, center, end);
            }
        }
    }

}
