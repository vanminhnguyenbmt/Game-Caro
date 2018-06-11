namespace GameCaro
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pnlChessBoard = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pctbAvatar = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLAN = new VistaButtonTest.VistaButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pctbMark = new System.Windows.Forms.PictureBox();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.prcbCoolDown = new System.Windows.Forms.ProgressBar();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.tmCoolDown = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerVsComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLaguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAnh = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMini = new System.Windows.Forms.Button();
            this.tmAnimationLabel = new System.Windows.Forms.Timer(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbAvatar)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbMark)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlChessBoard
            // 
            this.pnlChessBoard.Location = new System.Drawing.Point(12, 73);
            this.pnlChessBoard.Name = "pnlChessBoard";
            this.pnlChessBoard.Size = new System.Drawing.Size(696, 495);
            this.pnlChessBoard.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pctbAvatar);
            this.panel2.Location = new System.Drawing.Point(714, 74);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 250);
            this.panel2.TabIndex = 1;
            // 
            // pctbAvatar
            // 
            this.pctbAvatar.BackgroundImage = global::GameCaro.Properties.Resources.caro;
            this.pctbAvatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctbAvatar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctbAvatar.Location = new System.Drawing.Point(0, 0);
            this.pctbAvatar.Name = "pctbAvatar";
            this.pctbAvatar.Size = new System.Drawing.Size(250, 250);
            this.pctbAvatar.TabIndex = 0;
            this.pctbAvatar.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnLAN);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.pctbMark);
            this.panel3.Controls.Add(this.txtPlayerName);
            this.panel3.Controls.Add(this.prcbCoolDown);
            this.panel3.Controls.Add(this.txtIP);
            this.panel3.Location = new System.Drawing.Point(714, 330);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 239);
            this.panel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "5 items in a line to win";
            // 
            // btnLAN
            // 
            this.btnLAN.BackColor = System.Drawing.Color.Transparent;
            this.btnLAN.ButtonText = "Connect";
            this.btnLAN.Location = new System.Drawing.Point(4, 100);
            this.btnLAN.Name = "btnLAN";
            this.btnLAN.Size = new System.Drawing.Size(134, 32);
            this.btnLAN.TabIndex = 9;
            this.btnLAN.Click += new System.EventHandler(this.btnLAN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "5 items in a line to win";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "5 items in a line to win";
            // 
            // pctbMark
            // 
            this.pctbMark.Location = new System.Drawing.Point(147, 17);
            this.pctbMark.Name = "pctbMark";
            this.pctbMark.Size = new System.Drawing.Size(100, 107);
            this.pctbMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctbMark.TabIndex = 2;
            this.pctbMark.TabStop = false;
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(3, 17);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.ReadOnly = true;
            this.txtPlayerName.Size = new System.Drawing.Size(135, 20);
            this.txtPlayerName.TabIndex = 0;
            // 
            // prcbCoolDown
            // 
            this.prcbCoolDown.Location = new System.Drawing.Point(4, 44);
            this.prcbCoolDown.Name = "prcbCoolDown";
            this.prcbCoolDown.Size = new System.Drawing.Size(134, 23);
            this.prcbCoolDown.TabIndex = 1;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(4, 74);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(134, 20);
            this.txtIP.TabIndex = 3;
            this.txtIP.Text = "127.0.0.1";
            // 
            // tmCoolDown
            // 
            this.tmCoolDown.Tick += new System.EventHandler(this.tmCoolDown_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.gameModeToolStripMenuItem,
            this.changeLaguageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(12, 46);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(315, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.menuToolStripMenuItem.Text = "Bảng điều khiển";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.newGameToolStripMenuItem.Text = "Ván mới";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.undoToolStripMenuItem.Text = "Đi lại";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.quitToolStripMenuItem.Text = "Thoát";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // gameModeToolStripMenuItem
            // 
            this.gameModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playersToolStripMenuItem,
            this.playerVsComputerToolStripMenuItem});
            this.gameModeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gameModeToolStripMenuItem.Name = "gameModeToolStripMenuItem";
            this.gameModeToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.gameModeToolStripMenuItem.Text = "Chế độ chơi";
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.playersToolStripMenuItem.Text = "Người Với Người";
            this.playersToolStripMenuItem.Click += new System.EventHandler(this.playersToolStripMenuItem_Click);
            // 
            // playerVsComputerToolStripMenuItem
            // 
            this.playerVsComputerToolStripMenuItem.Name = "playerVsComputerToolStripMenuItem";
            this.playerVsComputerToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.playerVsComputerToolStripMenuItem.Text = "Người Với Máy";
            this.playerVsComputerToolStripMenuItem.Click += new System.EventHandler(this.playerVsComputerToolStripMenuItem_Click);
            // 
            // changeLaguageToolStripMenuItem
            // 
            this.changeLaguageToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.changeLaguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViet,
            this.tsmiAnh});
            this.changeLaguageToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.changeLaguageToolStripMenuItem.Name = "changeLaguageToolStripMenuItem";
            this.changeLaguageToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.changeLaguageToolStripMenuItem.Text = "Thay đổi ngôn ngữ";
            // 
            // tsmiViet
            // 
            this.tsmiViet.Checked = true;
            this.tsmiViet.CheckOnClick = true;
            this.tsmiViet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiViet.Name = "tsmiViet";
            this.tsmiViet.Size = new System.Drawing.Size(152, 22);
            this.tsmiViet.Text = "Tiếng Việt";
            this.tsmiViet.CheckedChanged += new System.EventHandler(this.tsmiViet_CheckedChanged);
            // 
            // tsmiAnh
            // 
            this.tsmiAnh.CheckOnClick = true;
            this.tsmiAnh.Name = "tsmiAnh";
            this.tsmiAnh.Size = new System.Drawing.Size(152, 22);
            this.tsmiAnh.Text = "Tiếng Anh";
            this.tsmiAnh.CheckedChanged += new System.EventHandler(this.tsmiAnh_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExit.Location = new System.Drawing.Point(935, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(27, 32);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "x";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMini
            // 
            this.btnMini.FlatAppearance.BorderSize = 0;
            this.btnMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMini.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.btnMini.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnMini.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMini.Location = new System.Drawing.Point(904, -2);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(27, 35);
            this.btnMini.TabIndex = 10;
            this.btnMini.Text = "-";
            this.btnMini.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMini.UseVisualStyleBackColor = true;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // tmAnimationLabel
            // 
            this.tmAnimationLabel.Tick += new System.EventHandler(this.tmAnimationLabel_Tick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.ButtonFace;
            this.linkLabel1.Location = new System.Drawing.Point(7, 2);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(234, 25);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Game Caro By Nguyên.UIT";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(974, 577);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnMini);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlChessBoard);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Game Caro LAN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctbAvatar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbMark)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlChessBoard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pctbAvatar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.PictureBox pctbMark;
        private System.Windows.Forms.ProgressBar prcbCoolDown;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Timer tmCoolDown;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerVsComputerToolStripMenuItem;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnMini;
        private System.Windows.Forms.Timer tmAnimationLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private VistaButtonTest.VistaButton btnLAN;
        private System.Windows.Forms.ToolStripMenuItem changeLaguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiViet;
        private System.Windows.Forms.ToolStripMenuItem tsmiAnh;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

