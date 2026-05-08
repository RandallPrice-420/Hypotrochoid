using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiGroupPanel
{
    public partial class RPSciFiGroupPanel : UserControl, IRPSciFiControl
    {
        // ---------------------------------------
        //  RPSciFi API Layer.
        // ---------------------------------------

        #region .  RPSciFi API Layer  .

        public string ControlId { get; set; } = Guid.NewGuid().ToString();
        private RPSciFiControlBus _bus;


        public RPSciFiControlType ControlType => RPSciFiControlType.GroupPanel;


        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        public Color GlowColor { get; set; } = Color.Cyan;



        public RPSciFiGroupPanel()
        {
            DoubleBuffered = true;
            Padding        = new Padding(10);
            BackColor      = Color.FromArgb(10, 10, 20);

        }   // RPSciFiGroupPanel()



        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;

            if ((rect.Height <= 0) || (rect.Width <= 0))
            {
                Debug.WriteLine($"rect:  {rect.ToString()}");
            }

            rect.Inflate(-2, -2);

            using (LinearGradientBrush bg = new
            (
                rect,
                Color.FromArgb(30, 30, 50),
                Color.FromArgb(5, 5, 10),
                90f
            ))
            {
                e.Graphics.FillRectangle(bg, rect);
            }

            using (Pen p = new(Color.FromArgb(160, GlowColor), 2))
            {
                e.Graphics.DrawRectangle(p, rect);
            }

            using Pen scan = new(Color.FromArgb(20, 255, 255, 255), 1);

            for (int y = rect.Top; y < rect.Bottom; y += 6)
            {
                e.Graphics.DrawLine(scan, rect.Left, y, rect.Right, y);
            }

        }   // OnPaintBackground()


    }   // class RPSciFiGroupPanel

}   // namespace Spirograph_v1.Controls.RPSciFiGroupPanel
