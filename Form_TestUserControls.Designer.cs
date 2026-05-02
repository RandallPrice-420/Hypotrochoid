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
            this.PanelRPSlider = new System.Windows.Forms.Panel();
            this.LabelRPSlider = new System.Windows.Forms.Label();
            this.GroupBox_Radius = new System.Windows.Forms.GroupBox();
            this.NumericUpDown_Radius = new System.Windows.Forms.NumericUpDown();
            this.SciFiPanelSliderControl = new System.Windows.Forms.Panel();
            this.LabelSciFiSliderControl = new System.Windows.Forms.Label();
            this.sciFiSliderControl1 = new Spirograph_v1.Controls.SciFiSlider.SciFiSliderControl();
            this.PanelRPSlider.SuspendLayout();
            this.GroupBox_Radius.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown_Radius).BeginInit();
            this.SciFiPanelSliderControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelRPSlider
            // 
            this.PanelRPSlider.BackColor = System.Drawing.Color.Silver;
            this.PanelRPSlider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelRPSlider.Controls.Add(this.LabelRPSlider);
            this.PanelRPSlider.Controls.Add(this.GroupBox_Radius);
            this.PanelRPSlider.Location = new System.Drawing.Point(12, 12);
            this.PanelRPSlider.Name = "PanelRPSlider";
            this.PanelRPSlider.Size = new System.Drawing.Size(325, 140);
            this.PanelRPSlider.TabIndex = 30;
            // 
            // LabelRPSlider
            // 
            this.LabelRPSlider.AutoSize = true;
            this.LabelRPSlider.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
            this.LabelRPSlider.ForeColor = System.Drawing.Color.Firebrick;
            this.LabelRPSlider.Location = new System.Drawing.Point(3, 3);
            this.LabelRPSlider.Name = "LabelRPSlider";
            this.LabelRPSlider.Size = new System.Drawing.Size(87, 25);
            this.LabelRPSlider.TabIndex = 36;
            this.LabelRPSlider.Text = "RPSlider";
            // 
            // GroupBox_Radius
            // 
            this.GroupBox_Radius.BackColor = System.Drawing.Color.Silver;
            this.GroupBox_Radius.Controls.Add(this.NumericUpDown_Radius);
            this.GroupBox_Radius.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GroupBox_Radius.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.GroupBox_Radius.ForeColor = System.Drawing.Color.Maroon;
            this.GroupBox_Radius.Location = new System.Drawing.Point(8, 36);
            this.GroupBox_Radius.Name = "GroupBox_Radius";
            this.GroupBox_Radius.Size = new System.Drawing.Size(302, 48);
            this.GroupBox_Radius.TabIndex = 29;
            this.GroupBox_Radius.TabStop = false;
            this.GroupBox_Radius.Text = "Inner Circle";
            // 
            // NumericUpDown_Radius
            // 
            this.NumericUpDown_Radius.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NumericUpDown_Radius.Location = new System.Drawing.Point(246, 19);
            this.NumericUpDown_Radius.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NumericUpDown_Radius.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.NumericUpDown_Radius.Name = "NumericUpDown_Radius";
            this.NumericUpDown_Radius.Size = new System.Drawing.Size(49, 23);
            this.NumericUpDown_Radius.TabIndex = 14;
            this.NumericUpDown_Radius.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // SciFiPanelSliderControl
            // 
            this.SciFiPanelSliderControl.BackColor = System.Drawing.Color.Silver;
            this.SciFiPanelSliderControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SciFiPanelSliderControl.Controls.Add(this.LabelSciFiSliderControl);
            this.SciFiPanelSliderControl.Controls.Add(this.sciFiSliderControl1);
            this.SciFiPanelSliderControl.Location = new System.Drawing.Point(12, 163);
            this.SciFiPanelSliderControl.Name = "SciFiPanelSliderControl";
            this.SciFiPanelSliderControl.Size = new System.Drawing.Size(325, 170);
            this.SciFiPanelSliderControl.TabIndex = 31;
            // 
            // LabelSciFiSliderControl
            // 
            this.LabelSciFiSliderControl.AutoSize = true;
            this.LabelSciFiSliderControl.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
            this.LabelSciFiSliderControl.ForeColor = System.Drawing.Color.Firebrick;
            this.LabelSciFiSliderControl.Location = new System.Drawing.Point(3, 3);
            this.LabelSciFiSliderControl.Name = "LabelSciFiSliderControl";
            this.LabelSciFiSliderControl.Size = new System.Drawing.Size(171, 25);
            this.LabelSciFiSliderControl.TabIndex = 37;
            this.LabelSciFiSliderControl.Text = "SciFiSliderControl";
            // 
            // sciFiSliderControl1
            // 
            this.sciFiSliderControl1.BackColor = System.Drawing.Color.Black;
            this.sciFiSliderControl1.GlowMode = Spirograph_v1.Controls.SciFiSlider.SciFiSliderControl.GlowColorMode.NeonCyan;
            this.sciFiSliderControl1.Location = new System.Drawing.Point(10, 34);
            this.sciFiSliderControl1.Maximum = 100;
            this.sciFiSliderControl1.Minimum = 0;
            this.sciFiSliderControl1.Name = "sciFiSliderControl1";
            this.sciFiSliderControl1.Size = new System.Drawing.Size(300, 120);
            this.sciFiSliderControl1.TabIndex = 0;
            this.sciFiSliderControl1.Title = "";
            this.sciFiSliderControl1.Value = 0;
            // 
            // Form_TestUserControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(349, 341);
            this.Controls.Add(this.SciFiPanelSliderControl);
            this.Controls.Add(this.PanelRPSlider);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form_TestUserControls";
            this.Text = "Test UserControls";
            this.PanelRPSlider.ResumeLayout(false);
            this.PanelRPSlider.PerformLayout();
            this.GroupBox_Radius.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown_Radius).EndInit();
            this.SciFiPanelSliderControl.ResumeLayout(false);
            this.SciFiPanelSliderControl.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Panel PanelRPSlider;
        private System.Windows.Forms.Label LabelRPSlider;
        private System.Windows.Forms.GroupBox GroupBox_Radius;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Radius;
        private System.Windows.Forms.Panel SciFiPanelSliderControl;
        private System.Windows.Forms.Label LabelSciFiSliderControl;
        private Controls.SciFiSlider.SciFiSliderControl sciFiSliderControl1;
    }
}