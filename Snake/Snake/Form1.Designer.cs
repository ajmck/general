namespace Snake
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.snakeTimer = new System.Windows.Forms.Timer(this.components);
            this.appleTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.twoPlayerWASDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelScore = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(735, 740);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // snakeTimer
            // 
            this.snakeTimer.Interval = 40;
            this.snakeTimer.Tick += new System.EventHandler(this.snakeTimer_Tick);
            // 
            // appleTimer
            // 
            this.appleTimer.Interval = 5000;
            this.appleTimer.Tick += new System.EventHandler(this.appleTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.labelScore});
            this.statusStrip1.Location = new System.Drawing.Point(0, 742);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 26, 0);
            this.statusStrip1.Size = new System.Drawing.Size(735, 35);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.twoPlayerWASDToolStripMenuItem,
            this.onePlayerToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(52, 33);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // twoPlayerWASDToolStripMenuItem
            // 
            this.twoPlayerWASDToolStripMenuItem.Name = "twoPlayerWASDToolStripMenuItem";
            this.twoPlayerWASDToolStripMenuItem.Size = new System.Drawing.Size(281, 34);
            this.twoPlayerWASDToolStripMenuItem.Text = "Two Player (WASD)";
            this.twoPlayerWASDToolStripMenuItem.Click += new System.EventHandler(this.twoPlayerWASDToolStripMenuItem_Click);
            // 
            // onePlayerToolStripMenuItem
            // 
            this.onePlayerToolStripMenuItem.Name = "onePlayerToolStripMenuItem";
            this.onePlayerToolStripMenuItem.Size = new System.Drawing.Size(281, 34);
            this.onePlayerToolStripMenuItem.Text = "One Player";
            this.onePlayerToolStripMenuItem.Click += new System.EventHandler(this.onePlayerToolStripMenuItem_Click);
            // 
            // labelScore
            // 
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(146, 30);
            this.labelScore.Text = "Black: 0 points";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 777);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Snakey";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer snakeTimer;
        private System.Windows.Forms.Timer appleTimer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem twoPlayerWASDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onePlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel labelScore;
    }
}

