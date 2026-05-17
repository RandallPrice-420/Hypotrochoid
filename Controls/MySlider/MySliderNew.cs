using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Spirograph_v1.Controls.MySlider
{
    public partial class MySliderNew : UserControl
    {
        // -------------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowColor
        //   Value
        // -------------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Browsable(true), Description("The color of glow around the slider.")]
        public Color GlowColor { get; set; } = Color.Cyan;


        [Category("Custom"), Browsable(true), Description("Current value of the Slider.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Value
        {
            get => _sliderValue;
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                }
            }
        }

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _isDragging
        //   _sliderValue
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private bool _isDragging;
        private int _sliderValue;

        #endregion



        // -------------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   MySliderNew()
        // -------------------------------------------------------------------------

        #region .  MySliderNew()  --  Constructor  .
        // -------------------------------------------------------------------------
        //   Method.......:  MySliderNew()
        //
        //   Description..:  The constructor for the MySliderNew class.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public MySliderNew()
        {
            InitializeComponent();

            // Set UserPaint once in constructor
            SetStyle(ControlStyles.ResizeRedraw, true);

        }   // MySliderNew()
        #endregion



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   GetSliderRect()  : Get the slider control rectangle.
        //
        //   SetValueFromX() : Set the slider value based on the X position of
        //                     the mouse click.
        // -------------------------------------------------------------------------

        #region .  GetSliderRect()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetSliderRect()
        //
        //   Description..:  Get the slider control rectangle.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private Rectangle GetSliderRect()
        {
            return new Rectangle(10, (Height / 2) + 4, Width - 84, 8);

        }   // GetSliderRect()
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
                Rectangle rect = GetSliderRect();
                float     pos  = (float)(x - rect.Left) / Math.Max(1, rect.Width);

                Value = slider.Minimum + (int)(pos * (slider.Maximum - slider.Minimum));
                Invalidate();
            }

        }   // SetValueFromX()
        #endregion


        private void UpdateValueFromMouse(int mouseX, EventArgs e = null)
        {
            Rectangle trackRect = new(10, slider.Height / 2 - 4, slider.Width - 20, 8);
            float percent       = (float)(mouseX - trackRect.Left) / trackRect.Width;
            int value           = (int)(slider.Minimum + percent * (slider.Maximum - slider.Minimum));

            slider.Value = Math.Max(slider.Minimum, Math.Min(slider.Maximum, value));

            //SliderValueChanged?.Invoke(this, e);

        }   // UpdateValueFromMouse



        // -------------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   numUpDown_ValueChanged()
        //   Slider_MouseDown()
        //   Slider_MouseMove()
        //   Slider_MouseUp()
        //   Slider_ValueChanged()
        // -------------------------------------------------------------------------

        #region .  numUpDown_ValueChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  numUpDown_ValueChanged()
        //
        //   Description..:  Handles the numUpDown_ValueChanged event to synchronize
        //                   the slider control with the numUpDown's value.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            slider.Value = (int)numUpDown.Value;

        }   // numUpDown_ValueChanged()
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

            this.BackColor = Color.Black;

            Rectangle rect = GetSliderRect();

            using (LinearGradientBrush bg = new
            (
                rect, Color.FromArgb(40, 40, 60), Color.FromArgb(10, 10, 20), 0f)
            )
            {
                e.Graphics.FillRectangle(bg, rect);
            }

            using (Pen p = new(Color.FromArgb(120, GlowColor), 2))
            {
                e.Graphics.DrawRectangle(p, rect);
            }

            float t   = (float)(Value - slider.Minimum) / Math.Max(1, (slider.Maximum - slider.Minimum));
            int knobX = rect.Left + (int)(t * rect.Width);

            Rectangle knob = new(knobX - 8, rect.Top - 6, 16, rect.Height + 12);

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


        #region .  Slider_MouseDown()  .
        // -------------------------------------------------------------------------
        //   Method.......:  Slider_MouseDown()
        //
        //   Description..:  Handles the Slider_MouseDown event to start dragging
        //                   the slider thumb.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void Slider_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _isDragging    = true;
            slider.Capture = true;

            //SetValueFromX(e.X);
            UpdateValueFromMouse(e.X);

        }   // Slider_MouseDown()
        #endregion


        #region .  Slider_MouseMove()  .
        // -------------------------------------------------------------------------
        //   Method.......:  Slider_MouseMove()
        //
        //   Description..:  Handles the Slider_MouseMove event to update the slider
        //                   value while dragging.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void Slider_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;

            //SetValueFromX(e.X);
            UpdateValueFromMouse(e.X);

        }   // Slider_MouseMove()
        #endregion


        #region .  Slider_MouseUp()  .
        // -------------------------------------------------------------------------
        //   Method.......:  Slider_MouseUp()
        //
        //   Description..:  Handles the Slider_MouseUp event to stop dragging
        //                   the slider thumb.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void Slider_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _isDragging    = false;
            slider.Capture = false;

        }   // Slider_MouseUp()
        #endregion


        #region .  Slider_ValueChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  Slider_ValueChanged()
        //
        //   Description..:  Handles the Slider_ValueChanged event to synchronize
        //                   the numUpDown control with the slider's value.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            numUpDown.Value = slider.Value;

        }   // Slider_ValueChanged()
        #endregion


    }   // class MySliderNew

}   // namespace Spirograph_v1.Controls.MySlider
