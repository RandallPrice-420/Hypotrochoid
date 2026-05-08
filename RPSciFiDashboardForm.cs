using Spirograph_v1.Controls.RPSciFiButton;
using Spirograph_v1.Controls.RPSciFiGroupPanel;
using Spirograph_v1.Controls.RPSciFiGuage;
using Spirograph_v1.Controls.RPSciFiKnob;
using Spirograph_v1.Controls.RPSciFiOscilloscope;
using Spirograph_v1.Controls.RPSciFiRadar;
using Spirograph_v1.Controls.RPSciFiSlider;
using Spirograph_v1.Controls.RPSciFiToggleSwitch;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace Spirograph_v1
{
    public partial class RPSciFiDashboardForm : Form
    {
        private RPSciFiButton         _btnBridge;
        private RPSciFiButton         _btnTestControls;

        private RPSciFiBridgeForm     _bridgeForm;
        private Form_TestUserControls _testControlsForm;

        private RPSciFiGroupPanel     _bottomDock;
        private RPSciFiGroupPanel     _centerDock;
        private RPSciFiGroupPanel     _leftDock;
        private RPSciFiGroupPanel     _rightDock;

        private RPSciFiGauge          _gaugePower;
        private RPSciFiGauge          _gaugeReactor;

        private RPSciFiOscilloscope   _oscilloscope;

        private RPSciFiButton         _btnWarp;
        private RPSciFiButton         _btnImpulse;

        private RPSciFiToggleSwitch   _toggleRedAlert;

        private RPSciFiSlider         _sliderPeaks;
        private RPSciFiSlider         _sliderPhase;
        private RPSciFiSlider         _sliderThrottle;

        private RPSciFiKnob           _knobShieldFreq;

        private RPSciFiRadar          _radarScanner;

        private Timer                 _hudTimer;
        private float                 _hudPhase;


        public RPSciFiDashboardForm()
        {
            Text           = "Starship Bridge – Sci‑Fi Dashboard";
            BackColor      = Color.FromArgb(5, 5, 12);
            Size           = new Size(1680, 1002);
            WindowState    = FormWindowState.Maximized;
            DoubleBuffered = true;

            BuildLayout();
            BuildHudTimer();

            // Subscribe to SizeChanged event.
            this.SizeChanged += Form_SizeChanged;

        }   // RPSciFiDashboardForm()



        private void BuildLayout()
        {
            #region .  Left Dock  .
            // ---------------------------------------------
            // LEFT DOCK – Main Power
            //             Reactor Load
            //             Warp DFrive
            //             Impulse
            // ---------------------------------------------
            _leftDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Cyan,
                Dock      = DockStyle.Left,
                Width     = 320
            };
            Controls.Add(_leftDock);

            // -------------------------
            // MAIN POWER
            // -------------------------
            _gaugePower = new RPSciFiGauge()
            {
                LabelText = "MAIN  POWER",
                GlowColor = Color.Cyan,
                Location  = new Point(40, 40)
            };
            _leftDock.Controls.Add(_gaugePower);

            // -------------------------
            // REACTOR LOAD
            // -------------------------
            _gaugeReactor = new RPSciFiGauge()
            {
                LabelText = "REACTOR  LOAD",
                GlowColor = Color.Orange,
                Location  = new Point(40, 220)
            };
            _leftDock.Controls.Add(_gaugeReactor);


            // -------------------------
            // WARP DRIVE
            // -------------------------
            _btnWarp = new RPSciFiButton()
            {
                Text      = "WARP DRIVE",
                Font      = new Font(Font.FontFamily, 16),
                GlowColor = Color.Cyan,
                Location  = new Point(40, 400),
                Size      = new Size(150, 50),
            };
            _btnWarp.Click += (s, e) =>
            {
                _gaugePower  .Value = (_gaugePower  .Value == 50f) ? 90f : 50f;
                _gaugeReactor.Value = (_gaugeReactor.Value == 50f) ? 80f : 50f;
            };
            _leftDock.Controls.Add(_btnWarp);


            // -----------------------------------
            // IMPULSE
            // -----------------------------------
            _btnImpulse = new RPSciFiButton()
            {
                Text      = "IMPULSE",
                Font      = new Font(Font.FontFamily, 16),
                GlowColor = Color.Orange,
                Location  = new Point(40, 470),
                Size      = new Size(150, 50),
            };
            _btnImpulse.Click += (s, e) =>
            {
                _gaugePower  .Value = (_gaugePower  .Value == 10f) ? 30f : 10f;
                _gaugeReactor.Value = (_gaugeReactor.Value == 20f) ? 35f : 20f;
            };
            _leftDock.Controls.Add(_btnImpulse);
            #endregion


            #region .  Right Dock  .
            // -----------------------------------------------------------------
            // RIGHT DOCK – Oscilloscope
            //              Phase
            //              Peaks
            //              Frequency
            //              Red Alert
            //              Radar
            // -----------------------------------------------------------------
            _rightDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Magenta,
                Dock      = DockStyle.Right,
                Width     = 360
            };
            Controls.Add(_rightDock);


            // -----------------------------------
            // OSCILLOSCOPE
            // -----------------------------------
            _oscilloscope = new RPSciFiOscilloscope()
            {
                GlowColor = Color.Lime,
                Location  = new Point(30, 30),
                Size      = new Size(300, 140)
            };
            _rightDock.Controls.Add(_oscilloscope);


            // -----------------------------------
            // PHASE
            // -----------------------------------
            var lblPhase = new Label()
            {
                Text      = "PHASE",
                AutoSize  = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location  = new Point(30, 200),
            };
            _rightDock.Controls.Add(lblPhase);

            _sliderPhase = new RPSciFiSlider()
            {
                GlowColor = Color.Cyan,
                Location  = new Point(30, 220),
                Size      = new Size(300, 40),
                Minimum   = 0,
                Maximum   = 100,
                Value     = 30
            };
            _rightDock.Controls.Add(_sliderPhase);
            _sliderPhase.ValueChanged += SliderPhase_ValueChanged;


            // -----------------------------------
            // PEAKS
            // -----------------------------------
            var lblPeaks = new Label()
            {
                Text      = "PEAKS",
                AutoSize  = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location  = new Point(30, 280),
            };
            _rightDock.Controls.Add(lblPeaks);

            _sliderPeaks = new RPSciFiSlider()
            {
                GlowColor = Color.Cyan,
                Location  = new Point(30, 300),
                Size      = new Size(300, 40),
                Minimum   = 1,
                Maximum   = 5,
                Value     = 3
            };
            _rightDock.Controls.Add(_sliderPeaks);
            _sliderPeaks.ValueChanged += SliderPeaks_ValueChanged;


            // -----------------------------------
            // FREQUENCY
            // -----------------------------------
            var lblShield = new Label()
            {
                Text      = "SHIELD  FREQ",
                AutoSize  = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location  = new Point(30, 365),
            };
            _rightDock.Controls.Add(lblShield);

            _knobShieldFreq = new RPSciFiKnob()
            {
                BackColor = Color.Transparent,
                GlowColor = Color.Cyan,
                Location  = new Point(30, 390),
                Size      = new Size(80, 80),
                Minimum   = 0,
                Maximum   = 100,
                Value     = 40
            };
            _rightDock.Controls.Add(_knobShieldFreq);


            // -----------------------------------
            // RED ALERT
            // -----------------------------------
            var lblAlert = new Label()
            {
                Text      = "RED  ALERT",
                AutoSize  = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location  = new Point(30, 490),
            };
            _rightDock.Controls.Add(lblAlert);

            _toggleRedAlert = new RPSciFiToggleSwitch()
            {
                Location = new Point(30, 510),
                OnColor  = Color.Red,
                OffColor = Color.DarkRed
            };
            _rightDock.Controls.Add(_toggleRedAlert);


            // -----------------------------------
            // RADAR
            // -----------------------------------
            var lblRadar = new Label()
            {
                Text      = "RADAR  SCANNER",
                AutoSize  = false,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location  = new Point(150, 365),
                Size      = new Size(150, 23),
                TextAlign = ContentAlignment.TopCenter,
            };
            _rightDock.Controls.Add(lblRadar);

            _radarScanner = new RPSciFiRadar()
            {
                BackColor    = Color.Transparent,
                ContactColor = Color.Cyan,
                GridColor    = Color.FromArgb(40, 255, 255, 255),
                SweepColor   = Color.Lime,
                Location     = new Point(150, 390),
                Size         = new Size(150, 150),
            };
            _rightDock.Controls.Add(_radarScanner);
            #endregion


            #region .  Bottom Dock  .
            // -----------------------------------------------------------------
            // BOTTOM DOCK – Throttle
            //               Bridge Button
            //               Test Controls
            // -----------------------------------------------------------------
            _bottomDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Cyan,
                Dock      = DockStyle.Bottom,
                Height    = 160
            };
            Controls.Add(_bottomDock);


            // -----------------------------------
            // THROTTLE
            // -----------------------------------
            var lblThrottle = new Label()
            {
                Text      = "THROTTLE",
                AutoSize  = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location  = new Point(40, 30),
            };
            _bottomDock.Controls.Add(lblThrottle);

            _sliderThrottle = new RPSciFiSlider()
            {
                GlowColor = Color.Cyan,
                Location  = new Point(40, 60),
                Size      = new Size(400, 40),
                Minimum   = 0,
                Maximum   = 100,
                Value     = 30
            };
            _bottomDock.Controls.Add(_sliderThrottle);


            // -----------------------------------
            // BRIDGE FORM
            // -----------------------------------
            _btnBridge = new()
            {
                Text     = "BRIDGE",
                Font     = new Font(Font.FontFamily, 16),
                Location = new Point(1740, 15),
                Size     = new Size(160, 50),
            };
            _bottomDock.Controls.Add(_btnBridge);

            _btnBridge.Click += (s, e) =>
            {
                if (!IsFormOpen<RPSciFiBridgeForm>())
                {
                    // Open the form.
                    _bridgeForm = new RPSciFiBridgeForm();
                    _bridgeForm.Show();
                }
                else
                {
                    // Optionally bring the existing form to front
                    RPSciFiBridgeForm bridgeForm = Application.OpenForms.OfType<RPSciFiBridgeForm>().FirstOrDefault();
                    if (bridgeForm != null)
                    {
                        bridgeForm.BringToFront();
                    }
                }
            };


            // -----------------------------------
            // TEST CONTROLS FORM
            // -----------------------------------
            _btnTestControls = new()
            {
                Text     = "Test Controls",
                Font     = new Font(Font.FontFamily, 16),
                Location = new Point(1740, 85),
                Size     = new Size(160, 50),
            };
            _bottomDock.Controls.Add(_btnTestControls);
            
            _btnTestControls.Click += (s, e) =>
            {
                _testControlsForm = new Form_TestUserControls();
                _testControlsForm.ShowDialog();
            };
            #endregion


            #region .  Center Dock  .
            // ---------------------------------------------
            // Center DOCK – HUD
            // ---------------------------------------------
            _centerDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Cyan,
                Dock      = DockStyle.Fill
            };
            Controls.Add(_centerDock);
            #endregion

            // Center dock background is main viewport (HUD overlay will sit over it)
        }


        #region .  OnPaint()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        #endregion
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawHudOverlay(e.Graphics, _centerDock.ClientRectangle);

            base.OnPaint(e);

        }   // OnPaint()



        #region .  Form_SizeChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  Form_SizeChanged()
        //   Description..:  Handles the Form_SizeChanged event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void Form_SizeChanged(object sender, EventArgs e)
        {
            // Force all controls to repaint when this form size changes.
            foreach (Control c in Controls)
            {
                c.Refresh();
            }

        }   // Form_SizeChanged()
        #endregion



        #region .  SliderPeaks_ValueChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  SliderPeaks_ValueChanged()
        //   Description..:  Handles the SliderPeaks_ValueChanged event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void SliderPeaks_ValueChanged(object sender, EventArgs e)
        {
            _oscilloscope.Peaks = ((RPSciFiSlider)sender).Value;
        }
        #endregion



        #region .  SliderPhase_ValueChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  SliderPhase_ValueChanged()
        //   Description..:  Handles the Name event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void SliderPhase_ValueChanged(object sender, EventArgs e)
        {
            _oscilloscope.Phase = ((RPSciFiSlider)sender).Value;

        }   // SliderPhase_ValueChanged()
        #endregion



        private void BuildHudTimer()
        {
            _hudTimer = new Timer { Interval = 40 };

            _hudTimer.Tick += (s, e) =>
            {
                _hudPhase += 0.03f;
                Invalidate();
            };

            _hudTimer.Start();

        }   // BuildHudTimer()


        private void DrawHudOverlay(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath gp = new())
            {
                int margin = 20;

                Rectangle hudRect = new
                (
                    rect.Left   + margin,
                    rect.Top    + margin,
                    rect.Width  - margin * 2,
                    rect.Height - margin * 2
                );

                gp.AddRectangle(hudRect);

                using (Pen p = new(Color.FromArgb(60, Color.Cyan), 2))
                {
                    g.DrawPath(p, gp);
                }

                // corner brackets
                int br = 40;

                using (Pen p = new(Color.FromArgb(120, Color.Cyan), 3))
                {
                    // TL
                    g.DrawLine(p, hudRect.Left,       hudRect.Top,          hudRect.Left + br,  hudRect.Top);
                    g.DrawLine(p, hudRect.Left,       hudRect.Top,          hudRect.Left,       hudRect.Top + br);

                    // TR
                    g.DrawLine(p, hudRect.Right - br, hudRect.Top,          hudRect.Right,      hudRect.Top);
                    g.DrawLine(p, hudRect.Right,      hudRect.Top,          hudRect.Right,      hudRect.Top + br);

                    // BL
                    g.DrawLine(p, hudRect.Left,       hudRect.Bottom - br,  hudRect.Left,       hudRect.Bottom);
                    g.DrawLine(p, hudRect.Left,       hudRect.Bottom,       hudRect.Left + br,  hudRect.Bottom);

                    // BR
                    g.DrawLine(p, hudRect.Right - br, hudRect.Bottom,       hudRect.Right,       hudRect.Bottom);
                    g.DrawLine(p, hudRect.Right,      hudRect.Bottom - br,  hudRect.Right,       hudRect.Bottom);
                }

                // moving scanline
                int scanY = hudRect.Top + (int)((Math.Sin(_hudPhase) * 0.5 + 0.5) * hudRect.Height);

                using (Pen scan = new(Color.FromArgb(40, Color.Cyan), 2))
                {
                    g.DrawLine(scan, hudRect.Left, scanY, hudRect.Right, scanY);
                }

                // subtle radial HUD glow
                using GraphicsPath circle = new();
                int r = Math.Min(hudRect.Width, hudRect.Height) / 2;

                Rectangle cRect = new
                (
                    hudRect.Left + hudRect.Width / 2 - r,
                    hudRect.Top + hudRect.Height / 2 - r,
                    r * 2,
                    r * 2
                );
                circle.AddEllipse(cRect);

                using PathGradientBrush pgb = new(circle);

                pgb.CenterColor    = Color.FromArgb(40, Color.Cyan);
                pgb.SurroundColors = [Color.FromArgb(0, 0, 0, 0)];

                g.FillPath(pgb, circle);
            }

        }   //, DrawHudOverlat()


        #region .  IsFormOpen()  .
        // -------------------------------------------------------------------------
        //   Method.......:  IsFormOpen()
        //   Description..:  Check if a form of a given type is already open.
        //   Parameters...:  T - the form control to check.
        //   Returns......:  bool - TRUE if the form is open; otherwise, FALSE.
        // -------------------------------------------------------------------------
        // 
        private bool IsFormOpen<T>() where T : Form
        {
            bool isOpen = Application.OpenForms.OfType<T>().Any();

            return isOpen;

        }   // IsFormOpen()
        #endregion



        // -------------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaintBackground()
        // -------------------------------------------------------------------------

        #region .  OnPaintBackground()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaintBackground()
        //   Description..:  
        //   Parameters...:  
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics  g    = e.Graphics;
            Rectangle rect = this.ClientRectangle;

            DrawHudOverlay(g, rect);

        }   // OnPaintBackground()
        #endregion


    }   // class RPSciFiDashboardForm

}   // namespace Spirograph_v1
