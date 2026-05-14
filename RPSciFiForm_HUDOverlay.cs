using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Spirograph_v1
{
    public partial class RPSciFiForm_HUDOverlay : Form
    {
        private Timer _anim;
        private float _scanOffset = 0;

        public bool ShowHexGrid { get; set; } = true;
        
        public bool ShowScanlines { get; set; } = true;



        public RPSciFiForm_HUDOverlay()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar   = false;
            TopMost         = true;
            BackColor       = Color.Magenta;
            TransparencyKey = Color.Magenta;
            DoubleBuffered  = true;

            _anim = new Timer { Interval = 30 };
            _anim.Tick += (s, e) =>
            {
                _scanOffset += 1;
                if (_scanOffset > Height) _scanOffset = 0;
                Invalidate();
            };
            _anim.Start();

        }   // SciFiHUDOverlay()



        protected override bool ShowWithoutActivation => true;


        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80000; // WS_EX_LAYERED
                cp.ExStyle |= 0x20;    // WS_EX_TRANSPARENT
                return cp;
            }
        }



        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint() event.
        //   Parameters...:  e - the event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (ShowHexGrid)   DrawHexGrid(g);

            if (ShowScanlines) DrawScanlines(g);

        }   // OnPaint()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   DrawHex()
        //   DrawHexGrid()
        //   DrawScanlines()
        // ---------------------------------------------------------------------


        #region .  DrawHex()  .
        // ---------------------------------------------------------------------
        //   Method.......:  DrawHex()
        //   Description..:  Describe
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void DrawHex(Graphics g, Pen pen, float x, float y, float size)
        {
            PointF[] pts = new PointF[6];

            for (int i = 0; i < 6; i++)
            {
                float angle = (float)(Math.PI / 3 * i);
                pts[i] = new PointF(
                    x + size * (float)Math.Cos(angle),
                    y + size * (float)Math.Sin(angle));
            }

            g.DrawPolygon(pen, pts);

        }   // DrawHex()
        #endregion


        #region .  DrawHexGrid()  .
        // ---------------------------------------------------------------------
        //   Method.......:  DrawHexGrid()
        //   Description..:  Describe
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void DrawHexGrid(Graphics g)
        {
            int   size = 40;
            float w    = size * 2;
            float h    = (float)(Math.Sqrt(3) * size);

            using var pen = new Pen(Color.FromArgb(30, 0, 255, 255), 1);

            for (float y = -h; y < Height + h; y += h)
            {
                for (float x = -w; x < Width + w; x += w * 0.75f)
                {
                    DrawHex(g, pen, x, y, size);
                }
            }

        }   // DrawHexGrid()
        #endregion


        #region .  DrawScanlines()  .
        // ---------------------------------------------------------------------
        //   Method.......:  DrawScanlines()
        //   Description..:  Describe
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void DrawScanlines(Graphics g)
        {
            using var pen = new Pen(Color.FromArgb(40, 0, 255, 255), 2);

            for (int y = 0; y < Height; y += 12)
            {
                g.DrawLine(pen, 0, y + _scanOffset, Width, y + _scanOffset);
            }

        }   // DrawScanlines()
        #endregion


    }   // class RPSciFiHUDOverlay

}   // namespace Spirograph_v1
