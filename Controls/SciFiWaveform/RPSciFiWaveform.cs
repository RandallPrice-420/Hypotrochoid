using System;
using System.ComponentModel;
using System.Drawing;


namespace Spirograph_v1.Controls.SciFiWaveform
{
    public partial class RPSciFiWaveform : SciFiSmartUserControl.RPSciFiSmartUserControl, IRPSciFiControl
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
        // RPSciFi API Layer : Ccontrols must implement this interface to be
        //                     compatible with the RPSciFi API system.
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
        public RPSciFiControlType ControlType => RPSciFiControlType.Slider;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            //// Publish events here.
            //ValueChanged += (s, e) =>
            //{
            //    _bus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            //};

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Amplitude        : The amplitude of the waveform that determines
        //                      how "tall" the waveform peaks are.
        //
        //   Frequency        : The frequency of the waveform in Hz, which
        //                      determines how many cycles occur per second.
        //
        //   GlowColor        : Color - The glow color around the slider.
        //
        //   MaximumAmplitude : The maximum value for the amplitude, which can be
        //                      adjusted to limit the peak values of the waveform.
        //
        //   MinimumAmplitude : The minimum value for the amplitude, which can be
        //                      adjusted to limit the minimum values of the waveform.
        //
        //   MaximumFrequency : The maximum value for the frequency, which can be
        //                      adjusted to limit how fast the waveform oscillates.
        //
        //   MinimumFrequency : The minimum value for the frequency, which can be
        //                      adjusted to limit how slow the waveform oscillates.
        //
        //   SelectedWaveform : A function that takes a time value (t) and
        //                      returns the corresponding amplitude of the
        //                      selected waveform at that time.  Must be a
        //                      Func<float, float> to allow for dynamic
        //                      waveform generation.
        //
        //   ValueDisplayMode : Determines how the amplitude and frequency
        //                      values are displayed (Labels, NumericUpDown,
        //                      Both, None).
        //
        //   ValueDisplayType : An enumeration that defines the display modes
        //                      for the amplitude and frequency values.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Browsable(true), Description("The amplitude of the waveform.")]
        public float Amplitude { get; private set; } = 1f;


        [Category("Sci-Fi"), Browsable(true), Description("The frequency of the waveform in Hz.")]
        public float Frequency { get; private set; } = 1f;


        [Category("Sci-Fi"), Description("The color of glow around the slider."), Browsable(true)]
        public Color GlowColor { get; set; } = Color.Cyan;


        [Category("Sci-Fi"), Description("The maximum value for the Frequency"), Browsable(true)]
        public int MaximumAmplitude
        {
            get => _maximumAmplitude;
            set
            {
                if (value > _maximumAmplitude)
                {
                    _maximumAmplitude = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, value);
                }
            }
        }


        [Category("Sci-Fi"), Description("The maximum value for the Frequency"), Browsable(true)]
        public int MaximumFrequency
        {
            get => _maximumFrequency;
            set
            {
                if (value > _maximumFrequency)
                {
                    _maximumFrequency = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, value);
                }
            }
        }


        [Category("Sci-Fi"), Description("The minimum value for the Frequency"), Browsable(true)]
        public int MinimumAmplitude
        {
            get => _minimumAmplitude;
            set
            {
                if (value < _minimumAmplitude)
                {
                    _minimumAmplitude = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, value);
                }
            }
        }


        [Category("Sci-Fi"), Description("The minimum value for the Frequency"), Browsable(true)]
        public int MinimumFrequency
        {
            get => _minimumFrequency;
            set
            {
                if (value < _minimumFrequency)
                {
                    _minimumFrequency = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, value);
                }
            }
        }


        [Category("Sci-Fi"), Browsable(false), Description("The selected waveform function.")]
        // Must be Browsable(false) to avoid serialization issues with Func<>
        public Func<float, float> SelectedWaveform { get; private set; }


        [Category("Sci-Fi"), Browsable(true), Description("The display mode for the value.")]
        public ValueDisplayType ValueDisplayMode { get; private set; }
        //public ValueDisplayType ValueDisplayMode
        //{
        //    get => _displayMode;
        //    set
        //    {
        //        _displayMode = value;
        //        Invalidate();
        //    }
        //}


        [Category("Sci-Fi"), Browsable(true), Description("The type of value display.")]
        public enum ValueDisplayType
        {
            Label,
            NumericUpDown,
            Both
        }

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _displayMode
        //   _maximumAmplitude
        //   _minimumAmplitude
        //   _maximumFrequency
        //   _minimumFrequency
        //   _suppressEvents 
        //
        //   _alienSignal   : A complex waveform that combines multiple sine
        //                    waves to create an otherworldly sound.
        //
        //   _harmonicStack : A complex waveform created by stacking multiple
        //                    sine waves of different frequencies and amplitudes.
        //
        //   _heartbeat     : A waveform that mimics the sound of a heartbeat,
        //                    with a sharp peak followed by a gradual decay.
        //
        //   _interference  : A waveform that simulates the effect of signal
        //                    interference, with random fluctuations and noise.
        //
        //   _plasma        : A waveform that simulates the chaotic, unpredictable
        //                    nature of plasma, with some rapid oscillations and
        //                    varying amplitudes.
        //
        //   _pulsar        : A waveform that simulates the pulsing nature of a
        //                    pulsar, with regular intervals and varying intensity.
        //
        //   _sawtooth      : A waveform that simulates a sawtooth wave, with a
        //                    linear rise and a sharp drop.
        //
        //   _sine          : A waveform that simulates a sine wave, with smooth
        //                    oscillations.
        //
        //   _square        : A waveform that simulates a square wave, with
        //                    abrupt transitions between high and low values.
        //
        //   _triangle      : A waveform that simulates a triangle wave, with
        //                    linear rises and falls.
        //
        //   _warpField     : A complex waveform that simulates the distortion
        //                    of space-time in a warp field, with some swirling
        //                    oscillations and varying frequencies.
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private int  _maximumAmplitude =  10;
        private int  _minimumAmplitude = -10;
        private int  _maximumFrequency =  10;
        private int  _minimumFrequency = -10;
        private bool _suppressEvents   = false;

        private ValueDisplayType _displayMode = ValueDisplayType.Both;
                                                                  
        private static readonly Func<float, float> _alienSignal   = t => (float)(Math.Sin(t * 5 + Math.Sin(t * 0.5)) * Math.Cos(t * 0.2));
                                                                  
        private static readonly Func<float, float> _harmonicStack = t => (float)
                                                                  (
                                                                      0.6 * Math.Sin(t * 2 * Math.PI) +
                                                                      0.3 * Math.Sin(t * 4 * Math.PI) +
                                                                      0.1 * Math.Sin(t * 8 * Math.PI)
                                                                  );
                                                                  
        private static readonly Func<float, float> _heartbeat     = t =>
                                                                  {
                                                                      float x = t % 1f;

                                                                      return (x < 0.1f) ? 1f - x * 10f
                                                                                        : (float)Math.Sin((x - 0.1f) * 10f);
                                                                  };
                                                                  
        private static readonly Func<float, float> _interference  = t => (float)(Math.Sin(t * 6) + Math.Sin(t * 13)) * 0.5f;
                                                                  
        private static readonly Func<float, float> _plasma        = t => (float)(Math.Sin(t * 4) * Math.Cos(t * 7));
                                                                  
        private static readonly Func<float, float> _pulsar        = t => (float)Math.Exp(-20 * Math.Abs(Math.Sin(t * Math.PI))) * 2f - 1f;
                                                                  
        private static readonly Func<float, float> _sawtooth      = t => (t % 1f) * 2f - 1f;
                                                                  
        private static readonly Func<float, float> _sine          = t => (float)Math.Sin(t * 2 * Math.PI);
                                                                  
        private static readonly Func<float, float> _square        = t => Math.Sign(Math.Sin(t * 2 * Math.PI));
                                                                  
        private static readonly Func<float, float> _triangle      = t =>
                                                                  {
                                                                       float v = (t % 1f) * 2f;
                                                                       return v < 1f ? v : 2f - v;
                                                                  };
                                                                  
        private static readonly Func<float, float> _warpField     = t => (float)(Math.Sin(t * 3) * Math.Sin(t * 17) * Math.Cos(t * 5));

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiWaveform()
        // ---------------------------------------------------------------------

        #region .  RPSciFiWaveform()  --  Constructor  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiWaveform()
        //
        //   Description..:  The constructor for the RPSciFiWaveform class.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiWaveform()
        {
            InitializeComponent();

            // Suppress events during initialization to avoid unwanted event firing.
            _suppressEvents = true;

            try
            {
                DoubleBuffered = true;
                BackColor      = Color.Black;

                // Amplitude  - Slider and NumericUpDown.
                sliderAmplitude.Maximum    = MaximumAmplitude;
                sliderAmplitude.Minimum    = MinimumAmplitude;
                sliderAmplitude.Value      = (int)Amplitude;
                //sliderAmplitude.ValueChanged += sliderAmplitude_ValueChanged;
                //sliderAmplitude.ValueChanged += (sender, value) => sliderAmplitude_ValueChanged(value);

                lblAmplitudeValue .Text    = $"{Amplitude:0.0}";
                numUpDownAmplitude.Value   = (decimal)Amplitude;
                //numUpDownAmplitude.ValueChanged += (sender, evt) => numUpDownAmplitude_ValueChanged();


                // Frequency  - Slider and NumericUpDown.
                sliderFrequency.Maximum    = MaximumFrequency;
                sliderFrequency.Minimum    = MinimumFrequency;
                sliderFrequency.Value      = (int)Frequency;
                //sliderFrequency.ValueChanged += (sender, value) => sliderFrequency_ValueChanged(value);

                lblFrequencyValue .Text    = $"{Frequency:0.0} Hz";
                numUpDownFrequency.Value   = (decimal)Frequency;
                numUpDownFrequency.ValueChanged += (sender, evt) => numUpDownFrequency_ValueChanged();

                // Waveform ComboBox.
                //cmbWaveforms.Width         = 150;
                cmbWaveforms.SelectedIndex = 0;
                cmbWaveforms.SelectedIndexChanged += (sender, evt) => UpdateWaveform();

                UpdateWaveform();
            }
            finally
            {
                _suppressEvents = false;
            }

        }   // RPSciFiWaveform()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   UpdateWaveform()
        // ---------------------------------------------------------------------

        #region .  UpdateWaveform()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateWaveform()
        //
        //   Description..:  
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateWaveform()
        {
            SelectedWaveform = cmbWaveforms.SelectedItem switch
            {
                "AlienSignal"   => t => Amplitude * RPSciFiWaveforms.AlienSignal(t   * Frequency),
                "HarmonicStack" => t => Amplitude * RPSciFiWaveforms.HarmonicStack(t * Frequency),
                "Heartbeat"     => t => Amplitude * RPSciFiWaveforms.Heartbeat(t     * Frequency),
                "Interference"  => t => Amplitude * RPSciFiWaveforms.Interference(t  * Frequency),
                "Plasma"        => t => Amplitude * RPSciFiWaveforms.Plasma(t        * Frequency),
                "Pulsar"        => t => Amplitude * RPSciFiWaveforms.Pulsar(t        * Frequency),
                "Sawtooth"      => t => Amplitude * RPSciFiWaveforms.Sawtooth(t      * Frequency),
                "Sine"          => t => Amplitude * RPSciFiWaveforms.Sine(t          * Frequency),
                "Square"        => t => Amplitude * RPSciFiWaveforms.Square(t        * Frequency),
                "Triangle"      => t => Amplitude * RPSciFiWaveforms.Triangle(t      * Frequency),
                "WarpField"     => t => Amplitude * RPSciFiWaveforms.WarpField(t     * Frequency),
                              _ => t => 0f
            };

            //WaveformChanged?.Invoke(this, SelectedWaveform);

        }   // UpdateWaveform()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   numUpDownAmplitude_ValueChanged()
        //   numUpDownFrequency_ValueChanged()
        //   sliderAmplitude_ValueChanged()
        //   sliderFrequency_ValueChanged()
        // ---------------------------------------------------------------------

        #region .  numUpDownAmplitude_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  numUpDownAmplitude_ValueChanged()
        //
        //   Description..:  Handles the numUpDownAmplitude_ValueChanged event
        //                   to synchronize Amplitude property, Label and Slider.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void numUpDownAmplitude_ValueChanged()
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = (int)numUpDownAmplitude.Value;

                Amplitude = value;
                lblAmplitudeValue.Text = $"{value:0.0}";
                sliderAmplitude.Value = value;
                sliderAmplitude.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // numUpDownAmplitude_ValueChanged()
        #endregion


        #region .  numUpDownFrequency_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  numUpDownFrequency_ValueChanged()
        //
        //   Description..:  Handles the numUpDownFrequency_ValueChanged event
        //                   to synchronize Frequency property, Label and Slider.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void numUpDownFrequency_ValueChanged()
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = (int)numUpDownFrequency.Value;

                Frequency = value;
                lblFrequencyValue.Text = $"{value:0.0} Hz";
                sliderFrequency.Value = value;
                sliderFrequency.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // numUpDownFrequency_ValueChanged()
        #endregion


        #region .  sliderAmplitude_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  sliderAmplitude_ValueChanged()
        //
        //   Description..:  Handles the sliderAmplitude_ValueChanged event
        //                   to update the Amplitude property and synchronizes the
        //                   Label and Slider.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void sliderAmplitude_ValueChanged(object sender, EventArgs evt)
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = sliderAmplitude.Value;

                Amplitude = value;
                lblAmplitudeValue.Text = $"{value:0.0}";
                numUpDownAmplitude.Value = value;
                numUpDownAmplitude.Invalidate();
                sliderAmplitude.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // sliderAmplitude_ValueChanged()
        #endregion


        #region .  sliderFrequency_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  sliderFrequency_ValueChanged()
        //
        //   Description..:  Handles the sliderFrequency_ValueChanged event
        //                   to update the Frequency property and synchronizes the
        //                   Label and Slider.
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void sliderFrequency_ValueChanged(object sender, EventArgs evt)
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = sliderFrequency.Value;

                Frequency                = value;
                lblFrequencyValue.Text   = $"{value:0.0} Hz";
                numUpDownFrequency.Value = value;
                numUpDownFrequency.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // sliderFrequency_ValueChanged()
        #endregion


    }   // class RPSciFiWaveform

}   // namespace Spirograph_v1.Controls.SciFiWaveform
