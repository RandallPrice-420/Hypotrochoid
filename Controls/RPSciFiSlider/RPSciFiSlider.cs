using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiSlider
{
    public partial class RPSciFiSlider : UserControl, IRPSciFiControl
    {
        // ---------------------------------------
        //  RPSciFi API Layer
        // ---------------------------------------
        public string ControlId { get; set; } = Guid.NewGuid().ToString();
        public RPSciFiControlType ControlType => RPSciFiControlType.Slider;
        private RPSciFiControlBus _bus;

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


        // -------------------------------------------------------------------------
        // Public Events:
        // --------------
        //   ValueChanged
        // -------------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler ValueChanged;

        #endregion



        // -------------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowColor
        //   Maximum
        //   Minimum
        //   Value
        // -------------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Custom Appearance")]
        [Description("The color of glow around the slider.")]
        [Browsable(true)]
        public Color GlowColor { get; set; } = Color.Cyan;


        [Category("Custom Appearance")]
        [Description("The maximum value for the slider")]
        [Browsable(true)]
        //public int Maximum   { get; set; } = 100;
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value > Minimum)
                {
                    _maximum = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        [Category("Custom Appearance")]
        [Description("The minimum value for the slider")]
        [Browsable(true)]
        //public int Minimum   { get; set; } = 0;
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (value < Maximum)
                {
                    _minimum = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        [Category("Custom Appearance")]
        [Description("The current value of the slider.")]
        [Browsable(true)]
        public int Value
        {
            get => _value;
            set
            {
                int v = Math.Max(Minimum, Math.Min(Maximum, value));
                if (_value != v)
                {
                    _value = v;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion



        // -------------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _dragging
        //   _maximum
        //   _minimum
        //   _value
        // -------------------------------------------------------------------------

        #region .  Public Properties  .

        private bool _dragging;
        private int  _maximum;
        private int  _minimum;
        private int  _value;

        #endregion



        // -------------------------------------------------------------------------
        // Constructor:
        // ------------
        //   RPSciFiSlider()
        // -------------------------------------------------------------------------

        #region .  RPSciFiSlider()  .
        // -------------------------------------------------------------------------
        //   Method.......:  RPSciFiSlider()
        //   Description..:  The constructor for the RPSciFiSlider class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public RPSciFiSlider()
        {
            DoubleBuffered = true;
            Height         = 32;

        }   // RPSciFiSlider()
        #endregion



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   GetTrackRect()
        //   SetValueFromX()
        // -------------------------------------------------------------------------

        #region .  GetTrackRect()  .
        // -------------------------------------------------------------------------
        //   Method.......:  GetTrackRect()
        //   Description..:  Get the slider control rectangle.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private Rectangle GetTrackRect()
        {
            return new Rectangle(10, Height / 2 - 4, Width - 20, 8);

        }   // GetTrackRect()
        #endregion


        #region .  SetValueFromX()  .
        // -------------------------------------------------------------------------
        //   Method.......:  SetValueFromX()
        //   Description..:  Describe
        //   Parameters...:  x - The X position of the mouse click.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void SetValueFromX(int x)
        {
            Rectangle track = GetTrackRect();
            float t = (float)(x - track.Left) / Math.Max(1, track.Width);
            Value = Minimum + (int)(t * (Maximum - Minimum));

        }   // SetValueFromX()
        #endregion



        // -------------------------------------------------------------------------
        // Protected Override Methods:
        // ---------------------------
        //   OnMouseDown()
        //   OnMouseMove()
        //   OnMouseUp()
        //   OnPaint()
        // -------------------------------------------------------------------------

        #region .  OnMouseDown()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnMouseDown()
        //   Description..:  Handle the mouse down event.
        //   Parameters...:  e - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging = true;
            SetValueFromX(e.X);
            base.OnMouseDown(e);

        }   // OnMouseDown()
        #endregion


        #region .  OnMouseMove()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnMouseMove()
        //   Description..:  Handle the OnMouseMove event.
        //   Parameters...:  e - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
                SetValueFromX(e.X);
            base.OnMouseMove(e);

        }   // OnMouseMove()
        #endregion


        #region .  OnMouseUp()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnMouseUp()
        //   Description..:  Handle the OnMouseUp event.
        //   Parameters...:  e - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;
            base.OnMouseUp(e);

        }   // OnMouseUp()
        #endregion


        #region .  OnPaint()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handle the OnPaint event.
        //   Parameters...:  e - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle track = GetTrackRect();

            using (LinearGradientBrush bg = new
            (
                track,
                Color.FromArgb(40, 40, 60),
                Color.FromArgb(10, 10, 20),
                0f)
            )
            {
                e.Graphics.FillRectangle(bg, track);
            }

            using (Pen p = new(Color.FromArgb(120, GlowColor), 2))
            {
                e.Graphics.DrawRectangle(p, track);
            }

            float t     = (float)(Value - Minimum) / Math.Max(1, (Maximum - Minimum));
            int   knobX = track.Left + (int)(t * track.Width);

            Rectangle knob = new(knobX - 8, track.Top - 6, 16, track.Height + 12);

            using GraphicsPath gp = new();
            gp.AddEllipse(knob);

            using (PathGradientBrush pgb = new(gp))
            {
                pgb.CenterColor = Color.FromArgb(180, GlowColor);
                pgb.SurroundColors = [Color.FromArgb(0, GlowColor)];
                e.Graphics.FillEllipse(pgb, knob);
            }
            using (Pen p = new(Color.FromArgb(220, GlowColor), 2))
            {
                e.Graphics.DrawEllipse(p, knob);
            }

        }   // OnPaint()
        #endregion


    }   // class RPSciFiSlider

}   // namespace Spirograph_v1.Controls.RPSciFiSlider
