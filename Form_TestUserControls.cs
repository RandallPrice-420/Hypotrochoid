using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Spirograph_v1
{
    public partial class Form_TestUserControls : Form, IRPSciFiControl
    {
        // ---------------------------------------------------------------------
        // RPSciFi API Layer : Controls must implement this interface to be
        //                     compatible with the RPSciFi system.
        //
        //   ControlId   : string - The unique identifier for the control.
        //   ControlType : RPSciFiControlType - The type of the control.
        //   _controlBus : RPSciFiControlBus  - The RPSciFi control bus.
        //   Register()  : Register the control with the RPSciFi control bus.
        // ---------------------------------------------------------------------

        #region . RPSciFi API Layer  .

        [Category("RPSciFi API Layer"), Description(""), Browsable(true)]
        public string ControlId { get; set; } = Guid.NewGuid().ToString();


        [Category("RPSciFi API Layer"), Description("The type of the control."), Browsable(true)]
        public RPSciFiControlType ControlType => RPSciFiControlType.Slider;


        [Category("RPSciFi API Layer"), Description("The RPSciFi control bus."), Browsable(false)]
        private RPSciFiControlBus _controlBus = new();


        [Category("RPSciFi API Layer"), Description("Register the control with the RPSciFi control bus."), Browsable(false)]
        public void Register(RPSciFiControlBus bus)
        {
            _controlBus = bus;
            bus.Register(this);

        }   // Register()

        #endregion



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

            // Set the default selected tab page.
            //tabControl.SelectedTab = tabPageButton;
            //tabControl.SelectedTab = tabPageSlider;
            tabControl.SelectedTab = tabPageOscilloscope;
            //tabControl.SelectedTab = tabPageWaveform;

            // Register the controls with the RPSciFi control bus.
            RegisterControls();

            // Subscribe to the control bus events to update the
            // labels on each tab page when a control value changes.
            _controlBus.OnEvent += evt =>
            {
                switch (evt.ControlType)
                {
                    // Button
                    case RPSciFiControlType.Button:
                        //lblTabButtonValueChanged.Text = $"Control ID: {evt.ControlId}\nControl Type: {evt.ControlType}\nEvent Name: {evt.EventName}\nValue: {evt.Value}";
                        break;

                    case RPSciFiControlType.Slider:
                        lblTabSliderValueChanged.Text   = $"Control ID: {evt.ControlId}\nControl Type: {evt.ControlType}\nEvent Name: {evt.EventName}\nValue: {evt.Value}";
                        break;

                    case RPSciFiControlType.ToggleSwitch:
                        //lblTabToggleSwitchValueChanged.Text = $"Control ID: {evt.ControlId}\nControl Type: {evt.ControlType}\nEvent Name: {evt.EventName}\nValue: {evt.Value}";
                        break;

                    // Oscilloscope2
                    case RPSciFiControlType.Waveform:
                        lblTabWaveformValueChanged.Text = $"Control ID: {evt.ControlId}\nControl Type: {evt.ControlType}\nEvent Name: {evt.EventName}\nValue: {evt.Value}";
                        break;

                    default:
                        break;
                }
            };

        }   // Form_TestUserControls()

        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   RegisterControls() - Register the controls with the RPSciFi control
        //                        bus.
        // ---------------------------------------------------------------------

        #region .  RegisterControls()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RegisterControls()
        //
        //   Description..:  
        //
        //   Parameters...:  None
        //
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void RegisterControls()
        {
            rpSciFiSlider2  .Register(_controlBus);
            rpSciFiWaveform1.Register(_controlBus);

        }   // RegisterControls()
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
