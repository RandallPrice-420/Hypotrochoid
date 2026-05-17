using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiNumericDial
{
    public partial class RPSciFiNumericDial : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   ValueChanged
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler ValueChanged;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : Controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.NumericDial;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus."), Browsable(false)]
        private RPSciFiControlBus _controlBus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

            // Publish events here.
            ValueChanged += (s, e) =>
            {
                _controlBus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            };

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Maximum
        //   Minimum
        //   Value
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi")] public int Maximum { get; set; } = 100;

        [Category("Sci-Fi")] public int Minimum { get; set; } = 0;

        [Category("Sci-Fi")] public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(Minimum, Math.Min(Maximum, value));
                _angle = 360f * ((_value - Minimum) / (float)(Maximum - Minimum));
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _angle
        //   _value
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private float _angle = 0;
        private int   _value = 0;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiNumericDial()
        // ---------------------------------------------------------------------

        #region .  RPSciFiNumericDial()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiNumericDial()
        //   Description..:  The constructor for the RPSciFiNumericDial class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiNumericDial()
        {
            DoubleBuffered = true;
            BackColor      = Color.Transparent;
            Width          = 120;
            Height         = 120;

            MouseMove += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    float dx = e.X - Width  / 2f;
                    float dy = e.Y - Height / 2f;

                    _angle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI + 180);

                    Value = (int)(Minimum + (_angle / 360f) * (Maximum - Minimum));
                }
            };

        }   // RPSciFiNumericDial()
        #endregion




        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            var g   = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color     GlowColor = Color.FromArgb(0, 255, 255);
            Rectangle rect      = new(0, 0, Width - 1, Height - 1);


            // Background
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
                pgb.CenterColor = Color.FromArgb(40, GlowColor);
                pgb.SurroundColors = [Color.FromArgb(5, 5, 10)];

                e.Graphics.FillEllipse(pgb, rect);
            }


            // Circles
            using var glowPen = new Pen(Color.FromArgb(80, GlowColor), 12);
            g.DrawEllipse(glowPen, 6, 6, Width - 12, Height - 12);

            using var ringPen = new Pen(Color.FromArgb(180, GlowColor), 4);
            g.DrawEllipse(ringPen, 10, 10, Width - 20, Height - 20);


            // Pointer
            float rad = (float)(_angle * Math.PI / 180);
            float cx  = Width  / 2f;
            float cy  = Height / 2f;
            float px  = cx + (float)Math.Cos(rad) * (Width  / 2f - 20);
            float py  = cy + (float)Math.Sin(rad) * (Height / 2f - 20);

            using var pointerPen = new Pen(Color.FromArgb(255, GlowColor), 4);
            g.DrawLine(pointerPen, cx, cy, px, py);


            // Value Text
            using var f = new Font("Segoe UI", 10, FontStyle.Bold);
            var text = Value.ToString();
            var  size = g.MeasureString(text, f);

            g.DrawString(text, f, Brushes.Blue, cx - size.Width / 2, cy - size.Height / 2);

        }   // OnPaint()
        #endregion


    }   // class SciFiNumericDial

}   // namespace Spirograph_v1.Controls.SciFiNumericDial
