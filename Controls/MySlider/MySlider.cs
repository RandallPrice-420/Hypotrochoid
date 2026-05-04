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

        private bool _suppressSync;
        private bool _isDragging;

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
                Height    =  30,
                Minimum   =   0,
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

            Size = new Size(500,500);
            PerformLayoutAdjust();

            _trackBar.ValueChanged += TrackBar_ValueChanged;
            _numericUpDown.ValueChanged += NumericUpDown_ValueChanged;

            // Click to jump
            _trackBar.MouseDown += TrackBar_MouseDown;
            // Drag support
            _trackBar.MouseMove += TrackBar_MouseMove;
            _trackBar.MouseUp += TrackBar_MouseUp;

            Resize += (_, __) => PerformLayoutAdjust();
        }

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
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressSync)
                return;
            try
            {
                _suppressSync = true;
                _numericUpDown.Value = Math.Min(_numericUpDown.Maximum, Math.Max(_numericUpDown.Minimum, _trackBar.Value));
            }
            finally { _suppressSync = false; }

            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressSync)
                return;
            try
            {
                _suppressSync = true;
                var newVal = (int)Math.Round(_numericUpDown.Value);
                _trackBar.Value = Math.Min(_trackBar.Maximum, Math.Max(_trackBar.Minimum, newVal));
            }
            finally { _suppressSync = false; }

            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        // Jump thumb to click position and start drag when left button pressed
        private void TrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _isDragging = true;
            _trackBar.Capture = true;

            UpdateValueFromMouse(e.X);
        }

        // While dragging update the value
        private void TrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
                return;

            // e.X may be outside bounds when dragging outside control; clamp in UpdateValueFromMouse
            UpdateValueFromMouse(e.X);
        }

        // Stop dragging
        private void TrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && !_isDragging)
                return;

            _isDragging = false;
            _trackBar.Capture = false;
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
