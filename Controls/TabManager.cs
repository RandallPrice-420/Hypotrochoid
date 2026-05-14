using System;
using System.Windows.Forms;


namespace Spirograph_v1.Controls
{
    public sealed class TabManager
    {
        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _loaded
        //   _tabControl
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private readonly bool[]     _loaded;
        private readonly TabControl _tabControl;

        #endregion



        // -------------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   TabManager()
        // -------------------------------------------------------------------------

        #region .  TabManager()  --  Constructor  .
        // -------------------------------------------------------------------------
        //   Method.......:  TabManager()
        //
        //   Description..:  The constructor for the TabManager class.
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public TabManager(TabControl tabControl)
        {
            _tabControl = tabControl ?? throw new ArgumentNullException(nameof(tabControl));
            _loaded     = new bool[_tabControl.TabPages.Count];

            _tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

        }   // TabManager()
        #endregion



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   InitializeTab()
        //   PreloadFirstTab()
        //   SetInitializer()
        // -------------------------------------------------------------------------

        #region .  InitializeTab()  .
        // -------------------------------------------------------------------------
        //   Method.......:  InitializeTab()
        //
        //   Description..:  
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void InitializeTab(TabPage page, int index)
        {
            if (page.Tag is Action init)
            {
                init();
                page.Tag = null;
            }

            _loaded[index] = true;

        }   // InitializeTab()
        #endregion


        #region .  PreloadFirstTab()  .
        // -------------------------------------------------------------------------
        //   Method.......:  PreloadFirstTab()
        //
        //   Description..:  
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public void PreloadFirstTab()
        {
            if (_tabControl.TabPages.Count == 0) return;

            InitializeTab(_tabControl.TabPages[0], 0);

        }   // PreloadFirstTab()
        #endregion


        #region .  SetInitializer()  .
        // -------------------------------------------------------------------------
        //   Method.......:  SetInitializer()
        //
        //   Description..:  
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public void SetInitializer(TabPage page, Action initializer)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            page.Tag = initializer;

        }   // SetInitializer()
        #endregion



        // -------------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   TabControl_SelectedIndexChanged()
        // -------------------------------------------------------------------------

        #region .  TabControl_SelectedIndexChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  TabControl_SelectedIndexChanged()
        //
        //   Description..:  Handles the TabControl_SelectedIndexChanged event to ?
        //
        //   Parameters...:  sender - The event source.
        //                   e      - The event arguments.
        //
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = _tabControl.SelectedIndex;

            if (index < 0 || index >= _loaded.Length) return;

            if (!_loaded[index])
            {
                InitializeTab(_tabControl.TabPages[index], index);
            }

        }   // TabControl_SelectedIndexChanged()
        #endregion


    }   // class TabManager

}   // namespace Spirograph_v1.Controls
