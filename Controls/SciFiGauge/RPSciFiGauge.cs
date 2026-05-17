using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiGuage
{
    public partial class RPSciFiGauge : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : Controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.Gauge;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus."), Browsable(false)]
        private RPSciFiControlBus _controlBus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowColor : Color  - The glow color of the gauge.
        //   LabelText : string - The label text displayed on the gauge.
        //   Minimum   : float  - The minimum value of the gauge.
        //   Maximum   : float  - The maximum value of the gauge.
        //   Value     : float  - The current value of the gauge.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The color of the gauge's glow effect."), Browsable(true)]
        public Color GlowColor  { get; set; } = Color.Cyan;


        [Category("Sci-Fi"), Description("The label text of the gauge."), Browsable(true)]
        public string LabelText { get; set; } = "POWER";


        [Category("Sci-Fi"), Description("The minimum value of the gauge."), Browsable(true)]
        public float Minimum    { get; set; } = 0f;


        [Category("Sci-Fi"), Description("The maximum value of the gauge."), Browsable(true)]
        public float Maximum    { get; set; } = 100f;


        [Category("Sci-Fi"), Description("The current value of the gauge."), Browsable(true)]
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

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _value
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private float _value = 50f;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiGauge()  --  Constructor
        // ---------------------------------------------------------------------

        #region .  RPSciFiGauge()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiGauge()
        //   Description..:  The constructor for the RPSciFiGauge class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiGauge()
        {
            DoubleBuffered = true;
            BackColor      = Color.Transparent;
            Size           = new Size(160, 160);

        }   // RPSciFiGauge()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to do_something.
        //   Parameters...:  e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect   = new(0, 0, Width - 1, Height - 1);
            Point     center = new(Width / 2, Height / 2 + 20);
            float     radius = Math.Min(Width, Height) / 2f - 10;

            // Gauge Background
            using (PathGradientBrush pgb = new
            (
            [
                        new PointF(rect.Left,  rect.Top),
                        new PointF(rect.Right, rect.Top),
                        new PointF(rect.Right, rect.Bottom),
                        new PointF(rect.Left,  rect.Bottom)
                   ]
            ))
            {
                pgb.CenterColor    = Color.FromArgb(40, GlowColor);
                pgb.SurroundColors = [Color.FromArgb(5, 5, 10)];

                e.Graphics.FillEllipse(pgb, rect);
            }

            using (Pen p = new(Color.FromArgb(200, GlowColor), 2))
            {
                e.Graphics.DrawEllipse(p, rect);
            }

            // Ticks
            int tickCount = 11;

            for (int i = 0; i < tickCount; i++)
            {
                float  t     = i / (float)(tickCount - 1);
                float  angle = 210f + t * 120f;
                double rad   = angle * Math.PI / 180.0;

                PointF inner = new
                (
                    center.X + (float)Math.Cos(rad) * (radius - 15),
                    center.Y + (float)Math.Sin(rad) * (radius - 15)
                );

                PointF outer = new
                (
                    center.X + (float)Math.Cos(rad) * (radius - 2),
                    center.Y + (float)Math.Sin(rad) * (radius - 2)
                );

                using Pen p = new(Color.FromArgb(160, Color.White), 1);

                e.Graphics.DrawLine(p, inner, outer);
            }

            // Needle
            float  nt     = (Value - Minimum) / Math.Max(1f, (Maximum - Minimum));
            float  nAngle = 210f + nt * 120f;
            double nRad   = nAngle * Math.PI / 180.0;

            PointF nEnd = new
            (
                center.X + (float)Math.Cos(nRad) * (radius - 20),
                center.Y + (float)Math.Sin(nRad) * (radius - 20)
            );

            using (Pen p = new(Color.FromArgb(230, Color.Red), 3))
            {
                e.Graphics.DrawLine(p, center, nEnd);
            }

            using (SolidBrush b = new(Color.FromArgb(220, Color.Black)))
            {
                e.Graphics.FillEllipse(b, center.X - 6, center.Y - 6, 12, 12);
            }

            using (Pen p = new(Color.FromArgb(220, GlowColor), 2))
            {
                e.Graphics.DrawEllipse(p, center.X - 6, center.Y - 6, 12, 12);
            }

            // Label
            using (StringFormat sf = new()
            {
                Alignment = StringAlignment.Center
            })

            using (SolidBrush b = new(Color.White))
            {
                e.Graphics.DrawString(LabelText, Font, b, new RectangleF(0, 10, Width, 20), sf);
            }

            base.OnPaint(e);

        }   // OnPaint()
        #endregion


    }   // class RPSciFiGuage

}   // namespace Spirograph_v1.Controls.RPSciFiGuage
