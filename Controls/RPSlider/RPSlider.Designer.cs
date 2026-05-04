namespace Spirograph_v1.Controls.RPSlider
{
    partial class RPSlider
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
            this.LabelTitle = new System.Windows.Forms.Label();
            this.LabelRange = new System.Windows.Forms.Label();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.NumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)this.TrackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown1).BeginInit();
            this.SuspendLayout();
            // 
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LabelTitle.Location = new System.Drawing.Point(0, 2);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(45, 19);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "{title}";
            // 
            // LabelRange
            // 
            this.LabelRange.AutoSize = true;
            this.LabelRange.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LabelRange.Location = new System.Drawing.Point(147, 2);
            this.LabelRange.Name = "LabelRange";
            this.LabelRange.Size = new System.Drawing.Size(58, 19);
            this.LabelRange.TabIndex = 1;
            this.LabelRange.Text = "{range}";
            // 
            // TrackBar1
            // 
            this.TrackBar1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.TrackBar1.AutoSize = false;
            this.TrackBar1.Location = new System.Drawing.Point(0, 20);
            this.TrackBar1.Maximum = 200;
            this.TrackBar1.MaximumSize = new System.Drawing.Size(400, 500);
            this.TrackBar1.Minimum = 1;
            this.TrackBar1.MinimumSize = new System.Drawing.Size(200, 30);
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(200, 30);
            this.TrackBar1.TabIndex = 3;
            this.TrackBar1.TickFrequency = 10;
            this.TrackBar1.Value = 1;
            this.TrackBar1.ValueChanged += this.TrackBar1_ValueChanged;
            this.TrackBar1.MouseDown += this.TrackBar1_MouseDown;
            this.TrackBar1.MouseMove += this.TrackBar1_MouseMove;
            this.TrackBar1.MouseUp += this.TrackBar1_MouseUp;
            // 
            // NumericUpDown1
            // 
            this.NumericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.NumericUpDown1.AutoSize = true;
            this.NumericUpDown1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.NumericUpDown1.Location = new System.Drawing.Point(206, 20);
            this.NumericUpDown1.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.NumericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.NumericUpDown1.Name = "NumericUpDown1";
            this.NumericUpDown1.Size = new System.Drawing.Size(48, 23);
            this.NumericUpDown1.TabIndex = 4;
            this.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            this.NumericUpDown1.ValueChanged += this.NumericUpDown1_ValueChanged;
            // 
            // RPSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.NumericUpDown1);
            this.Controls.Add(this.TrackBar1);
            this.Controls.Add(this.LabelRange);
            this.Controls.Add(this.LabelTitle);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "RPSlider";
            this.Size = new System.Drawing.Size(260, 53);
            this.SizeChanged += this.RPSlider_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)this.TrackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDown1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label LabelTitle;
        private System.Windows.Forms.Label LabelRange;
        private System.Windows.Forms.TrackBar TrackBar1;
        private System.Windows.Forms.NumericUpDown NumericUpDown1;
    }
}
