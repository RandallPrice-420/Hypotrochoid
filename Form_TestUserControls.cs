using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spirograph_v1
{
    public partial class Form_TestUserControls : Form
    {
        // -------------------------------------------------------------------------
        // Public Constructor:
        // -------------------
        //   Form_TestUserControls()  --  Constructor
        // -------------------------------------------------------------------------

        #region .  Form_TestUserControls()  -  Constructor()  .
        // -------------------------------------------------------------------------
        //   Method.......:  Form_TestUserControls()  -  Constructor
        //   Description..:  
        //   Parameters...:  
        //   Returns......:  
        // -------------------------------------------------------------------------
        public Form_TestUserControls()
        {
            InitializeComponent();
            Text = "Test UserControls";

        }   // Form_TestUserControls()
        #endregion



        // -------------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   rpSciFiButton1_Click()
        //   rpSciFiButton2_Click()
        //   rpSciFiToggleSwitch1_Click()
        // -------------------------------------------------------------------------

        #region .  rpSciFiButton1_Click()  .
        // -------------------------------------------------------------------------
        //   Method.......:  rpSciFiButton1_Click()
        //   Description..:  Handles the button click event to update the Start and
        //                   Stop buttons Enabled property.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void rpSciFiButton1_Click(object sender, EventArgs e)
        {
            LabelStartStop.Location = new Point(160, 60);
            LabelStartStop.Text     = "Click STOP";
            rpSciFiButton1.Enabled  = false;
            rpSciFiButton2.Enabled  = true;

        }   // rpSciFiButton1_Click()
        #endregion


        #region .  rpSciFiButton2_Click()  .
        // -------------------------------------------------------------------------
        //   Method.......:  rpSciFiButton2_Click()
        //   Description..:  Handles the button click event to update the Start and
        //                   Stop buttons Enabled property.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void rpSciFiButton2_Click(object sender, EventArgs e)
        {
            LabelStartStop.Location = new Point(20, 60);
            LabelStartStop.Text     = "Click START";
            rpSciFiButton1.Enabled  = true;
            rpSciFiButton2.Enabled  = false;

        }   // rpSciFiButton2_Click()
        #endregion


        #region .  rpSciFiToggleSwitch1_Click()  .
        // -------------------------------------------------------------------------
        //   Method.......:  rpSciFiToggleSwitch1_Click()
        //   Description..:  Handles the toggle switch click event to update the
        //                   Start and Stop buttons Enabled property.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void rpSciFiToggleSwitch1_Click(object sender, EventArgs e)
        {
            LabelInfo2.Text = (rpSciFiToggleSwitch1.IsOn) ? "ON" : "OFF";

        }   // rpSciFiToggleSwitch1_Click()
        #endregion


    }   // classForm_TestUserControls

}   // namespace Spirograph_v1
