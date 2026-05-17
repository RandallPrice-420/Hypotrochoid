using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiFreqSpecAnalyzer
{
    public partial class RPSciFiFreqSpecAnalyzer : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   ValueChanged  --  COMMENTED OUT
        // ---------------------------------------------------------------------

        #region .  Public Events  --  COMMENTED OUT  .

        //public event EventHandler ValueChanged;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : Controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.FreqSpecAnalyzer;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus."), Browsable(false)]
        private RPSciFiControlBus _controlBus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   SampleCount : The number of samples for the frequency spectrum analyzer.
        //
        //   Waveform    : The waveform function to analyze, where the input is time
        //                 (0 to 1) and the output is amplitude (-1 to 1).
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The number of samples for the frequency spectrum analyzer."), Browsable(true)]
        public int SampleCount { get; set; } = 256;

        [Category("Sci-Fi"), Description("The waveform function to analyze, where the input is time (0 to 1) and the output is amplitude (-1 to 1)."), Browsable(false)]
        // Must be Browsable(false) to avoid serialization issues with Func<>
        public Func<float, float> Waveform { get; set; }

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiFreqSpecAnalyzer()
        // ---------------------------------------------------------------------

        #region .  RPSciFiFreqSpecAnalyzer()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiFreqSpecAnalyzer()
        //   Description..:  The constructor for the RPSciFiFreqSpecAnalyzer class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiFreqSpecAnalyzer()
        {
            DoubleBuffered = true;
            BackColor = Color.Black;

            _controlBus.OnEvent += evt =>
            {
                Console.WriteLine($"{evt.ControlId} fired {evt.EventName} = {evt.Value}");
            };

        }   // RPSciFiFreqSpecAnalyzer()()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Waveform == null || SampleCount <= 0) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float[] samples = new float[SampleCount];

            for (int i = 0; i < SampleCount; i++)
            {
                float t = i / (float)SampleCount;
                samples[i] = Waveform(t);
            }

            Complex[] spectrum = new Complex[SampleCount];

            for (int k = 0; k < SampleCount; k++)
            {
                Complex sum = Complex.Zero;
				
                for (int n = 0; n < SampleCount; n++)
                {
                    double angle = -2.0 * Math.PI * k * n / SampleCount;
                    sum += samples[n] * new Complex(Math.Cos(angle), Math.Sin(angle));
                }
				
                spectrum[k] = sum;
            }

            float   maxMag = 0;
            float[] mags   = new float[SampleCount / 2];
			
            for (int i = 0; i < mags.Length; i++)
            {
                mags[i] = (float)spectrum[i].Magnitude;

                if (mags[i] > maxMag) maxMag = mags[i];
            }
			
            if (maxMag <= 0) maxMag = 1;

            int   bars     = mags.Length;
            float barWidth = Width / (float)bars;

            using var barBrush = new LinearGradientBrush
			(
                new Rectangle(0, 0, Width, Height),
                Color.FromArgb(0, 255, 255),
                Color.FromArgb(0, 100, 255),
                LinearGradientMode.Vertical
			);

            for (int i = 0; i < bars; i++)
            {
                float norm = mags[i] / maxMag;
                float h    = norm * Height;
                float x    = i * barWidth;
                float y    = Height - h;

                g.FillRectangle(barBrush, x, y, barWidth - 1, h);
            }

            using var border = new Pen(Color.FromArgb(150, 0, 255, 255), 2);
            g.DrawRectangle(border, 1, 1, Width - 3, Height - 3);

            base.OnPaint(e);
			
        }	// OnPaint()
        #endregion
		
		
    }   // class RPSciFiFreqSpecAnalyzer

}	// namespace Spirograph_v1.Controls.RPSciFiFreqSpecAnalyzer
