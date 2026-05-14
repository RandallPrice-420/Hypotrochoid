using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiOscilloscope2
{
    public partial class RPSciFiOscilloscope2 : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.Oscilloscope2;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        // --------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Amplitude
        //   Frequency
        //   ScrollSpeed
        //   Waveform
        // --------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The amplitude of the waveform."), Browsable(true)]
        public float Amplitude   { get; set; } = 1f;


        [Category("Sci-Fi"), Description("The frequency of the waveform."), Browsable(true)]
        public float Frequency   { get; set; } = 1f;


        [Category("Sci-Fi"), Description("The scroll speed of the waveform in pixels per second."), Browsable(true)]
        public float ScrollSpeed { get; set; } = 200f;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Sci-Fi"), Description("The waveform function for the oscilloscope."), Browsable(false)]
        public Func<float, float> Waveform { get; set; }
        //public Func<float, float> Waveform
        //{
        //    get;
        //    set
        //    {
        //        if (value != null)
        //        {
        //            field = value;
        //            Invalidate();
        //        }
        //    }
        //}

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _anim
        //   _wavePen
        //   _sw
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private readonly Timer     _anim;
        private readonly Pen       _wavePen;
        private readonly Stopwatch _sw = Stopwatch.StartNew();

        #endregion



        // --------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiOscilloscope2()
        // --------------------------------------------------------------------

        #region .  RPSciFiOscilloscope2()  .
        // --------------------------------------------------------------------
        //   Method.......:  RPSciFiOscilloscope2()
        //   Description..:  The constructor for the RPSciFiOscilloscope2 class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        public RPSciFiOscilloscope2()
        {
            DoubleBuffered = true;

            _wavePen = new Pen(Color.FromArgb(255, 0, 255, 255), 2);

            // Default waveform: sine wave
            Waveform = t => (float)Math.Sin(t * 6.28318f) + (float)Math.Cos(t * 0.3F);

            _anim = new Timer { Interval = 1 };
            _anim.Tick += (s, e) => Invalidate();
            _anim.Start();
			
        }   // RPSciFiOscilloscope2()
        #endregion



        // --------------------------------------------------------------------
        // Public Control Events:
        // ----------------------
        //   OnAmplitudeChanged()  --  COMMENTED OUT
        //   OnFrequencyChanged()  --  COMMENTED OUT
        //   OnWaveformChanged()   --  COMMENTED OUT
        // --------------------------------------------------------------------

        #region .  OnAmplitudeChanged()  --  COMMENTED OUT  .
        //// --------------------------------------------------------------------
        ////   Method.......:  OnAmplitudeChanged()
        ////   Description..:  Handles the OnAmplitudeChanged event to ?
        ////   Parameters...:  sender - The event source.
        ////                   e      - The event arguments.
        ////   Returns......:  Nothing
        //// --------------------------------------------------------------------
        //public void OnAmplitudeChanged(object sender, int value)
        //{
        //    Invalidate();

        //}   // OnAmplitudeChanged()
        #endregion


        #region .  OnFrequencyChanged()  --  COMMENTED OUT  .
        //// --------------------------------------------------------------------
        ////   Method.......:  OnFrequencyChanged()
        ////   Description..:  Handles the OnFrequencyChanged event to ?
        ////   Parameters...:  sender - The event source.
        ////                   e      - The event arguments.
        ////   Returns......:  Nothing
        //// --------------------------------------------------------------------
        //public void OnFrequencyChanged(object sender, int value)
        //{
        //    Invalidate();

        //}   // OnFrequencyChanged()
        #endregion


        #region .  OnWaveformChanged()  --  COMMENTED OUT  .
        //// --------------------------------------------------------------------
        ////   Method.......:  OnWaveformChanged()
        ////   Description..:  Handles the OnWaveformChanged event to ?
        ////   Parameters...:  sender - The event source.
        ////                   e      - The event arguments.
        ////   Returns......:  Nothing
        //// --------------------------------------------------------------------
        //public void OnWaveformChanged(object sender, Func<float, float> waveform)
        //{
        //    Waveform = waveform;

        //    Invalidate();

        //}   // OnWaveformChanged()
        #endregion



        // --------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // --------------------------------------------------------------------

        #region .  OnPaint()  .
        // --------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // --------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float  time   = _sw.ElapsedMilliseconds / 1000f;
            float  offset = time * ScrollSpeed;
            float  mid    = Height / 2f;
            float  scale  = Height / 3f;
            PointF prev   = new(0, mid);

            for (int x = 1; x < Width; x++)
            {
                float sampleT = (x + offset) * 0.01f;
                float y       = mid + Waveform(sampleT) * scale;
                PointF next   = new(x, y);
				
                g.DrawLine(_wavePen, prev, next);
				
                prev = next;
            }

            base.OnPaint(e);

        }	// OnPaint()
        #endregion


    }   // class RPSciFiOscilloscope2

}	// namespace Spirograph_v1.Controls.RPSciFiOscilloscope2
