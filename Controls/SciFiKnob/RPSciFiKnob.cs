using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiKnob
{
    public partial class RPSciFiKnob : UserControl, IRPSciFiControl
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
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.Knob;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            // Publish events here.
            ValueChanged += (s, e) =>
            {
                _bus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            };

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowColor
        //   Maximum
        //   Minimum
        //   Value
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        public Color GlowColor { get; set; } = Color.Cyan;

        public float Minimum { get; set; } = 0f;
        
        public float Maximum { get; set; } = 100f;

        public float Value
        {
            get => _value;
            set
            {
                float v = Math.Max(Minimum, Math.Min(Maximum, value));
                if (Math.Abs(_value - v) > 0.001f)
                {
                    _value = v;
                    _angle = 225f - (v - Minimum) / Math.Max(1f, (Maximum - Minimum)) * 270f;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _angle
        //   _dragging
        //   _value
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private float _angle = 225f; // 225–-45 range
        private bool  _dragging;
        private float _value;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiKnob()
        // ---------------------------------------------------------------------

        #region .  RPSciFiKnob()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiKnob()
        //   Description..:  The constructor for the RPSciFiKnob class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiKnob()
        {
            DoubleBuffered = true;
            BackColor      = Color.Transparent;
            Size           = new Size(60, 60);

        }   // RPSciFiKnob()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   UpdateFromMouse()
        // ---------------------------------------------------------------------

        #region .  UpdateFromMouse()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateFromMouse()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateFromMouse(Point p)
        {
            Point center = new(Width / 2, Height / 2);
            float dx     = p.X - center.X;
            float dy     = p.Y - center.Y;
            float ang    = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
            float mapped = ang + 180f; // 0–360

            if (mapped < 45 || mapped > 315) return; // dead zone

            float t = (mapped - 45f) / 270f;
            Value   = Minimum + t * (Maximum - Minimum);

        }   // UpdateFromMouse()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnMouseDown()
        //   OnMouseMove()
        //   OnMouseUp()
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnMouseDown()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseDown()
        //   Description..:  Handles the OnMouseDown event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging = true;
            Capture   = true;

            UpdateFromMouse(e.Location);

            base.OnMouseDown(e);

        }   // OnMouseDown()
        #endregion


        #region .  OnMouseMove()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseMove()
        //   Description..:  Handles the OnMouseMove event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                UpdateFromMouse(e.Location);
            }

            base.OnMouseMove(e);

        }   // OnMouseMove()
        #endregion


        #region .  OnMouseUp()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseUp()
        //   Description..:  Handles the OnMouseUp event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;
            Capture   = false;

            base.OnMouseUp(e);

        }   // OnMouseUp()
        #endregion


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
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

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

            using (Pen p = new(Color.FromArgb(200, GlowColor), 2))
            {
                e.Graphics.DrawEllipse(p, rect);
            }

            Point  center = new(Width / 2, Height / 2);
            float  radius = Width / 2f - 8;
            double rad    = _angle * Math.PI / 180.0;

            PointF end = new
            (
                center.X + (float)Math.Cos(rad) * radius,
                center.Y + (float)Math.Sin(rad) * radius
            );

            using (Pen p = new(Color.FromArgb(230, Color.White), 3))
            {
                e.Graphics.DrawLine(p, center, end);
            }

        }   // OnPaint()
        #endregion


    }   // class RPSciFiKnob

}   // namespace Spirograph_v1.Controls.RPSciFiKnob
