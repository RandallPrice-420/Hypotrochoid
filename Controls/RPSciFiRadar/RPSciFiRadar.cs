using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiRadar
{
    public partial class RPSciFiRadar : UserControl, IRPSciFiControl
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
        //  Radar Logic
        // ---------------------------------------

        public Color ContactColor { get; set; } = Color.Cyan;

        public Color GridColor    { get; set; } = Color.FromArgb(40, 255, 255, 255);

        public Color SweepColor   { get; set; } = Color.Lime;




        private readonly Timer  _timer;
        private readonly Random _rnd = new();
        private float  _sweepAngle;

        private List<RadarContact> _contacts = new List<RadarContact>();



        public class RadarContact
        {
            public float  Angle;
            public float  Distance; // 0–1
            public string Id;
        }



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

        }



        public void AddRandomContact()
        {
            var c = new RadarContact
            {
                Angle    = (float)(_rnd.NextDouble() * 360),
                Distance = (float)(_rnd.NextDouble()),
                Id       = Guid.NewGuid().ToString()
            };
            _contacts.Add(c);

            _bus?.Publish(ControlId, ControlType, "ContactAdded", c.Id);

        }   // AddRandomContact()



        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect   = ClientRectangle;
            int       r      = Math.Min(rect.Width, rect.Height) / 2 - 10;
            Point     center = new(rect.Width / 2, rect.Height / 2);

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

                // Lock-on event
                if (Math.Abs(c.Angle - _sweepAngle) < 2f)
                {
                    _bus?.Publish(ControlId, ControlType, "ContactSweep", c.Id);
                }
            }

        }   // OnPaint()


    }   // RPSciFiRadar

}   // namespace Spirograph_v1.Controls.RPSciFiRadar
