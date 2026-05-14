using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiCameraFeed
{
    public partial class RPSciFiCameraFeed : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   Clicked
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        //public event EventHandler Clicked;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.CameraFeed;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            //// Publish events here.
            //Clicked += (s, e) =>
            //{
            //    _bus?.Publish(ControlId, ControlType, "SignalToggled", _signalLost);
            //};

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _frame
        //   _phase
        //   _signalLost
        //   _timer
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private Image _frame;
        private float _phase;
        private bool  _signalLost;
        private Timer _timer;

        #endregion



        // --------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Frame      : Image - The current frame of the camera feed.
        //   NoiseColor : Color - The color of the noise overlay.
        //   TintColor  : Color - The color of the tint overlay.
        // --------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The current frame of the camera feed."), Browsable(true)]
        public Image Frame { get => _frame; set { _frame = value; Invalidate(); } }


        [Category("Sci-Fi"), Description("The color of the noise overlay."), Browsable(true)]
        public Color NoiseColor { get; set; } = Color.LightGreen;


        [Category("Sci-Fi"), Description("The maximum size of the particles."), Browsable(true)]
        public static int MaxParticleSize { get; set; } = 10;


        [Category("Sci-Fi"), Description("The color of the tint overlay."), Browsable(true)]
        public Color TintColor { get; set; } = Color.FromArgb(0, 255, 128);

        #endregion



        // --------------------------------------------------------------------
        // Public Constructor(s):
        // ----------------------
        //   Method()
        // --------------------------------------------------------------------

        #region .  RPSciFiCameraFeed()  .
        // --------------------------------------------------------------------
        //   Method.......:  RPSciFiCameraFeed()
        //   Description..:  The constructor for the RPSciFiCameraFeed class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        public RPSciFiCameraFeed()
        {
            DoubleBuffered = true;
            BackColor      = Color.Black;
            Font           = new Font("Consolas", 10, FontStyle.Bold);

            _timer = new Timer { Interval = 1 };
            _timer.Tick += (s, e) =>
            {
                _phase += 0.1f;
                Invalidate();
            };
            _timer.Start();

        }   // RPSciFiCameraFeed()
        #endregion



        // --------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // --------------------------------------------------------------------

        #region .  OnMouseClick()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseClick()
        //   Description..:  Handle the MouseClick event to start dragging the
        //                   slider and update the Value based on mouse position.
        //   Parameters...:  e - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseClick(MouseEventArgs e)
        {
            _signalLost = !_signalLost;

            Invalidate();

            base.OnMouseClick(e);

        }   // OnMouseClick()
        #endregion


        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to do_something.
        //   Parameters...:  e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

            var g   = e.Graphics;
            Rectangle  rect = ClientRectangle;

            Color glow = Color.FromArgb(0, 255, 255);


            #region.  Old Background  --  CAN DELETE  .
            //using (LinearGradientBrush bg = new
            //(
            //    rect,
            //    Color.FromArgb(10, 10, 10),
            //    Color.FromArgb(0, 0, 0),
            //    90f
            //))
            //{
            //    e.Graphics.FillRectangle(bg, rect);
            //}
            #endregion


            using var bg = new LinearGradientBrush(rect,
                                                                   Color.FromArgb(20, glow),
                                                                   Color.FromArgb(5, 0, 0, 0),
                                                                   LinearGradientMode.Vertical);
            g.FillRectangle(bg, ClientRectangle);

            if (_frame != null)
            {
                g.DrawImage(_frame, 10, 10, Width - 20, Height - 20);
            }

            using var borderPen = new Pen(Color.FromArgb(150, glow), 2);
            g.DrawRectangle(borderPen, 1, 1, Width - 3, Height - 3);

            using var glowPen = new Pen(Color.FromArgb(50, glow), 8);
            g.DrawRectangle(glowPen, 4, 4, Width - 9, Height - 9);

            if (_signalLost)
            {
                DrawNoise(e.Graphics, rect);
                DrawOverlayText(e.Graphics, rect, "NO SIGNAL", Color.Red);
            }
            else
            {
                DrawFakeScene(e.Graphics, rect);
            }

            DrawScanlines(e.Graphics, rect);
            DrawBorder(e.Graphics, rect);

        }   // OnPaint()
        #endregion



        // --------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   DrawBorder()
        //   DrawFakeScene()
        //   DrawNoise()
        //   DrawOverlayText()
        //   DrawScanlines()
        // --------------------------------------------------------------------

        #region .  DrawBorder()  .
        // --------------------------------------------------------------------
        //   Method.......:  DrawBorder()
        //   Description..:  Draws the border of the control.
        //   Parameters...:  g    - The graphics object.
        //                   rect - The rectangle defining the border area.
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        private void DrawBorder(Graphics g, Rectangle rect)
        {
            using Pen p = new(Color.FromArgb(160, TintColor), 2);

            g.DrawRectangle(p, rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2);

        }   // DrawBorder()
        #endregion


        #region .  DrawFakeScene()  .
        // --------------------------------------------------------------------
        //   Method.......:  DrawFakeScene()
        //   Description..:  Draws a fake scene on the control.
        //   Parameters...:  g    - The graphics object.
        //                   rect - The rectangle defining the drawing area.
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
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

            DrawOverlayText(g, rect, "CAMERA 03", Color.FromArgb(200, TintColor));

        }   // DrawFakeScene()
        #endregion


        #region .  DrawNoise()  .
        // --------------------------------------------------------------------
        //   Method.......:  DrawNoise()
        //   Description..:  Draws noise on the control.
        //   Parameters...:  g    - The graphics object.
        //                   rect - The rectangle defining the drawing area.
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        private static void DrawNoise(Graphics g, Rectangle rect)
        {
            rect.Inflate(-10, -10);
            var rnd = new Random();

            using var b = new SolidBrush(Color.FromArgb(200, 255, 255, 255));

            for (int i = 0; i < 200; i++)
            {
                int x = rnd.Next(rect.Left, rect.Right);
                int y = rnd.Next(rect.Top,  rect.Bottom);
                int w = RandomParticlesize(rnd, MaxParticleSize);
                int h = rnd.Next(1, 3);
                g.FillRectangle(b, x, y, w, h);
            }

        }   // DrawNoise()
        #endregion


        #region .  DrawOverlayText()  .
        // --------------------------------------------------------------------
        //   Method.......:  DrawOverlayText()
        //   Description..:  Draws overlay text on the control.
        //   Parameters...:  g    - The graphics object.
        //                   rect - The rectangle defining the drawing area.
        //                   text - The text to draw.
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        private void DrawOverlayText(Graphics g, Rectangle rect, string text, Color color)
        {
            using var b = new SolidBrush(color);

            Rectangle    r  = new(rect.Left + 4, rect.Bottom - 30, rect.Right - 4, rect.Bottom - 4);
            StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            //g.DrawString(text, Font, b, r, sf);
            g.DrawString(text, Font, b, rect.Left + 4, rect.Bottom - 23);

        }   // DrawOverlayText()
        #endregion


        #region .  DrawScanlines()  .
        // --------------------------------------------------------------------
        //   Method.......:  DrawScanlines()
        //   Description..:  Draws scanlines on the control.
        //   Parameters...:  g    - The graphics object.
        //                   rect - The rectangle defining the drawing area.
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        private static void DrawScanlines(Graphics g, Rectangle rect)
        {
            using Pen p = new(Color.FromArgb(40, 0, 0, 0), 2);

            for (int y = rect.Top; y < rect.Bottom; y += 4)
            {
                g.DrawLine(p, rect.Left, y, rect.Right, y);
            }

        }   // DrawScanlines()
        #endregion


        #region .  RandomParticlesize()  .
        // --------------------------------------------------------------------
        //   Method.......:  RandomParticlesize()
        //   Description..:  Generates a random particle size.
        //   Parameters...:  rnd     - The random number generator.
        //                   maxSize - The maximum particle size.
        //   Returns......:  A random particle size.
        // --------------------------------------------------------------------
        private static int RandomParticlesize(Random rnd, int maxSize)
        {
            return rnd.Next(1, maxSize + 1);

        }   // RandomParticlesize()
        #endregion


    }   // class RPSciFiCameraFeed

}   // namespace Spirograph_v1.Controls.RPSciFiCameraFeed
