using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.SciFiSlider
{
    public partial class SciFiSliderControl : UserControl
    {
        public enum GlowColorMode
        {
            NeonCyan,
            PlasmaPurple,
            ReactorGreen
        }

        private Label         lblTitle;
        private Label         lblValue;
        private TrackBar      track;
        private NumericUpDown num;
        private GlowColorMode _glowMode = GlowColorMode.NeonCyan;


        public SciFiSliderControl()
        {
            DoubleBuffered = true;
            BackColor      = Color.Black;

            lblTitle = new Label()
            {
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(10, 10)
            };

            lblValue = new Label()
            {
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 9, FontStyle.Regular),
                AutoSize  = true,
                Location  = new Point(10, 35)
            };

            track = new TrackBar()
            {
                Minimum       = 0,
                Maximum       = 100,
                TickFrequency = 10,
                Location      = new Point(10, 60),
                Width         = 200
            };

            num = new NumericUpDown()
            {
                Minimum  = 0,
                Maximum  = 100,
                Location = new Point(220, 60),
                Width    = 60
            };

            num.ValueChanged += (s, e) =>
            {
                track.Value   = (int)num.Value;
                lblValue.Text = num.Value.ToString();
                NumericValueChanged?.Invoke(this, EventArgs.Empty);
            };

            track.Scroll += (s, e) =>
            {
                num.Value     = track.Value;
                lblValue.Text = track.Value.ToString();
                SliderValueChanged?.Invoke(this, EventArgs.Empty);
            };

            Controls.Add(lblTitle);
            Controls.Add(lblValue);
            Controls.Add(track);
            Controls.Add(num);

            Height = 120;
            Width  = 300;
        }


        // ---------------------------
        // PUBLIC PROPERTIES
        // ---------------------------

        [Category("Sci-Fi")]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        [Category("Sci-Fi")]
        public int Value
        {
            get => track.Value;
            set
            {
                track.Value   = value;
                num.Value     = value;
                lblValue.Text = value.ToString();
                Invalidate();
            }
        }

        [Category("Sci-Fi")]
        public int Minimum
        {
            get => track.Minimum;
            set
            {
                track.Minimum = value;
                num.Minimum   = value;
            }
        }

        [Category("Sci-Fi")]
        public int Maximum
        {
            get => track.Maximum;
            set
            {
                track.Maximum = value;
                num.Maximum   = value;
            }
        }

        [Category("Sci-Fi")]
        public GlowColorMode GlowMode
        {
            get => _glowMode;
            set { _glowMode = value; Invalidate(); }
        }


        // ---------------------------
        // EVENTS
        // ---------------------------

        public event EventHandler SliderValueChanged;
        public event EventHandler NumericValueChanged;


        // ---------------------------
        // CUSTOM PAINT (SCI-FI LOOK)
        // ---------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

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
                                                                       Color.FromArgb(5, 0, 0, 0),
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

        }   // OnPaint()


    }   // class SciFiSliderControl

}