using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Spirograph_v1.Controls.MySlider
{
    public partial class MySlider : UserControl
    {
        private readonly Panel         _container;
        private readonly Label         _labelTitle;
        private readonly TrackBar      _trackBar;
        private readonly NumericUpDown _numericUpDown;

        private bool          _suppressSync;
        private Label         lblTitle;
        private TrackBar      sliderFrequency;
        private Label         lblFrequencyValue;
        private NumericUpDown numUpDownFrequency;
        private ComboBox      cmbWaveforms;
        private NumericUpDown numericUpDown1;
        private Label         lblAmplitudeValue;
        private TrackBar      trackBar1;
        private Label         lblAmplitude;
        private Label lblWaveform;
        private bool          _isDragging;

        public event EventHandler ValueChanged;


        public MySlider()
        {
            _container = new Panel
            {
                Dock    = DockStyle.Fill,
                Padding = new Padding(4),
                Size    = new Size(600, 500)
            };

            _labelTitle = new Label
            {
                AutoSize = true,
                Location = new Point(4, 4),
                Font     = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text     = "MySlider"
            };

            _trackBar = new TrackBar
            {
                Location  = new Point(4, _labelTitle.Bottom + 6),
                Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize  = false,
                Height    = 30,
                Minimum   = 0,
                Maximum   = 100,
                Size      = new Size(300, 30),
                TickStyle = TickStyle.BottomRight,
            };

            _numericUpDown = new NumericUpDown
            {
                Width     = 50,
                TextAlign = HorizontalAlignment.Right,
                Anchor    = AnchorStyles.Top | AnchorStyles.Right
            };

            _container.Controls.Add(_labelTitle);
            _container.Controls.Add(_trackBar);
            _container.Controls.Add(_numericUpDown);
            Controls.Add(_container);

            Size = new Size(500, 500);
            PerformLayoutAdjust();

            _trackBar.ValueChanged      += TrackBar_ValueChanged;
            _numericUpDown.ValueChanged += NumericUpDown_ValueChanged;

            _trackBar.MouseDown += TrackBar_MouseDown;      // Click to jump
            _trackBar.MouseMove += TrackBar_MouseMove;      // Drag support
            _trackBar.MouseUp   += TrackBar_MouseUp;

            Resize += (sender, evt) => PerformLayoutAdjust();

        }   // MySlider()


        private void PerformLayoutAdjust()
        {
            const int padding = 4;
            const int gap     = 8;

            int numericWidth   = _numericUpDown.Width;
            int trackLeft      = padding;
            int availableWidth = Math.Max(0, Width - trackLeft - padding - gap - numericWidth);
            int trackWidth     = Math.Max(120, availableWidth);

            _trackBar.Location = new Point(trackLeft, _labelTitle.Bottom + 6);
            _trackBar.Width    = trackWidth;
            _trackBar.Height   = 30;

            _numericUpDown.Location = new Point(_trackBar.Right + gap, _trackBar.Top + Math.Max(0, (_trackBar.Height - _numericUpDown.Height) / 2));

            var requiredHeight = Math.Max(_trackBar.Bottom, _numericUpDown.Bottom) + 6;

            if (Height < requiredHeight) Height = requiredHeight;

        }   // PerformLayoutAdjust()


        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressSync) return;

            try
            {
                _suppressSync        = true;
                _numericUpDown.Value = Math.Min(_numericUpDown.Maximum,
                                       Math.Max(_numericUpDown.Minimum, _trackBar.Value));
            }
            finally
            {
                _suppressSync = false;
            }

            ValueChanged?.Invoke(this, EventArgs.Empty);

        }   // TrackBar_ValueChanged()


        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressSync) return;

            try
            {
                _suppressSync   = true;
                var newVal  = (int)Math.Round(_numericUpDown.Value);
                _trackBar.Value = Math.Min(_trackBar.Maximum, Math.Max(_trackBar.Minimum, newVal));
            }
            finally
            {
                _suppressSync = false;
            }

            ValueChanged?.Invoke(this, EventArgs.Empty);

        }   // NumericUpDown_ValueChanged()


        // Jump thumb to click position and start drag when left button pressed
        private void TrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _isDragging       = true;
            _trackBar.Capture = true;

            UpdateValueFromMouse(e.X);

        }   // TrackBar_MouseDown()


        // While dragging update the value
        private void TrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;

            // e.X may be outside bounds when dragging outside control; clamp in UpdateValueFromMouse
            UpdateValueFromMouse(e.X);

        }   // TrackBar_MouseMove()


        // Stop dragging
        private void TrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button != MouseButtons.Left) && (!_isDragging)) return;

            _isDragging       = false;
            _trackBar.Capture = false;

        }   // TrackBar_MouseUp()


        private void InitializeComponent()
        {
            lblTitle = new Label();
            sliderFrequency = new TrackBar();
            lblFrequencyValue = new Label();
            numUpDownFrequency = new NumericUpDown();
            cmbWaveforms = new ComboBox();
            numericUpDown1 = new NumericUpDown();
            lblAmplitudeValue = new Label();
            trackBar1 = new TrackBar();
            lblAmplitude = new Label();
            lblWaveform = new Label();
            ((ISupportInitialize)sliderFrequency).BeginInit();
            ((ISupportInitialize)numUpDownFrequency).BeginInit();
            ((ISupportInitialize)numericUpDown1).BeginInit();
            ((ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Cyan;
            lblTitle.Location = new Point(10, 45);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(84, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Frequency:";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.UseWaitCursor = true;
            // 
            // sliderFrequency
            // 
            sliderFrequency.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sliderFrequency.AutoSize = false;
            sliderFrequency.Location = new Point(7, 70);
            sliderFrequency.Margin = new Padding(0);
            sliderFrequency.Name = "sliderFrequency";
            sliderFrequency.Size = new Size(230, 25);
            sliderFrequency.TabIndex = 1;
            sliderFrequency.TickStyle = TickStyle.None;
            // 
            // lblFrequencyValue
            // 
            lblFrequencyValue.ForeColor = Color.Cyan;
            lblFrequencyValue.Location = new Point(173, 45);
            lblFrequencyValue.Name = "lblFrequencyValue";
            lblFrequencyValue.Size = new Size(60, 20);
            lblFrequencyValue.TabIndex = 2;
            lblFrequencyValue.Text = "-10.0 Hz";
            lblFrequencyValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numUpDownFrequency
            // 
            numUpDownFrequency.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            numUpDownFrequency.ForeColor = Color.Firebrick;
            numUpDownFrequency.Location = new Point(241, 67);
            numUpDownFrequency.Margin = new Padding(0);
            numUpDownFrequency.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numUpDownFrequency.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numUpDownFrequency.Name = "numUpDownFrequency";
            numUpDownFrequency.Size = new Size(55, 25);
            numUpDownFrequency.TabIndex = 3;
            numUpDownFrequency.TextAlign = HorizontalAlignment.Center;
            // 
            // cmbWaveforms
            // 
            cmbWaveforms.FormattingEnabled = true;
            cmbWaveforms.Location = new Point(100, 15);
            cmbWaveforms.Name = "cmbWaveforms";
            cmbWaveforms.Size = new Size(196, 23);
            cmbWaveforms.TabIndex = 5;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            numericUpDown1.ForeColor = Color.Firebrick;
            numericUpDown1.Location = new Point(241, 122);
            numericUpDown1.Margin = new Padding(0);
            numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(55, 25);
            numericUpDown1.TabIndex = 9;
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            // 
            // lblAmplitudeValue
            // 
            lblAmplitudeValue.ForeColor = Color.Cyan;
            lblAmplitudeValue.Location = new Point(173, 100);
            lblAmplitudeValue.Name = "lblAmplitudeValue";
            lblAmplitudeValue.Size = new Size(60, 20);
            lblAmplitudeValue.TabIndex = 8;
            lblAmplitudeValue.Text = "-10.0";
            lblAmplitudeValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // trackBar1
            // 
            trackBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            trackBar1.AutoSize = false;
            trackBar1.Location = new Point(7, 125);
            trackBar1.Margin = new Padding(0);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(230, 25);
            trackBar1.TabIndex = 7;
            trackBar1.TickStyle = TickStyle.None;
            // 
            // lblAmplitude
            // 
            lblAmplitude.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAmplitude.ForeColor = Color.Cyan;
            lblAmplitude.Location = new Point(10, 100);
            lblAmplitude.Name = "lblAmplitude";
            lblAmplitude.Size = new Size(84, 20);
            lblAmplitude.TabIndex = 6;
            lblAmplitude.Text = "Amplitude:";
            lblAmplitude.TextAlign = ContentAlignment.MiddleLeft;
            lblAmplitude.UseWaitCursor = true;
            // 
            // lblWaveform
            // 
            lblWaveform.AutoSize = true;
            lblWaveform.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWaveform.ForeColor = Color.Cyan;
            lblWaveform.Location = new Point(10, 15);
            lblWaveform.Name = "lblWaveform";
            lblWaveform.Size = new Size(83, 19);
            lblWaveform.TabIndex = 4;
            lblWaveform.Text = "Waveform:";
            lblWaveform.TextAlign = ContentAlignment.MiddleLeft;
            lblWaveform.UseWaitCursor = true;
            // 
            // MySlider
            // 
            BackColor = Color.Black;
            Controls.Add(numericUpDown1);
            Controls.Add(lblAmplitudeValue);
            Controls.Add(trackBar1);
            Controls.Add(lblAmplitude);
            Controls.Add(cmbWaveforms);
            Controls.Add(lblWaveform);
            Controls.Add(numUpDownFrequency);
            Controls.Add(lblFrequencyValue);
            Controls.Add(sliderFrequency);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Margin = new Padding(0);
            Name = "MySlider";
            Size = new Size(310, 160);
            ((ISupportInitialize)sliderFrequency).EndInit();
            ((ISupportInitialize)numUpDownFrequency).EndInit();
            ((ISupportInitialize)numericUpDown1).EndInit();
            ((ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void UpdateValueFromMouse(int mouseX)
        {
            int trackWidth = _trackBar.ClientSize.Width;
            if (trackWidth <= 0)
                return;

            // Map mouseX to 0..1 (clamped) even if outside control bounds
            float percent = (float)mouseX / trackWidth;
            percent = Math.Max(0f, Math.Min(1f, percent));

            int newValue = _trackBar.Minimum + (int)Math.Round(percent * (_trackBar.Maximum - _trackBar.Minimum));

            Value = Math.Max(_trackBar.Minimum, Math.Min(_trackBar.Maximum, newValue));
        }

        // Public API

        public string Title
        {
            get => _labelTitle.Text;
            set
            {
                _labelTitle.Text = value ?? string.Empty;
                PerformLayoutAdjust();
            }
        }

        public int Minimum
        {
            get => _trackBar.Minimum;
            set
            {
                _trackBar.Minimum = value;
                _numericUpDown.Minimum = value;
                if (_trackBar.Value < value)
                    Value = value;
            }
        }

        public int Maximum
        {
            get => _trackBar.Maximum;
            set
            {
                _trackBar.Maximum = value;
                _numericUpDown.Maximum = value;
                if (_trackBar.Value > value)
                    Value = value;
            }
        }

        public int Value
        {
            get => _trackBar.Value;
            set
            {
                var v = Math.Max(_trackBar.Minimum, Math.Min(_trackBar.Maximum, value));
                try
                {
                    _suppressSync = true;
                    _trackBar.Value = v;
                    _numericUpDown.Value = v;
                }
                finally
                {
                    _suppressSync = false;
                }

                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // Design-time exposed TrackBar properties
        [Category("TrackBar"), Description("Location of the inner TrackBar."), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Point TrackBarLocation
        {
            get => _trackBar.Location;
            set
            {
                _trackBar.Location = value;
                PerformLayoutAdjust();
            }
        }

        [Category("TrackBar"), Description("Size of the inner TrackBar."), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Size TrackBarSize
        {
            get => _trackBar.Size;
            set
            {
                _trackBar.Size = value;
                PerformLayoutAdjust();
            }
        }

        [Category("TrackBar"), Description("Minimum value of the TrackBar."), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TrackBarMinimum
        {
            get => _trackBar.Minimum;
            set
            {
                _trackBar.Minimum = value;
                _numericUpDown.Minimum = value;
                if (_trackBar.Value < value)
                    Value = value;
            }
        }

        [Category("TrackBar"), Description("Maximum value of the TrackBar."), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TrackBarMaximum
        {
            get => _trackBar.Maximum;
            set
            {
                _trackBar.Maximum = value;
                _numericUpDown.Maximum = value;
                if (_trackBar.Value > value)
                    Value = value;
            }
        }

        [Category("TrackBar"), Description("Current value of the TrackBar."), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TrackBarValue
        {
            get => _trackBar.Value;
            set => Value = value;
        }
    }

}   // namespace Spirograph_v1.Controls
