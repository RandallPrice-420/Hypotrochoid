using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiRadar
{
    public partial class RPSciFiRadar : UserControl, IRPSciFiControl
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
        public RPSciFiControlType ControlType => RPSciFiControlType.Radar;


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
        //   _rnd
        //   _sweepAngle
        //   _timer
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private List<RadarContact> _contacts = new List<RadarContact>();
        private readonly Random    _rnd      = new();
        private float              _sweepAngle;
        private readonly Timer     _timer;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor(s):
        // ----------------------
        //   RPSciFiRadar()
        // ---------------------------------------------------------------------

        #region .  RPSciFiRadar()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiRadar()
        //   Description..:  The constructor for the RPSciFiRadar class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiRadar()
        {
            DoubleBuffered = true;
            Size           = new Size(260, 260);

            _timer = new Timer { Interval = 1 };
            _timer.Tick += (s, e) =>
            {
                _sweepAngle += 2f;

                if (_sweepAngle >= 360f)
                {
                    _sweepAngle = 0f;
                    AddRandomContact();
                };

                Invalidate();
            };

            //AddRandomContact();

            _timer.Start();

        }   // RPSciFiRadar()
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
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect1 = new(0, 0, Width - 1, Height - 1);

            using (PathGradientBrush pgb = new
            (
            [
                    new PointF(rect1.Left,  rect1.Top),
                    new PointF(rect1.Right, rect1.Top),
                    new PointF(rect1.Right, rect1.Bottom),
                    new PointF(rect1.Left,  rect1.Bottom)
                   ]
            ))
            {
                pgb.CenterColor = Color.FromArgb(40, GlowColor);
                pgb.SurroundColors = [Color.FromArgb(5, 5, 10)];
                e.Graphics.FillEllipse(pgb, rect1);
            }

            Rectangle rect2  = ClientRectangle;
            int       r      = Math.Min(rect2.Width, rect2.Height) / 2 - 10;
            Point     center = new(rect2.Width / 2, rect2.Height / 2);

            // Grid.
            using (Pen p = new(GridColor, 1))
            {
                e.Graphics.DrawEllipse(p, center.X - r,     center.Y - r,     r * 2,        r * 2);
                e.Graphics.DrawEllipse(p, center.X - r / 2, center.Y - r / 2, r,            r);
                e.Graphics.DrawLine   (p, center.X - r,     center.Y,         center.X + r, center.Y);
                e.Graphics.DrawLine   (p, center.X,         center.Y - r,     center.X,     center.Y + r);
            }

            // Sweep.
            using (Pen p = new Pen(Color.FromArgb(180, SweepColor), 3))
            {
                double rad = _sweepAngle * Math.PI / 180.0;
                PointF end = new
                (
                    center.X + (float)Math.Cos(rad) * r,
                    center.Y + (float)Math.Sin(rad) * r
                );
                e.Graphics.DrawLine(p, center, end);
            }

            // Contacts.
            foreach (var c in _contacts)
            {
                double rad = c.Angle * Math.PI / 180.0;
                float dist = c.Distance * r;

                PointF pos = new
                (
                    center.X + (float)Math.Cos(rad) * dist,
                    center.Y + (float)Math.Sin(rad) * dist
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

            base.OnPaint(e);

        }   // OnPaint()
        #endregion


    }   // RPSciFiRadar


    public class RadarContact
    {
        public float  Angle;
        public float  Distance; // 0–1
        public string Id;
    }


}   // namespace Spirograph_v1.Controls.RPSciFiRadar
