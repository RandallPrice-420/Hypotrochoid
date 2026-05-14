using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Spirograph_v1
{
    public partial class RPSciFiForm_Base : Form
    {
        public enum GlowColorMode { NeonCyan, PlasmaPurple, ReactorGreen }


        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   GlowMode
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        public GlowColorMode GlowMode { get; set; } = GlowColorMode.NeonCyan;

        #endregion



        // ---------------------------------------------------------------------
        // Override Properties:
        // --------------------
        //   CreateParams
        // ---------------------------------------------------------------------

        #region .  Override Properties  .

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                var cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }

        }   // CreateParams

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _dragging
        //   _dragStart
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private bool  _dragging;
        private Point _dragStart;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor(s):
        // ----------------------
        //   RPSciFiBaseForm()
        // ---------------------------------------------------------------------

        #region .  RPSciFiBaseForm()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiBaseForm()
        //   Description..:  The constructor for the RPSciFiBaseForm class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiForm_Base()
        {
            DoubleBuffered  = true;
            FormBorderStyle = FormBorderStyle.None;
            Padding         = new Padding(2);
            BackColor       = Color.Black;

            // Enable shadow
            this.SetStyle(ControlStyles.ResizeRedraw, true);

        }   // SciFiFormBase()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnMouseDown()
        //   OnMouseMove()
        //   OnMouse()
        //   OnPaint()
        // ---------------------------------------------------------------------

        #region .  OnMouseDown()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseDown()
        //   Description..:  Handles the OnMouseDown event to allow dragging the form
        //                   by clicking and dragging the top area (titlebar) of the
        //                   form.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (e.Y < 40))
            {
                _dragging  = true;
                _dragStart = e.Location;
            }
            base.OnMouseDown(e);

        }   // OnMouseDown()
        #endregion


        #region .  OnMouseMove()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseMove()
        //   Description..:  Handles the OnMouseMove event to allow dragging the form
        //                   by clicking and dragging the top area (titlebar) of the
        //                   form.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                Location = new Point
                (
                    Location.X + e.X - _dragStart.X,
                    Location.Y + e.Y - _dragStart.Y
                );
            }
            base.OnMouseMove(e);

        }   // OnMouseMove()
        #endregion


        #region .  OnMouseUp()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnMouseUp()
        //   Description..:  Handles the OnMouseUp event to stop dragging the form.
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


        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  Handles the OnPaint event to draw the custom form
        //                   appearance.
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color glow = GlowMode switch
            {
                GlowColorMode.NeonCyan     => Color.FromArgb(0, 255, 255),
                GlowColorMode.PlasmaPurple => Color.FromArgb(200, 0, 255),
                GlowColorMode.ReactorGreen => Color.FromArgb(0, 255, 100),
                                         _ => Color.Cyan
            };

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // Outer glow.
            using (var glowPen = new Pen(Color.FromArgb(50, glow), 12))
            {
                g.DrawRectangle(glowPen, rect);
            }

            // Hologram border.
            using (var borderPen = new Pen(Color.FromArgb(255, glow), 2))
            {
                g.DrawRectangle(borderPen, rect);
            }

            // Titlebar text.
            using var f = new Font("Segoe UI", 12, FontStyle.Bold);
            g.DrawString(Text, f, Brushes.White, 20, 10);

        }   // OnPaint()
        #endregion


    }   // class RPSciFiBaseForm

}   // namespace Spirograph_v1
