using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiSlider
{
    public partial class RPSciFiSlider : Control
    {
        private int _value;
        public int Minimum { get; set; } = 0;
        public int Maximum { get; set; } = 100;


        public int Value
        {
            get => _value;
            set
            {
                int v = Math.Max(Minimum, Math.Min(Maximum, value));
                if (_value != v)
                {
                    _value = v;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        public event EventHandler ValueChanged;

        public Color GlowColor { get; set; } = Color.Cyan;

        private bool _dragging;


        public RPSciFiSlider()
        {
            DoubleBuffered = true;
            Height = 32;
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging = true;
            SetValueFromX(e.X);
            base.OnMouseDown(e);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
                SetValueFromX(e.X);
            base.OnMouseMove(e);
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;
            base.OnMouseUp(e);
        }


        private void SetValueFromX(int x)
        {
            Rectangle track = GetTrackRect();
            float t = (float)(x - track.Left) / Math.Max(1, track.Width);
            Value = Minimum + (int)(t * (Maximum - Minimum));
        }


        private Rectangle GetTrackRect()
        {
            return new Rectangle(10, Height / 2 - 4, Width - 20, 8);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle track = GetTrackRect();

            using (LinearGradientBrush bg = new(
                track,
                Color.FromArgb(40, 40, 60),
                Color.FromArgb(10, 10, 20),
                0f))
            {
                e.Graphics.FillRectangle(bg, track);
            }

            using (Pen p = new(Color.FromArgb(120, GlowColor), 2))
                e.Graphics.DrawRectangle(p, track);

            float t = (float)(Value - Minimum) / Math.Max(1, (Maximum - Minimum));
            int knobX = track.Left + (int)(t * track.Width);

            Rectangle knob = new(knobX - 8, track.Top - 6, 16, track.Height + 12);

            using GraphicsPath gp = new();
            gp.AddEllipse(knob);
            using (PathGradientBrush pgb = new(gp))
            {
                pgb.CenterColor = Color.FromArgb(180, GlowColor);
                pgb.SurroundColors = [Color.FromArgb(0, GlowColor)];
                e.Graphics.FillEllipse(pgb, knob);
            }
            using (Pen p = new(Color.FromArgb(220, GlowColor), 2))
                e.Graphics.DrawEllipse(p, knob);
        }


    }   // class RPSciFiSlider

}   // namespace Spirograph_v1.Controls.RPSciFiSlider
