using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiButton
{
    public partial class RPSciFiButton : UserControl
    {
        private float _energyOffset;
        private bool  _hover;
        private bool  _pressed;
        private float _pulsePhase;
        private float _rippleRadius;
        private Point _rippleCenter;

        private readonly Random _rnd = new();
        private readonly Timer  _energyTimer;
        private readonly Timer  _pulseTimer;
        private readonly Timer  _rippleTimer;
        private readonly Timer  _sparkTimer;
        private readonly Timer  _lightningTimer;

        private readonly List<Spark>        _sparks = [];
        private readonly List<List<PointF>> _lightningArcs = [];


        private struct Spark
        {
            public float  Life;
            public PointF Position;
            public PointF Velocity;
        }



        [Category("Custom Appearance")]
        [Description("The color of glow around an object.")]
        [Browsable(true)]
        public Color GlowColor      { get; set; } = Color.Cyan;

        [Category("Custom Appearance")]
        [Description("The color of button when it is enabled.")]
        [Browsable(true)]
        public Color BaseColor      { get; set; } = Color.FromArgb(30, 30, 50);

        [Category("Custom Appearance")]
        [Description("The color of button when it is enabled.")]
        [Browsable(true)]
        public Color DisabledColor  { get; set; } = Color.FromArgb(60, 60, 60);

        [Category("Custom Appearance")]
        [Description("The color of the button text when it is disabled.")]
        [Browsable(true)]
        public Color TextColorDisabled { get; set; }

        [Category("Custom Appearance")]
        [Description("The color of the button text when it is enabled.")]
        [Browsable(true)]
        public Color TextColorEnabled { get; set; }

        [Description("The button text."), Category("Data")]
        public string ButtonText
        {
            get => Text;
            set => Text = value;
        }

        public RPSciFiButton()
        {
            //FlatStyle = FlatStyle.Flat;
            //FlatAppearance.BorderSize = 0;
            DoubleBuffered = true;

            Font      = new Font("Segoe UI", 10, FontStyle.Bold);
            ForeColor = (Enabled) ? TextColorEnabled : TextColorDisabled;

            _pulseTimer      = new Timer { Interval = 30 };
            _pulseTimer.Tick += (s, e) => { _pulsePhase += 0.05f; Invalidate(); };
            _pulseTimer.Start();

            _rippleTimer       = new Timer { Interval = 16 };
            _rippleTimer.Tick += (s, e) =>
            {
                _rippleRadius += 4;

                if (_rippleRadius > Width * 1.5f)
                    _rippleTimer.Stop();

                Invalidate();
            };

            _energyTimer       = new Timer { Interval = 30 };
            _energyTimer.Tick += (s, e) =>
            {
                _energyOffset += 4;

                if (_energyOffset > Width * 2)
                    _energyOffset = -Width;

                Invalidate();
            };
            _energyTimer.Start();

            _sparkTimer       = new Timer { Interval = 16 };
            _sparkTimer.Tick += (s, e) =>
            {
                for (int i = _sparks.Count - 1; i >= 0; i--)
                {
                    Spark sp = _sparks[i];
                    sp.Position = new PointF(sp.Position.X + sp.Velocity.X,
                                             sp.Position.Y + sp.Velocity.Y);
                    sp.Life -= 0.04f;
                    if (sp.Life <= 0)
                        _sparks.RemoveAt(i);
                    else
                        _sparks[i] = sp;
                }
                Invalidate();
            };
            _sparkTimer.Start();

            _lightningTimer = new Timer { Interval = 60 };
            _lightningTimer.Tick += (s, e) =>
            {
                if (_hover && Enabled)
                    GenerateLightningArcs();
                else
                    _lightningArcs.Clear();

                Invalidate();
            };
            _lightningTimer.Start();
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            _hover = true;
            Invalidate();
            base.OnMouseEnter(e);
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            _hover = false;
            _pressed = false;
            _lightningArcs.Clear();
            Invalidate();
            base.OnMouseLeave(e);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Enabled)
            {
                _pressed = true;
                _rippleCenter = e.Location;
                _rippleRadius = 0;
                _rippleTimer.Start();

                EmitSparks(e.Location);

                Invalidate();
            }
            base.OnMouseDown(e);
        }


        private void EmitSparks(Point location)
        {
            for (int i = 0; i < 16; i++)
            {
                float angle = (float)(_rnd.NextDouble() * Math.PI * 2);
                float speed = 2f + (float)_rnd.NextDouble() * 3f;

                _sparks.Add(new Spark
                {
                    Position = location,
                    Velocity = new PointF((float)Math.Cos(angle) * speed,
                                          (float)Math.Sin(angle) * speed),
                    Life = 1f
                });
            }
        }


        private void GenerateLightningArcs()
        {
            _lightningArcs.Clear();

            Rectangle rect = ClientRectangle;
            rect.Inflate(-4, -4);
            PointF center = new(rect.Left + rect.Width / 2f, rect.Top + rect.Height / 2f);

            int arcCount = 3;
            for (int i = 0; i < arcCount; i++)
            {
                List<PointF> arc = [];

                int    side  = _rnd.Next(4);
                PointF start = side switch
                {
                    0 => new PointF(rect.Left, _rnd.Next(rect.Top,   rect.Bottom)),   // left
                    1 => new PointF(rect.Right, _rnd.Next(rect.Top,  rect.Bottom)),   // right
                    2 => new PointF(_rnd.Next(rect.Left, rect.Right), rect.Top),      // top
                    _ => new PointF(_rnd.Next(rect.Left, rect.Right), rect.Bottom)    // bottom
                };

                int segments = 6;
                PointF current = start;
                for (int s = 0; s < segments; s++)
                {
                    float t = (s + 1f) / segments;
                    PointF target = new(
                    Lerp(start.X, center.X, t),
                    Lerp(start.Y, center.Y, t));

                    float jitterX = (float)(_rnd.NextDouble() - 0.5) * 10f;
                    float jitterY = (float)(_rnd.NextDouble() - 0.5) * 10f;

                    PointF next = new(target.X + jitterX, target.Y + jitterY);
                    arc.Add(current);
                    current = next;
                }
                arc.Add(center);
                _lightningArcs.Add(arc);
            }
        }


        private static float Lerp(float a, float b, float t) => a + (b - a) * t;


        protected override void OnMouseUp(MouseEventArgs e)
        {
            _pressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode     = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = ClientRectangle;
            rect.Inflate(-2, -2);

            Color bg = Enabled ? BaseColor : DisabledColor;
            using (SolidBrush b = new(bg))
                e.Graphics.FillRectangle(b, rect);

            if (Enabled)
            {
                DrawHexGrid   (e.Graphics, rect);
                DrawScanlines (e.Graphics, rect);
                DrawEnergyFlow(e.Graphics, rect);
                DrawDistortion(e.Graphics, rect);

                if (_hover)
                    DrawPulseGlow(e.Graphics, rect);

                if (_pressed)
                    DrawPressedOverlay(e.Graphics, rect);

                DrawRipple   (e.Graphics);
                DrawSparks   (e.Graphics);
                DrawLightning(e.Graphics);
            }

            DrawBorder(e.Graphics, rect);

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                rect,
                Enabled ? ForeColor : Color.Gray,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }


        private void DrawLightning(Graphics g)
        {
            if (_lightningArcs.Count == 0)
                return;
            using Pen p     = new(Color.FromArgb(180, GlowColor), 2);
            using Pen outer = new(Color.FromArgb(80, Color.White), 4);
            foreach (var arc in _lightningArcs)
            {
                if (arc.Count < 2)
                    continue;
                g.DrawLines(outer, arc.ToArray());
                g.DrawLines(p, arc.ToArray());
            }
        }


        private void DrawEnergyFlow(Graphics g, Rectangle rect)
        {
            Rectangle band = new(
                (int)_energyOffset,
                rect.Top,
                rect.Width / 3,
                rect.Height);

            using LinearGradientBrush lg = new(
                band,
                Color.FromArgb(0, GlowColor),
                Color.FromArgb(120, GlowColor),
                0f);
            g.FillRectangle(lg, band);
        }


        private void DrawDistortion(Graphics g, Rectangle rect)
        {
            Bitmap bmp = new(rect.Width, rect.Height);
            using (Graphics temp = Graphics.FromImage(bmp))
            {
                temp.CopyFromScreen(PointToScreen(rect.Location), Point.Empty, rect.Size);
            }

            for (int y = 0; y < rect.Height; y++)
            {
                int offset = (int)(Math.Sin((y + _pulsePhase * 20) * 0.15f) * 3);
                g.DrawImage(bmp,
                    new Rectangle(rect.Left + offset, rect.Top + y, rect.Width, 1),
                    new Rectangle(0, y, rect.Width, 1),
                    GraphicsUnit.Pixel);
            }
        }


        private void DrawSparks(Graphics g)
        {
            foreach (var sp in _sparks)
            {
                int alpha = (int)(sp.Life * 255);
                using SolidBrush b = new(Color.FromArgb(alpha, GlowColor));
                g.FillEllipse(b, sp.Position.X - 2, sp.Position.Y - 2, 4, 4);
            }
        }


        private void DrawPulseGlow(Graphics g, Rectangle rect)
        {
            float glow = (float)(Math.Sin(_pulsePhase) * 0.5 + 0.5);
            int  alpha = (int)(120 + glow * 80);

            using Pen p = new(Color.FromArgb(alpha, GlowColor), 4);
            g.DrawRectangle(p, rect);
        }


        private void DrawPressedOverlay(Graphics g, Rectangle rect)
        {
            using SolidBrush b = new(Color.FromArgb(100, GlowColor));
            g.FillRectangle(b, rect);
        }


        private void DrawRipple(Graphics g)
        {
            if (!_rippleTimer.Enabled)
                return;

            using Pen p = new(Color.FromArgb(120, GlowColor), 3);
            g.DrawEllipse(p,
                _rippleCenter.X - _rippleRadius,
                _rippleCenter.Y - _rippleRadius,
                _rippleRadius * 2,
                _rippleRadius * 2);
        }


        private static void DrawScanlines(Graphics g, Rectangle rect)
        {
            using Pen p = new(Color.FromArgb(25, 255, 255, 255), 1);
            for (int y = rect.Top; y < rect.Bottom; y += 4)
                g.DrawLine(p, rect.Left, y, rect.Right, y);
        }


        private void DrawHexGrid(Graphics g, Rectangle rect)
        {
            int size = 12;
            using Pen p = new(Color.FromArgb(25, GlowColor), 1);
            for (int y = rect.Top; y < rect.Bottom + size; y += size)
            {
                for (int x = rect.Left; x < rect.Right + size; x += size)
                    DrawHex(g, p, x, y, size / 2);
            }
        }


        private static void DrawHex(Graphics g, Pen p, int cx, int cy, int r)
        {
            PointF[] pts = new PointF[6];
            for (int i = 0; i < 6; i++)
            {
                float angle = (float)(Math.PI / 3 * i);
                pts[i] = new PointF(
                    cx + r * (float)Math.Cos(angle),
                    cy + r * (float)Math.Sin(angle));
            }
            g.DrawPolygon(p, pts);
        }


        private void DrawBorder(Graphics g, Rectangle rect)
        {
            using Pen p = new(Color.FromArgb(200, GlowColor), 2);
            g.DrawRectangle(p, rect);
        }

    }

}