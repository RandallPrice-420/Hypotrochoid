using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiRadar2
{
    public partial class RPSciFiRadar2 : UserControl, IRPSciFiControl
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
        public RPSciFiControlType ControlType => RPSciFiControlType.Radar2;


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



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   ContactColor
        //   GlowColor
        //   GridColor
        //   SweepColor
        //   SweepSpeed
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Browsable(true), Description("The color of the radar contacts.")]
        public Color ContactColor { get; set; } = Color.Cyan;


        [Category("Sci-Fi"), Browsable(true), Description("The color of the radar glow.")]
        public Color GlowColor { get; set; } = Color.Cyan;


        [Category("Sci-Fi"), Browsable(true), Description("The color of the radar grid.")]
        public Color GridColor { get; set; } = Color.FromArgb(40, 255, 255, 255);


        [Category("Sci-Fi"), Browsable(true), Description("The color of the radar sweep.")]
        public Color SweepColor { get; set; } = Color.Lime;


        [Category("Sci-Fi"), Browsable(true), Description("The speed of the radar sweep in degrees per second.")]
        public float SweepSpeed { get; set; } = 45f;      //180f;      // degrees per second

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _contacts
        //   _anim
        //   _rnd
        //   _ringPen
        //   _sweepPen
        //   _sw
        //   _sweepAngle
        //   _timer
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private List<RadarContact> _contacts = new List<RadarContact>();

        private readonly Timer     _anim;
        private readonly Random    _rnd = new();
        private readonly Pen       _ringPen;
        private readonly Pen       _sweepPen;
        private readonly Stopwatch _sw = Stopwatch.StartNew();

        private float _sweepAngle;

        #endregion



        // --------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiRadar2()
        // --------------------------------------------------------------------

        #region .  RPSciFiRadar2()  .
        // --------------------------------------------------------------------
        //   Method.......:  RPSciFiRadar2()
        //   Description..:  The constructor for the RPSciFiRadar2 class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        public RPSciFiRadar2()
        {
            DoubleBuffered = true;

            _sweepPen = new Pen(Color.FromArgb(200, 0, 255, 255), 3);
            _ringPen  = new Pen(Color.FromArgb( 80, 0, 255, 255), 2);

            _anim = new Timer { Interval = 1 };
            _anim.Tick += (s, e) =>
            {
                float t = _sw.ElapsedMilliseconds / 1000f;
                _sweepAngle = (t * SweepSpeed) % 360f;

                //AddRandomContact();

                Invalidate();
            };

            _anim.Start();

        }   // RPSciFiRadar2()
        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   AddRandomContact()
        // ---------------------------------------------------------------------

        #region .  AddRandomContact()  .
        // ---------------------------------------------------------------------
        //   Method.......:  AddRandomContact()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void AddRandomContact()
        {
            var c = new RadarContact
            {
                Angle    = (float)(_rnd.NextDouble() * 360),
                Distance = (float)(_rnd.NextDouble()),
                Id       = Guid.NewGuid().ToString()
            };
            _contacts.Add(c);

            _controlBus?.Publish(ControlId, ControlType, "ContactAdded", c.Id);

        }   // AddRandomContact()
        #endregion



        // --------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // --------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  The constructor for the OnPaint() event.
        //   Parameters...:  e - the event arguments
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

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

            float cx = Width  / 2f;
            float cy = Height / 2f;
            float r  = Math.Min(cx, cy) - 10;

            // Rings.
            for (int i = 1; i <= 3; i++)
            {
                g.DrawEllipse
                (
                    _ringPen,
                    cx - r  * i / 3,
                    cy - r  * i / 3,
                    (r * 2) * i / 3,
                    (r * 2) * i / 3
                );
            }

            // Grid.
            using (Pen p = new(GridColor, 1))
            {
                e.Graphics.DrawLine(p, cx - r, cy,     cx + r, cy);
                e.Graphics.DrawLine(p, cx,     cy - r, cx,     cy + r);
            }

            // Sweep line.
            float rad = _sweepAngle * (float)Math.PI / 180f;
            float x   = cx + (float)Math.Cos(rad) * r;
            float y   = cy + (float)Math.Sin(rad) * r;

            g.DrawLine(_sweepPen, cx, cy, x, y);

            // Contacts.
            foreach (var c in _contacts)
            {
                rad        = c.Angle * (float)Math.PI / 180f;
                float dist = c.Distance * r;

                PointF pos = new
                (
                    cx + (float)Math.Cos(rad) * dist,
                    cy + (float)Math.Sin(rad) * dist
                );

                using (SolidBrush b = new(ContactColor))
                {
                    e.Graphics.FillEllipse(b, pos.X - 4, pos.Y - 4, 8, 8);
                }

                // Lock-on event.
                if (Math.Abs(c.Angle - _sweepAngle) < 2f)
                {
                    _controlBus?.Publish(ControlId, ControlType, "ContactSweep", c.Id);
                }
            }

        }   // OnPaint()
        #endregion


    }   // class RPSciFiRadar2



    public class RadarContact
    {
        public float  Angle;
        public float  Distance; // 0–1
        public string Id;
    }


}	// namespace Spirograph_v1.Controls.RPSciFiRadar2
