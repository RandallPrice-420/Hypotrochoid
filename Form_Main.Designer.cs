using Spirograph_v1.Controls.Knob;
using Spirograph_v1.Controls.TrackBar;

namespace Spirograph_v1
{
    partial class Form_Main
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
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.BtnSave = new System.Windows.Forms.Button();
            this.ColorDialog = new System.Windows.Forms.ColorDialog();
            this.BtnColor = new System.Windows.Forms.Button();
            this.LabelIteration = new System.Windows.Forms.Label();
            this.LabelIterationCount = new System.Windows.Forms.Label();
            this.BtnRedraw = new System.Windows.Forms.Button();
            this.knobSciFiPlus1 = new KnobSciFiPlus();
            this.TextIterations = new System.Windows.Forms.TextBox();
            this.LabelIter = new System.Windows.Forms.Label();
            this.RPSlider_InnerCircle = new Spirograph_v1.Controls.RPSlider.RPSlider();
            this.RPSlider_OuterCircle = new Spirograph_v1.Controls.RPSlider.RPSlider();
            ((System.ComponentModel.ISupportInitialize)this.PictureBox).BeginInit();
            this.SuspendLayout();
            // 
            // BtnQuit
            // 
            this.BtnQuit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnQuit.Location = new System.Drawing.Point(646, 12);
            this.BtnQuit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.Size = new System.Drawing.Size(88, 27);
            this.BtnQuit.TabIndex = 6;
            this.BtnQuit.Text = "Quit";
            this.BtnQuit.UseVisualStyleBackColor = true;
            this.BtnQuit.Click += this.BtnQuit_Click;
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.PictureBox.BackColor = System.Drawing.Color.FloralWhite;
            this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PictureBox.Location = new System.Drawing.Point(14, 208);
            this.PictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(720, 479);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox.TabIndex = 7;
            this.PictureBox.TabStop = false;
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Title = "Save Image As...";
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnSave.Location = new System.Drawing.Point(550, 12);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(88, 27);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += this.BtnSave_Click;
            // 
            // BtnColor
            // 
            this.BtnColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnColor.Location = new System.Drawing.Point(648, 45);
            this.BtnColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnColor.Name = "BtnColor";
            this.BtnColor.Size = new System.Drawing.Size(88, 27);
            this.BtnColor.TabIndex = 16;
            this.BtnColor.Text = "Color";
            this.BtnColor.UseVisualStyleBackColor = true;
            this.BtnColor.Click += this.BtnColor_Click;
            // 
            // LabelIteration
            // 
            this.LabelIteration.AutoSize = true;
            this.LabelIteration.Location = new System.Drawing.Point(447, 18);
            this.LabelIteration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelIteration.Name = "LabelIteration";
            this.LabelIteration.Size = new System.Drawing.Size(54, 15);
            this.LabelIteration.TabIndex = 17;
            this.LabelIteration.Text = "Iteration:";
            // 
            // LabelIterationCount
            // 
            this.LabelIterationCount.AutoSize = true;
            this.LabelIterationCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.LabelIterationCount.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelIterationCount.Location = new System.Drawing.Point(510, 18);
            this.LabelIterationCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelIterationCount.Name = "LabelIterationCount";
            this.LabelIterationCount.Size = new System.Drawing.Size(14, 13);
            this.LabelIterationCount.TabIndex = 18;
            this.LabelIterationCount.Text = "0";
            this.LabelIterationCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnRedraw
            // 
            this.BtnRedraw.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.BtnRedraw.Location = new System.Drawing.Point(550, 45);
            this.BtnRedraw.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnRedraw.Name = "BtnRedraw";
            this.BtnRedraw.Size = new System.Drawing.Size(88, 27);
            this.BtnRedraw.TabIndex = 19;
            this.BtnRedraw.Text = "Redraw";
            this.BtnRedraw.UseVisualStyleBackColor = true;
            this.BtnRedraw.Click += this.BtnRedraw_Click;
            // 
            // knobSciFiPlus1
            // 
            this.knobSciFiPlus1.Location = new System.Drawing.Point(312, 12);
            this.knobSciFiPlus1.MinimumSize = new System.Drawing.Size(100, 100);
            this.knobSciFiPlus1.Mode = KnobSciFiPlus.ColorMode.NeonCyan;
            this.knobSciFiPlus1.Name = "knobSciFiPlus1";
            this.knobSciFiPlus1.Size = new System.Drawing.Size(100, 100);
            this.knobSciFiPlus1.TabIndex = 20;
            this.knobSciFiPlus1.Text = "knobSciFiPlus1";
            this.knobSciFiPlus1.Value = 50;
            // 
            // TextIterations
            // 
            this.TextIterations.Location = new System.Drawing.Point(468, 48);
            this.TextIterations.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextIterations.Name = "TextIterations";
            this.TextIterations.Size = new System.Drawing.Size(56, 23);
            this.TextIterations.TabIndex = 8;
            this.TextIterations.Text = "50";
            this.TextIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelIter
            // 
            this.LabelIter.AutoSize = true;
            this.LabelIter.Location = new System.Drawing.Point(432, 51);
            this.LabelIter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelIter.Name = "LabelIter";
            this.LabelIter.Size = new System.Drawing.Size(27, 15);
            this.LabelIter.TabIndex = 9;
            this.LabelIter.Text = "Iter:";
            // 
            // RPSlider_InnerCircle
            // 
            this.RPSlider_InnerCircle.AutoSize = true;
            this.RPSlider_InnerCircle.BackColor = System.Drawing.Color.Bisque;
            this.RPSlider_InnerCircle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.RPSlider_InnerCircle.Location = new System.Drawing.Point(12, 12);
            this.RPSlider_InnerCircle.Maximum = 100;
            this.RPSlider_InnerCircle.Minimum = 1;
            this.RPSlider_InnerCircle.Name = "RPSlider_InnerCircle";
            this.RPSlider_InnerCircle.Size = new System.Drawing.Size(260, 53);
            this.RPSlider_InnerCircle.TabIndex = 21;
            this.RPSlider_InnerCircle.Title = "Inner Circle";
            this.RPSlider_InnerCircle.Value = 100;
            // 
            // RPSlider_OuterCircle
            // 
            this.RPSlider_OuterCircle.AutoSize = true;
            this.RPSlider_OuterCircle.BackColor = System.Drawing.Color.Bisque;
            this.RPSlider_OuterCircle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.RPSlider_OuterCircle.Location = new System.Drawing.Point(12, 71);
            this.RPSlider_OuterCircle.Maximum = 200;
            this.RPSlider_OuterCircle.Minimum = 1;
            this.RPSlider_OuterCircle.Name = "RPSlider_OuterCircle";
            this.RPSlider_OuterCircle.Size = new System.Drawing.Size(260, 53);
            this.RPSlider_OuterCircle.TabIndex = 22;
            this.RPSlider_OuterCircle.Title = "Outer Circle";
            this.RPSlider_OuterCircle.Value = 100;
            // 
            // Form_Main
            // 
            this.AcceptButton = this.BtnQuit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 701);
            this.Controls.Add(this.RPSlider_InnerCircle);
            this.Controls.Add(this.RPSlider_OuterCircle);
            this.Controls.Add(this.knobSciFiPlus1);
            this.Controls.Add(this.BtnRedraw);
            this.Controls.Add(this.LabelIterationCount);
            this.Controls.Add(this.LabelIteration);
            this.Controls.Add(this.BtnColor);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.LabelIter);
            this.Controls.Add(this.TextIterations);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.PictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spirograph-v1";
            this.ResizeEnd += this.Form_Main_ResizeEnd;
            ((System.ComponentModel.ISupportInitialize)this.PictureBox).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnQuit;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.ColorDialog ColorDialog;
        private System.Windows.Forms.Button BtnColor;
        private System.Windows.Forms.Label LabelIteration;
        private System.Windows.Forms.Label LabelIterationCount;
        private System.Windows.Forms.Button BtnRedraw;
        private KnobSciFiPlus knobSciFiPlus1;
        private System.Windows.Forms.TextBox TextIterations;
        private System.Windows.Forms.Label LabelIter;
        private Controls.RPSlider.RPSlider RPSlider_InnerCircle;
        private Controls.RPSlider.RPSlider RPSlider_OuterCircle;
    }
}

