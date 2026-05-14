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
            LabelFrequency = new System.Windows.Forms.Label();
            rpSciFiSliderFrequency = new Spirograph_v1.Controls.RPSciFiSlider.RPSciFiSlider();
            numericUpDownFrequency = new System.Windows.Forms.NumericUpDown();
            numericUpDownAmplitude = new System.Windows.Forms.NumericUpDown();
            rpSciFiSliderAmplitude = new Spirograph_v1.Controls.RPSciFiSlider.RPSciFiSlider();
            LabelAmplitude = new System.Windows.Forms.Label();
            LabelFrequencyValue = new System.Windows.Forms.Label();
            LabelAmplitudeValue = new System.Windows.Forms.Label();
            LabelWaveform = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFrequency).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAmplitude).BeginInit();
            SuspendLayout();
            // 
            // cmbWaveforms
            // 
            cmbWaveforms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbWaveforms.FormattingEnabled = true;
            cmbWaveforms.Items.AddRange(new object[] { "AlienSignal", "HarmonicStack", "Heartbeat", "Interference", "Plasma", "Pulsar", "Sawtooth", "Sine", "Square", "Triangle", "WarpField" });
            cmbWaveforms.Location = new System.Drawing.Point(88, 15);
            cmbWaveforms.Margin = new System.Windows.Forms.Padding(0);
            cmbWaveforms.MaxDropDownItems = 11;
            cmbWaveforms.Name = "cmbWaveforms";
            cmbWaveforms.Size = new System.Drawing.Size(160, 23);
            cmbWaveforms.TabIndex = 0;
            // 
            // LabelFrequency
            // 
            LabelFrequency.AutoSize = true;
            LabelFrequency.ForeColor = System.Drawing.Color.Aqua;
            LabelFrequency.Location = new System.Drawing.Point(11, 52);
            LabelFrequency.Margin = new System.Windows.Forms.Padding(0);
            LabelFrequency.Name = "LabelFrequency";
            LabelFrequency.Size = new System.Drawing.Size(68, 15);
            LabelFrequency.TabIndex = 1;
            LabelFrequency.Text = "Frequency:";
            // 
            // rpSciFiSliderFrequency
            // 
            rpSciFiSliderFrequency.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rpSciFiSliderFrequency.BackColor = System.Drawing.Color.Transparent;
            rpSciFiSliderFrequency.ControlId = "959d6c0a-1357-490f-92fe-a895b4d35234";
            rpSciFiSliderFrequency.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiSliderFrequency.Location = new System.Drawing.Point(5, 70);
            rpSciFiSliderFrequency.Margin = new System.Windows.Forms.Padding(0);
            rpSciFiSliderFrequency.Maximum = 10;
            rpSciFiSliderFrequency.Minimum = -10;
            rpSciFiSliderFrequency.Name = "rpSciFiSliderFrequency";
            rpSciFiSliderFrequency.Size = new System.Drawing.Size(186, 24);
            rpSciFiSliderFrequency.TabIndex = 2;
            rpSciFiSliderFrequency.TickFrequency = 0;
            rpSciFiSliderFrequency.Value = 0;
            rpSciFiSliderFrequency.ValueChanged += rpSciFiSliderFrequency_ValueChanged;
            // 
            // numericUpDownFrequency
            // 
            numericUpDownFrequency.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            numericUpDownFrequency.DecimalPlaces = 1;
            numericUpDownFrequency.Location = new System.Drawing.Point(193, 71);
            numericUpDownFrequency.Margin = new System.Windows.Forms.Padding(0);
            numericUpDownFrequency.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownFrequency.Minimum = new decimal(new int[] { 100, 0, 0, -2147418112 });
            numericUpDownFrequency.Name = "numericUpDownFrequency";
            numericUpDownFrequency.Size = new System.Drawing.Size(55, 23);
            numericUpDownFrequency.TabIndex = 3;
            numericUpDownFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericUpDownAmplitude
            // 
            numericUpDownAmplitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            numericUpDownAmplitude.DecimalPlaces = 1;
            numericUpDownAmplitude.Location = new System.Drawing.Point(195, 128);
            numericUpDownAmplitude.Margin = new System.Windows.Forms.Padding(0);
            numericUpDownAmplitude.Maximum = new decimal(new int[] { 100, 0, 0, 65536 });
            numericUpDownAmplitude.Minimum = new decimal(new int[] { 100, 0, 0, -2147418112 });
            numericUpDownAmplitude.Name = "numericUpDownAmplitude";
            numericUpDownAmplitude.Size = new System.Drawing.Size(53, 23);
            numericUpDownAmplitude.TabIndex = 6;
            numericUpDownAmplitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rpSciFiSliderAmplitude
            // 
            rpSciFiSliderAmplitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rpSciFiSliderAmplitude.BackColor = System.Drawing.Color.Transparent;
            rpSciFiSliderAmplitude.ControlId = "959d6c0a-1357-490f-92fe-a895b4d35234";
            rpSciFiSliderAmplitude.GlowColor = System.Drawing.Color.Cyan;
            rpSciFiSliderAmplitude.Location = new System.Drawing.Point(5, 128);
            rpSciFiSliderAmplitude.Margin = new System.Windows.Forms.Padding(0);
            rpSciFiSliderAmplitude.Maximum = 10;
            rpSciFiSliderAmplitude.Minimum = -10;
            rpSciFiSliderAmplitude.Name = "rpSciFiSliderAmplitude";
            rpSciFiSliderAmplitude.Size = new System.Drawing.Size(186, 24);
            rpSciFiSliderAmplitude.TabIndex = 5;
            rpSciFiSliderAmplitude.TickFrequency = 0;
            rpSciFiSliderAmplitude.Value = 0;
            rpSciFiSliderAmplitude.ValueChanged += rpSciFiSliderAmplitude_ValueChanged;
            // 
            // LabelAmplitude
            // 
            LabelAmplitude.AutoSize = true;
            LabelAmplitude.ForeColor = System.Drawing.Color.Aqua;
            LabelAmplitude.Location = new System.Drawing.Point(11, 110);
            LabelAmplitude.Margin = new System.Windows.Forms.Padding(0);
            LabelAmplitude.Name = "LabelAmplitude";
            LabelAmplitude.Size = new System.Drawing.Size(68, 15);
            LabelAmplitude.TabIndex = 4;
            LabelAmplitude.Text = "Amplitude:";
            // 
            // LabelFrequencyValue
            // 
            LabelFrequencyValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            LabelFrequencyValue.ForeColor = System.Drawing.Color.Aqua;
            LabelFrequencyValue.Location = new System.Drawing.Point(131, 52);
            LabelFrequencyValue.Margin = new System.Windows.Forms.Padding(0);
            LabelFrequencyValue.Name = "LabelFrequencyValue";
            LabelFrequencyValue.Size = new System.Drawing.Size(50, 15);
            LabelFrequencyValue.TabIndex = 7;
            LabelFrequencyValue.Text = "0.0 Hz";
            LabelFrequencyValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelAmplitudeValue
            // 
            LabelAmplitudeValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            LabelAmplitudeValue.ForeColor = System.Drawing.Color.Aqua;
            LabelAmplitudeValue.Location = new System.Drawing.Point(131, 110);
            LabelAmplitudeValue.Margin = new System.Windows.Forms.Padding(0);
            LabelAmplitudeValue.Name = "LabelAmplitudeValue";
            LabelAmplitudeValue.Size = new System.Drawing.Size(50, 15);
            LabelAmplitudeValue.TabIndex = 8;
            LabelAmplitudeValue.Text = "0.0";
            LabelAmplitudeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelWaveform
            // 
            LabelWaveform.AutoSize = true;
            LabelWaveform.ForeColor = System.Drawing.Color.Aqua;
            LabelWaveform.Location = new System.Drawing.Point(11, 18);
            LabelWaveform.Margin = new System.Windows.Forms.Padding(0);
            LabelWaveform.Name = "LabelWaveform";
            LabelWaveform.Size = new System.Drawing.Size(70, 15);
            LabelWaveform.TabIndex = 9;
            LabelWaveform.Text = "Waveform:";
            // 
            // RPSciFiWaveform
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Black;
            Controls.Add(LabelWaveform);
            Controls.Add(LabelAmplitudeValue);
            Controls.Add(LabelFrequencyValue);
            Controls.Add(numericUpDownAmplitude);
            Controls.Add(rpSciFiSliderAmplitude);
            Controls.Add(LabelAmplitude);
            Controls.Add(numericUpDownFrequency);
            Controls.Add(rpSciFiSliderFrequency);
            Controls.Add(LabelFrequency);
            Controls.Add(cmbWaveforms);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            Name = "RPSciFiWaveform";
            Size = new System.Drawing.Size(260, 166);
            ((System.ComponentModel.ISupportInitialize)numericUpDownFrequency).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAmplitude).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWaveforms;
        private System.Windows.Forms.Label LabelFrequency;
        private RPSciFiSlider.RPSciFiSlider rpSciFiSliderFrequency;
        private System.Windows.Forms.NumericUpDown numericUpDownFrequency;
        private System.Windows.Forms.NumericUpDown numericUpDownAmplitude;
        private RPSciFiSlider.RPSciFiSlider rpSciFiSliderAmplitude;
        private System.Windows.Forms.Label LabelAmplitude;
        private System.Windows.Forms.Label LabelFrequencyValue;
        private System.Windows.Forms.Label LabelAmplitudeValue;
        private System.Windows.Forms.Label LabelWaveform;
    }
}
