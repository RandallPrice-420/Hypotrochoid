namespace Spirograph_v1.Controls.SciFiWaveform
{
    partial class RPSciFiWaveform
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
            numUpDownAmplitude = new System.Windows.Forms.NumericUpDown();
            lblAmplitudeValue = new System.Windows.Forms.Label();
            sliderAmplitude = new Spirograph_v1.Controls.RPSciFiSlider.RPSciFiSlider();
            lblAmplitude = new System.Windows.Forms.Label();
            cmbWaveforms = new System.Windows.Forms.ComboBox();
            lblWaveform = new System.Windows.Forms.Label();
            numUpDownFrequency = new System.Windows.Forms.NumericUpDown();
            lblFrequencyValue = new System.Windows.Forms.Label();
            sliderFrequency = new Spirograph_v1.Controls.RPSciFiSlider.RPSciFiSlider();
            lblFrequency = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)numUpDownAmplitude).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUpDownFrequency).BeginInit();
            SuspendLayout();
            // 
            // numUpDownAmplitude
            // 
            numUpDownAmplitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            numUpDownAmplitude.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            numUpDownAmplitude.ForeColor = System.Drawing.Color.Firebrick;
            numUpDownAmplitude.Location = new System.Drawing.Point(245, 130);
            numUpDownAmplitude.Margin = new System.Windows.Forms.Padding(0);
            numUpDownAmplitude.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numUpDownAmplitude.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numUpDownAmplitude.Name = "numUpDownAmplitude";
            numUpDownAmplitude.Size = new System.Drawing.Size(55, 25);
            numUpDownAmplitude.TabIndex = 19;
            numUpDownAmplitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAmplitudeValue
            // 
            lblAmplitudeValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblAmplitudeValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblAmplitudeValue.ForeColor = System.Drawing.Color.Cyan;
            lblAmplitudeValue.Location = new System.Drawing.Point(177, 105);
            lblAmplitudeValue.Name = "lblAmplitudeValue";
            lblAmplitudeValue.Size = new System.Drawing.Size(60, 20);
            lblAmplitudeValue.TabIndex = 18;
            lblAmplitudeValue.Text = "-10.0";
            lblAmplitudeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sliderAmplitude
            // 
            sliderAmplitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sliderAmplitude.BackColor = System.Drawing.Color.Transparent;
            sliderAmplitude.ControlId = "21d4434e-2cce-432b-ac75-6fae2117c87d";
            sliderAmplitude.GlowColor = System.Drawing.Color.Cyan;
            sliderAmplitude.Location = new System.Drawing.Point(11, 130);
            sliderAmplitude.Margin = new System.Windows.Forms.Padding(0);
            sliderAmplitude.Maximum = 10;
            sliderAmplitude.Minimum = -10;
            sliderAmplitude.Name = "sliderAmplitude";
            sliderAmplitude.Size = new System.Drawing.Size(225, 24);
            sliderAmplitude.TabIndex = 17;
            sliderAmplitude.TickFrequency = 0;
            sliderAmplitude.Value = 0;
            // 
            // lblAmplitude
            // 
            lblAmplitude.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblAmplitude.ForeColor = System.Drawing.Color.Cyan;
            lblAmplitude.Location = new System.Drawing.Point(14, 105);
            lblAmplitude.Name = "lblAmplitude";
            lblAmplitude.Size = new System.Drawing.Size(84, 20);
            lblAmplitude.TabIndex = 16;
            lblAmplitude.Text = "Amplitude:";
            lblAmplitude.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblAmplitude.UseWaitCursor = true;
            // 
            // cmbWaveforms
            // 
            cmbWaveforms.FormattingEnabled = true;
            cmbWaveforms.Location = new System.Drawing.Point(104, 20);
            cmbWaveforms.Name = "cmbWaveforms";
            cmbWaveforms.Size = new System.Drawing.Size(196, 23);
            cmbWaveforms.TabIndex = 15;
            // 
            // lblWaveform
            // 
            lblWaveform.AutoSize = true;
            lblWaveform.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblWaveform.ForeColor = System.Drawing.Color.Cyan;
            lblWaveform.Location = new System.Drawing.Point(14, 20);
            lblWaveform.Name = "lblWaveform";
            lblWaveform.Size = new System.Drawing.Size(83, 19);
            lblWaveform.TabIndex = 14;
            lblWaveform.Text = "Waveform:";
            lblWaveform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblWaveform.UseWaitCursor = true;
            // 
            // numUpDownFrequency
            // 
            numUpDownFrequency.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            numUpDownFrequency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            numUpDownFrequency.ForeColor = System.Drawing.Color.Firebrick;
            numUpDownFrequency.Location = new System.Drawing.Point(245, 75);
            numUpDownFrequency.Margin = new System.Windows.Forms.Padding(0);
            numUpDownFrequency.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numUpDownFrequency.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numUpDownFrequency.Name = "numUpDownFrequency";
            numUpDownFrequency.Size = new System.Drawing.Size(55, 25);
            numUpDownFrequency.TabIndex = 13;
            numUpDownFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblFrequencyValue
            // 
            lblFrequencyValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblFrequencyValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblFrequencyValue.ForeColor = System.Drawing.Color.Cyan;
            lblFrequencyValue.Location = new System.Drawing.Point(177, 50);
            lblFrequencyValue.Name = "lblFrequencyValue";
            lblFrequencyValue.Size = new System.Drawing.Size(60, 20);
            lblFrequencyValue.TabIndex = 12;
            lblFrequencyValue.Text = "-10.0 Hz";
            lblFrequencyValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sliderFrequency
            // 
            sliderFrequency.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sliderFrequency.BackColor = System.Drawing.Color.Transparent;
            sliderFrequency.ControlId = "555f65d7-c6ce-41fd-b989-b2a6d6b6a232";
            sliderFrequency.GlowColor = System.Drawing.Color.Cyan;
            sliderFrequency.Location = new System.Drawing.Point(11, 75);
            sliderFrequency.Margin = new System.Windows.Forms.Padding(0);
            sliderFrequency.Maximum = 10;
            sliderFrequency.Minimum = -10;
            sliderFrequency.Name = "sliderFrequency";
            sliderFrequency.Size = new System.Drawing.Size(225, 24);
            sliderFrequency.TabIndex = 11;
            sliderFrequency.TickFrequency = 0;
            sliderFrequency.Value = 0;
            // 
            // lblFrequency
            // 
            lblFrequency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblFrequency.ForeColor = System.Drawing.Color.Cyan;
            lblFrequency.Location = new System.Drawing.Point(14, 50);
            lblFrequency.Name = "lblFrequency";
            lblFrequency.Size = new System.Drawing.Size(84, 20);
            lblFrequency.TabIndex = 10;
            lblFrequency.Text = "Frequency:";
            lblFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblFrequency.UseWaitCursor = true;
            // 
            // RPSciFiWaveform
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Black;
            Controls.Add(cmbWaveforms);
            Controls.Add(lblAmplitude);
            Controls.Add(lblAmplitudeValue);
            Controls.Add(lblFrequency);
            Controls.Add(lblFrequencyValue);
            Controls.Add(lblWaveform);
            Controls.Add(numUpDownAmplitude);
            Controls.Add(numUpDownFrequency);
            Controls.Add(sliderAmplitude);
            Controls.Add(sliderFrequency);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            ForeColor = System.Drawing.Color.Black;
            Name = "RPSciFiWaveform";
            Size = new System.Drawing.Size(320, 175);
            ((System.ComponentModel.ISupportInitialize)numUpDownAmplitude).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUpDownFrequency).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox      cmbWaveforms;
        private System.Windows.Forms.Label         lblAmplitude;
        private System.Windows.Forms.Label         lblAmplitudeValue;
        private System.Windows.Forms.Label         lblFrequency;
        private System.Windows.Forms.Label         lblFrequencyValue;
        private System.Windows.Forms.Label         lblWaveform;
        private System.Windows.Forms.NumericUpDown numUpDownAmplitude;
        private System.Windows.Forms.NumericUpDown numUpDownFrequency;
        private RPSciFiSlider.RPSciFiSlider        sliderAmplitude;
        private RPSciFiSlider.RPSciFiSlider        sliderFrequency;
    }
}
