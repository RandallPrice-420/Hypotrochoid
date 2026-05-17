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
            cmbWaveforms = new System.Windows.Forms.ComboBox();
            lblWaveform = new System.Windows.Forms.Label();
            sliderFrequency = new Spirograph_v1.Controls.RPSciFiSlider.RPSciFiSlider();
            sliderAmplitude = new Spirograph_v1.Controls.RPSciFiSlider.RPSciFiSlider();
            SuspendLayout();
            // 
            // cmbWaveforms
            // 
            cmbWaveforms.FormattingEnabled = true;
            cmbWaveforms.Items.AddRange(new object[] { "AlienSignal", "HarmonicStack", "Heartbeat", "Interference", "Plasma", "Pulsar", "Sawtooth", "Sine", "Square", "Triangle", "WarpField" });
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
            // sliderFrequency
            // 
            sliderFrequency.BackColor = System.Drawing.Color.Black;
            sliderFrequency.Border_Style = System.Windows.Forms.BorderStyle.Fixed3D;
            sliderFrequency.ControlId = "efe20452-6355-4149-97a0-5406b920ca2f";
            sliderFrequency.GlowColor = System.Drawing.Color.Cyan;
            sliderFrequency.LabelTitle = "Frequency:";
            sliderFrequency.LabelValue = "0.0 Hz";
            sliderFrequency.Location = new System.Drawing.Point(14, 46);
            sliderFrequency.Margin = new System.Windows.Forms.Padding(0);
            sliderFrequency.Maximum = 10;
            sliderFrequency.Minimum = -10;
            sliderFrequency.Name = "sliderFrequency";
            sliderFrequency.Size = new System.Drawing.Size(286, 64);
            sliderFrequency.TabIndex = 20;
            sliderFrequency.TickFrequency = 0;
            sliderFrequency.Value = 4;
            sliderFrequency.ValueChanged += sliderFrequency_ValueChanged;
            // 
            // sliderAmplitude
            // 
            sliderAmplitude.BackColor = System.Drawing.Color.Black;
            sliderAmplitude.Border_Style = System.Windows.Forms.BorderStyle.None;
            sliderAmplitude.ControlId = "3daea115-3e11-4e36-8a70-0a70cd124149";
            sliderAmplitude.GlowColor = System.Drawing.Color.Cyan;
            sliderAmplitude.LabelTitle = "Amplitude:";
            sliderAmplitude.LabelValue = "0.0";
            sliderAmplitude.Location = new System.Drawing.Point(14, 100);
            sliderAmplitude.Margin = new System.Windows.Forms.Padding(0);
            sliderAmplitude.Maximum = 10;
            sliderAmplitude.Minimum = -10;
            sliderAmplitude.Name = "sliderAmplitude";
            sliderAmplitude.Size = new System.Drawing.Size(286, 64);
            sliderAmplitude.TabIndex = 21;
            sliderAmplitude.TickFrequency = 0;
            sliderAmplitude.Value = 0;
            sliderAmplitude.ValueChanged += sliderAmplitude_ValueChanged;
            // 
            // RPSciFiWaveform
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Black;
            Controls.Add(sliderAmplitude);
            Controls.Add(sliderFrequency);
            Controls.Add(cmbWaveforms);
            Controls.Add(lblWaveform);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            ForeColor = System.Drawing.Color.Black;
            Name = "RPSciFiWaveform";
            Size = new System.Drawing.Size(320, 174);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWaveforms;
        private System.Windows.Forms.Label    lblWaveform;
        private RPSciFiSlider.RPSciFiSlider   sliderFrequency;
        private RPSciFiSlider.RPSciFiSlider   sliderAmplitude;
    }
}
