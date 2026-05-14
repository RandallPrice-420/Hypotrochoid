using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiToggleSwitch
{
    public partial class RPSciFiToggleSwitch : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   Toggled
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler Toggled;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : All controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.ToggleSwitch;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            // Publish events here.
            Toggled += (s, e) =>
            {
                _bus?.Publish(ControlId, ControlType, "Toggled", IsOn);
            };

        }   // Register()

        #endregion



        public bool   IsOn      { get; set; }

        public Color  OffColor  { get; set; } = Color.Red;

        public Color  OnColor   { get; set; } = Color.Lime;


        public RPSciFiToggleSwitch()
        {
            DoubleBuffered = true;
            Size           = new Size(60, 26);
            Cursor         = Cursors.Hand;

            Toggled += (s, e) =>
            {
                // You can add additional logic here if needed when the toggle state changes.
            };
        }


        protected override void OnClick(EventArgs e)
        {
            IsOn = !IsOn;
            Invalidate();

            Toggled?.Invoke(this, EventArgs.Empty);

            base.OnClick(e);

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect   = new(0, 0, Width - 1, Height - 1);
            int       radius = rect.Height;

            using (GraphicsPath gp = new())
            {
                gp.AddArc(rect.Left,           rect.Top, radius, radius,  90, 180);
                gp.AddArc(rect.Right - radius, rect.Top, radius, radius, 270, 180);
                gp.CloseFigure();

                Color c = (IsOn) ? OnColor : OffColor;

                using (LinearGradientBrush bg = new
                (
                    rect,
                    Color.FromArgb(40, c),
                    Color.FromArgb(5, 0, 0, 0),
                    0f
                ))
                {
                    e.Graphics.FillPath(bg, gp);
                }

                using (Pen p = new(Color.FromArgb(180, c), 2))
                {
                    e.Graphics.DrawPath(p, gp);
                }

                int knobSize = rect.Height - 4;
                int knobX    = (IsOn) ? rect.Right - knobSize - 2 : rect.Left + 2;

                Rectangle knob = new(knobX, rect.Top + 2, knobSize, knobSize);

                using (SolidBrush b = new(Color.FromArgb(220, Color.White)))
                {
                    e.Graphics.FillEllipse(b, knob);
                }

                using (Pen p = new(Color.FromArgb(200, c), 2))
                {
                    e.Graphics.DrawEllipse(p, knob);
                }
            }

        }   // OnPaint()


    }   // class RPSciFiToggleSwitch

}   // namespace Spirograph_v1.Controls.RPSciFiToggleSwitch
