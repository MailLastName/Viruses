namespace Mail.Viruses.FunnyVirus
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Player = new PictureBox();
            Ground = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)Player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Ground).BeginInit();
            SuspendLayout();
            // 
            // Player
            // 
            Player.BackColor = Color.Transparent;
            Player.BackgroundImageLayout = ImageLayout.None;
            Player.Location = new Point(0, 0);
            Player.Name = "Player";
            Player.Size = new Size(100, 100);
            Player.SizeMode = PictureBoxSizeMode.Zoom;
            Player.TabIndex = 0;
            Player.TabStop = false;
            // 
            // Ground
            // 
            Ground.BackColor = SystemColors.Control;
            Ground.Location = new Point(0, 351);
            Ground.Name = "Ground";
            Ground.Size = new Size(2000, 99);
            Ground.SizeMode = PictureBoxSizeMode.StretchImage;
            Ground.TabIndex = 1;
            Ground.TabStop = false;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Ground);
            Controls.Add(Player);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainWindow";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Funny Virus";
            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;
            ((System.ComponentModel.ISupportInitialize)Player).EndInit();
            ((System.ComponentModel.ISupportInitialize)Ground).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Player;
        private PictureBox Ground;
    }
}
