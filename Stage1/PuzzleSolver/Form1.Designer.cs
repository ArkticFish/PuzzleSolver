namespace PuzzleSolver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.game1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyPeasyGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.game3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamePanel = new MyPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.solveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(426, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalGameToolStripMenuItem,
            this.game1ToolStripMenuItem,
            this.easyPeasyGameToolStripMenuItem,
            this.game3ToolStripMenuItem,
            this.customGameToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // normalGameToolStripMenuItem
            // 
            this.normalGameToolStripMenuItem.Name = "normalGameToolStripMenuItem";
            this.normalGameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.normalGameToolStripMenuItem.Text = "Normal Game";
            this.normalGameToolStripMenuItem.Click += new System.EventHandler(this.normalGameToolStripMenuItem_Click);
            // 
            // game1ToolStripMenuItem
            // 
            this.game1ToolStripMenuItem.Name = "game1ToolStripMenuItem";
            this.game1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.game1ToolStripMenuItem.Text = "Game 1";
            this.game1ToolStripMenuItem.Click += new System.EventHandler(this.game1ToolStripMenuItem_Click);
            // 
            // easyPeasyGameToolStripMenuItem
            // 
            this.easyPeasyGameToolStripMenuItem.Name = "easyPeasyGameToolStripMenuItem";
            this.easyPeasyGameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.easyPeasyGameToolStripMenuItem.Text = "Game 2";
            this.easyPeasyGameToolStripMenuItem.Click += new System.EventHandler(this.easyPeasyGameToolStripMenuItem_Click);
            // 
            // game3ToolStripMenuItem
            // 
            this.game3ToolStripMenuItem.Name = "game3ToolStripMenuItem";
            this.game3ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.game3ToolStripMenuItem.Text = "Blocks";
            this.game3ToolStripMenuItem.Click += new System.EventHandler(this.game3ToolStripMenuItem_Click);
            // 
            // customGameToolStripMenuItem
            // 
            this.customGameToolStripMenuItem.Name = "customGameToolStripMenuItem";
            this.customGameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.customGameToolStripMenuItem.Text = "Custom Game";
            this.customGameToolStripMenuItem.Click += new System.EventHandler(this.customGameToolStripMenuItem_Click);
            // 
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.solveToolStripMenuItem.Text = "Solve";
            this.solveToolStripMenuItem.Click += new System.EventHandler(this.solveToolStripMenuItem_Click);
            // 
            // gamePanel
            // 
            this.gamePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gamePanel.BackgroundImage")));
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamePanel.Location = new System.Drawing.Point(12, 27);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(400, 500);
            this.gamePanel.TabIndex = 5;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gamePanel_Paint);
            this.gamePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gamePanel_MouseDown);
            this.gamePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gamePanel_MouseMove);
            this.gamePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gamePanel_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 537);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Klotski Puzzle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customGameToolStripMenuItem;
        private MyPanel gamePanel;
        private System.Windows.Forms.ToolStripMenuItem easyPeasyGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem game1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem game3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
    }
}

