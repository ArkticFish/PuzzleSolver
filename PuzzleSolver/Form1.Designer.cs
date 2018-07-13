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
            this.blockCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.walkingPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip1.Size = new System.Drawing.Size(604, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalGameToolStripMenuItem,
            this.blockCheckToolStripMenuItem,
            this.walkingPuzzleToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // normalGameToolStripMenuItem
            // 
            this.normalGameToolStripMenuItem.Name = "normalGameToolStripMenuItem";
            this.normalGameToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.normalGameToolStripMenuItem.Text = "Normal Game";
            this.normalGameToolStripMenuItem.Click += new System.EventHandler(this.normalGameToolStripMenuItem_Click);
            // 
            // blockCheckToolStripMenuItem
            // 
            this.blockCheckToolStripMenuItem.Name = "blockCheckToolStripMenuItem";
            this.blockCheckToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.blockCheckToolStripMenuItem.Text = "Block Check";
            this.blockCheckToolStripMenuItem.Click += new System.EventHandler(this.blockCheckToolStripMenuItem_Click);
            // 
            // walkingPuzzleToolStripMenuItem
            // 
            this.walkingPuzzleToolStripMenuItem.Name = "walkingPuzzleToolStripMenuItem";
            this.walkingPuzzleToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.walkingPuzzleToolStripMenuItem.Text = "Walking Puzzle";
            this.walkingPuzzleToolStripMenuItem.Click += new System.EventHandler(this.walkingPuzzleToolStripMenuItem_Click);
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
            this.gamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gamePanel.BackgroundImage")));
            this.gamePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamePanel.Location = new System.Drawing.Point(12, 27);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(580, 685);
            this.gamePanel.TabIndex = 5;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gamePanel_Paint);
            this.gamePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gamePanel_MouseDown);
            this.gamePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gamePanel_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 724);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Klotski Puzzle";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalGameToolStripMenuItem;
        private MyPanel gamePanel;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blockCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem walkingPuzzleToolStripMenuItem;
    }
}

