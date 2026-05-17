using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiTerminal
{
    public partial class RPSciFiTerminal : UserControl, IRPSciFiControl
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
        //  RPSciFi API Layer : Controls must implement this interface to be
        //                      compatible with the RPSciFi system.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description("The unique identifier for the control."), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.Terminal;


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
        // Public Properties:
        // ------------------
        //   CursorColor : Color - Color of the cursor in the terminal.
        //   TextColor   : Color of the text in the terminal.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The color of the cursor in the terminal."), Browsable(true)]                      
        public Color CursorColor { get; set; } = Color.Lime;


        [Category("Sci-Fi"), Description("The color of the text in the terminal."), Browsable(true)]
        public Color TextColor { get; set; } = Color.Lime;

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _lines
        //   _currentInput
        //   _cursorTimer
        //   _cursorVisible
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private List<string> _lines         = [];
        private string       _currentInput  = "";
        private Timer        _cursorTimer;
        private bool         _cursorVisible = true;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiTerminal()
        // ---------------------------------------------------------------------

        #region .  RPSciFiTerminal()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiTerminal()
        //   Description..:  The constructor for the RPSciFiTerminal class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiTerminal()
        {
            DoubleBuffered = true;
            BackColor      = Color.Black;
            ForeColor      = TextColor;
            Font           = new Font("Consolas", 12);

            _cursorTimer = new Timer { Interval = 500 };
            _cursorTimer.Tick += (s, e) =>
            {
                _cursorVisible = !_cursorVisible;
                Invalidate();
            };

            _cursorTimer.Start();

            KeyDown += Terminal_KeyDown;

        }   // RPSciFiTerminal()
        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   WriteLine()
        // ---------------------------------------------------------------------

        #region .  WriteLine()  .
        // ---------------------------------------------------------------------
        //   Method.......:  WriteLine()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void WriteLine(string text)
        {
            _lines.Add(text);

            Invalidate();

        }   // WriteLine()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        //   Terminal_KeyDown()
        // ---------------------------------------------------------------------

        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            int y = 5;
            foreach (var line in _lines)
            {
                e.Graphics.DrawString(line, Font, new SolidBrush(TextColor), 5, y);
                y += Font.Height + 2;
            }

            string cursor = _cursorVisible ? "_" : " ";
            e.Graphics.DrawString("> " + _currentInput + cursor, Font, new SolidBrush(CursorColor), 5, y);

            base.OnPaint(e);

        }   // OnPaint()
        #endregion


        #region .  Terminal_KeyDown()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Terminal_KeyDown()
        //   Description..:  Handles the Terminal_KeyDown event to ?
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Terminal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (_currentInput.Length > 0)
                {
                    _currentInput = _currentInput[..^1];
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                _lines.Add("> " + _currentInput);
                _controlBus?.Publish(ControlId, ControlType, "Command", _currentInput);
                _currentInput = "";
            }
            else
            {
                char c = (char)e.KeyValue;
                if (!char.IsControl(c))
                {
                    _currentInput += c;
                }
            }

            Invalidate();

        }   // Terminal_KeyDown()
        #endregion


    }   // class RPSciFiTerminal

}   // namespace Spirograph_v1.Controls.RPSciFiTerminal
