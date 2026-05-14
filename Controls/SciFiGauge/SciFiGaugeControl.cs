using Spirograph_v1.Controls.SciFiSmartUserControl;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.SciFiGauge
{
    public partial class SciFiGaugeControl : RPSciFiSmartUserControl
    {
        private float _value;

        [Category("Sci-Fi"), Description("The value of the gauge (0 to 1)."), Browsable(true)]
        public float Value
        {
            get => _value;
            set
            {
                _value = Math.Max(0, Math.Min(1, value));
                Invalidate();
            }
        }


        public SciFiGaugeControl()
        {
            Size = new Size(160, 80);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = ClientRectangle;
            rect.Inflate(-10, -10);

            using var      arcPen    = new Pen(Color.FromArgb(0, 220, 255), 4f);
            using var fillBrush = new SolidBrush(Color.FromArgb(40, 0, 220, 255));

            var  startAngle = 180;
            var sweepAngle = 180 * _value;

            g.DrawArc(arcPen, rect, 180, 180);
            g.FillPie(fillBrush, rect, startAngle, sweepAngle);

        }   // OnPaint()


    }   // class SciFiGaugeControl

}   // namespace Spirograph_v1.Controls.SciFiGauge
