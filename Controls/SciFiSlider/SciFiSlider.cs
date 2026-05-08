using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.SciFiSlider
{
    public partial class SciFiSlider : UserControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   NumericValueChanged  : Triggered when the numeric value changes.
        //   SliderValueChanged   : Triggered when the slider value changes.
        //   SliderMaximumChanged : Triggered when the slider maximum changes.
        //   SliderMinimumChanged : Triggered when the slider minimum changes.
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler NumericValueChanged;
        public event EventHandler SliderValueChanged;
        //public event EventHandler SliderMaximumChanged;
        //public event EventHandler SliderMinimumChanged;

        #endregion



        // ---------------------------------------------------------------------
        // Public Enumerations:
        // --------------------
        //   GlowColorMode : Color mode of the SciFiSlider Control.
        // ---------------------------------------------------------------------

        #region .  Public Enumerations  .

        public enum GlowColorMode
        {
            NeonCyan,
            PlasmaPurple,
            ReactorGreen
        }

        #endregion



        // -------------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _glowMode      : Enum for the glow color mode values.
        //   _isDragging    : The flag to track if the slider is being dragged.
        //   _numericUpDown : The NumericUpDown control for numeric input.
        //   _sliderMaximum : The maximum value for the slider.
        //   _sliderMinimum : The minimum value for the slider.
        //   _title         : The Label control to display the title.
        // -------------------------------------------------------------------------

        #region .  Private Variables  .

        private readonly Label _title;

        private GlowColorMode _glowMode   = GlowColorMode.NeonCyan;
        private bool          _isDragging = false;
        private NumericUpDown _numericUpDown;
        private int           _sliderMaximum;
        private int           _sliderMinimum;

        #endregion



        // -------------------------------------------------------------------------
        // Constructor:
        // ------------
        //   SciFiSlider()  --  Constructor
        // -------------------------------------------------------------------------

        #region .  SciFiSlider()  --  Constructor  .

        // -------------------------------------------------------------------------
        //   Method.......:  SciFiSlider()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public SciFiSlider()
        {
            BackColor         = Color.Black;
            DoubleBuffered    = true;
            Height            = 120;
            Width             = 300;


            // Subscribe to events for synchronization between NumericUpDown
            // and TrackBar, position slider thumb based on mouse position,
            // and fire custom events when values change.
            _numericUpDown.ValueChanged += (s, e) =>
            {
                trackBar.Value = (int)_numericUpDown.Value;
                NumericValueChanged?.Invoke(this, EventArgs.Empty);
            };

            trackBar.MouseDown += (s, e) =>
            {
                _isDragging = true;
                UpdateValueFromMouse(e.X);
            };

            trackBar.MouseMove += (s, e) =>
            {
                if (_isDragging)
                {
                    UpdateValueFromMouse(e.X);
                }
            };

            trackBar.MouseUp   += (s, e) =>
            {
                _isDragging = false;
            };

            trackBar.Scroll    += (s, e) =>
            {
                _numericUpDown.Value = trackBar.Value;
                SliderValueChanged?.Invoke(this, EventArgs.Empty);
            };

        }   // SciFiSliderControl()
        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowMode              : Get/set glow color mode of the Slider.
        //   NumericUpDownLocation : Get/set NumericUpDown control location.
        //   NumericUpDownSize     : Get/set NumericUpDown control size.
        //   ShowNumericUpDown     : Toggle  NumericUpDown control visibility.
        //   ShowSliderTitle       : Toggle  Title label visibility.
        //   SliderLocation        : Get/set TrackBar control location.
        //   SliderMaximum         : Get/set Slider and NumericUpDown maximum.
        //   SliderMinimum         : Get/set Slider and NumericUpDown minimum.
        //   SliderSize            : Get/set TrackBar control size.
        //   SliderTickFrequency   : Get/set TrackBar tick frequency.
        //   SliderTickStyle       : Get/set TrackBar tick style.
        //   Title                 : Get/set Title label.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi")]
        public GlowColorMode GlowMode
        {
            get => _glowMode;
            set { _glowMode = value; Invalidate(); }
        }


        // Replace the following property implementations to avoid recursive property access
        // and ambiguity between the property and the field.

        [Category("Slider")]
        public int SliderMaximum
        {
            get => _sliderMaximum;
            set
            {
                _sliderMaximum = value;
                trackBar.Maximum = value;
                _numericUpDown.Maximum = value;
            }
        }


        [Category("Slider")]
        public int SliderMinimum
        {
            get => _sliderMinimum;
            set
            {
                _sliderMinimum = value;
                trackBar.Minimum = value;
                _numericUpDown.Minimum = value;
            }
        }


        [Category("NumericUpDown")]
        public Point NumericUpDownLocation
        {
            get => _numericUpDown.Location;
            set { _numericUpDown.Location = value; Invalidate(); }
        }


        [Category("NumericUpDown")]
        public Size NumericUpDownSize
        {
            get => _numericUpDown.Size;
            set { _numericUpDown.Size = value; Invalidate(); }
        }


        #region .  Show... Properties  --  NOT WORKING  .
        //[Category("NumericUpDown")]
        //public bool ShowNumericUpDown
        //{
        //    get => isShowNumericUpDown;
        //    set { isShowNumericUpDown = value; Invalidate(); }
        //}


        //[Category("TrackBar")]
        //public bool ShowSliderTitle
        //{
        //    get => isShowTitle;
        //    set { isShowTitle = value; Invalidate(); }
        //}
        #endregion


        [Category("Slider")]
        public Point SliderLocation
        {
            get => trackBar.Location;
            set { trackBar.Location = value; Invalidate(); }
        }


        [Category("Slider")]
        public Size SliderSize
        {
            get => trackBar.Size;
            set { trackBar.Size = value; Invalidate(); }
        }


        [Category("Slider")]
        public int SliderTickFrequency
        {
            get => trackBar.TickFrequency;
            set { trackBar.TickFrequency = value; Invalidate(); }
        }


        [Category("Slider")]
        public TickStyle SliderTickStyle
        {
            get => trackBar.TickStyle;
            set { trackBar.TickStyle = value; Invalidate(); }
        }


        [Category("Appearance")]
        [Description("The text for the control title.")]
        public override string Text
        {
            get => _title.Text;
            set { _title.Text = value; Invalidate(); }
        }

        #endregion



        // ---------------------------------------------------------------------
        // Override Methods:
        // -----------------
        //   OnPaint()  : Custom paint method to render the Sci-Fi slider.
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Custom paint method to render the Sci-Fi slider with
        //                   glow effects and holographic design.
        //   Parameters...:  e - PaintEventArgs containing the graphics context.
        //   Returns......:  None
        // -------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            var g   = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color glow = _glowMode switch
            {
                GlowColorMode.NeonCyan     => Color.FromArgb(0, 255, 255),
                GlowColorMode.PlasmaPurple => Color.FromArgb(200, 0, 255),
                GlowColorMode.ReactorGreen => Color.FromArgb(0, 255, 100),
                                         _ => Color.Cyan
            };

            // Plasma gradient background.
            using (var brush = new LinearGradientBrush(ClientRectangle,
                                                                       Color.FromArgb(20, glow),
                                                                       Color.FromArgb(5, 80, 80, 80),
                                                                       LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            // Hologram border.
            using (var pen = new Pen(Color.FromArgb(150, glow), 2))
            {
                g.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
            }

            // Outer glow.
            using (var glowPen = new Pen(Color.FromArgb(50, glow), 8))
            {
                g.DrawRectangle(glowPen, 4, 4, Width - 9, Height - 9);
            }

            base.OnPaint(e);

        }   // OnPaint()
        #endregion



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   UpdateValueFromMouse() : Updates slider value based on mouse position during dragging.
        // -------------------------------------------------------------------------

        private void UpdateValueFromMouse(int mouseX)
        {
            try
            {
                Rectangle trackRect = new(10, trackBar.Height / 2 - 4, trackBar.Width - 20, 8);
                float     percent   = (float)(mouseX - trackRect.Left) / trackRect.Width;
                int       value     = (int)(trackBar.Minimum + percent * (trackBar.Maximum - trackBar.Minimum));

                trackBar.Value = Math.Max(trackBar.Minimum, Math.Min(trackBar.Maximum, value));
            }
            catch
            {
                MessageBox.Show("An error occurred while updating the Slider with the mouse position.\r\n\r\nError:  The Image was not saved.", "Save Image Cancelled",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }   // UpdateValueFromMouse


    }   // class SciFiSliderControl

}   // namespace Spirograph_v1.Controls.SciFiSlider
