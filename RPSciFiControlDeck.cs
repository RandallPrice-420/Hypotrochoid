using System.Windows.Forms;


namespace Spirograph_v1
{
    public partial class RPSciFiControlDeck : RPSciFiForm_Base
    {

        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _hud
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private readonly RPSciFiForm_HUDOverlay _hud;

        #endregion



        // ---------------------------------------------------------------------
        // Public Constructor(s):
        // ----------------------
        //   RPSciFiControlDeck()
        // ---------------------------------------------------------------------

        #region .  RPSciFiControlDeck()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RPSciFiControlDeck()
        //   Description..:  The constructor for the RPSciFiControlDeck class.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public RPSciFiControlDeck()
        {
            InitializeComponent();

            _hud        = new RPSciFiForm_HUDOverlay();
            _hud.Bounds = Screen.PrimaryScreen.Bounds;
            _hud.Show();

        }   //  RPSciFiControlDeck()
        #endregion


    }   // class RPSciFiControlDeck

}   // namespace Spirograph_v1
