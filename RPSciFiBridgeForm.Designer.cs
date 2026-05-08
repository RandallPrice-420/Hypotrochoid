namespace Spirograph_v1
{
    partial class RPSciFiBridgeForm
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
            SuspendLayout();
            // 
            // RPSciFiBridgeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(380, 157);
            FormBorderStyle     = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Name                = "RPSciFiBridgeForm";
            Text                = "RP SciFi Bridge Form";
            WindowState         = System.Windows.Forms.FormWindowState.Maximized;
            SizeChanged += Form_SizeChanged;
            ResumeLayout(false);
        }

        #endregion
    }
}