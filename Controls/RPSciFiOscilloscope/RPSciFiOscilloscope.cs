using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiOscilloscope
{
    public partial class RPSciFiOscilloscope : UserControl
    {
        private readonly List<float> _samples = [];
        private readonly Timer _timer;
        private float _phase;


        public Color GlowColor { get; set; } = Color.Lime;


        public RPSciFiOscilloscope()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(5, 5, 10);
            Size = new Size(300, 140);

            _timer = new Timer { Interval = 30 };
            _timer.Tick += (s, e) =>
            {
                _phase += 0.15f;
                AddSample((float)Math.Sin(_phase) * 0.7f + (float)(0.3f * Math.Sin(_phase * 3)));
            };
            _timer.Start();
        }


        private void AddSample(float v)
        {
            _samples.Add(v);

            if (_samples.Count > Width)
                _samples.RemoveAt(0);

            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            rect.Inflate(-2, -2);

            using (LinearGradientBrush bg = new(
                rect,
                Color.FromArgb(10, 40, 10),
                Color.FromArgb(5, 5, 10),
                90f))
            {
                e.Graphics.FillRectangle(bg, rect);
            }

            using (Pen grid = new(Color.FromArgb(40, 0, 255, 0), 1))
            {
                for (int x = rect.Left; x < rect.Right; x += 20)
                    e.Graphics.DrawLine(grid, x, rect.Top, x, rect.Bottom);

                for (int y = rect.Top; y < rect.Bottom; y += 20)
                    e.Graphics.DrawLine(grid, rect.Left, y, rect.Right, y);
            }

            using (Pen border = new(Color.FromArgb(160, GlowColor), 2))
            {
                e.Graphics.DrawRectangle(border, rect);
            }

            if (_samples.Count > 1)
            {
                using Pen wave = new(Color.FromArgb(220, GlowColor), 2);
                float midY = rect.Top + rect.Height / 2f;
                float scaleY = rect.Height / 2.2f;

                PointF prev = new(rect.Right, midY);
                int idx = _samples.Count - 1;

                for (int x = rect.Right; x >= rect.Left && idx >= 0; x--, idx--)
                {
                    float v = _samples[idx];

                    PointF cur = new(x, midY - v * scaleY);

                    if (x != rect.Right)
                        e.Graphics.DrawLine(wave, prev, cur);
                    prev = cur;
                }
            }
        }
    }

}