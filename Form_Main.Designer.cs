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
            BtnQuit = new System.Windows.Forms.Button();
            PicSpirograph = new System.Windows.Forms.PictureBox();
            DlgSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            BtnSave = new System.Windows.Forms.Button();
            DlgColorDialog = new System.Windows.Forms.ColorDialog();
            BtnColor = new System.Windows.Forms.Button();
            BtnRedraw = new System.Windows.Forms.Button();
            RPSlider_InnerCircle = new Spirograph_v1.Controls.RPSlider.RPSlider();
            RPSlider_OuterCircle = new Spirograph_v1.Controls.RPSlider.RPSlider();
            BtnControls = new System.Windows.Forms.Button();
            RPSlider_Iterations = new Spirograph_v1.Controls.RPSlider.RPSlider();
            CboColorPresets = new System.Windows.Forms.ComboBox();
            LblIterationsCount = new System.Windows.Forms.Label();
            LblPenColor = new System.Windows.Forms.Label();
            LblIterations = new System.Windows.Forms.Label();
            ChkMultiColorGradient = new System.Windows.Forms.CheckBox();
            panel1 = new System.Windows.Forms.Panel();
            ChkIterations = new System.Windows.Forms.CheckBox();
            ChkOuterCircleRadius = new System.Windows.Forms.CheckBox();
            ChkInnerCircleRadius = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            ChkGradientButtons = new System.Windows.Forms.CheckBox();
            RPSciFi_Button_Start = new Spirograph_v1.Controls.RPSciFiButton.RPSciFiButton();
            Btn_AllControls = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)PicSpirograph).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnQuit
            // 
            BtnQuit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnQuit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            BtnQuit.Location = new System.Drawing.Point(193, 622);
            BtnQuit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnQuit.Name = "BtnQuit";
            BtnQuit.Size = new System.Drawing.Size(82, 27);
            BtnQuit.TabIndex = 6;
            BtnQuit.Text = "Quit";
            BtnQuit.UseVisualStyleBackColor = true;
            BtnQuit.Click += BtnQuit_Click;
            // 
            // PicSpirograph
            // 
            PicSpirograph.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            PicSpirograph.BackColor = System.Drawing.Color.FloralWhite;
            PicSpirograph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            PicSpirograph.Location = new System.Drawing.Point(292, 12);
            PicSpirograph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            PicSpirograph.Name = "PicSpirograph";
            PicSpirograph.Size = new System.Drawing.Size(678, 635);
            PicSpirograph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            PicSpirograph.TabIndex = 7;
            PicSpirograph.TabStop = false;
            // 
            // DlgSaveFileDialog
            // 
            DlgSaveFileDialog.Title = "Save Image As...";
            // 
            // BtnSave
            // 
            BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            BtnSave.Location = new System.Drawing.Point(103, 622);
            BtnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new System.Drawing.Size(82, 27);
            BtnSave.TabIndex = 15;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnColor
            // 
            BtnColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnColor.Location = new System.Drawing.Point(336, 502);
            BtnColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnColor.Name = "BtnColor";
            BtnColor.Size = new System.Drawing.Size(96, 27);
            BtnColor.TabIndex = 16;
            BtnColor.Text = "Color";
            BtnColor.UseVisualStyleBackColor = true;
            BtnColor.Visible = false;
            // 
            // BtnRedraw
            // 
            BtnRedraw.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnRedraw.Enabled = false;
            BtnRedraw.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            BtnRedraw.Location = new System.Drawing.Point(13, 622);
            BtnRedraw.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnRedraw.Name = "BtnRedraw";
            BtnRedraw.Size = new System.Drawing.Size(82, 27);
            BtnRedraw.TabIndex = 19;
            BtnRedraw.Text = "Redraw";
            BtnRedraw.UseVisualStyleBackColor = true;
            BtnRedraw.Click += BtnRedraw_Click;
            // 
            // RPSlider_InnerCircle
            // 
            RPSlider_InnerCircle.AutoSize = true;
            RPSlider_InnerCircle.BackColor = System.Drawing.Color.DodgerBlue;
            RPSlider_InnerCircle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            RPSlider_InnerCircle.ForeColor = System.Drawing.Color.Cyan;
            RPSlider_InnerCircle.Location = new System.Drawing.Point(14, 14);
            RPSlider_InnerCircle.Name = "RPSlider_InnerCircle";
            RPSlider_InnerCircle.NumericUpDownValue = new decimal(new int[] { 179, 0, 0, 0 });
            RPSlider_InnerCircle.Size = new System.Drawing.Size(265, 53);
            RPSlider_InnerCircle.SliderMaximum = 200;
            RPSlider_InnerCircle.SliderMinimum = 1;
            RPSlider_InnerCircle.SliderValue = 179;
            RPSlider_InnerCircle.TabIndex = 21;
            RPSlider_InnerCircle.TitleLabel = "Inner Circle Radius";
            // 
            // RPSlider_OuterCircle
            // 
            RPSlider_OuterCircle.AutoSize = true;
            RPSlider_OuterCircle.BackColor = System.Drawing.Color.DodgerBlue;
            RPSlider_OuterCircle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            RPSlider_OuterCircle.ForeColor = System.Drawing.Color.Cyan;
            RPSlider_OuterCircle.Location = new System.Drawing.Point(14, 73);
            RPSlider_OuterCircle.Name = "RPSlider_OuterCircle";
            RPSlider_OuterCircle.NumericUpDownValue = new decimal(new int[] { 100, 0, 0, 0 });
            RPSlider_OuterCircle.Size = new System.Drawing.Size(265, 53);
            RPSlider_OuterCircle.SliderMaximum = 200;
            RPSlider_OuterCircle.SliderMinimum = 1;
            RPSlider_OuterCircle.SliderValue = 100;
            RPSlider_OuterCircle.TabIndex = 22;
            RPSlider_OuterCircle.TitleLabel = "Outer Circle Radius";
            // 
            // BtnControls
            // 
            BtnControls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnControls.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            BtnControls.Location = new System.Drawing.Point(12, 502);
            BtnControls.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnControls.Name = "BtnControls";
            BtnControls.Size = new System.Drawing.Size(96, 27);
            BtnControls.TabIndex = 24;
            BtnControls.Text = "Controls Form";
            BtnControls.UseVisualStyleBackColor = true;
            BtnControls.Click += BtnControlsForm_Click;
            // 
            // RPSlider_Iterations
            // 
            RPSlider_Iterations.AutoSize = true;
            RPSlider_Iterations.BackColor = System.Drawing.Color.DodgerBlue;
            RPSlider_Iterations.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            RPSlider_Iterations.ForeColor = System.Drawing.Color.Cyan;
            RPSlider_Iterations.Location = new System.Drawing.Point(14, 132);
            RPSlider_Iterations.Name = "RPSlider_Iterations";
            RPSlider_Iterations.NumericUpDownValue = new decimal(new int[] { 50, 0, 0, 0 });
            RPSlider_Iterations.Size = new System.Drawing.Size(265, 53);
            RPSlider_Iterations.SliderMaximum = 200;
            RPSlider_Iterations.SliderMinimum = 1;
            RPSlider_Iterations.SliderValue = 50;
            RPSlider_Iterations.TabIndex = 27;
            RPSlider_Iterations.TitleLabel = "Iterations";
            // 
            // CboColorPresets
            // 
            CboColorPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CboColorPresets.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            CboColorPresets.FormattingEnabled = true;
            CboColorPresets.Location = new System.Drawing.Point(179, 192);
            CboColorPresets.Name = "CboColorPresets";
            CboColorPresets.Size = new System.Drawing.Size(100, 25);
            CboColorPresets.Sorted = true;
            CboColorPresets.TabIndex = 26;
            CboColorPresets.SelectedIndexChanged += CboColorPresets_SelectedIndexChanged;
            // 
            // LblIterationsCount
            // 
            LblIterationsCount.BackColor = System.Drawing.Color.Transparent;
            LblIterationsCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            LblIterationsCount.ForeColor = System.Drawing.Color.Bisque;
            LblIterationsCount.Location = new System.Drawing.Point(225, 295);
            LblIterationsCount.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            LblIterationsCount.Name = "LblIterationsCount";
            LblIterationsCount.Size = new System.Drawing.Size(54, 19);
            LblIterationsCount.TabIndex = 28;
            LblIterationsCount.Text = "0";
            LblIterationsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPenColor
            // 
            LblPenColor.BackColor = System.Drawing.Color.Transparent;
            LblPenColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            LblPenColor.ForeColor = System.Drawing.Color.Cyan;
            LblPenColor.Location = new System.Drawing.Point(14, 195);
            LblPenColor.Name = "LblPenColor";
            LblPenColor.Size = new System.Drawing.Size(125, 20);
            LblPenColor.TabIndex = 29;
            LblPenColor.Text = "Pen Color Preset:";
            // 
            // LblIterations
            // 
            LblIterations.AutoSize = true;
            LblIterations.BackColor = System.Drawing.Color.Transparent;
            LblIterations.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            LblIterations.ForeColor = System.Drawing.Color.Cyan;
            LblIterations.Location = new System.Drawing.Point(14, 295);
            LblIterations.Name = "LblIterations";
            LblIterations.Size = new System.Drawing.Size(167, 19);
            LblIterations.TabIndex = 30;
            LblIterations.Text = "Line Drawing Iterations:";
            // 
            // ChkMultiColorGradient
            // 
            ChkMultiColorGradient.BackColor = System.Drawing.Color.Transparent;
            ChkMultiColorGradient.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            ChkMultiColorGradient.ForeColor = System.Drawing.Color.Cyan;
            ChkMultiColorGradient.Location = new System.Drawing.Point(12, 246);
            ChkMultiColorGradient.Name = "ChkMultiColorGradient";
            ChkMultiColorGradient.Size = new System.Drawing.Size(255, 20);
            ChkMultiColorGradient.TabIndex = 31;
            ChkMultiColorGradient.Text = "Multi-Color Gradient Background";
            ChkMultiColorGradient.UseVisualStyleBackColor = false;
            ChkMultiColorGradient.CheckedChanged += ChkMultiColorGradient_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.DodgerBlue;
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            panel1.Controls.Add(ChkIterations);
            panel1.Controls.Add(RPSciFi_Button_Start);
            panel1.Controls.Add(ChkOuterCircleRadius);
            panel1.Controls.Add(ChkInnerCircleRadius);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(14, 340);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(265, 147);
            panel1.TabIndex = 32;
            // 
            // ChkIterations
            // 
            ChkIterations.BackColor = System.Drawing.Color.Transparent;
            ChkIterations.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            ChkIterations.ForeColor = System.Drawing.Color.Cyan;
            ChkIterations.Location = new System.Drawing.Point(10, 86);
            ChkIterations.Name = "ChkIterations";
            ChkIterations.Size = new System.Drawing.Size(95, 20);
            ChkIterations.TabIndex = 34;
            ChkIterations.Text = "Iterations";
            ChkIterations.UseVisualStyleBackColor = false;
            // 
            // ChkOuterCircleRadius
            // 
            ChkOuterCircleRadius.BackColor = System.Drawing.Color.Transparent;
            ChkOuterCircleRadius.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            ChkOuterCircleRadius.ForeColor = System.Drawing.Color.Cyan;
            ChkOuterCircleRadius.Location = new System.Drawing.Point(10, 60);
            ChkOuterCircleRadius.Name = "ChkOuterCircleRadius";
            ChkOuterCircleRadius.Size = new System.Drawing.Size(60159, 20);
            ChkOuterCircleRadius.TabIndex = 33;
            ChkOuterCircleRadius.Text = "Outer Circle Radius";
            ChkOuterCircleRadius.UseVisualStyleBackColor = false;
            // 
            // ChkInnerCircleRadius
            // 
            ChkInnerCircleRadius.BackColor = System.Drawing.Color.Transparent;
            ChkInnerCircleRadius.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            ChkInnerCircleRadius.ForeColor = System.Drawing.Color.Cyan;
            ChkInnerCircleRadius.Location = new System.Drawing.Point(10, 34);
            ChkInnerCircleRadius.Name = "ChkInnerCircleRadius";
            ChkInnerCircleRadius.Size = new System.Drawing.Size(160, 20);
            ChkInnerCircleRadius.TabIndex = 32;
            ChkInnerCircleRadius.Text = "Inner Circle Radius";
            ChkInnerCircleRadius.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.Color.CornflowerBlue;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.Aquamarine;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(261, 25);
            label1.TabIndex = 0;
            label1.Text = "Animation";
            label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChkGradientButtons
            // 
            ChkGradientButtons.BackColor = System.Drawing.Color.Transparent;
            ChkGradientButtons.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            ChkGradientButtons.ForeColor = System.Drawing.Color.Cyan;
            ChkGradientButtons.Location = new System.Drawing.Point(14, 220);
            ChkGradientButtons.Name = "ChkGradientButtons";
            ChkGradientButtons.Size = new System.Drawing.Size(145, 20);
            ChkGradientButtons.TabIndex = 33;
            ChkGradientButtons.Text = "Gradient Buttons";
            ChkGradientButtons.UseVisualStyleBackColor = false;
            ChkGradientButtons.CheckedChanged += ChkGradientButtons_CheckedChanged;
            // 
            // RPSciFi_Button_Start
            // 
            RPSciFi_Button_Start.BaseColor = System.Drawing.Color.FromArgb(30, 30, 50);
            RPSciFi_Button_Start.DisabledColor = System.Drawing.Color.FromArgb(60, 60, 60);
            RPSciFi_Button_Start.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            RPSciFi_Button_Start.ForeColor = System.Drawing.Color.White;
            RPSciFi_Button_Start.GlowColor = System.Drawing.Color.Cyan;
            RPSciFi_Button_Start.Location = new System.Drawing.Point(5, 112);
            RPSciFi_Button_Start.Name = "RPSciFi_Button_Start";
            RPSciFi_Button_Start.Size = new System.Drawing.Size(100, 30);
            RPSciFi_Button_Start.TabIndex = 34;
            // 
            // Btn_AllControls
            // 
            Btn_AllControls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            Btn_AllControls.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            Btn_AllControls.Location = new System.Drawing.Point(179, 502);
            Btn_AllControls.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Btn_AllControls.Name = "Btn_AllControls";
            Btn_AllControls.Size = new System.Drawing.Size(96, 27);
            Btn_AllControls.TabIndex = 35;
            Btn_AllControls.Text = "All Controls";
            Btn_AllControls.UseVisualStyleBackColor = true;
            Btn_AllControls.Click += BtnAll_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.RoyalBlue;
            ClientSize = new System.Drawing.Size(984, 661);
            Controls.Add(Btn_AllControls);
            Controls.Add(ChkGradientButtons);
            Controls.Add(panel1);
            Controls.Add(ChkMultiColorGradient);
            Controls.Add(LblIterations);
            Controls.Add(LblPenColor);
            Controls.Add(LblIterationsCount);
            Controls.Add(RPSlider_Iterations);
            Controls.Add(CboColorPresets);
            Controls.Add(BtnControls);
            Controls.Add(RPSlider_InnerCircle);
            Controls.Add(RPSlider_OuterCircle);
            Controls.Add(BtnRedraw);
            Controls.Add(BtnColor);
            Controls.Add(BtnSave);
            Controls.Add(BtnQuit);
            Controls.Add(PicSpirograph);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormMain";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Spirograph-v1";
            ResizeEnd += Form_Main_ResizeEnd;
            ((System.ComponentModel.ISupportInitialize)PicSpirograph).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.Button BtnControls;
        private System.Windows.Forms.ComboBox CboColorPresets;
        private Controls.RPSlider.RPSlider RPSlider_Iterations;
        private System.Windows.Forms.Label LblIterationsCount;
        private System.Windows.Forms.Label LblPenColor;
        private System.Windows.Forms.Label LblIterations;
        private System.Windows.Forms.CheckBox ChkMultiColorGradient;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkIterations;
        private System.Windows.Forms.CheckBox ChkOuterCircleRadius;
        private System.Windows.Forms.CheckBox ChkInnerCircleRadius;
        private System.Windows.Forms.CheckBox ChkGradientButtons;
        private Controls.RPSciFiButton.RPSciFiButton RPSciFi_Button_Start;
        private System.Windows.Forms.Button Btn_AllControls;
    }
}

