using System;
using System.Drawing;
using System.Windows.Forms;

using Spirograph_v1.Controls.MySlider;


namespace Spirograph_v1
{
    public partial class Form_TestUserControls : Form
    {
        private readonly MySlider _mySlider;

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

            #region .  Old Code  --  CAN DELETE  .
            ////_mySlider = new MySlider
            ////{
            ////    Location = new Point(12, 12),
            ////    Size     = new Size(360, 80),
            ////    Title    = "Example Slider",
            ////    Minimum  = 1,
            ////    Maximum  = 200,
            ////    Value    = 51
            ////};

            ////_mySlider.ValueChanged += MySlider_ValueChanged;

            ////Controls.Add(_mySlider);

            ////// Optional: a label to display current value
            ////var valueLabel = new Label
            ////{
            ////    Location = new Point(12, _mySlider.Bottom + 8),
            ////    AutoSize = true,
            ////    Text     = $"Value: {_mySlider.Value}"
            ////};
            ////Controls.Add(valueLabel);

            ////_mySlider.ValueChanged += (_, __) => valueLabel.Text = $"Value: {_mySlider.Value}";
            ////ClientSize = new Size(400, valueLabel.Bottom + 12);
            #endregion

        }   // Form_TestUserControls()
        #endregion


        #region .  MySlider_ValueChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  MySlider_ValueChanged
        //   Description..:  
        //   Parameters...:  
        //   Returns......:  
        // -------------------------------------------------------------------------
        private void MySlider_ValueChanged(object sender, EventArgs e)
        {
            // placeholder if further handling is needed

        }   // MySlider_ValueChanged()
        #endregion


        private void rpSciFiButton1_Click(object sender, EventArgs e)
        {
            LabelInfo.Location     = new Point(160, 60);
            LabelInfo.Text         = "2.  Click STOP";
            rpSciFiButton1.Enabled = false;
            rpSciFiButton2.Enabled = true;

        }

        private void rpSciFiButton2_Click(object sender, EventArgs e)
        {
            LabelInfo.Location     = new Point(20, 60);
            LabelInfo.Text         = "1.  Click START";
            rpSciFiButton1.Enabled = true;
            rpSciFiButton2.Enabled = false;

        }

        private void rpSciFiToggleSwitch1_Click(object sender, EventArgs e)
        {
            LabelInfo2.Text = (rpSciFiToggleSwitch1.IsOn) ? "ON" : "OFF";

        }


    }   // classForm_TestUserControls

}   // namespace Spirograph_v1
