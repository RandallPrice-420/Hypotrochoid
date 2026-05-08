using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiTerminal
{
    public partial class RPSciFiTerminal : UserControl, IRPSciFiControl
    {
        // ---------------------------------------
        //  RPSciFi API Layer
        // ---------------------------------------

        public string ControlId { get; set; } = Guid.NewGuid().ToString();
        public RPSciFiControlType ControlType => RPSciFiControlType.GroupPanel;
        private RPSciFiControlBus _bus;

        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);
        }


        // ---------------------------------------
        //  Terminal Logic
        // ---------------------------------------

        private List<string> _lines         = [];
        private string       _currentInput  = "";
        private Timer        _cursorTimer;
        private bool         _cursorVisible = true;



        public Color TextColor { get; set; } = Color.Lime;

        public Color CursorColor { get; set; } = Color.Lime;



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
        }



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
                _bus?.Publish(ControlId, ControlType, "Command", _currentInput);
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
        }

        public void WriteLine(string text)
        {
            _lines.Add(text);
            Invalidate();
        }

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
        }
    }

}