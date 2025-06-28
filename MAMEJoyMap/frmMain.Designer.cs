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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu4WayDiagonal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu4WaySticky = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu8Way = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.picMain = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.picKey = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.picMario = new System.Windows.Forms.PictureBox();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKey)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMario)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuMaps,
            this.mnuAbout});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(331, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuSaveAs,
            this.mnuSave});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(123, 22);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(123, 22);
            this.mnuSaveAs.Text = "Save As...";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(123, 22);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuMaps
            // 
            this.mnuMaps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClear,
            this.toolStripMenuItem1,
            this.mnu4WayDiagonal,
            this.mnu4WaySticky,
            this.mnu8Way});
            this.mnuMaps.Name = "mnuMaps";
            this.mnuMaps.Size = new System.Drawing.Size(48, 20);
            this.mnuMaps.Text = "Maps";
            // 
            // mnuClear
            // 
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(156, 22);
            this.mnuClear.Text = "Clear";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(153, 6);
            // 
            // mnu4WayDiagonal
            // 
            this.mnu4WayDiagonal.Name = "mnu4WayDiagonal";
            this.mnu4WayDiagonal.Size = new System.Drawing.Size(156, 22);
            this.mnu4WayDiagonal.Text = "4 Way Diagonal";
            this.mnu4WayDiagonal.Click += new System.EventHandler(this.mnu4WayDiagonal_Click);
            // 
            // mnu4WaySticky
            // 
            this.mnu4WaySticky.Name = "mnu4WaySticky";
            this.mnu4WaySticky.Size = new System.Drawing.Size(156, 22);
            this.mnu4WaySticky.Text = "4 Way Sticky";
            this.mnu4WaySticky.Click += new System.EventHandler(this.mnu4WaySticky_Click);
            // 
            // mnu8Way
            // 
            this.mnu8Way.Name = "mnu8Way";
            this.mnu8Way.Size = new System.Drawing.Size(156, 22);
            this.mnu8Way.Text = "8 Way";
            this.mnu8Way.Click += new System.EventHandler(this.mnu8Way_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(52, 20);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.Color.Black;
            this.picMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMain.Location = new System.Drawing.Point(12, 41);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(217, 217);
            this.picMain.TabIndex = 1;
            this.picMain.TabStop = false;
            this.picMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseMove);
            this.picMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseDown);
            this.picMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Up-Left.png");
            this.imageList1.Images.SetKeyName(1, "Up.png");
            this.imageList1.Images.SetKeyName(2, "Up-Right.png");
            this.imageList1.Images.SetKeyName(3, "Left.png");
            this.imageList1.Images.SetKeyName(4, "Center.png");
            this.imageList1.Images.SetKeyName(5, "Right.png");
            this.imageList1.Images.SetKeyName(6, "Down-Left.png");
            this.imageList1.Images.SetKeyName(7, "Down.png");
            this.imageList1.Images.SetKeyName(8, "Down-Right.png");
            this.imageList1.Images.SetKeyName(9, "Sticky.png");
            this.imageList1.Images.SetKeyName(10, "Mario.png");
            // 
            // picKey
            // 
            this.picKey.BackColor = System.Drawing.Color.Black;
            this.picKey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picKey.Location = new System.Drawing.Point(246, 41);
            this.picKey.Name = "picKey";
            this.picKey.Size = new System.Drawing.Size(73, 97);
            this.picKey.TabIndex = 2;
            this.picKey.TabStop = false;
            this.picKey.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picKey_MouseMove);
            this.picKey.Click += new System.EventHandler(this.picKey_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 272);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(331, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(316, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // picMario
            // 
            this.picMario.BackColor = System.Drawing.Color.Black;
            this.picMario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMario.Location = new System.Drawing.Point(246, 161);
            this.picMario.Name = "picMario";
            this.picMario.Size = new System.Drawing.Size(73, 97);
            this.picMario.TabIndex = 4;
            this.picMario.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 294);
            this.Controls.Add(this.picMario);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.picKey);
            this.Controls.Add(this.picMain);
            this.Controls.Add(this.mnuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAME Joystick Mapper v1.4";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKey)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

