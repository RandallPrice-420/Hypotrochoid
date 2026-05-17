using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiOscilloscope
{
    public partial class RPSciFiOscilloscope : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   ValueChanged  --  Not needed for this control, but included for
        //                     consistency with other controls.
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        //public event EventHandler ValueChanged;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : Controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.Oscilloscope;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus."), Browsable(false)]
        private RPSciFiControlBus _controlBus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

            //// Publish events here.
            //ValueChanged += (s, e) =>
            //{
            //    _controlBus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            //};

        }   // Register()

        #endregion



        // --------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowColor
        //   Peaks
        //   Phase
        // --------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The color of the glow effect."), Browsable(true)]
        public Color GlowColor { get; set; } = Color.Lime;


        [Category("Sci-Fi"), Description("The number of 'peaks' in each phase of the wave."), Browsable(true)]
        public float Peaks { get; set; } = 3f;


        [Category("Sci-Fi"), Description("Distance between waves, smaller the value the wider distance."), Browsable(true)]
        public float Phase { get; set; } = 0.15f;

        #endregion



        private readonly List<float> _samples = [];
        private readonly Timer       _timer;



        public RPSciFiOscilloscope()
        {
            DoubleBuffered = true;
            Size           = new Size(300, 140);
            BackColor      = Color.FromArgb(5, 5, 10);

            _timer = new Timer { Interval = 30 };
            _timer.Tick += (s, e) =>
            {
                Phase += 0.15f;

                AddSample((float)Math.Sin(Phase) * 0.7f + (float)(0.3f * Math.Sin(Phase * Peaks)));
            };
            _timer.Start();

        }   // RPSciFiOscilloscope()



        private void AddSample(float v)
        {
            _samples.Add(v);

            if (_samples.Count > Width)
            {
                _samples.RemoveAt(0);
            }

            Invalidate();

        }   // AddSample()



        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            rect.Inflate(-2, -2);

            using (LinearGradientBrush bg = 
                new
                (
                    rect,
                    Color.FromArgb(10, 40, 10),
                    Color.FromArgb(5, 5, 10),
                    90f
                )
            )
            {
                e.Graphics.FillRectangle(bg, rect);
            }

            // Draw grid.
            using (Pen grid = new(Color.FromArgb(40, 0, 255, 0), 1))
            {
                // Vertical lines.
                for (int x = rect.Left; x < rect.Right; x += 20)
                {
                    e.Graphics.DrawLine(grid, x, rect.Top, x, rect.Bottom);
                }

                // Horizontalal lines.
                for (int y = rect.Top; y < rect.Bottom; y += 20)
                {
                    e.Graphics.DrawLine(grid, rect.Left, y, rect.Right, y);
                }
            }

            // Draw border.
            using (Pen border = new(Color.FromArgb(160, GlowColor), 2))
            {
                e.Graphics.DrawRectangle(border, rect);
            }

            // Draw wave.
            if (_samples.Count > 1)
            {
                using Pen wave = new(Color.FromArgb(220, GlowColor), 2);

                float midY   = rect.Top + rect.Height / 2f;
                float scaleY = rect.Height / 2.2f;

                PointF prev = new(rect.Right, midY);
                int    idx  = _samples.Count - 1;

                for (int x = rect.Right; x >= rect.Left && idx >= 0; x--, idx--)
                {
                    float v = _samples[idx];

                    PointF cur = new(x, midY - v * scaleY);

                    if (x != rect.Right)
                    {
                        e.Graphics.DrawLine(wave, prev, cur);
                    }

                    prev = cur;
                }
            }

            base.OnPaint(e);

        }   // OnPaint()


    }   // class RPSciFiOscilloscope

}   // namespace Spirograph_v1.Controls.RPSciFiOscilloscope
