using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiCameraFeed
{
    public partial class RPSciFiCameraFeed : UserControl, IRPSciFiControl
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
        //  Camera Feed Logic
        // ---------------------------------------

        private Timer _timer;
        private float _phase;
        private bool  _signalLost;



        public Color TintColor { get; set; } = Color.FromArgb(0, 255, 128);



        public RPSciFiCameraFeed()
        {
            DoubleBuffered = true;
            BackColor      = Color.Black;

            _timer = new Timer { Interval = 1 };
            _timer.Tick += (s, e) =>
            {
                _phase += 0.1f;
                Invalidate();
            };
            _timer.Start();

            this.Click += (s, e) =>
            {
                _signalLost = !_signalLost;
                _bus?.Publish(ControlId, ControlType, "SignalToggled", _signalLost);
                Invalidate();
            };

        }   // RPSciFiCameraFeed()



        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            Rectangle rect = ClientRectangle;

            using (LinearGradientBrush bg = new
            (
                rect,
                Color.FromArgb(10, 10, 10),
                Color.FromArgb(0, 0, 0),
                90f
            ))
            {
                e.Graphics.FillRectangle(bg, rect);
            }

            if (_signalLost)
            {
                DrawNoise(e.Graphics, rect);
                DrawOverlayText(e.Graphics, rect, "NO SIGNAL");
            }
            else
            {
                DrawFakeScene(e.Graphics, rect);
            }

            DrawScanlines(e.Graphics, rect);
            DrawBorder(e.Graphics, rect);

        }   // OnPaint()


        private void DrawBorder(Graphics g, Rectangle rect)
        {
            using Pen p = new(Color.FromArgb(160, TintColor), 2);

            g.DrawRectangle(p, rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2);

        }   // DrawBorder()


        private void DrawFakeScene(Graphics g, Rectangle rect)
        {
            using (Pen grid = new(Color.FromArgb(40, TintColor), 1))
            {
                for (int x = rect.Left; x < rect.Right; x += 20)
                {
                    g.DrawLine(grid, x, rect.Top, x, rect.Bottom);
                }

                for (int y = rect.Top; y < rect.Bottom; y += 20)
                {
                    g.DrawLine(grid, rect.Left, y, rect.Right, y);
                }
            }

            using (Pen p = new(Color.FromArgb(180, TintColor), 2))
            {
                int cx = rect.Left + rect.Width  / 2;
                int cy = rect.Top  + rect.Height / 2;
                int r  = Math.Min(rect.Width, rect.Height) / 3;

                g.DrawEllipse(p, cx - r, cy - r, r * 2, r * 2);
            }

            DrawOverlayText(g, rect, "CAMERA 03");

        }   // DrawFakeScene()


        private static void DrawNoise(Graphics g, Rectangle rect)
        {
            var rnd = new Random();

            using var b = new SolidBrush(Color.FromArgb(40, 255, 255, 255));

            for (int i = 0; i < 200; i++)
            {
                int x = rnd.Next(rect.Left, rect.Right);
                int y = rnd.Next(rect.Top,  rect.Bottom);

                g.FillRectangle(b, x, y, 2, 2);
            }

        }   // DrawNoise()


        private void DrawOverlayText(Graphics g, Rectangle rect, string text)
        {
            using var b  = new SolidBrush(Color.FromArgb(200, TintColor));

            StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };

            g.DrawString(text, Font, b, rect, sf);

        }   // DrawOverlayText()


        private static void DrawScanlines(Graphics g, Rectangle rect)
        {
            using Pen p = new(Color.FromArgb(40, 0, 0, 0), 2);

            for (int y = rect.Top; y < rect.Bottom; y += 4)
            {
                g.DrawLine(p, rect.Left, y, rect.Right, y);
            }

        }   // DrawScanlines()


    }   // class RPSciFiCameraFeed

}   // namespace Spirograph_v1.Controls.RPSciFiCameraFeed