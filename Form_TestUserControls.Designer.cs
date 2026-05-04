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
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.NumericUpDown_InnerCircle = new System.Windows.Forms.NumericUpDown();
            this.PanelContainer = new System.Windows.Forms.Panel();
            this.mySlider1 = new Spirograph_v1.Controls.MySlider.MySlider();
            this.LabelMySlider = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.PanelRPSlider.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown_InnerCircle).BeginInit();
            this.PanelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.trackBar1).BeginInit();
            this.SuspendLayout();
            // 
            // PanelRPSlider
            // 
            this.PanelRPSlider.BackColor = System.Drawing.Color.Silver;
            this.PanelRPSlider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelRPSlider.Controls.Add(this.trackBar1);
            this.PanelRPSlider.Controls.Add(this.LabelRPSlider);
            this.PanelRPSlider.Controls.Add(this.GroupBox1);
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
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Silver;
            this.GroupBox1.Controls.Add(this.NumericUpDown_InnerCircle);
            this.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.GroupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.GroupBox1.Location = new System.Drawing.Point(8, 29);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(302, 48);
            this.GroupBox1.TabIndex = 29;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Inner Circle";
            // 
            // NumericUpDown_InnerCircle
            // 
            this.NumericUpDown_InnerCircle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NumericUpDown_InnerCircle.Location = new System.Drawing.Point(199, 19);
            this.NumericUpDown_InnerCircle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NumericUpDown_InnerCircle.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.NumericUpDown_InnerCircle.Name = "NumericUpDown_InnerCircle";
            this.NumericUpDown_InnerCircle.Size = new System.Drawing.Size(50, 23);
            this.NumericUpDown_InnerCircle.TabIndex = 14;
            this.NumericUpDown_InnerCircle.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // PanelContainer
            // 
            this.PanelContainer.BackColor = System.Drawing.Color.Silver;
            this.PanelContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelContainer.Controls.Add(this.mySlider1);
            this.PanelContainer.Controls.Add(this.LabelMySlider);
            this.PanelContainer.Location = new System.Drawing.Point(12, 163);
            this.PanelContainer.Name = "PanelContainer";
            this.PanelContainer.Size = new System.Drawing.Size(325, 100);
            this.PanelContainer.TabIndex = 31;
            // 
            // mySlider1
            // 
            this.mySlider1.Location = new System.Drawing.Point(8, 29);
            this.mySlider1.Maximum = 100;
            this.mySlider1.Minimum = 0;
            this.mySlider1.Name = "mySlider1";
            this.mySlider1.Size = new System.Drawing.Size(248, 61);
            this.mySlider1.TabIndex = 38;
            this.mySlider1.Title = "MySlider";
            this.mySlider1.TrackBarLocation = new System.Drawing.Point(4, 25);
            this.mySlider1.TrackBarMaximum = 100;
            this.mySlider1.TrackBarMinimum = 0;
            this.mySlider1.TrackBarSize = new System.Drawing.Size(182, 30);
            this.mySlider1.TrackBarValue = 0;
            this.mySlider1.Value = 0;
            // 
            // LabelMySlider
            // 
            this.LabelMySlider.AutoSize = true;
            this.LabelMySlider.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
            this.LabelMySlider.ForeColor = System.Drawing.Color.Firebrick;
            this.LabelMySlider.Location = new System.Drawing.Point(3, 3);
            this.LabelMySlider.Name = "LabelMySlider";
            this.LabelMySlider.Size = new System.Drawing.Size(91, 25);
            this.LabelMySlider.TabIndex = 37;
            this.LabelMySlider.Text = "MySlider";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(8, 83);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 15;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // Form_TestUserControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.PanelContainer);
            this.Controls.Add(this.PanelRPSlider);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form_TestUserControls";
            this.Text = "Test UserControls";
            this.PanelRPSlider.ResumeLayout(false);
            this.PanelRPSlider.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown_InnerCircle).EndInit();
            this.PanelContainer.ResumeLayout(false);
            this.PanelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.trackBar1).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Panel PanelRPSlider;
        private System.Windows.Forms.Label LabelRPSlider;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.NumericUpDown NumericUpDown_InnerCircle;
        private System.Windows.Forms.Panel PanelContainer;
        private System.Windows.Forms.Label LabelMySlider;
        private Controls.SciFiSlider.SciFiSlider sciFiSliderControl1;
        private Controls.MySlider.MySlider mySlider1;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}