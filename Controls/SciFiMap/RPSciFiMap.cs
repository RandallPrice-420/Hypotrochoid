using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiMap
{
    public partial class RPSciFiMap : UserControl, IRPSciFiControl
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
        public RPSciFiControlType ControlType => RPSciFiControlType.Map;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus."), Browsable(false)]
        private RPSciFiControlBus _controlBus;


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

            //// Publish events here.
            //ValueChanged += (s, e) =>
            //{
            //    _controlBus?.Publish(ControlId, ControlType, "ValueChanged", Value);
            //};

        }   // Register()

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GridColor   : Color - The color of the grid lines on the map.
        //   MarkerColor : Color - The color of the markers on the map.
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        [Category("Sci-Fi"), Description("The color of the grid lines on the map."), Browsable(true)]
        public Color GridColor { get; set; } = Color.FromArgb(40, 255, 255, 255);


        [Category("Sci-Fi"), Description("The color of the markers on the map."), Browsable(true)]
        public Color MarkerColor { get; set; } = Color.Cyan;

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _dragging
        //   _dragStart
        //   _offset
        //   _zoom
        //   _markers
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private bool   _dragging;
        private Point  _dragStart;
        private PointF _offset = new(0, 0);
        private float  _zoom = 1f;

        private readonly List<MapMarker> _markers = [];

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiMap()  --  Constructor
        // ---------------------------------------------------------------------

        #region .  RPSciFiMap()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiMap()
        //   Description..:  The constructor for the RPSciFiMap class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiMap()
        {
            DoubleBuffered = true;
            BackColor      = Color.FromArgb(5, 5, 10);

        }   // RPSciFiMap()
        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   AddMarker()
        // ---------------------------------------------------------------------

        #region .  AddMarker()  .
        // ---------------------------------------------------------------------
        //   Method.......:  AddMarker()
        //   Description..:  
        //   Parameters...:  Id - 
        //                   x  - 
        //                   y  - 
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void AddMarker(string id, float x, float y)
        {
            _markers.Add(new MapMarker { Id = id, Position = new PointF(x, y) });
            _controlBus?.Publish(ControlId, ControlType, "MarkerAdded", id);

            Invalidate();

        }   // AddMarker()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnMouseDown()
        //   OnMouseMove()
        //   OnMouseUp()
        //   OnMouseUp()
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnMouseDown()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseDown()
        //   Description..:  Handles the OnMouseDown event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging  = true;
            _dragStart = e.Location;

            base.OnMouseDown(e);

        }   // OnMouseDown()
        #endregion


        #region .  OnMouseMove()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseMove()
        //   Description..:  Handles the OnMouseMove event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                _offset.X += (e.X - _dragStart.X) / _zoom;
                _offset.Y += (e.Y - _dragStart.Y) / _zoom;
                _dragStart = e.Location;

                Invalidate();
            }

            base.OnMouseMove(e);

        }   // OnMouseMove()
        #endregion


        #region .  OnMouseUp()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseUp()
        //   Description..:  Handles the OnMouseUp event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;

            base.OnMouseUp(e);

        }   // OnMouseUp()
        #endregion


        #region .  OnMouseWheel()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseWheel()
        //   Description..:  Handles the OnMouseWheel event to do_something.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            _zoom += e.Delta > 0 ? 0.1f : -0.1f;
            _zoom  = Math.Max(0.2f, Math.Min(3f, _zoom));

            _controlBus?.Publish(ControlId, ControlType, "ZoomChanged", _zoom);

            Invalidate();
            base.OnMouseWheel(e);

        }   // OnMouseWheel()
        #endregion


        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to do_something.
        //                   e - The event arguments..
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            float zoom40 = _zoom * 40;

            // Grid.
            using (Pen p = new(GridColor, 1))
            {
                for (float x = -_offset.X % zoom40; x < rect.Width; x += zoom40)
                {
                    e.Graphics.DrawLine(p, x, 0, x, rect.Height);
                }

                for (float y = -_offset.Y % zoom40; y < rect.Height; y += zoom40)
                {
                    e.Graphics.DrawLine(p, 0, y, rect.Width, y);
                }
            }

            // Markers.
            foreach (var m in _markers)
            {
                float px = (m.Position.X + _offset.X) * _zoom;
                float py = (m.Position.Y + _offset.Y) * _zoom;

                using SolidBrush b = new(MarkerColor);

                e.Graphics.FillEllipse(b, px - 5, py - 5, 10, 10);
            }

        }   // OnPaint()
        #endregion


    }   // class RPSciFiRadar



    public class MapMarker
    {
        public string Id;
        public PointF Position;
    }

}   // namespace Spirograph_v1.Controls.RPSciFiRadar
