using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.SciFiWaveform
{
    public partial class RPSciFiWaveform : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   WaveformChanged
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler<Func<float, float>> WaveformChanged;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Browsable(true), Description("The unique identifier for the control.")]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Browsable(true), Description("The type of the control.")]
        public RPSciFiControlType ControlType => RPSciFiControlType.Waveform;


        [Category("RPSciFi API Layer"), Browsable(false), Description("The RPSciFi control bus for communication.")]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Browsable(false), Description("Register the control with the RPSciFi control bus.")]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            // Publish events here.
            WaveformChanged += (sender, e) =>
            {
                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);
            };

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
        //   _suppressEvents 
        //   _scaleFactor
        //   _alienSignal
        //   _harmonicStack
        //   _heartbeat
        //   _interference
        //   _plasma
        //   _pulsar
        //   _sawtooth
        //   _sine
        //   _square
        //   _triangle
        //   _warpField
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private bool _suppressEvents = false;

        //private ValueDisplayType _displayMode = ValueDisplayType.Label;

        private static readonly Func<float, float> _alienSignal = t => (float)(Math.Sin(t * 5 + Math.Sin(t * 0.5)) * Math.Cos(t * 0.2));

        private static readonly Func<float, float> _harmonicStack = t => (float)
                                                                             (
                                                                                 0.6 * Math.Sin(t * 2 * Math.PI) +
                                                                                 0.3 * Math.Sin(t * 4 * Math.PI) +
                                                                                 0.1 * Math.Sin(t * 8 * Math.PI)
                                                                             );

        private static readonly Func<float, float> _heartbeat = t =>
                                                                             {
                                                                                 float x = t % 1f;
                                                                                 return (x < 0.1f) ? 1f - x * 10f
                                                                                                   : (float)Math.Sin((x - 0.1f) * 10f);
                                                                             };

        private static readonly Func<float, float> _interference = t => (float)(Math.Sin(t * 6) + Math.Sin(t * 13)) * 0.5f;

        private static readonly Func<float, float> _plasma = t => (float)(Math.Sin(t * 4) * Math.Cos(t * 7));

        private static readonly Func<float, float> _pulsar = t => (float)Math.Exp(-20 * Math.Abs(Math.Sin(t * Math.PI))) * 2f - 1f;

        private static readonly Func<float, float> _sawtooth = t => (t % 1f) * 2f - 1f;

        private static readonly Func<float, float> _sine = t => (float)Math.Sin(t * 2 * Math.PI);

        private static readonly Func<float, float> _square = t => Math.Sign(Math.Sin(t * 2 * Math.PI));

        private static readonly Func<float, float> _triangle = t =>
                                                                             {
                                                                                 float v = (t % 1f) * 2f;
                                                                                 return v < 1f ? v : 2f - v;
                                                                             };

        private static readonly Func<float, float> _warpField = t => (float)(Math.Sin(t * 3) * Math.Sin(t * 17) * Math.Cos(t * 5));

        #endregion


        private enum NumericUpDownControl { Amplitude, Frequency, }
        private enum SliderControl { Amplitude, Frequency, }



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiWaveform()
        // ---------------------------------------------------------------------

        #region .  RPSciFiWaveform()  --  Constructor  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiWaveform()
        //   Description..:  The constructor for the RPSciFiWaveform class.
        //   Parameters...:  None
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
                BackColor = Color.Black;

                // Amplitude  - Slider and NumericUpDown.
                rpSciFiSliderAmplitude.Maximum = -10;
                rpSciFiSliderAmplitude.Minimum = 10;
                rpSciFiSliderAmplitude.Value = 0;
                //rpSciFiSliderAmplitude.ValueChanged += rpSciFiSliderAmplitude_ValueChanged;
                //rpSciFiSliderAmplitude.ValueChanged += (sender, value) => rpSciFiSliderAmplitude_ValueChanged(value);

                LabelAmplitudeValue.Text = $"{Amplitude:0.0}";
                numericUpDownAmplitude.Value = (decimal)Amplitude;
                numericUpDownAmplitude.ValueChanged += (sender, evt) => numericUpDownAmplitude_ValueChanged();


                // Frequency  - Slider and NumericUpDown.
                rpSciFiSliderFrequency.Maximum = -10;
                rpSciFiSliderFrequency.Minimum = 10;
                rpSciFiSliderFrequency.Value = 0;
                //rpSciFiSliderFrequency.ValueChanged += (sender, value) => rpSciFiSliderFrequency_ValueChanged(value);

                LabelFrequencyValue.Text = $"{Frequency:0.0} Hz";
                numericUpDownFrequency.Value = (decimal)Frequency;
                numericUpDownFrequency.ValueChanged += (sender, evt) => numericUpDownFrequency_ValueChanged();

                // Waveform ComboBox.
                cmbWaveforms.Width = 150;
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
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateWaveform()
        {
            SelectedWaveform = cmbWaveforms.SelectedItem switch
            {
                "AlienSignal" => t => Amplitude * RPSciFiWaveforms.AlienSignal(t * Frequency),
                "HarmonicStack" => t => Amplitude * RPSciFiWaveforms.HarmonicStack(t * Frequency),
                "Heartbeat" => t => Amplitude * RPSciFiWaveforms.Heartbeat(t * Frequency),
                "Interference" => t => Amplitude * RPSciFiWaveforms.Interference(t * Frequency),
                "Plasma" => t => Amplitude * RPSciFiWaveforms.Plasma(t * Frequency),
                "Pulsar" => t => Amplitude * RPSciFiWaveforms.Pulsar(t * Frequency),
                "Sawtooth" => t => Amplitude * RPSciFiWaveforms.Sawtooth(t * Frequency),
                "Sine" => t => Amplitude * RPSciFiWaveforms.Sine(t * Frequency),
                "Square" => t => Amplitude * RPSciFiWaveforms.Square(t * Frequency),
                "Triangle" => t => Amplitude * RPSciFiWaveforms.Triangle(t * Frequency),
                "WarpField" => t => Amplitude * RPSciFiWaveforms.WarpField(t * Frequency),
                _ => t => 0f
            };

            WaveformChanged?.Invoke(this, SelectedWaveform);

        }   // UpdateWaveform()
        #endregion


        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   numericUpDownAmplitude_ValueChanged()
        //   numericUpDownFrequency_ValueChanged()
        //   rpSciFiSliderAmplitude_ValueChanged()
        //   rpSciFiSliderFrequency_ValueChanged()
        // ---------------------------------------------------------------------

        #region .  numericUpDownAmplitude_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  numericUpDownAmplitude_ValueChanged()
        //   Description..:  Handles the numericUpDownAmplitude_ValueChanged event
        //                   to synchronize Amplitude property, Label and Slider.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void numericUpDownAmplitude_ValueChanged()
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = (int)numericUpDownAmplitude.Value;

                Amplitude = value;
                LabelAmplitudeValue.Text = $"{value:0.0}";
                rpSciFiSliderAmplitude.Value = value;
                rpSciFiSliderAmplitude.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // numericUpDownAmplitude_ValueChanged()
        #endregion


        #region .  numericUpDownFrequency_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  numericUpDownFrequency_ValueChanged()
        //   Description..:  Handles the numericUpDownFrequency_ValueChanged event
        //                   to synchronize Frequency property, Label and Slider.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void numericUpDownFrequency_ValueChanged()
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = (int)numericUpDownFrequency.Value;

                Frequency = value;
                LabelFrequencyValue.Text = $"{value:0.0} Hz";
                rpSciFiSliderFrequency.Value = value;
                rpSciFiSliderFrequency.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // numericUpDownFrequency_ValueChanged()
        #endregion


        #region .  rpSciFiSliderAmplitude_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  rpSciFiSliderAmplitude_ValueChanged()
        //   Description..:  Handles the rpSciFiSliderAmplitude_ValueChanged event
        //                   to update the Amplitude property and synchronizes the
        //                   Label and Slider.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void rpSciFiSliderAmplitude_ValueChanged(object sender, EventArgs evt)
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = rpSciFiSliderAmplitude.Value;

                Amplitude = value;
                LabelAmplitudeValue.Text = $"{value:0.0}";
                numericUpDownAmplitude.Value = value;
                numericUpDownAmplitude.Invalidate();
                rpSciFiSliderAmplitude.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // rpSciFiSliderAmplitude_ValueChanged()
        #endregion


        #region .  rpSciFiSliderFrequency_ValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  rpSciFiSliderFrequency_ValueChanged()
        //   Description..:  Handles the rpSciFiSliderFrequency_ValueChanged event
        //                   to update the Frequency property and synchronizes the
        //                   Label and Slider.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void rpSciFiSliderFrequency_ValueChanged(object sender, EventArgs evt)
        {
            // Only raise event if not suppressing.
            if (!_suppressEvents)
            {
                _suppressEvents = true;

                int value = rpSciFiSliderFrequency.Value;

                Frequency = value;
                LabelFrequencyValue.Text = $"{value:0.0} Hz";
                numericUpDownFrequency.Value = value;
                numericUpDownFrequency.Invalidate();

                _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

                _suppressEvents = false;
            }

        }   // rpSciFiSliderFrequency_ValueChanged()
        #endregion


        #region .  cmbWaveforms_SelectedIndexChanged()  --  COMMENTED OUT  .
        //// -------------------------------------------------------------------------
        ////   Method.......:  cmbWaveforms_SelectedIndexChanged()
        ////   Description..:  Handles the cmbWaveforms_SelectedIndexChanged event to
        ////                   update the selected waveform.
        ////   Parameters...:  sender - The event source.
        ////                   e      - The event arguments.
        ////   Returns......:  Nothing
        //// -------------------------------------------------------------------------
        //private void cmbWaveforms_SelectedIndexChanged()
        //{
        //    UpdateWaveform();

        //    //WaveformChanged?.Invoke(sender, SelectedWaveform);
        //    _bus?.Publish(ControlId, ControlType, "WaveformChanged", SelectedWaveform);

        //}   // cmbWaveforms_SelectedIndexChanged()
        #endregion


        private void rpSciFiSliderFrequency_ValueChanged(object sender, int e)
        {

        }

        private void rpSciFiSliderAmplitude_ValueChanged(object sender, int e)
        {

        }
    }   // class RPSciFiWaveform

}   // namespace Spirograph_v1.Controls.SciFiWaveform
