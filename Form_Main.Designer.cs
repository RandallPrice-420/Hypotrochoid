using Spirograph_v1.Controls.Knob;

namespace Spirograph_v1
{
    partial class FormMain
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
            this.BtnQuit = new System.Windows.Forms.Button();
            this.PicSpirograph = new System.Windows.Forms.PictureBox();
            this.DlgSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.BtnSave = new System.Windows.Forms.Button();
            this.DlgColorDialog = new System.Windows.Forms.ColorDialog();
            this.BtnColor = new System.Windows.Forms.Button();
            this.BtnRedraw = new System.Windows.Forms.Button();
            this.RPSlider_InnerCircle = new Spirograph_v1.Controls.RPSlider.RPSlider();
            this.RPSlider_OuterCircle = new Spirograph_v1.Controls.RPSlider.RPSlider();
            this.BtnControls = new System.Windows.Forms.Button();
            this.RPSlider_Iterations = new Spirograph_v1.Controls.RPSlider.RPSlider();
            this.CboColorPresets = new System.Windows.Forms.ComboBox();
            this.LblIterationsCount = new System.Windows.Forms.Label();
            this.LblPenColor = new System.Windows.Forms.Label();
            this.LblIterations = new System.Windows.Forms.Label();
            this.ChkMultiColorGradient = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)this.PicSpirograph).BeginInit();
            this.SuspendLayout();
            // 
            // BtnQuit
            // 
            this.BtnQuit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnQuit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnQuit.Location = new System.Drawing.Point(193, 328);
            this.BtnQuit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.Size = new System.Drawing.Size(82, 27);
            this.BtnQuit.TabIndex = 6;
            this.BtnQuit.Text = "Quit";
            this.BtnQuit.UseVisualStyleBackColor = true;
            this.BtnQuit.Click += this.BtnQuit_Click;
            // 
            // PicSpirograph
            // 
            this.PicSpirograph.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.PicSpirograph.BackColor = System.Drawing.Color.FloralWhite;
            this.PicSpirograph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PicSpirograph.Location = new System.Drawing.Point(295, 14);
            this.PicSpirograph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PicSpirograph.Name = "PicSpirograph";
            this.PicSpirograph.Size = new System.Drawing.Size(671, 635);
            this.PicSpirograph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicSpirograph.TabIndex = 7;
            this.PicSpirograph.TabStop = false;
            // 
            // DlgSaveFileDialog
            // 
            this.DlgSaveFileDialog.Title = "Save Image As...";
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Location = new System.Drawing.Point(103, 328);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(82, 27);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += this.BtnSave_Click;
            // 
            // BtnColor
            // 
            this.BtnColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnColor.Location = new System.Drawing.Point(117, 622);
            this.BtnColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnColor.Name = "BtnColor";
            this.BtnColor.Size = new System.Drawing.Size(96, 27);
            this.BtnColor.TabIndex = 16;
            this.BtnColor.Text = "Color";
            this.BtnColor.UseVisualStyleBackColor = true;
            this.BtnColor.Visible = false;
            // 
            // BtnRedraw
            // 
            this.BtnRedraw.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnRedraw.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnRedraw.Location = new System.Drawing.Point(13, 328);
            this.BtnRedraw.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnRedraw.Name = "BtnRedraw";
            this.BtnRedraw.Size = new System.Drawing.Size(82, 27);
            this.BtnRedraw.TabIndex = 19;
            this.BtnRedraw.Text = "Redraw";
            this.BtnRedraw.UseVisualStyleBackColor = true;
            this.BtnRedraw.Click += this.BtnRedraw_Click;
            // 
            // RPSlider_InnerCircle
            // 
            this.RPSlider_InnerCircle.AutoSize = true;
            this.RPSlider_InnerCircle.BackColor = System.Drawing.Color.DodgerBlue;
            this.RPSlider_InnerCircle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.RPSlider_InnerCircle.ForeColor = System.Drawing.Color.Cyan;
            this.RPSlider_InnerCircle.Location = new System.Drawing.Point(14, 14);
            this.RPSlider_InnerCircle.Name = "RPSlider_InnerCircle";
            this.RPSlider_InnerCircle.NumericUpDownValue = new decimal(new int[] { 179, 0, 0, 0 });
            this.RPSlider_InnerCircle.Size = new System.Drawing.Size(265, 53);
            this.RPSlider_InnerCircle.SliderMaximum = 200;
            this.RPSlider_InnerCircle.SliderMinimum = 1;
            this.RPSlider_InnerCircle.SliderValue = 179;
            this.RPSlider_InnerCircle.TabIndex = 21;
            this.RPSlider_InnerCircle.TitleLabel = "Inner Circle Radius";
            // 
            // RPSlider_OuterCircle
            // 
            this.RPSlider_OuterCircle.AutoSize = true;
            this.RPSlider_OuterCircle.BackColor = System.Drawing.Color.DodgerBlue;
            this.RPSlider_OuterCircle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.RPSlider_OuterCircle.ForeColor = System.Drawing.Color.Cyan;
            this.RPSlider_OuterCircle.Location = new System.Drawing.Point(14, 73);
            this.RPSlider_OuterCircle.Name = "RPSlider_OuterCircle";
            this.RPSlider_OuterCircle.NumericUpDownValue = new decimal(new int[] { 100, 0, 0, 0 });
            this.RPSlider_OuterCircle.Size = new System.Drawing.Size(265, 53);
            this.RPSlider_OuterCircle.SliderMaximum = 200;
            this.RPSlider_OuterCircle.SliderMinimum = 1;
            this.RPSlider_OuterCircle.SliderValue = 100;
            this.RPSlider_OuterCircle.TabIndex = 22;
            this.RPSlider_OuterCircle.TitleLabel = "Outer Circle Radius";
            // 
            // BtnControls
            // 
            this.BtnControls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnControls.Location = new System.Drawing.Point(13, 622);
            this.BtnControls.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnControls.Name = "BtnControls";
            this.BtnControls.Size = new System.Drawing.Size(96, 27);
            this.BtnControls.TabIndex = 24;
            this.BtnControls.Text = "Controls Form";
            this.BtnControls.UseVisualStyleBackColor = true;
            this.BtnControls.Visible = false;
            this.BtnControls.Click += this.BtnControlsForm_Click;
            // 
            // RPSlider_Iterations
            // 
            this.RPSlider_Iterations.AutoSize = true;
            this.RPSlider_Iterations.BackColor = System.Drawing.Color.DodgerBlue;
            this.RPSlider_Iterations.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.RPSlider_Iterations.ForeColor = System.Drawing.Color.Cyan;
            this.RPSlider_Iterations.Location = new System.Drawing.Point(14, 132);
            this.RPSlider_Iterations.Name = "RPSlider_Iterations";
            this.RPSlider_Iterations.NumericUpDownValue = new decimal(new int[] { 50, 0, 0, 0 });
            this.RPSlider_Iterations.Size = new System.Drawing.Size(265, 53);
            this.RPSlider_Iterations.SliderMaximum = 200;
            this.RPSlider_Iterations.SliderMinimum = 1;
            this.RPSlider_Iterations.SliderValue = 50;
            this.RPSlider_Iterations.TabIndex = 27;
            this.RPSlider_Iterations.TitleLabel = "Iterations";
            // 
            // CboColorPresets
            // 
            this.CboColorPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboColorPresets.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.CboColorPresets.FormattingEnabled = true;
            this.CboColorPresets.Location = new System.Drawing.Point(180, 196);
            this.CboColorPresets.Name = "CboColorPresets";
            this.CboColorPresets.Size = new System.Drawing.Size(100, 25);
            this.CboColorPresets.Sorted = true;
            this.CboColorPresets.TabIndex = 26;
            this.CboColorPresets.SelectedIndexChanged += this.CboColorPresets_SelectedIndexChanged;
            // 
            // LblIterationsCount
            // 
            this.LblIterationsCount.BackColor = System.Drawing.Color.Transparent;
            this.LblIterationsCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblIterationsCount.ForeColor = System.Drawing.Color.Bisque;
            this.LblIterationsCount.Location = new System.Drawing.Point(229, 259);
            this.LblIterationsCount.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LblIterationsCount.Name = "LblIterationsCount";
            this.LblIterationsCount.Size = new System.Drawing.Size(54, 19);
            this.LblIterationsCount.TabIndex = 28;
            this.LblIterationsCount.Text = "0";
            this.LblIterationsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPenColor
            // 
            this.LblPenColor.AutoSize = true;
            this.LblPenColor.BackColor = System.Drawing.Color.Transparent;
            this.LblPenColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblPenColor.ForeColor = System.Drawing.Color.Cyan;
            this.LblPenColor.Location = new System.Drawing.Point(14, 199);
            this.LblPenColor.Name = "LblPenColor";
            this.LblPenColor.Size = new System.Drawing.Size(80, 19);
            this.LblPenColor.TabIndex = 29;
            this.LblPenColor.Text = "Pen Color:";
            // 
            // LblIterations
            // 
            this.LblIterations.AutoSize = true;
            this.LblIterations.BackColor = System.Drawing.Color.Transparent;
            this.LblIterations.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblIterations.ForeColor = System.Drawing.Color.Cyan;
            this.LblIterations.Location = new System.Drawing.Point(14, 260);
            this.LblIterations.Name = "LblIterations";
            this.LblIterations.Size = new System.Drawing.Size(115, 19);
            this.LblIterations.TabIndex = 30;
            this.LblIterations.Text = "Draw Iterations:";
            // 
            // ChkMultiColorGradient
            // 
            this.ChkMultiColorGradient.AutoSize = true;
            this.ChkMultiColorGradient.BackColor = System.Drawing.Color.Transparent;
            this.ChkMultiColorGradient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkMultiColorGradient.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ChkMultiColorGradient.ForeColor = System.Drawing.Color.Cyan;
            this.ChkMultiColorGradient.Location = new System.Drawing.Point(14, 228);
            this.ChkMultiColorGradient.Name = "ChkMultiColorGradient";
            this.ChkMultiColorGradient.Size = new System.Drawing.Size(268, 23);
            this.ChkMultiColorGradient.TabIndex = 31;
            this.ChkMultiColorGradient.Text = "Multi-Color Gradient Background:   ";
            this.ChkMultiColorGradient.UseVisualStyleBackColor = false;
            this.ChkMultiColorGradient.CheckedChanged += this.ChkMultiColorGradient_CheckedChanged;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.ChkMultiColorGradient);
            this.Controls.Add(this.LblIterations);
            this.Controls.Add(this.LblPenColor);
            this.Controls.Add(this.LblIterationsCount);
            this.Controls.Add(this.RPSlider_Iterations);
            this.Controls.Add(this.CboColorPresets);
            this.Controls.Add(this.BtnControls);
            this.Controls.Add(this.RPSlider_InnerCircle);
            this.Controls.Add(this.RPSlider_OuterCircle);
            this.Controls.Add(this.BtnRedraw);
            this.Controls.Add(this.BtnColor);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.PicSpirograph);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spirograph-v1";
            this.ResizeEnd += this.Form_Main_ResizeEnd;
            ((System.ComponentModel.ISupportInitialize)this.PicSpirograph).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnQuit;
        private System.Windows.Forms.PictureBox PicSpirograph;
        private System.Windows.Forms.SaveFileDialog DlgSaveFileDialog;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.ColorDialog DlgColorDialog;
        private System.Windows.Forms.Button BtnColor;
        private System.Windows.Forms.Button BtnRedraw;
        private Controls.RPSlider.RPSlider RPSlider_InnerCircle;
        private Controls.RPSlider.RPSlider RPSlider_OuterCircle;
        private Controls.SciFiSlider.SciFiSlider sciFiSliderControl_InnerCircle;
        private System.Windows.Forms.Button BtnControls;
        private System.Windows.Forms.ComboBox CboColorPresets;
        private Controls.RPSlider.RPSlider RPSlider_Iterations;
        private System.Windows.Forms.Label LblIterationsCount;
        private System.Windows.Forms.Label LblPenColor;
        private System.Windows.Forms.Label LblIterations;
        private System.Windows.Forms.CheckBox ChkMultiColorGradient;
    }
}

