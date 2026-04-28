using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace Spirograph_v1
{
    public class FloatTrackBar : Control
    {
        public event EventHandler ValueChanged;


        private int  _minimum    =   1;
        private int  _maximum    = 100;
        private int  _value      =   1;
        private bool _isDragging = false;

        private TickStyle _tickStyle = System.Windows.Forms.TickStyle.None;


        public int Minimum
        {
            get => this._minimum;
            set { this._minimum = value; Invalidate(); }
        }


        public int Maximum
        {
            get => this._maximum;
            set { this._maximum = value; Invalidate(); }
        }


        public TickStyle TickStyle
        {
            get => this._tickStyle;
            set { this._tickStyle = value; Invalidate(); }
        }


        public int Value
        {
            get => this._value;
            set
            {
                this._value = Math.Max(this._minimum, Math.Min(this._maximum, value));
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public FloatTrackBar()
        {
            DoubleBuffered = true;
            Height         =  40;
            Width          = 300;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle trackRect = new Rectangle(10, Height / 2 - 4, Width - 20, 8);

            // Draw track
            if (TrackBarRenderer.IsSupported)
            {
                TrackBarRenderer.DrawHorizontalTrack(e.Graphics, trackRect);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.LightGray, trackRect);
            }

            // Calculate thumb position
            float range   = this._maximum - this._minimum;
            float percent = (this._value - this._minimum) / range;
            int   thumbX  = (int)(trackRect.Left + percent * trackRect.Width);

            Rectangle thumbRect = new Rectangle(thumbX - 6, trackRect.Top - 6, 12, 20);

            // Draw thumb
            if (TrackBarRenderer.IsSupported)
            {
                TrackBarRenderer.DrawHorizontalThumb(e.Graphics, thumbRect, TrackBarThumbState.Normal);
            }
            else
            {
                e.Graphics.FillEllipse(Brushes.SteelBlue, thumbRect);
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this._isDragging = true;
            UpdateValueFromMouse(e.X);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this._isDragging)
                UpdateValueFromMouse(e.X);
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this._isDragging = false;
        }


        private void UpdateValueFromMouse(int mouseX)
        {
            Rectangle trackRect = new Rectangle(10, Height / 2 - 4, Width - 20, 8);
            float     percent   = (float)(mouseX - trackRect.Left) / trackRect.Width;

            Value = (int)(this._minimum + percent * (this._maximum - this._minimum));
        }


    }   // FloatTrackBar()

}   // Spirograph_v1
