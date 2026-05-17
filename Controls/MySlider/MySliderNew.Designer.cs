namespace Spirograph_v1.Controls.MySlider
{
    partial class MySliderNew
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            numUpDown = new System.Windows.Forms.NumericUpDown();
            lblValue = new System.Windows.Forms.Label();
            lblTitle = new System.Windows.Forms.Label();
            slider = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)numUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)slider).BeginInit();
            SuspendLayout();
            // 
            // numUpDown
            // 
            numUpDown.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            numUpDown.ForeColor = System.Drawing.Color.Firebrick;
            numUpDown.Location = new System.Drawing.Point(235, 24);
            numUpDown.Margin = new System.Windows.Forms.Padding(0);
            numUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numUpDown.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numUpDown.Name = "numUpDown";
            numUpDown.Size = new System.Drawing.Size(50, 25);
            numUpDown.TabIndex = 7;
            numUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numUpDown.ValueChanged += numUpDown_ValueChanged;
            // 
            // lblValue
            // 
            lblValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblValue.ForeColor = System.Drawing.Color.Cyan;
            lblValue.Location = new System.Drawing.Point(167, 1);
            lblValue.Name = "lblValue";
            lblValue.Size = new System.Drawing.Size(60, 20);
            lblValue.TabIndex = 6;
            lblValue.Text = "{value}";
            lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.Cyan;
            lblTitle.Location = new System.Drawing.Point(4, 1);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(84, 20);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "{title}";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblTitle.UseWaitCursor = true;
            // 
            // slider
            // 
            slider.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            slider.AutoSize = false;
            slider.Location = new System.Drawing.Point(4, 76);
            slider.Margin = new System.Windows.Forms.Padding(0);
            slider.Minimum = -10;
            slider.Name = "slider";
            slider.Size = new System.Drawing.Size(227, 25);
            slider.TabIndex = 5;
            slider.TickStyle = System.Windows.Forms.TickStyle.None;
            slider.ValueChanged += Slider_ValueChanged;
            slider.MouseDown += Slider_MouseDown;
            slider.MouseMove += Slider_MouseMove;
            slider.MouseUp += Slider_MouseUp;
            // 
            // MySliderNew
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Black;
            BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            Controls.Add(numUpDown);
            Controls.Add(lblValue);
            Controls.Add(slider);
            Controls.Add(lblTitle);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "MySliderNew";
            Size = new System.Drawing.Size(300, 136);
            ((System.ComponentModel.ISupportInitialize)numUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)slider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TrackBar slider;
    }
}
