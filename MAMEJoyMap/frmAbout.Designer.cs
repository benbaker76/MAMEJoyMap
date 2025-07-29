namespace MAMEJoyMap
{
    partial class frmAbout
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
            lblAbout = new Label();
            butOkay = new Button();
            SuspendLayout();
            // 
            // lblAbout
            // 
            lblAbout.Location = new Point(14, 8);
            lblAbout.Margin = new Padding(4, 0, 4, 0);
            lblAbout.Name = "lblAbout";
            lblAbout.Size = new Size(169, 57);
            lblAbout.TabIndex = 0;
            lblAbout.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // butOkay
            // 
            butOkay.Location = new Point(31, 72);
            butOkay.Margin = new Padding(4, 3, 4, 3);
            butOkay.Name = "butOkay";
            butOkay.Size = new Size(130, 33);
            butOkay.TabIndex = 1;
            butOkay.Text = "OK";
            butOkay.UseVisualStyleBackColor = true;
            butOkay.Click += butOkay_Click;
            // 
            // frmAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(194, 115);
            Controls.Add(butOkay);
            Controls.Add(lblAbout);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmAbout";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Button butOkay;
    }
}