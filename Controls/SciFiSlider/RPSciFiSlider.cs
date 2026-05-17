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
        //   LabelTitleChanged  : Raised when the LabelTitle property changes.
        //   LabelValueChanged  : Raised when the LabelValue property changes.
        //   SliderValueChanged : Raised when the slider value changes.
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler<string> LabelTitleChanged;
        public event EventHandler<string> LabelValueChanged;
        public event EventHandler<int>    ValueChanged;

        #endregion



        // ---------------------------------------------------------------------
        // RPSciFi API Layer : Controls must implement this interface to be
        //                     compatible with the RPSciFi system.
        //
        //   ControlId   : string - The unique identifier for the control.
        //
        //   ControlType : RPSciFiControlType - The type of the control.
        //
        //   _controlBus : RPSciFiControlBus  - The RPSciFi control bus.
        //
        //   Register()  : Register the control with the RPSciFi control bus.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Browsable(true), Description("The unique identifier for the control.")]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Browsable(true), Description("The type of the control.")]
        public RPSciFiControlType ControlType => RPSciFiControlType.Slider;


        [Category("RPSciFi API Layer"), Browsable(true), Description("The RPSciFi control bus.")]
        private RPSciFiControlBus _controlBus;


        [Category("RPSciFi API Layer"), Browsable(true), Description("Register the control with the RPSciFi control bus.")]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Border_Style  : BorderStyle - The border style around the slider.
        //   GlowColor     : Color  - The color of the glow around the slider.
        //   LabelTitle    : string - The title label of the slider.
        //   LabelValue    : string - The value label of the slider.
        //   Maximum       : int    - The maximum value for the slider.
        //   Minimum       : int    - The minimum value for the slider.
        //   TickFrequency : int    - The frequency of the ticks on the slider.
        //   Value         : int    - The current value of the slider.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Browsable(true), Description("The border style around the slider.")]
        public BorderStyle Border_Style
        {
            get => this.BorderStyle;
            set
            {
                if (value == this.BorderStyle) return;

                this.BorderStyle = value;
                this.Invalidate();

                _controlBus?.Publish(ControlId, ControlType, "BorderStyleChanged", value);
            }
        }


        [Category("Sci-Fi"), Browsable(true), Description("The color of glow around the slider.")]
        public Color GlowColor { get; set; } = Color.Cyan;


        [Category("Sci-Fi"), Browsable(true), Description("The title label of the slider.")]
        public string LabelTitle
        {
            get => lblTitle.Text;
            set
            {
                if (value == lblTitle.Text) return;

                lblTitle.Text = value;
                lblTitle.Invalidate();

                //LabelTitleChanged?.Invoke(this, value);
                //_controlBus?.Publish(ControlId, ControlType, "LabelTitleChanged", value);
            }
        }


        [Category("Sci-Fi"), Browsable(true), Description("The value label of the slider.")]
        public string LabelValue

        {
            get => lblValue.Text;
            set
            {
                if (value == lblValue.Text) return;

                lblValue.Text = value;
                lblValue.Invalidate();

                //LabelValueChanged?.Invoke(this, value);
                //_controlBus?.Publish(ControlId, ControlType, "LabelValueChanged", value);
            }
        }


        [Category("Sci-Fi"), Browsable(true), Description("The maximum value for the slider")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if ((value == _maximum) || (value < _minimum)) return;

                _maximum = value;
                this.Invalidate();

                ValueChanged?.Invoke(this, value);
                _controlBus?.Publish(ControlId, ControlType, "ValueChanged", value);
            }
        }


        [Category("Sci-Fi"), Browsable(true), Description("The minimum value for the slider")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                if ((value == _minimum) || (value > _maximum)) return;

                _minimum = value;
                this.Invalidate();

                ValueChanged?.Invoke(this, value);
                _controlBus?.Publish(ControlId, ControlType, "ValueChanged", value);
            }
        }


        [Category("Sci-Fi"), Browsable(true), Description("The current value of the slider.")]
        public int TickFrequency
        {
            get => _tickFrequency;
            set
            {
                if ((value == _tickFrequency) || (value < _minimum) || (value > _maximum)) return;

                _tickFrequency = value;
                 this.Invalidate();

                ValueChanged?.Invoke(this, value);
                _controlBus?.Publish(ControlId, ControlType, "ValueChanged", value);
            }
        }


        [Category("Sci-Fi"), Browsable(true), Description("The current value of the slider.")]
        public int Value
        {
            get => _value;
            set
            {
                if ((value == _value) || (value < _minimum) || (value > _maximum)) return;

                _value        = value;
                lblValue.Text = value.ToString();
                lblValue.Invalidate();

                ValueChanged?.Invoke(this, value);
                _controlBus?.Publish(ControlId, ControlType, "ValueChanged", value);
            }
        }

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _dragging      : bool   - Indicates if the slider is being dragged.
        //   _labelTitle    : string - The title label of the slider.
        //   _labelValue    : string - The value label of the slider.
        //   _maximum       : int    - The maximum value for the slider.
        //   _minimum       : int    - The minimum value for the slider.
        //   _tickFrequency : int    - The frequency of the ticks on the slider.
        //   _value         : int    - The current value of the slider.
        // ---------------------------------------------------------------------

        #region .  Private Properties  .
        private bool        _dragging;
        private int         _tickFrequency;

        private BorderStyle _borderStyle;
        private string      _labelTitle;
        private string      _labelValue;
        private int         _maximum;
        private int         _minimum;
        private int         _value;

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
            InitializeComponent();

            DoubleBuffered  = true;
            BorderStyle     = BorderStyle.None;
            Minimum         = -10;
            Maximum         =  10;
            Value           =   0;
            numUpDown.Value = Value;

        }   // RPSciFiSlider()
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
            return new Rectangle(10, (Height / 2) + 2, Width - 90, 8);

        }   // GetTrackRect()
        #endregion


        #region .  SetValueFromX()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SetValueFromX()
        //
        //   Description..:  Set the slider value based on the X position of the
        //                   mouse click.
        //
        //   Parameters...:  x : int - The X position of the mouse click.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SetValueFromX(int x)
        {
            if (Value != x)
            {
                Rectangle rect = GetTrackRect();
                float pos = (float)(x - rect.Left) / Math.Max(1, rect.Width);

                Value = Minimum + (int)(pos * (Maximum - Minimum));
                numUpDown.Value = Value;

                Invalidate();
            }

        }   // SetValueFromX()
        #endregion



        // ---------------------------------------------------------------------
        // Protected Override Methods:
        // ---------------------------
        //   numUpDown_ValueChanged() : Handle the numUpDown_ValueChanged event to
        //                              update the Value and LabelValue properties,
        //                              and publish the ValueChanged event to the
        //                              RPSciFi control bus.
        //
        //   OnMouseDown()    : Handle the MouseDown event to start dragging the
        //                      slider and updating the value using the based on
        //                      the X position of the mouse click.
        //
        //   OnMouseMove()    : Handle the MouseMove event to update the slider
        //                      value while dragging.
        //
        //   OnMouseUp()      : Handle the MouseUp event to stop dragging the
        //                      slider.
        //
        //   OnPaint()        : Handle the Paint event to draw the slider.
        //
        //   OnValueChanged() : Handle the ValueChanged event to publish the
        //                      event to the RPSciFi control bus.
        //
        //   Note:  These methods are overridden to provide custom behavior for
        //          the slider control.
        // ---------------------------------------------------------------------

        #region .  numUpDown_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  numUpDown_ValueChanged()
        //
        //   Description..:  Handle the numUpDown_ValueChanged event.  Raise the
        //                   public ValueChanged event for listening subscribers.
        //
        //   Parameters...:  e - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            Value      = (int)numUpDown.Value;
            LabelValue = $"{Value:0.0}";

            _controlBus?.Publish(ControlId, ControlType, "ValueChanged", Value);

        }   // numUpDown_ValueChanged()
        #endregion


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

            BackColor = Color.Black;

            Rectangle rect = GetTrackRect();

            using (LinearGradientBrush bg = new
            (
                rect,
                Color.FromArgb(40, 40, 60),
                Color.FromArgb(10, 10, 20),
                0f)
            )
            {
                e.Graphics.FillRectangle(bg, rect);
            }

            using (Pen p = new(Color.FromArgb(120, GlowColor), 2))
            {
                e.Graphics.DrawRectangle(p, rect);
            }

            float t = (float)(Value - Minimum) / Math.Max(1, (Maximum - Minimum));
            int knobX = rect.Left + (int)(t * rect.Width);

            Rectangle knob = new(knobX - 8, rect.Top - 6, 16, rect.Height + 12);

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
