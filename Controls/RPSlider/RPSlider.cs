using System;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSlider
{
    public partial class RPSlider : UserControl
    {
        // -------------------------------------------------------------------------
        // Public Events:
        // --------------
        //   ValueChanged
        // -------------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler ValueChanged;

        #endregion



        // -------------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Title
        //   Value
        // -------------------------------------------------------------------------

        #region .  Public Properties  .

        public int Minimum
        {
            get => this._minimum;
            set { this._minimum = value; Invalidate(); }

        }   // Minimum

        public int Maximum
        {
            get => this._maximum;
            set { this._maximum = value; Invalidate(); }

        }   // Maximum

        public string Title
        {
            get => this._title;
            set { this._title = value; Invalidate(); }

        }   // Minimum

        public int Value
        {
            get => this._value;
            set
            {
                this._value = Math.Max(this._minimum, Math.Min(this._maximum, value));
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }

        }   // Value
        public NumericUpDown NumericUpDown => NumericUpDown1;

        #endregion



        // -------------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _isDragging
        //   _minimum
        //   _maximum
        //   _title
        //   _value
        // -------------------------------------------------------------------------

        #region .  Private Properties  .

        private bool   _isDragging = false;
        private int    _minimum    =   1;
        private int    _maximum    = 100;
        private int    _value      =   1;
        private string _title;

        #endregion


        public RPSlider()
        {
            InitializeComponent();
            Setup();

        }   // RPSlider()



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   Setup()
        //   NumericUpDown1_ValueChanged()
        //   RPSlider_SizeChanged()
        //   TrackBar1_MouseDown()
        //   TrackBar1_MouseMove()
        //   TrackBar1_MouseUp()
        //   TrackBar1_ValueChanged()
        //   UpdateRangeLabelLocation()
        //   RaiseValueChanged()
        //   UpdateValueFromMouse()
        // -------------------------------------------------------------------------

        private void Setup()
        {
            DoubleBuffered    = true;

            TrackBar1.Minimum = (int)NumericUpDown1.Minimum;
            TrackBar1.Maximum = (int)NumericUpDown1.Maximum;
            TrackBar1.Value   = this.Value;

            LabelTitle.Text   = "RP_Slider";
            LabelRange.Text   = $"({TrackBar1.Minimum} - {TrackBar1.Maximum})";

            UpdateRangeLabelLocation();

        }   // Setup()


        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            TrackBar1.Value = (int)NumericUpDown1.Value;
            RaiseValueChanged(TrackBar1.Value);

        }   // NumericUpDown1_ValueChanged()


        private void RPSlider_SizeChanged(object sender, EventArgs e)
        {
            UpdateRangeLabelLocation();

        }   // RPSlider_SizeChanged()


        private void TrackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this._isDragging = true;

            UpdateValueFromMouse(e.X);

        }   // TrackBar1_MouseDown()


        private void TrackBar1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this._isDragging)
            {
                UpdateValueFromMouse(e.X);
            }

        }   // TrackBar1_MouseMove()


        private void TrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this._isDragging = false;

        }   // TrackBar1_MouseUp()


        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown1.Value = TrackBar1.Value;
            RaiseValueChanged((int)NumericUpDown1.Value);

        }   // TrackBar1_ValueChanged()


        private void UpdateRangeLabelLocation()
        {
            LabelRange.Location = new Point(TrackBar1.Width - LabelRange.Width - 3, LabelRange.Location.Y);

        }   // TrackBar1_ValueChanged()


        private void RaiseValueChanged(int newValue)
        {
            this.Value = newValue;
            ValueChanged?.Invoke(this, EventArgs.Empty);

        }   // RaiseValueChanged()


        private void UpdateValueFromMouse(int mouseX)
        {
            Rectangle trackRect = new(10, TrackBar1.Height / 2 - 4, TrackBar1.Width - 20, 8);
            float     percent   = (float)(mouseX - trackRect.Left) / trackRect.Width;
            int       value     = (int)(TrackBar1.Minimum + percent * (TrackBar1.Maximum - TrackBar1.Minimum));

            TrackBar1.Value = Math.Max(TrackBar1.Minimum, Math.Min(TrackBar1.Maximum, value));

            this.Value = TrackBar1.Value;
            RaiseValueChanged(this.Value);

        }   // UpdateValueFromMouse()


    }   // class RPSlider

}   // namespace Spirograph_v1.Controls.RPSlider
