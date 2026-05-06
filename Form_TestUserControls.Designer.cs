namespace Spirograph_v1
{
    partial class Form_TestUserControls
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl = new System.Windows.Forms.TabControl();
            tabPageButton = new System.Windows.Forms.TabPage();
            LabelInfo = new System.Windows.Forms.Label();
            rpSciFiButton2 = new Spirograph_v1.Controls.RPSciFiButton.RPSciFiButton();
            rpSciFiButton1 = new Spirograph_v1.Controls.RPSciFiButton.RPSciFiButton();
            tabPagePanel = new System.Windows.Forms.TabPage();
            rpSciFiGroupPanel1 = new Spirograph_v1.Controls.RPSciFiGroupPanel.RPSciFiGroupPanel();
            tabPageGuage = new System.Windows.Forms.TabPage();
            rpSciFiGuage1 = new Spirograph_v1.Controls.RPSciFiGuage.RPSciFiGuage();
            tabPageKnob = new System.Windows.Forms.TabPage();
            rpSciFiKnob1 = new Spirograph_v1.Controls.RPSciFiKnob.RPSciFiKnob();
            tabPageOscilloscope = new System.Windows.Forms.TabPage();
            rpSciFiOscilloscope1 = new Spirograph_v1.Controls.RPSciFiOscilloscope.RPSciFiOscilloscope();
            tabPageSlider = new System.Windows.Forms.TabPage();
            rpSciFiSlider1 = new Spirograph_v1.Controls.RPSciFiSlider.RPSciFiSlider();
            tabPageToggleSwitch = new System.Windows.Forms.TabPage();
            LabelInfo2 = new System.Windows.Forms.Label();
            rpSciFiToggleSwitch1 = new Spirograph_v1.Controls.RPSciFiToggleSwitch.RPSciFiToggleSwitch();
            label1 = new System.Windows.Forms.Label();
            tabControl.SuspendLayout();
            tabPageButton.SuspendLayout();
            tabPagePanel.SuspendLayout();
            tabPageGuage.SuspendLayout();
            tabPageKnob.SuspendLayout();
            tabPageOscilloscope.SuspendLayout();
            tabPageSlider.SuspendLayout();
            tabPageToggleSwitch.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageButton);
            tabControl.Controls.Add(tabPagePanel);
            tabControl.Controls.Add(tabPageGuage);
            tabControl.Controls.Add(tabPageKnob);
            tabControl.Controls.Add(tabPageOscilloscope);
            tabControl.Controls.Add(tabPageSlider);
            tabControl.Controls.Add(tabPageToggleSwitch);
            tabControl.HotTrack = true;
            tabControl.Location = new System.Drawing.Point(22, 60);
            tabControl.Multiline = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(500, 250);
            tabControl.TabIndex = 36;
            // 
            // tabPageButton
            // 
            tabPageButton.Controls.Add(LabelInfo);
            tabPageButton.Controls.Add(rpSciFiButton2);
            tabPageButton.Controls.Add(rpSciFiButton1);
            tabPageButton.Location = new System.Drawing.Point(4, 24);
            tabPageButton.Name = "tabPageButton";
            tabPageButton.Size = new System.Drawing.Size(692, 222);
            tabPageButton.TabIndex = 0;
            tabPageButton.Text = "Button";
            // 
            // LabelInfo
            // 
            LabelInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            LabelInfo.ForeColor = System.Drawing.Color.Firebrick;
            LabelInfo.Location = new System.Drawing.Point(20, 60);
            LabelInfo.Name = "LabelInfo";
            LabelInfo.Size = new System.Drawing.Size(100, 15);
            LabelInfo.TabIndex = 3;
            LabelInfo.Text = "1.  Click START";
            LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rpSciFiButton2
            // 
            rpSciFiButton2.BaseColor = System.Drawing.Color.FromArgb(30, 30, 50);
            rpSciFiButton2.ButtonText = "STOP";
            rpSciFiButton2.DisabledColor = System.Drawing.Color.FromArgb(60, 60, 60);
            rpSciFiButton2.Enabled = false;
            rpSciFiButton2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            rpSciFiButton2.ForeColor = System.Drawing.Color.MediumSpringGreen;
            rpSciFiButton2.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiButton2.Location = new System.Drawing.Point(160, 20);
            rpSciFiButton2.Name = "rpSciFiButton2";
            rpSciFiButton2.Size = new System.Drawing.Size(100, 35);
            rpSciFiButton2.TabIndex = 2;
            rpSciFiButton2.TextColorDisabled = System.Drawing.SystemColors.GrayText;
            rpSciFiButton2.TextColorEnabled = System.Drawing.Color.MediumSpringGreen;
            rpSciFiButton2.Click += rpSciFiButton2_Click;
            // 
            // rpSciFiButton1
            // 
            rpSciFiButton1.BaseColor = System.Drawing.Color.FromArgb(30, 30, 50);
            rpSciFiButton1.ButtonText = "START";
            rpSciFiButton1.DisabledColor = System.Drawing.Color.FromArgb(60, 60, 60);
            rpSciFiButton1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            rpSciFiButton1.ForeColor = System.Drawing.Color.MediumSpringGreen;
            rpSciFiButton1.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiButton1.Location = new System.Drawing.Point(20, 20);
            rpSciFiButton1.Name = "rpSciFiButton1";
            rpSciFiButton1.Size = new System.Drawing.Size(100, 35);
            rpSciFiButton1.TabIndex = 1;
            rpSciFiButton1.TextColorDisabled = System.Drawing.SystemColors.GrayText;
            rpSciFiButton1.TextColorEnabled = System.Drawing.Color.MediumSpringGreen;
            rpSciFiButton1.Click += rpSciFiButton1_Click;
            // 
            // tabPagePanel
            // 
            tabPagePanel.Controls.Add(rpSciFiGroupPanel1);
            tabPagePanel.Location = new System.Drawing.Point(4, 24);
            tabPagePanel.Name = "tabPagePanel";
            tabPagePanel.Padding = new System.Windows.Forms.Padding(3);
            tabPagePanel.Size = new System.Drawing.Size(692, 222);
            tabPagePanel.TabIndex = 7;
            tabPagePanel.Text = "GroupPanel";
            tabPagePanel.UseVisualStyleBackColor = true;
            // 
            // rpSciFiGroupPanel1
            // 
            rpSciFiGroupPanel1.BackColor = System.Drawing.Color.FromArgb(10, 10, 20);
            rpSciFiGroupPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            rpSciFiGroupPanel1.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiGroupPanel1.Location = new System.Drawing.Point(3, 3);
            rpSciFiGroupPanel1.Name = "rpSciFiGroupPanel1";
            rpSciFiGroupPanel1.Padding = new System.Windows.Forms.Padding(10);
            rpSciFiGroupPanel1.Size = new System.Drawing.Size(150, 216);
            rpSciFiGroupPanel1.TabIndex = 0;
            // 
            // tabPageGuage
            // 
            tabPageGuage.Controls.Add(rpSciFiGuage1);
            tabPageGuage.Location = new System.Drawing.Point(4, 24);
            tabPageGuage.Margin = new System.Windows.Forms.Padding(0);
            tabPageGuage.Name = "tabPageGuage";
            tabPageGuage.Padding = new System.Windows.Forms.Padding(3);
            tabPageGuage.Size = new System.Drawing.Size(492, 222);
            tabPageGuage.TabIndex = 2;
            tabPageGuage.Text = "Guage";
            tabPageGuage.UseVisualStyleBackColor = true;
            // 
            // rpSciFiGuage1
            // 
            rpSciFiGuage1.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiGuage1.LabelText = "POWER";
            rpSciFiGuage1.Location = new System.Drawing.Point(150, 11);
            rpSciFiGuage1.Maximum = 100F;
            rpSciFiGuage1.Minimum = 0F;
            rpSciFiGuage1.Name = "rpSciFiGuage1";
            rpSciFiGuage1.Size = new System.Drawing.Size(200, 200);
            rpSciFiGuage1.TabIndex = 0;
            rpSciFiGuage1.Value = 50F;
            // 
            // tabPageKnob
            // 
            tabPageKnob.Controls.Add(rpSciFiKnob1);
            tabPageKnob.Location = new System.Drawing.Point(4, 24);
            tabPageKnob.Name = "tabPageKnob";
            tabPageKnob.Padding = new System.Windows.Forms.Padding(3);
            tabPageKnob.Size = new System.Drawing.Size(492, 222);
            tabPageKnob.TabIndex = 3;
            tabPageKnob.Text = "Knob";
            tabPageKnob.UseVisualStyleBackColor = true;
            // 
            // rpSciFiKnob1
            // 
            rpSciFiKnob1.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiKnob1.Location = new System.Drawing.Point(150, 11);
            rpSciFiKnob1.Maximum = 100F;
            rpSciFiKnob1.Minimum = 0F;
            rpSciFiKnob1.Name = "rpSciFiKnob1";
            rpSciFiKnob1.Size = new System.Drawing.Size(200, 200);
            rpSciFiKnob1.TabIndex = 0;
            rpSciFiKnob1.Value = 0F;
            // 
            // tabPageOscilloscope
            // 
            tabPageOscilloscope.Controls.Add(rpSciFiOscilloscope1);
            tabPageOscilloscope.Location = new System.Drawing.Point(4, 24);
            tabPageOscilloscope.Name = "tabPageOscilloscope";
            tabPageOscilloscope.Padding = new System.Windows.Forms.Padding(3);
            tabPageOscilloscope.Size = new System.Drawing.Size(492, 222);
            tabPageOscilloscope.TabIndex = 6;
            tabPageOscilloscope.Text = "Oscilloscope";
            tabPageOscilloscope.UseVisualStyleBackColor = true;
            // 
            // rpSciFiOscilloscope1
            // 
            rpSciFiOscilloscope1.BackColor = System.Drawing.Color.FromArgb(5, 5, 10);
            rpSciFiOscilloscope1.Dock = System.Windows.Forms.DockStyle.Fill;
            rpSciFiOscilloscope1.GlowColor = System.Drawing.Color.Lime;
            rpSciFiOscilloscope1.Location = new System.Drawing.Point(3, 3);
            rpSciFiOscilloscope1.Name = "rpSciFiOscilloscope1";
            rpSciFiOscilloscope1.Size = new System.Drawing.Size(486, 216);
            rpSciFiOscilloscope1.TabIndex = 0;
            // 
            // tabPageSlider
            // 
            tabPageSlider.Controls.Add(rpSciFiSlider1);
            tabPageSlider.Location = new System.Drawing.Point(4, 24);
            tabPageSlider.Name = "tabPageSlider";
            tabPageSlider.Padding = new System.Windows.Forms.Padding(3);
            tabPageSlider.Size = new System.Drawing.Size(492, 222);
            tabPageSlider.TabIndex = 4;
            tabPageSlider.Text = "Slider";
            tabPageSlider.UseVisualStyleBackColor = true;
            // 
            // rpSciFiSlider1
            // 
            rpSciFiSlider1.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiSlider1.Location = new System.Drawing.Point(50, 50);
            rpSciFiSlider1.Maximum = 100;
            rpSciFiSlider1.Minimum = 0;
            rpSciFiSlider1.Name = "rpSciFiSlider1";
            rpSciFiSlider1.Size = new System.Drawing.Size(400, 23);
            rpSciFiSlider1.TabIndex = 0;
            rpSciFiSlider1.Text = "rpSciFiSlider1";
            rpSciFiSlider1.Value = 0;
            // 
            // tabPageToggleSwitch
            // 
            tabPageToggleSwitch.Controls.Add(LabelInfo2);
            tabPageToggleSwitch.Controls.Add(rpSciFiToggleSwitch1);
            tabPageToggleSwitch.Location = new System.Drawing.Point(4, 24);
            tabPageToggleSwitch.Name = "tabPageToggleSwitch";
            tabPageToggleSwitch.Padding = new System.Windows.Forms.Padding(3);
            tabPageToggleSwitch.Size = new System.Drawing.Size(492, 222);
            tabPageToggleSwitch.TabIndex = 5;
            tabPageToggleSwitch.Text = "ToggleSwitch";
            tabPageToggleSwitch.UseVisualStyleBackColor = true;
            // 
            // LabelInfo2
            // 
            LabelInfo2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            LabelInfo2.ForeColor = System.Drawing.Color.Firebrick;
            LabelInfo2.Location = new System.Drawing.Point(20, 60);
            LabelInfo2.Name = "LabelInfo2";
            LabelInfo2.Size = new System.Drawing.Size(60, 15);
            LabelInfo2.TabIndex = 1;
            LabelInfo2.Text = "OFF";
            LabelInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rpSciFiToggleSwitch1
            // 
            rpSciFiToggleSwitch1.Location = new System.Drawing.Point(20, 20);
            rpSciFiToggleSwitch1.Name = "rpSciFiToggleSwitch1";
            rpSciFiToggleSwitch1.OffColor = System.Drawing.Color.Red;
            rpSciFiToggleSwitch1.OnColor = System.Drawing.Color.Lime;
            rpSciFiToggleSwitch1.Size = new System.Drawing.Size(60, 25);
            rpSciFiToggleSwitch1.TabIndex = 0;
            rpSciFiToggleSwitch1.Click += rpSciFiToggleSwitch1_Click;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.Firebrick;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(508, 40);
            label1.TabIndex = 37;
            label1.Text = "Test My SciFi UserControls";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_TestUserControls
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.LightGray;
            ClientSize = new System.Drawing.Size(544, 331);
            Controls.Add(label1);
            Controls.Add(tabControl);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "Form_TestUserControls";
            Text = "Test UserControls";
            tabControl.ResumeLayout(false);
            tabPageButton.ResumeLayout(false);
            tabPagePanel.ResumeLayout(false);
            tabPageGuage.ResumeLayout(false);
            tabPageKnob.ResumeLayout(false);
            tabPageOscilloscope.ResumeLayout(false);
            tabPageSlider.ResumeLayout(false);
            tabPageToggleSwitch.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageButton;
        private System.Windows.Forms.TabPage tabPageGroupPanel;
        private System.Windows.Forms.TabPage tabPageGuage;
        private System.Windows.Forms.TabPage tabPageKnob;
        private System.Windows.Forms.TabPage tabPageSlider;
        private System.Windows.Forms.TabPage tabPageToggleSwitch;
        private System.Windows.Forms.TabPage tabPageOscilloscope;
        private Controls.SciFiSlider.SciFiSlider sciFiSliderControl1;
        private Controls.MySlider.MySlider mySlider1;
        private Controls.RPSciFiButton.RPSciFiButton rpSciFiButton1;
        private Controls.RPSciFiButton.RPSciFiButton rpSciFiButton2;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.TabPage tabPagePanel;
        private Controls.RPSciFiGroupPanel.RPSciFiGroupPanel rpSciFiGroupPanel1;
        private Controls.RPSciFiGuage.RPSciFiGuage rpSciFiGuage1;
        private Controls.RPSciFiKnob.RPSciFiKnob rpSciFiKnob1;
        private Controls.RPSciFiOscilloscope.RPSciFiOscilloscope rpSciFiOscilloscope1;
        private Controls.RPSciFiSlider.RPSciFiSlider rpSciFiSlider1;
        private Controls.RPSciFiToggleSwitch.RPSciFiToggleSwitch rpSciFiToggleSwitch1;
        private System.Windows.Forms.Label LabelInfo2;
        private System.Windows.Forms.Label label1;
    }
}