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
        public event EventHandler SliderMaximumChanged;
        public event EventHandler SliderMinimumChanged;

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
        //   glowMode            : Enum to select the glow color mode for the slider.
        //   isDragging          : Flag to track if the slider is being dragged.
        //   isShowNumericUpDown : Controls visibility of the NumericUpDown control.
        //   isShowRange         : Controls visibility of the slider range label.
        //   isShowTitle         : Controls visibility of the title label.
        //   numericUpDown       : NumericUpDown control for numeric input.
        //   title               : Label control for displaying the title.
        //   trackBar            : TrackBar control for the slider.
        // -------------------------------------------------------------------------

        #region .  Private Variables  .

        private GlowColorMode glowMode   = GlowColorMode.NeonCyan;
        private bool          isDragging = false;
        private bool          isShowNumericUpDown;
        private bool          isShowTitle;
        private NumericUpDown numericUpDown;
        private int           sliderMaximum;
        private int           sliderMinimum;
        private Label         title;

        #endregion



        // -------------------------------------------------------------------------
        // Constructor:
        // ------------
        //   SciFiSlider()  --  Constructor
        // -------------------------------------------------------------------------

        #region .  SciFiSlider()  --  Constructor  .
        public SciFiSlider()
        {
            BackColor         = Color.Black;
            DoubleBuffered    = true;
            Height            = 120;
            Width             = 300;

            #region .  Old Code  --  CAN DELETE  .
            //title             = new Label()
            //{
            //    AutoSize      = true,
            //    Font          = new Font("Segoe UI", 10, FontStyle.Bold),
            //    ForeColor     = Color.White,
            //    Location      = new Point(10, 10),
            //    //Visible       = isShowTitle
            //};

            //trackBar          = new TrackBar()
            //{
            //    Anchor        = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            //    Minimum       = 1,
            //    Maximum       = 100,
            //    TickFrequency = 10,
            //    Location      = new Point(10, 40),
            //    Size          = new Size(200, 30)
            //};

            //numericUpDown     = new NumericUpDown()
            //{
            //    Anchor        = AnchorStyles.Top | AnchorStyles.Right,
            //    Font          = new Font("Segoe UI", 9, FontStyle.Bold),
            //    Maximum       = 1,
            //    Minimum       = 100,
            //    Location      = new Point(Width - 100, 10),
            //    Size          = new Size(50, 23),
            //    TextAlign     = HorizontalAlignment.Center,
            //    //Visible       = isShowNumericUpDown
            //};

            //Controls.Add(title);
            //Controls.Add(trackBar);
            //Controls.Add(numericUpDown);
            #endregion


            // Subscribe to events for synchronization between NumericUpDown
            // and TrackBar, position slider thumb based on mouse position,
            // and fire custom events when values change.
            numericUpDown.ValueChanged += (s, e) =>
            {
                trackBar.Value = (int)numericUpDown.Value;
                NumericValueChanged?.Invoke(this, EventArgs.Empty);
            };

            trackBar.MouseDown += (s, e) =>
            {
                isDragging = true;
                UpdateValueFromMouse(e.X);
            };

            trackBar.MouseMove += (s, e) =>
            {
                if (isDragging)
                {
                    UpdateValueFromMouse(e.X);
                }
            };

            trackBar.MouseUp   += (s, e) =>
            {
                isDragging = false;
            };

            trackBar.Scroll    += (s, e) =>
            {
                numericUpDown.Value = trackBar.Value;
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
            get => glowMode;
            set { glowMode = value; Invalidate(); }
        }


        // Replace the following property implementations to avoid recursive property access
        // and ambiguity between the property and the field.

        [Category("Slider")]
        public int SliderMaximum
        {
            get => sliderMaximum;
            set
            {
                sliderMaximum = value;
                trackBar.Maximum = value;
                numericUpDown.Maximum = value;
            }
        }


        [Category("Slider")]
        public int SliderMinimum
        {
            get => sliderMinimum;
            set
            {
                sliderMinimum = value;
                trackBar.Minimum = value;
                numericUpDown.Minimum = value;
            }
        }


        [Category("NumericUpDown")]
        public Point NumericUpDownLocation
        {
            get => numericUpDown.Location;
            set { numericUpDown.Location = value; Invalidate(); }
        }


        [Category("NumericUpDown")]
        public Size NumericUpDownSize
        {
            get => numericUpDown.Size;
            set { numericUpDown.Size = value; Invalidate(); }
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
            get => title.Text;
            set { title.Text = value; Invalidate(); }
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

            Color glow = glowMode switch
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
