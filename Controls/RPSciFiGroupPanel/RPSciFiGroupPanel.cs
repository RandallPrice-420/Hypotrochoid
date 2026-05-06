using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.RPSciFiGroupPanel
{
    public partial class RPSciFiGroupPanel : UserControl
    {
        public Color GlowColor { get; set; } = Color.Cyan;

        public RPSciFiGroupPanel()
        {
            DoubleBuffered = true;
            Padding = new Padding(10);
            BackColor = Color.FromArgb(10, 10, 20);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            rect.Inflate(-2, -2);

            using (LinearGradientBrush bg = new(
                rect,
                Color.FromArgb(30, 30, 50),
                Color.FromArgb(5, 5, 10),
                90f))
            {
                e.Graphics.FillRectangle(bg, rect);
            }

            using (Pen p = new(Color.FromArgb(160, GlowColor), 2))
                e.Graphics.DrawRectangle(p, rect);

            using Pen scan = new(Color.FromArgb(20, 255, 255, 255), 1);
            for (int y = rect.Top; y < rect.Bottom; y += 6)
                e.Graphics.DrawLine(scan, rect.Left, y, rect.Right, y);
        }
    }

}
