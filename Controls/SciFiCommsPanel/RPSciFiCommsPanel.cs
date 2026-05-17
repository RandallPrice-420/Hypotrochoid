using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiCommsPanel
{
    public partial class RPSciFiCommsPanel : UserControl, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   Clicked
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        //public event EventHandler Clicked;

        #endregion



        // ---------------------------------------------------------------------
        //  RPSciFi API Layer : Controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.CommsPanel;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus."), Browsable(false)]
        private RPSciFiControlBus _controlBus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _encrypted
        //   _phase
        //   _timer
        //   _wave
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private bool  _encrypted = true;
        private float _phase;

        private readonly Timer       _timer;
        private readonly List<float> _wave = new();

        #endregion



        // ---------------------------------------------------------------------
        // Public Variables:
        // -----------------
        //   EncryptedColor
        //   WaveColor
        // ---------------------------------------------------------------------

        #region .  Public Variables  .

        public Color EncryptedColor { get; set; } = Color.OrangeRed;

        public Color WaveColor      { get; set; } = Color.Lime;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor(s):
        // ----------------------
        //   RPSciFiCommsPanel()
        // ---------------------------------------------------------------------

        #region .  RPSciFiCommsPanel()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiCommsPanel()
        //   Description..:  The constructor for the RPSciFiCommsPanel class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiCommsPanel()
        {
            DoubleBuffered = true;
            BackColor      = Color.FromArgb(5, 5, 10);

            _timer = new Timer { Interval = 40 };
            _timer.Tick += (s, e) =>
            {
                _phase += 0.2f;
                float v = (float)(Math.Sin(_phase) * 0.7 + 0.3 * Math.Sin(_phase * 3));

                _wave.Add(v);
                if (_wave.Count > Width)
                {
                    _wave.RemoveAt(0);
                }

                Invalidate();
            };

            _timer.Start();

            //Clicked += (s, e) =>
            //{
            //    _encrypted = !_encrypted;
            //    Invalidate();
            //};

        }
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event.
        //   Parameters...:  e - the event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(5, 5, 10));

            Rectangle rect = ClientRectangle;
            rect.Inflate(-4, -4);

            using (Pen grid = new(Color.FromArgb(40, 0, 255, 0), 1))
            {
                for (int x = rect.Left; x < rect.Right; x += 20)
                {
                    e.Graphics.DrawLine(grid, x, rect.Top, x, rect.Bottom);
                }

                for (int y = rect.Top; y < rect.Bottom; y += 20)
                {
                    e.Graphics.DrawLine(grid, rect.Left, y, rect.Right, y);
                }
            }

            using (Pen border = new(Color.FromArgb(120, WaveColor), 2))
            {
                e.Graphics.DrawRectangle(border, rect);
            }

            if (_wave.Count > 1)
            {
                using Pen p  = new(Color.FromArgb(220, WaveColor), 2);
                float midY   = rect.Top + rect.Height / 2f;
                float scaleY = rect.Height / 2.2f;

                PointF prev = new(rect.Right, midY);
                int    idx  = _wave.Count - 1;

                for (int x = rect.Right; x >= rect.Left && idx >= 0; x--, idx--)
                {
                    float  v   = _wave[idx];
                    PointF cur = new(x, midY - v * scaleY);

                    if (x != rect.Right)
                    {
                        e.Graphics.DrawLine(p, prev, cur);
                    }
                    prev = cur;
                }
            }

            string status      = _encrypted ? "ENCRYPTED" : "OPEN CHANNEL";
            Color  statusColor = _encrypted ? EncryptedColor : WaveColor; 

            using var b = new SolidBrush(statusColor);
            e.Graphics.DrawString(status, Font, b, rect.Left + 4, rect.Top + 4);

        }   // OnPaint()
        #endregion


    }   // class RPSciFiCommsPanel

}   // namespace Spirograph_v1.Controls.RPSciFiCommsPanel
