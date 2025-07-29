namespace MAMEJoyMap
{
    partial class frmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            mnuMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            mnuOpen = new ToolStripMenuItem();
            mnuSaveAs = new ToolStripMenuItem();
            mnuSave = new ToolStripMenuItem();
            mnuMaps = new ToolStripMenuItem();
            mnuClear = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            mnu4WayDiagonal = new ToolStripMenuItem();
            mnu4WaySticky = new ToolStripMenuItem();
            mnu8Way = new ToolStripMenuItem();
            mnuAbout = new ToolStripMenuItem();
            picMain = new PictureBox();
            imageList1 = new ImageList(components);
            picKey = new PictureBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            picMario = new PictureBox();
            mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picKey).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMario).BeginInit();
            SuspendLayout();
            // 
            // mnuMain
            // 
            mnuMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, mnuMaps, mnuAbout });
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Padding = new Padding(7, 2, 0, 2);
            mnuMain.Size = new Size(337, 24);
            mnuMain.TabIndex = 0;
            mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuOpen, mnuSaveAs, mnuSave });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // mnuOpen
            // 
            mnuOpen.Name = "mnuOpen";
            mnuOpen.Size = new Size(123, 22);
            mnuOpen.Text = "Open";
            mnuOpen.Click += mnuOpen_Click;
            // 
            // mnuSaveAs
            // 
            mnuSaveAs.Name = "mnuSaveAs";
            mnuSaveAs.Size = new Size(123, 22);
            mnuSaveAs.Text = "Save As...";
            mnuSaveAs.Click += mnuSaveAs_Click;
            // 
            // mnuSave
            // 
            mnuSave.Name = "mnuSave";
            mnuSave.Size = new Size(123, 22);
            mnuSave.Text = "Save";
            mnuSave.Click += mnuSave_Click;
            // 
            // mnuMaps
            // 
            mnuMaps.DropDownItems.AddRange(new ToolStripItem[] { mnuClear, toolStripMenuItem1, mnu4WayDiagonal, mnu4WaySticky, mnu8Way });
            mnuMaps.Name = "mnuMaps";
            mnuMaps.Size = new Size(48, 20);
            mnuMaps.Text = "Maps";
            // 
            // mnuClear
            // 
            mnuClear.Name = "mnuClear";
            mnuClear.Size = new Size(156, 22);
            mnuClear.Text = "Clear";
            mnuClear.Click += mnuClear_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(153, 6);
            // 
            // mnu4WayDiagonal
            // 
            mnu4WayDiagonal.Name = "mnu4WayDiagonal";
            mnu4WayDiagonal.Size = new Size(156, 22);
            mnu4WayDiagonal.Text = "4 Way Diagonal";
            mnu4WayDiagonal.Click += mnu4WayDiagonal_Click;
            // 
            // mnu4WaySticky
            // 
            mnu4WaySticky.Name = "mnu4WaySticky";
            mnu4WaySticky.Size = new Size(156, 22);
            mnu4WaySticky.Text = "4 Way Sticky";
            mnu4WaySticky.Click += mnu4WaySticky_Click;
            // 
            // mnu8Way
            // 
            mnu8Way.Name = "mnu8Way";
            mnu8Way.Size = new Size(156, 22);
            mnu8Way.Text = "8 Way";
            mnu8Way.Click += mnu8Way_Click;
            // 
            // mnuAbout
            // 
            mnuAbout.Name = "mnuAbout";
            mnuAbout.Size = new Size(52, 20);
            mnuAbout.Text = "About";
            mnuAbout.Click += mnuAbout_Click;
            // 
            // picMain
            // 
            picMain.BackColor = Color.Black;
            picMain.Cursor = Cursors.Hand;
            picMain.Location = new Point(14, 47);
            picMain.Margin = new Padding(4, 3, 4, 3);
            picMain.Name = "picMain";
            picMain.Size = new Size(217, 217);
            picMain.TabIndex = 1;
            picMain.TabStop = false;
            picMain.MouseDown += picMain_MouseDown;
            picMain.MouseMove += picMain_MouseMove;
            picMain.MouseUp += picMain_MouseUp;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "Up-Left.png");
            imageList1.Images.SetKeyName(1, "Up.png");
            imageList1.Images.SetKeyName(2, "Up-Right.png");
            imageList1.Images.SetKeyName(3, "Left.png");
            imageList1.Images.SetKeyName(4, "Center.png");
            imageList1.Images.SetKeyName(5, "Right.png");
            imageList1.Images.SetKeyName(6, "Down-Left.png");
            imageList1.Images.SetKeyName(7, "Down.png");
            imageList1.Images.SetKeyName(8, "Down-Right.png");
            imageList1.Images.SetKeyName(9, "Sticky.png");
            imageList1.Images.SetKeyName(10, "Mario.png");
            // 
            // picKey
            // 
            picKey.BackColor = Color.Black;
            picKey.Cursor = Cursors.Hand;
            picKey.Location = new Point(249, 47);
            picKey.Margin = new Padding(4, 3, 4, 3);
            picKey.Name = "picKey";
            picKey.Size = new Size(73, 97);
            picKey.TabIndex = 2;
            picKey.TabStop = false;
            picKey.Click += picKey_Click;
            picKey.MouseMove += picKey_MouseMove;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 278);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(337, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(369, 17);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // picMario
            // 
            picMario.BackColor = Color.Black;
            picMario.BorderStyle = BorderStyle.FixedSingle;
            picMario.Cursor = Cursors.Hand;
            picMario.Location = new Point(249, 167);
            picMario.Margin = new Padding(4, 3, 4, 3);
            picMario.Name = "picMario";
            picMario.Size = new Size(73, 97);
            picMario.TabIndex = 4;
            picMario.TabStop = false;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(337, 300);
            Controls.Add(picMario);
            Controls.Add(statusStrip1);
            Controls.Add(picKey);
            Controls.Add(picMain);
            Controls.Add(mnuMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mnuMain;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MAME Joystick Mapper v1.4";
            Load += frmMain_Load;
            mnuMain.ResumeLayout(false);
            mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)picKey).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picMario).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox picKey;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem mnuMaps;
        private System.Windows.Forms.ToolStripMenuItem mnu4WayDiagonal;
        private System.Windows.Forms.ToolStripMenuItem mnu4WaySticky;
        private System.Windows.Forms.ToolStripMenuItem mnu8Way;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.PictureBox picMario;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem mnuClear;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

