using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiHoloTable
{
    public partial class RPSciFiHoloTable : UserControl, IRPSciFiControl
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
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.HoloTable;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            //// Publish events here.
            //ValueChanged += (s, e) =>
            //{
            //    _bus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            //};

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GridColor : Color - The color of the holographic grid lines.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        public Color GridColor { get; set; } = Color.FromArgb(40, 0, 255, 255);

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _objects
        //   _rnd
        //   _rotationSpeed
        //   _timer
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private readonly List<HoloObject> _objects       = [];
        private readonly Random           _rnd           = new();
        private readonly float            _rotationSpeed = 0.5f;
        private readonly Timer            _timer;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiHoloTable()
        // ---------------------------------------------------------------------

        #region .  RPSciFiHoloTable()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiHoloTable()
        //   Description..:  The constructor for the RPSciFiHoloTable class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiHoloTable()
        {
            DoubleBuffered = true;
            BackColor      = Color.FromArgb(5, 5, 15);

            _timer = new Timer { Interval = 40 };
            _timer.Tick += (s, e) =>
            {
                foreach (var o in _objects)
                {
                    o.Angle += _rotationSpeed;
                }

                Invalidate();
            };

            _timer.Start();

        }   // RPSciFiHoloTable()
        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   AddRandomObject()
        // ---------------------------------------------------------------------

        #region .  AddRandomObject()  .
        // ---------------------------------------------------------------------
        //   Method.......:  AddRandomObject()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void AddRandomObject()
        {
            var obj = new HoloObject
            {
                Id     = Guid.NewGuid().ToString(),
                Angle  = (float)_rnd.NextDouble() * 360,
                Color  = Color.FromArgb(180, 0, 255, 255),
                Height = 10 + (float)_rnd.NextDouble() * 30,
                Radius = 40 + (float)_rnd.NextDouble() * 80,
            };

            _objects.Add(obj);
            _bus?.Publish(ControlId, ControlType, "ObjectAdded", obj.Id);

        }   // AddRandomObject()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles 
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect   = ClientRectangle;
            Point     center = new(rect.Width / 2, rect.Height / 2 + 20);

            using (Pen p = new(GridColor, 1))
            {
                e.Graphics.DrawEllipse(p, center.X - 100, center.Y - 40, 200,            80);
                e.Graphics.DrawEllipse(p, center.X -  60, center.Y - 24, 120,            48);
                e.Graphics.DrawLine   (p, center.X - 120, center.Y,      center.X + 120, center.Y);
            }

            foreach (var o in _objects)
            {
                double rad = o.Angle * Math.PI / 180.0;
                float  x   = center.X + (float)Math.Cos(rad) * o.Radius;
                float  y   = center.Y + (float)Math.Sin(rad) * (o.Radius * 0.4f) - o.Height;

                using (SolidBrush b = new(o.Color))
                {
                    e.Graphics.FillEllipse(b, x - 6, y - 6, 12, 12);
                }

                if (Math.Abs(o.Angle % 360) < 2f)
                {
                    _bus?.Publish(ControlId, ControlType, "ObjectAligned", o.Id);
                }
            }

            base.OnPaint(e);

        }   // OnPaint()
        #endregion



        private class HoloObject
        {
            public string Id;
            public float  Radius;
            public float  Angle;
            public float  Height;
            public Color  Color;
        }


    }   // class RPSciFiTerminal

}   // namespace Spirograph_v1.Controls.RPSciFiTerminal
