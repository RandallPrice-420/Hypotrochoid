using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Spirograph_v1.Controls.RPSciFiInventory
{
    public partial class RPSciFiInventory : UserControl, IRPSciFiControl
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
        public RPSciFiControlType ControlType => RPSciFiControlType.Inventory;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus for communication."), Browsable(false)]
        private RPSciFiControlBus _bus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _bus = bus;
            bus.Register(this);

            //// Publish events here.
            //ValueChanged += (s, e) =>
            //{
            //    _bus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            //};

        }   // Register()

        #endregion



        private readonly List<InventoryItem> _items = new();

        private int _cols          = 4;
        private int _rows          = 3;
        private int _selectedIndex = -1;



        public RPSciFiInventory()
        {
            DoubleBuffered = true;
            BackColor      = Color.FromArgb(5, 5, 10);

        }   // RPSciFiInventory()()



        public void SetGrid(int cols, int rows)
        {
            _cols = Math.Max(1, cols);
            _rows = Math.Max(1, rows);
            Invalidate();

        }   // SetGrid()


        public void AddItem(string id, string label, Color color)
        {
            _items.Add(new InventoryItem { Id = id, Label = label, Color = color });
            _bus?.Publish(ControlId, ControlType, "ItemAdded", id);
            Invalidate();

        }   // AddItem()



        private int HitTest(Point p)
        {
            if (_cols <= 0 || _rows <= 0) return -1;

            Rectangle rect = ClientRectangle;
            rect.Inflate(-8, -8);

            int cellW = rect.Width  / _cols;
            int cellH = rect.Height / _rows;

            int col = (p.X - rect.Left) / cellW;
            int row = (p.Y - rect.Top)  / cellH;

            if (col < 0 || col >= _cols || row < 0 || row >= _rows) return -1;

            return row * _cols + col;

        }   // HitTest()



        protected override void OnMouseDown(MouseEventArgs e)
        {
            int index = HitTest(e.Location);

            if (index >= 0 && index < _items.Count)
            {
                _selectedIndex = index;
                _bus?.Publish(ControlId, ControlType, "ItemSelected", _items[index].Id);
                Invalidate();
            }

            base.OnMouseDown(e);

        }   // OnMouseDown()


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            rect.Inflate(-8, -8);

            using Pen grid = new(Color.FromArgb(40, 255, 255, 255), 1);

            int cellW = rect.Width  / _cols;
            int cellH = rect.Height / _rows;

            for (int c = 0; c <= _cols; c++)
            {
                int x = rect.Left + c * cellW;
                e.Graphics.DrawLine(grid, x, rect.Top, x, rect.Bottom);
            }

            for (int r = 0; r <= _rows; r++)
            {
                int y = rect.Top + r * cellH;
                e.Graphics.DrawLine(grid, rect.Left, y, rect.Right, y);
            }

            for (int i = 0; i < _items.Count; i++)
            {
                int row = i / _cols;
                int col = i % _cols;

                Rectangle cell = new
                (
                    rect.Left + col * cellW + 4,
                    rect.Top  + row * cellH + 4,
                    cellW - 8,
                    cellH - 8
                );

                var item = _items[i];
                using (SolidBrush b = new(item.Color))
                {
                    e.Graphics.FillRectangle(b, cell);
                }

                if (i == _selectedIndex)
                {
                    using Pen p = new(Color.FromArgb(220, Color.Cyan), 2);
                    e.Graphics.DrawRectangle(p, cell);
                }

                using var tb = new SolidBrush(Color.White);
                e.Graphics.DrawString(item.Label, Font, tb, cell.Left + 4, cell.Top + 4);
            }

            base.OnPaint(e);

        }   // OnPaint()


    }   // class RPSciFiInventory



    public class InventoryItem
    {
        public string Id;
        public string Label;
        public Color  Color;
    }

}   // namespace Spirograph_v1.Controls.RPSciFiInventory
