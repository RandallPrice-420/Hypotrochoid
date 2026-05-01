using Spirograph_v1.Controls.TrackBar;

namespace Spirograph_v1
{
    partial class Form_TrackBar
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
            this.GroupBox_Radius = new System.Windows.Forms.GroupBox();
            this.NumericUpDown_Radius = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSlider = new System.Windows.Forms.Label();
            this.lblNumericUpDown_InnerCircle = new System.Windows.Forms.Label();
            this.lblSlideArea = new System.Windows.Forms.Label();
            this.pictureBox_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Left = new System.Windows.Forms.PictureBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.GroupBox_Radius.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown_Radius).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Right).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Left).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox_Radius
            // 
            this.GroupBox_Radius.BackColor = System.Drawing.Color.PapayaWhip;
            this.GroupBox_Radius.Controls.Add(this.NumericUpDown_Radius);
            this.GroupBox_Radius.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GroupBox_Radius.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.GroupBox_Radius.ForeColor = System.Drawing.Color.Maroon;
            this.GroupBox_Radius.Location = new System.Drawing.Point(152, 64);
            this.GroupBox_Radius.Name = "GroupBox_Radius";
            this.GroupBox_Radius.Size = new System.Drawing.Size(380, 48);
            this.GroupBox_Radius.TabIndex = 21;
            this.GroupBox_Radius.TabStop = false;
            this.GroupBox_Radius.Text = "Inner Circle";
            // 
            // NumericUpDown_Radius
            // 
            this.NumericUpDown_Radius.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NumericUpDown_Radius.Location = new System.Drawing.Point(313, 16);
            this.NumericUpDown_Radius.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NumericUpDown_Radius.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.NumericUpDown_Radius.Name = "NumericUpDown_Radius";
            this.NumericUpDown_Radius.Size = new System.Drawing.Size(57, 23);
            this.NumericUpDown_Radius.TabIndex = 14;
            this.NumericUpDown_Radius.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(18, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "GroupBox with Title";
            // 
            // lblSlider
            // 
            this.lblSlider.AutoSize = true;
            this.lblSlider.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSlider.ForeColor = System.Drawing.Color.Red;
            this.lblSlider.Location = new System.Drawing.Point(283, 46);
            this.lblSlider.Name = "lblSlider";
            this.lblSlider.Size = new System.Drawing.Size(39, 15);
            this.lblSlider.TabIndex = 23;
            this.lblSlider.Text = "Slider";
            // 
            // lblNumericUpDown_InnerCircle
            // 
            this.lblNumericUpDown_InnerCircle.AutoSize = true;
            this.lblNumericUpDown_InnerCircle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumericUpDown_InnerCircle.ForeColor = System.Drawing.Color.Red;
            this.lblNumericUpDown_InnerCircle.Location = new System.Drawing.Point(538, 82);
            this.lblNumericUpDown_InnerCircle.Name = "lblNumericUpDown_InnerCircle";
            this.lblNumericUpDown_InnerCircle.Size = new System.Drawing.Size(104, 15);
            this.lblNumericUpDown_InnerCircle.TabIndex = 24;
            this.lblNumericUpDown_InnerCircle.Text = "NumericUpDown";
            // 
            // lblSlideArea
            // 
            this.lblSlideArea.AutoSize = true;
            this.lblSlideArea.Location = new System.Drawing.Point(272, 128);
            this.lblSlideArea.Name = "lblSlideArea";
            this.lblSlideArea.Size = new System.Drawing.Size(59, 15);
            this.lblSlideArea.TabIndex = 26;
            this.lblSlideArea.Text = "Slide Area";
            // 
            // pictureBox_Right
            // 
            this.pictureBox_Right.Location = new System.Drawing.Point(338, 113);
            this.pictureBox_Right.Name = "pictureBox_Right";
            this.pictureBox_Right.Size = new System.Drawing.Size(100, 30);
            this.pictureBox_Right.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Right.TabIndex = 27;
            this.pictureBox_Right.TabStop = false;
            // 
            // pictureBox_Left
            // 
            this.pictureBox_Left.Location = new System.Drawing.Point(166, 113);
            this.pictureBox_Left.Name = "pictureBox_Left";
            this.pictureBox_Left.Size = new System.Drawing.Size(100, 30);
            this.pictureBox_Left.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Left.TabIndex = 28;
            this.pictureBox_Left.TabStop = false;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblHeading.Location = new System.Drawing.Point(18, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(237, 25);
            this.lblHeading.TabIndex = 29;
            this.lblHeading.Text = "WinForm Control - Slider";
            // 
            // Form_TrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 183);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.pictureBox_Left);
            this.Controls.Add(this.pictureBox_Right);
            this.Controls.Add(this.lblSlideArea);
            this.Controls.Add(this.lblNumericUpDown_InnerCircle);
            this.Controls.Add(this.lblSlider);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GroupBox_Radius);
            this.Name = "Form_TrackBar";
            this.Text = "FormTrackBar";
            this.GroupBox_Radius.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown_Radius).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Right).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Left).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_Radius;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Radius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSlider;
        private System.Windows.Forms.Label lblNumericUpDown_InnerCircle;
        private System.Windows.Forms.Label lblSlideArea;
        private System.Windows.Forms.PictureBox pictureBox_Right;
        private System.Windows.Forms.PictureBox pictureBox_Left;
        private System.Windows.Forms.Label lblHeading;
    }
}