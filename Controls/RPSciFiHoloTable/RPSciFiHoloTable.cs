using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiHoloTable
{
    public partial class RPSciFiHoloTable : UserControl, IRPSciFiControl
    {
        // ---------------------------------------
        //  RPSciFi API Layer
        // ---------------------------------------

        public string ControlId { get; set; } = Guid.NewGuid().ToString();
        public RPSciFiControlType ControlType => RPSciFiControlType.GroupPanel;
        private RPSciFiControlBus _bus;

        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);
        }

        // ---------------------------------------
        //  Terminal Logic
        // ---------------------------------------

        private readonly List<HoloObject> _objects       = [];
        private readonly Random           _rnd           = new();
        private readonly float            _rotationSpeed = 0.5f;
        private readonly Timer            _timer;



        public Color GridColor { get; set; } = Color.FromArgb(40, 0, 255, 255);



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

        }



        public void AddRandomObject()
        {
            var obj = new HoloObject
            {
                Id     = Guid.NewGuid().ToString(),
                Radius = 40 + (float)_rnd.NextDouble() * 80,
                Angle  = (float)_rnd.NextDouble() * 360,
                Height = 10 + (float)_rnd.NextDouble() * 30,
                Color  = Color.FromArgb(180, 0, 255, 255)
            };
            _objects.Add(obj);
            _bus?.Publish(ControlId, ControlType, "ObjectAdded", obj.Id);

        }



        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect   = ClientRectangle;
            Point     center = new(rect.Width / 2, rect.Height / 2 + 20);

            using (Pen p = new(GridColor, 1))
            {
                e.Graphics.DrawEllipse(p, center.X - 100, center.Y - 40, 200, 80);
                e.Graphics.DrawEllipse(p, center.X -  60, center.Y - 24, 120, 48);
                e.Graphics.DrawLine   (p, center.X - 120, center.Y, center.X + 120, center.Y);
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

        }   // OnPaint()


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
