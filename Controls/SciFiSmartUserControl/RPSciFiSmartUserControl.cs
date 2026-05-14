using System;
using System.Drawing;
using System.Windows.Forms;


namespace Spirograph_v1.Controls.SciFiSmartUserControl
{
    public partial class RPSciFiSmartUserControl : UserControl
    {

        // -------------------------------------------------------------------------
        // Private Properties:
        // ------------------
        //   Initializing - Indicates whether the control is currently initializing.
        // -------------------------------------------------------------------------

        #region .  Private Properties  .

        private protected bool Initializing { get; private set; }

        #endregion



        // -------------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   RPSciFiSmartUserControl()
        // -------------------------------------------------------------------------

        #region .  RPSciFiSmartUserControl()  --  Constructor  .
        // -------------------------------------------------------------------------
        //   Method.......:  RPSciFiSmartUserControl()
        //
        //   Description..:  The constructor for the RPSciFiSmartUserControl class.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public RPSciFiSmartUserControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint  |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);

            BackColor = Color.FromArgb( 10,  10,  20);
            ForeColor = Color.FromArgb(180, 240, 255);
            Font      = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

        }   // RPSciFiSmartUserControl()
        #endregion



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   Init()
        // -------------------------------------------------------------------------

        #region .  Init()  .
        // -------------------------------------------------------------------------
        //   Method.......:  Init()
        //
        //   Description..:  
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        protected void Init(Action initAction)
        {
            Initializing = true;
            initAction?.Invoke();
            Initializing = false;
            Invalidate();

        }   // Init()
        #endregion



        // -------------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   OnPaint()
        // -------------------------------------------------------------------------

        #region .  OnPaint()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //
        //   Description..:  Handles the OnPaint event to ?
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var borderPen = new Pen(Color.FromArgb( 0, 200, 255), 1.5f);
            using var glowPen   = new Pen(Color.FromArgb(40, 200, 255), 4.0f);

            var rect = ClientRectangle;
            rect.Inflate(-2, -2);
            g.DrawRectangle(borderPen, rect);

            var glowRect = rect;
            glowRect.Inflate(-4, -4);
            g.DrawRectangle(glowPen, glowRect);

        }   // OnPaint()
        #endregion


    }   // class RPSciFiSmartUserControl

}   // namespace Spirograph_v1.Controls.SciFiSmartUserControl
