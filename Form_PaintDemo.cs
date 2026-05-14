using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1
{
    public partial class Form_PaintDemo : Form
    {
        public Form_PaintDemo()
        {
            Text = "OnPaint Layering Demo";
            Size = new      Size(800, 300);

            Label lblStuff = new()
            {
                AutoSize = false,
                Location = new Point(20, 40),
                Size     = new Size(180, 50),
                Text     = "This demo shows how the order of base.OnPaint(e) and custom drawing affects layering.",
            };

            var   panel1 = new OverlayPanel    { BackColor = Color.Tan,   Location = new Point( 20, 40), Size = new Size(220, 200) };
            var panel2 = new BackgroundPanel { BackColor = Color.Wheat, Location = new Point(280, 40), Size = new Size(220, 200) };
            var panel3 = new CustomOnlyPanel { BackColor = Color.Green, Location = new Point(540, 40), Size = new Size(220, 200) };

            Label lblText1 = new() { AutoSize = true, Location = new Point(10, 10), Text = "Some really really long text that stretches", };
            Label lblText2 = new() { AutoSize = true, Location = new Point(10, 10), Text = "Some really really long text that stretches", };
            Label lblText3 = new() { AutoSize = true, Location = new Point(10, 10), Text = "Some really really long text that stretches", };

            panel1.Controls.Add(lblText1);
            panel2.Controls.Add(lblText2);
            panel3.Controls.Add(lblText3);

            Label lblBefore = new() { AutoSize = true, Location = new Point( 20, 10), Text = "base.OnPaint(e) BEFORE" };
            Label lblAfter  = new() { AutoSize = true, Location = new Point(280, 10), Text = "base.OnPaint(e) AFTER", };
            Label lblSkip   = new() { AutoSize = true, Location = new Point(540, 10), Text = "SKIP base.OnPaint(e)", };

            Controls.Add(lblBefore);
            Controls.Add(lblAfter);
            Controls.Add(lblSkip);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel3);
        }

        // Case 1: base.OnPaint BEFORE custom code (OverlayPanel)
        class OverlayPanel : Panel
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                // -----------------------------------------------------------------
                // Draw default stuff first, which will UNDERLAY the custom drawing.
                // -----------------------------------------------------------------
                base.OnPaint(e);

                // Then draw the custom stuff.
                BackColor = Color.LightYellow;

                using (var pen = new Pen(Color.Red, 3))
                {
                    e.Graphics.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
                }
                e.Graphics.DrawString("Overlay", Font, Brushes.Red, 10, 10);
            }
        }

        // Case 2: base.OnPaint AFTER custom code (BackgroundPanel)
        class BackgroundPanel : Panel
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                // Draw custom stuff first.
                using (var pen = new Pen(Color.Yellow, 3))
                {
                    e.Graphics.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
                }

                using (var brush = new LinearGradientBrush(ClientRectangle, Color.LightBlue, Color.White, 45))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }

                e.Graphics.DrawString("Background", Font, Brushes.Blue, 10, 10);

                // -----------------------------------------------------------------
                // Draw default stuff last, which will OVERLAY the custom drawing.
                // -----------------------------------------------------------------
                base.OnPaint(e);
            }
        }

        // Case 3: Skip base.OnPaint (CustomOnlyPanel)
        class CustomOnlyPanel : Panel
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.Clear(Color.Black);
                e.Graphics.FillEllipse(Brushes.Yellow, 40, 40, 100, 100);
                e.Graphics.DrawString("Custom Only", Font, Brushes.White, 10, 10);
            }
        }
    }
}
