using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiSlider
{
    public partial class RPSciFiSlider : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   ValueChanged : EventHandler<int> - Raised when the slider value changes.
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler<int> ValueChanged;

        #endregion



        // ---------------------------------------------------------------------
        // RPSciFi API Layer : All controls must implement this interface to be
        //                     compatible with the RPSciFi system.
        //
        //   ControlId   : strings - The unique identifier for the control.
        //   ControlType : RPSciFiControlType - The type of the control.
        //   _bus        : RPSciFiControlBus  - The RPSciFi control bus for communication.
        //   Register()  : Register the control with the RPSciFi control bus.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description(""), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public  RPSciFiControlType ControlType => RPSciFiControlType.Slider;


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
        //   GlowColor : Color - The color of the glow around the slider.
        //   Maximum   : int   - The maximum value for the slider.
        //   Minimum   : int   - The minimum value for the slider.
        //   Value     : int   - The current value of the slider.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The color of glow around the slider."), Browsable(true)]
        public Color GlowColor { get; set; } = Color.Cyan;


        [Category("Sci-Fi"), Description("The maximum value for the slider"), Browsable(true)]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value > _minimum)
                {
                    _maximum = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, value);
                }
            }
        }


        [Category("Sci-Fi"), Description("The minimum value for the slider"), Browsable(true)]
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (value < _maximum)
                {
                    _minimum = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, value);
                }
            }
        }


        [Category("Sci-Fi"), Description("The current value of the slider."), Browsable(true)]
        public int Value { get; set; }
        //public int Value
        //{
        //    get => _value;
        //    set
        //    {
        //        int v = Math.Max(_minimum, Math.Min(_maximum, value));
        //        if (_value != v)
        //        {
        //            _value = v;
        //            Invalidate();
        //            ValueChanged?.Invoke(this, value);
        //        }
        //    }
        //}


        [Category("Sci-Fi"), Description("The current value of the slider."), Browsable(true)]
        public int TickFrequency
        {
            get => _tickFrequency;
            set
            {
                _tickFrequency = value;
                int v = Math.Max(_minimum, Math.Min(_maximum, value));
                if (_value != v)
                {
                    _value = v;
                    Invalidate();
                    ValueChanged?.Invoke(this, value);
                }
            }
        }
        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _dragging      : bool - Indicates if the slider is being dragged.
        //   _maximum       : int  - The maximum value for the slider.
        //   _minimum       : int  - The minimum value for the slider.
        //   _tickFrequency : int  - The frequency of the ticks on the slider.
        //   _value         : int  - The current value of the slider.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        private bool _dragging;
        private int  _maximum;
        private int  _minimum;
        private int  _tickFrequency;
        private int  _value;

        #endregion



        // ---------------------------------------------------------------------
        // Constructor:
        // ------------
        //   RPSciFiSlider() : The constructor for the RPSciFiSlider class.
        // ---------------------------------------------------------------------

        #region .  RPSciFiSlider()  --  Constructor  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiSlider()
        //
        //   Description..:  The constructor for the RPSciFiSlider class.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiSlider()
        {
            DoubleBuffered = true;
            Height         = 32;
            ValueChanged  += RPSciFiSlider_ValueChanged;
            //{
            //    // Publish the ValueChanged event to the RPSciFi control bus.
            //    _bus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            //};

        }   // RPSciFiSlider()

        private void RPSciFiSlider_ValueChanged(object s, int e)
        {
            // Publish the ValueChanged event to the RPSciFi control bus.
            _bus?.Publish(ControlId, ControlType, "ValueChanged", e);

        }
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   GetTrackRect()  : Get the slider control rectangle.
        //
        //   SetValueFromX() : Set the slider value based on the X position of
        //                     the mouse click.
        // ---------------------------------------------------------------------

        #region .  GetTrackRect()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetTrackRect()
        //
        //   Description..:  Get the slider control rectangle.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private Rectangle GetTrackRect()
        {
            return new Rectangle(10, (Height / 2) - 4, Width - 20, 8);

        }   // GetTrackRect()
        #endregion


        #region .  SetValueFromX()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SetValueFromX()
        //
        //   Description..:  Set the slider value based on the X position of the
        //                   mouse click.
        //
        //   Parameters...:  x - The X position of the mouse click.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SetValueFromX(int x)
        {
            if (Value != x)
            {
                Rectangle rect = GetTrackRect();
                float     pos  = (float)(x - rect.Left) / Math.Max(1, rect.Width);

                Value = Minimum + (int)(pos * (Maximum - Minimum));
                Invalidate();
            }

        }   // SetValueFromX()
        #endregion



        // ---------------------------------------------------------------------
        // Protected Override Methods:
        // ---------------------------
        //   OnMouseDown() : Handle the MouseDown event to start dragging the
        //                   slider and update the value based on mouse position.
        //
        //   OnMouseMove() : Handle the MouseMove event to update the slider
        //                   value while dragging.
        //
        //   OnMouseUp()   : Handle the MouseUp event to stop dragging the slider.
        //
        //   OnPaint()     : Handle the Paint event to draw the slider.
        // ---------------------------------------------------------------------

        #region .  OnMouseDown()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseDown()
        //
        //   Description..:  Handle the MouseDown event to start dragging the
        //                   slider and update the Value based on mouse position.
        //
        //   Parameters...:  e - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _dragging = true;

            SetValueFromX(e.X);

        }   // OnMouseDown()
        #endregion


        #region .  OnMouseMove()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseMove()
        //
        //   Description..:  Handle the MouseMove event to update the slider
        //                   value while dragging.
        //
        //   Parameters...:  e - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_dragging)
            {
                SetValueFromX(e.X);
            }

        }   // OnMouseMove()
        #endregion


        #region .  OnMouseUp()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseUp()
        //
        //   Description..:  Handle the MouseUp event to stop dragging the slider.
        //
        //   Parameters...:  e - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _dragging = false;

        }   // OnMouseUp()
        #endregion


        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //
        //   Description..:  Handle the Paint event to draw the slider.
        //
        //   Parameters...:  e - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            BackColor = Color.Transparent;

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
                pgb.CenterColor    = Color.FromArgb(180, GlowColor);
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
