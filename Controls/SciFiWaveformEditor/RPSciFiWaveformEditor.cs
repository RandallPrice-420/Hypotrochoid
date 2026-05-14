using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiWaveformEditor
{
    public partial class RPSciFiWaveformEditor : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   WaveformChanged
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler WaveformChanged;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        public string ControlId { get; set; } = Guid.NewGuid().ToString();
        public RPSciFiControlType ControlType => RPSciFiControlType.WaveformEditor;

        private RPSciFiControlBus _bus;

        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Amplitude
        //   Frequency
        //   SelectedWaveform
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("RPSciFi"), Description("The amplitude of the waveform."), Browsable(true)]
        public float Amplitude { get; private set; } = 1f;


        [Category("RPSciFi"), Description("The frequency of the waveform."), Browsable(true)]
        public float Frequency { get; private set; } = 1f;


        [Browsable(false)]
        public Func<float, float> SelectedWaveform { get; private set; }

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _cmbPresets
        //   _trkAmplitude
        //   _trkFrequency
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private readonly ComboBox _cmbPresets;
        private readonly TrackBar _trkAmplitude;
        private readonly TrackBar _trkFrequency;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiWaveformEditor()
        // ---------------------------------------------------------------------

        #region .  RPSciFiWaveformEditor()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiWaveformEditor()
        //   Description..:  The constructor for the RPSciFiWaveformEditor class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiWaveformEditor()
        {
            DoubleBuffered = true;
            BackColor = Color.Black;

            _cmbPresets = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location      = new Point(10, 10),
                Width         = 180
            };
            _cmbPresets.Items.AddRange(new object[]
            {
                "AlienSignal", "HarmonicStack", "Heartbeat", "Interference",
                "Plasma",      "Pulsar",        "Sawtooth",  "Sine",
                "Square",      "Triangle",      "WarpField",
            });
            _cmbPresets.SelectedIndexChanged += (s, e) => UpdateWaveform();

            Label lblFrequency = new()
            {
                Location  = new Point(10, 45),
                ForeColor = Color.White,
                Text      = "Frequency"
            };

            _trkFrequency = new TrackBar
            {
                Location      = new Point(10, 65),
                Maximum       = 20,
                Minimum       = 1,
                TickFrequency = 1,
                Value         = 5,
                Height        = 25,
                Width         = 180
            };
            _trkFrequency.Scroll += (s, e) =>
            {
                Frequency = _trkFrequency.Value / 5f;
                WaveformChanged?.Invoke(this, EventArgs.Empty);
            };

            Label lblAmplitude = new()
            {
                Location  = new Point(10, 110),
                ForeColor = Color.White,
                Text      = "Amplitude"
            };

            _trkAmplitude = new TrackBar
            {
                Location      = new Point(10, 130),
                Maximum       = 20,
                Minimum       = 1,
                TickFrequency = 1,
                Value         = 10,
                Height        = 25,
                Width         = 180
            };
            _trkAmplitude.Scroll += (s, e) =>
            {
                Amplitude = _trkAmplitude.Value / 10f;
                WaveformChanged?.Invoke(this, EventArgs.Empty);
            };

            Controls.Add(_cmbPresets);
            Controls.Add(lblFrequency);
            Controls.Add(_trkFrequency);
            Controls.Add(lblAmplitude);
            Controls.Add(_trkAmplitude);

            Height = 170;
            Width  = 210;

            _cmbPresets.SelectedIndex = 0;

        }   // RPSciFiWaveformEditor()
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
            SelectedWaveform = _cmbPresets.SelectedItem switch
            {
                "AlienSignal"   => t => Amplitude * RPSciFiWaveforms.AlienSignal  (t * Frequency),
                "HarmonicStack" => t => Amplitude * RPSciFiWaveforms.HarmonicStack(t * Frequency),
                "Heartbeat"     => t => Amplitude * RPSciFiWaveforms.Heartbeat    (t * Frequency),
                "Interference"  => t => Amplitude * RPSciFiWaveforms.Interference (t * Frequency),
                "Plasma"        => t => Amplitude * RPSciFiWaveforms.Plasma       (t * Frequency),
                "Pulsar"        => t => Amplitude * RPSciFiWaveforms.Pulsar       (t * Frequency),
                "Sawtooth"      => t => Amplitude * RPSciFiWaveforms.Sawtooth     (t * Frequency),
                "Sine"          => t => Amplitude * RPSciFiWaveforms.Sine         (t * Frequency),
                "Square"        => t => Amplitude * RPSciFiWaveforms.Square       (t * Frequency),
                "Triangle"      => t => Amplitude * RPSciFiWaveforms.Triangle     (t * Frequency),
                "WarpField"     => t => Amplitude * RPSciFiWaveforms.WarpField    (t * Frequency),
                              _ => t => 0f
            };

            WaveformChanged?.Invoke(this, EventArgs.Empty);

        }   // UpdateWaveform()
        #endregion


        protected override bool ShowFocusCues => false;


    }   // class RPSciFiWaveformEditor

}	//	namespace Spirograph_v1.Controls.RPSciFiWaveformEditor
