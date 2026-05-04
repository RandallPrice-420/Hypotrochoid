using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.Knob
{
    public class KnobSciFiPlus : Control
    {
        private int      _value     = 50;
        private bool     _dragging  = false;
        private float    _glowPulse = 0;
        private readonly Timer _pulseTimer;


        public enum ColorMode
        {
            NeonCyan,
            PlasmaPurple,
            ReactorGreen,
            DangerRed
        }


        public ColorMode Mode { get; set; } = ColorMode.NeonCyan;


        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(0, Math.Min(100, value));
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public event EventHandler ValueChanged;


        public KnobSciFiPlus()
        {
            DoubleBuffered = true;
            ResizeRedraw   = true;
            MinimumSize    = new Size(100, 100);

            _pulseTimer = new Timer
            {
                Interval = 30
            };

            _pulseTimer.Tick += (s, e) =>
            {
                _glowPulse += 0.05f;

                if (_glowPulse > Math.PI * 2)
                {
                    _glowPulse = 0;
                }
                Invalidate();
            };
            _pulseTimer.Start();

            MouseEnter += (s, e) => { _hover = true;  Invalidate(); };
            MouseLeave += (s, e) => { _hover = false; Invalidate(); };

        }


        private bool _hover = false;


        private Color GetGlowColor() => Mode switch
        {
            ColorMode.NeonCyan     => Color.Cyan,
            ColorMode.PlasmaPurple => Color.MediumPurple,
            ColorMode.ReactorGreen => Color.Lime,
            ColorMode.DangerRed    => Color.Red,
                                 _ => Color.Cyan

        };  // GetGlowColor()


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g      = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int size = Math.Min(Width, Height);
            int cx   = Width  / 2;
            int cy   = Height / 2;

            Rectangle rect = new(cx - size / 2 + 5,
                                          cy - size / 2 + 5,
                                          size - 10,
                                          size - 10);

            Color glow = GetGlowColor();

            // --- Pulsing Glow ---
            float pulse      = (float)(Math.Sin(_glowPulse) * 20 + 40);
            Color pulseColor = Color.FromArgb((int)pulse, glow);

            // --- Hologram Drop Shadow ---
            using (SolidBrush shadow = new(Color.FromArgb(60, glow)))
            {
                g.FillEllipse(shadow, rect.X + 6, rect.Y + 6, rect.Width, rect.Height);
            }

            // --- Outer Glow Ring ---
            using (GraphicsPath gp = new())
            {
                gp.AddEllipse(rect);

                using PathGradientBrush pgb = new(gp);
                pgb.CenterColor = pulseColor;
                pgb.SurroundColors = [Color.FromArgb(0, glow)];

                g.FillEllipse(pgb, rect);
            }

            // --- Carbon Fiber Texture ---
            using (TextureBrush tb = new(CreateCarbonFiberTexture()))
            {
                g.FillEllipse(tb, rect);
            }

            // --- Metallic Rim ---
            using (Pen rim = new(Color.FromArgb(120, 200, 255), 4))
            {
                g.DrawEllipse(rim, rect);
            }

            // --- Indicator Arc ---
            float sweep = (float)(_value / 100.0 * 270);

            using (Pen arc = new(glow, _hover ? 8 : 6))
            {
                arc.StartCap = LineCap.Round;
                arc.EndCap   = LineCap.Round;

                g.DrawArc(arc, rect, -135, sweep);
            }

            // --- Center Glow ---
            Rectangle inner = new(rect.X + 20,
                                           rect.Y + 20,
                                           rect.Width  - 40,
                                           rect.Height - 40);

            using (GraphicsPath gp = new())
            {
                gp.AddEllipse(inner);

                using PathGradientBrush pgb = new(gp);
                pgb.CenterColor = Color.FromArgb(150, glow);
                pgb.SurroundColors = [Color.FromArgb(10, glow)];
                g.FillEllipse(pgb, inner);
            }

            // --- Value Text ---
            using (StringFormat sf = new()
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })

            using (Brush tb = new SolidBrush(Color.White))
            {
                using Font f = new("Segoe UI", 14, FontStyle.Bold);
                g.DrawString(_value.ToString(), f, tb, new PointF(cx, cy), sf);
            }

        }   // OnPaint()


        private static Bitmap CreateCarbonFiberTexture()
        {
            Bitmap bmp = new(8, 8);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(25, 25, 30));

                using Pen p = new(Color.FromArgb(40, 40, 50), 2);
                g.DrawLine(p, 0, 0, 8, 8);
                g.DrawLine(p, 0, 8, 8, 0);
            }

            return bmp;

        }   // CreateCarbonFiberTexture()


        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging = true;

            UpdateValueFromMouse(e.Location);
            PlayTick();

        }   // OnMouseDown()


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                UpdateValueFromMouse(e.Location);
                PlayTick();
            }

        }   // OnMouseMove()


        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;

        }   // OnMouseUp()


        private static void PlayTick()
        {
            SystemSounds.Asterisk.Play();

        }   // PlayTick()


        private void UpdateValueFromMouse(Point p)
        {
            int    cx         = Width  / 2;
            int    cy         = Height / 2;
            double dx         = p.X - cx;
            double dy         = p.Y - cy;
            double angle      = Math.Atan2(dy, dx) * 180 / Math.PI + 180;
            double normalized = (angle + 135 + 360) % 360;

            if (normalized <= 270)
            {
                Value = (int)(normalized / 270 * 100);
            }

        }   // UpdateValueFromMouse()


    }   // class KnobSciFiPlus

}   // namespace Spirograph_v1.Controls.Custom
