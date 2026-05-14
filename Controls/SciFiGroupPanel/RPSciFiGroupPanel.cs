using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiGroupPanel
{
    public partial class RPSciFiGroupPanel : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   ValueChanged  --  Not needed for this control, but included for
        //                     consistency with other controls.
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        //public event EventHandler ValueChanged;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.GroupPanel;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowColor : The glow color of the group panel.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The glow color of the group panel."), Browsable(true)]
        public Color GlowColor { get; set; } = Color.Cyan;

        #endregion



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
