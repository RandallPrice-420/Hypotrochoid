using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiTabbedPanel
{
    public partial class RPSciFiTabbedPanel : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   TabChanged : EventHandler - Raised when the selected tab changes.
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event EventHandler TabChanged;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.TabbedPanel;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            // Publish events here.
            TabChanged += (s, e) =>
            {
                _bus?.Publish(ControlId, ControlType, "TabChanged", ControlId);
            };

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   BackColor     : Color - The background color of the control.
        //   ForeColor     : Color - The color of the text on the tabs.
        //   GlowColor     : Color - The color of the glow around the slider.
        //   SelectedIndex : int   - The index of the currently selected tab.
        //   Tabs          : List<string> - The list of tabs.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Browsable(true), Description("The background color of the control.")]
        public override Color BackColor
        {
            get => _backColor;
            set { _backColor = value; Invalidate(); }
        }

        [Category("Sci-Fi"), Description("The color of the text on the tabs."), Browsable(true)]
        public override Color ForeColor
        {
            get => _foreColor;
            set { _foreColor = value; Invalidate(); }
        }

        [Category("Sci-Fi"), Browsable(true), Description("The color of the glow around the slider.")]
        public Color GlowColor
        {
            get => _glowColor;
            set { _glowColor = value; Invalidate(); }
        }

        [Category("Sci-Fi"), Browsable(true), Description("The index of the currently selected tab.")]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = Math.Max(0, Math.Min(_tabs.Count - 1, value));
                Invalidate();

                TabChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Category("Sci-Fi"), Browsable(true), Description("The list of tabs.")]
        public List<string> Tabs
        {
            get => _tabs;
            set { _tabs = value; Invalidate(); }
        }

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _backColor     : Color - The background color of the control.
        //   _foreColor     : Color - The color of the text on the tabs.
        //   _glowColor     : Color - The color of the glow around the slider.
        //   _selectedIndex : int - The index of the currently selected tab.
        //   _tabs          : List<string> - The list of tabs.
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private Color _backColor     = Color.DarkBlue;
        private Color _foreColor     = Color.White;
        private Color _glowColor     = Color.Cyan;
        private int   _selectedIndex = 0;

        private List<string> _tabs = new();

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor(s):
        // ----------------------
        //   RPSciFiTabbedPanel() : This initializes a new instance of the
        //                          RPSciFiTabbedPanel control with default
        //                          settings.
        // ---------------------------------------------------------------------

        #region .  RPSciFiTabbedPanel()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiTabbedPanel()
        //
        //   Description..:  Initializes a new instance of the RPSciFiTabbedPanel
        //                   control with default settings.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiTabbedPanel()
        {
            DoubleBuffered = true;
            Height         = 40;
            Tabs           = ["Tab 1", "Tab 2", "Tab 3"];

            GlowColor = Color.Cyan;

            MouseClick += (s, e) =>
            {
                int tabWidth  = Width / _tabs.Count;
                SelectedIndex = e.X   / tabWidth;
            };

        }   // RPSciFiTabbedPanel()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint() : Handles the OnPaint event to draw the control.
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //
        //   Description..:  Handles the OnPaint event to draw the control.
        //
        //   Parameters...:  e - the event arguments
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

//            Color glowColor = Color.FromArgb(0, 255, 255);
            int   tabWidth  = Width / _tabs.Count;

            for (int i = 0; i < _tabs.Count; i++)
            {
                Rectangle rect = new Rectangle(i * tabWidth, 0, tabWidth, Height);

                using var bg = new LinearGradientBrush
                (
                    rect,
                    i == (_selectedIndex) ? Color.FromArgb(100, Color.Aqua) : Color.FromArgb(60, Color.Aqua),
                    i == (_selectedIndex) ? Color.FromArgb( 60, Color.Aqua) : Color.FromArgb(20, Color.Aqua),          //(5, 0, 0, 0),
                    LinearGradientMode.Vertical
                );

                g.FillRectangle(bg, rect);

                using var borderPen = new Pen(Color.FromArgb(150, GlowColor), 2);
                g.DrawRectangle(borderPen, rect);

                using var f = new Font("Segoe UI", 9, FontStyle.Bold);
                var size = g.MeasureString(_tabs[i], f);

                g.DrawString(_tabs[i], f, new SolidBrush(ForeColor), rect.X + (tabWidth - size.Width) / 2, (Height - size.Height) / 2);
            }

            base.OnPaint(e);

        }   // OnPaint()
        #endregion


    }   // class RPSciFiTabbedPanel

}   // namespace Spirograph_v1.Controls.RPSciFiTabbedPanel
