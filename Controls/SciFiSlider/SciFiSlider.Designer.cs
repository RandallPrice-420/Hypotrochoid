namespace Spirograph_v1.Controls.SciFiSlider
{
    partial class SciFiSlider
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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.lblTitle = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)this.trackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDown).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(3, 25);
            this.trackBar.Maximum = 100;
            this.trackBar.Minimum = 1;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(250, 45);
            this.trackBar.TabIndex = 0;
            this.trackBar.TickFrequency = 10;
            this.trackBar.Value = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Cyan;
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(96, 15);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "{title goes here}";
            // 
            // numericUpDown
            // 
            this.numericUpDown.AutoSize = true;
            this.numericUpDown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.numericUpDown.Location = new System.Drawing.Point(257, 25);
            this.numericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown.TabIndex = 2;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // SciFiSlider
            // 
            this.BackColor = System.Drawing.Color.Navy;
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.trackBar);
            this.DoubleBuffered = true;
            this.Name = "SciFiSlider";
            this.Size = new System.Drawing.Size(318, 80);
            ((System.ComponentModel.ISupportInitialize)this.trackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDown).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label lblTitle;
    }
}
