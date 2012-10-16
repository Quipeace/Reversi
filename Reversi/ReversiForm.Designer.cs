namespace Reversi
{
    partial class ReversiForm
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
            this.btStart = new System.Windows.Forms.Button();
            this.gbPreGameControls = new System.Windows.Forms.GroupBox();
            this.pnBoardSize = new System.Windows.Forms.Panel();
            this.gbInGameControls = new System.Windows.Forms.GroupBox();
            this.pnScoreKeeper = new System.Windows.Forms.Panel();
            this.lbPlayerTurn = new System.Windows.Forms.Label();
            this.tbHelp = new System.Windows.Forms.TrackBar();
            this.btEndGame = new System.Windows.Forms.Button();
            this.pnBoard = new System.Windows.Forms.Panel();
            this.gbPreGameControls.SuspendLayout();
            this.gbInGameControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(6, 19);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 121);
            this.btStart.TabIndex = 3;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // gbPreGameControls
            // 
            this.gbPreGameControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPreGameControls.Controls.Add(this.pnBoardSize);
            this.gbPreGameControls.Controls.Add(this.btStart);
            this.gbPreGameControls.Location = new System.Drawing.Point(12, 12);
            this.gbPreGameControls.Name = "gbPreGameControls";
            this.gbPreGameControls.Size = new System.Drawing.Size(500, 147);
            this.gbPreGameControls.TabIndex = 3;
            this.gbPreGameControls.TabStop = false;
            this.gbPreGameControls.Text = "New Game";
            // 
            // pnBoardSize
            // 
            this.pnBoardSize.BackColor = System.Drawing.Color.Transparent;
            this.pnBoardSize.Location = new System.Drawing.Point(87, 19);
            this.pnBoardSize.Name = "pnBoardSize";
            this.pnBoardSize.Size = new System.Drawing.Size(121, 121);
            this.pnBoardSize.TabIndex = 4;
            this.pnBoardSize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnBoardSize_MouseClick);
            this.pnBoardSize.MouseLeave += new System.EventHandler(this.pnBoardSize_MouseLeave);
            this.pnBoardSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnBoardSize_MouseMove);
            // 
            // gbInGameControls
            // 
            this.gbInGameControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInGameControls.Controls.Add(this.pnScoreKeeper);
            this.gbInGameControls.Controls.Add(this.lbPlayerTurn);
            this.gbInGameControls.Controls.Add(this.tbHelp);
            this.gbInGameControls.Controls.Add(this.btEndGame);
            this.gbInGameControls.Location = new System.Drawing.Point(12, 12);
            this.gbInGameControls.Name = "gbInGameControls";
            this.gbInGameControls.Size = new System.Drawing.Size(500, 147);
            this.gbInGameControls.TabIndex = 4;
            this.gbInGameControls.TabStop = false;
            this.gbInGameControls.Text = "In Game";
            this.gbInGameControls.Visible = false;
            // 
            // pnScoreKeeper
            // 
            this.pnScoreKeeper.Location = new System.Drawing.Point(91, 19);
            this.pnScoreKeeper.Name = "pnScoreKeeper";
            this.pnScoreKeeper.Size = new System.Drawing.Size(252, 121);
            this.pnScoreKeeper.TabIndex = 7;
            // 
            // lbPlayerTurn
            // 
            this.lbPlayerTurn.AutoSize = true;
            this.lbPlayerTurn.Location = new System.Drawing.Point(349, 19);
            this.lbPlayerTurn.Name = "lbPlayerTurn";
            this.lbPlayerTurn.Size = new System.Drawing.Size(107, 13);
            this.lbPlayerTurn.TabIndex = 5;
            this.lbPlayerTurn.Text = "Zwart is aan de beurt";
            // 
            // tbHelp
            // 
            this.tbHelp.LargeChange = 1;
            this.tbHelp.Location = new System.Drawing.Point(449, 19);
            this.tbHelp.Maximum = 2;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbHelp.Size = new System.Drawing.Size(45, 121);
            this.tbHelp.TabIndex = 6;
            this.tbHelp.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbHelp.ValueChanged += new System.EventHandler(this.tbHelp_ValueChanged);
            // 
            // btEndGame
            // 
            this.btEndGame.Location = new System.Drawing.Point(6, 19);
            this.btEndGame.Name = "btEndGame";
            this.btEndGame.Size = new System.Drawing.Size(75, 121);
            this.btEndGame.TabIndex = 0;
            this.btEndGame.Text = "End Game";
            this.btEndGame.UseVisualStyleBackColor = true;
            this.btEndGame.Click += new System.EventHandler(this.btEndGame_Click);
            // 
            // pnBoard
            // 
            this.pnBoard.Location = new System.Drawing.Point(12, 174);
            this.pnBoard.Name = "pnBoard";
            this.pnBoard.Size = new System.Drawing.Size(200, 100);
            this.pnBoard.TabIndex = 5;
            this.pnBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnBoard_MouseClick);
            // 
            // ReversiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 171);
            this.Controls.Add(this.gbInGameControls);
            this.Controls.Add(this.pnBoard);
            this.Controls.Add(this.gbPreGameControls);
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.gbPreGameControls.ResumeLayout(false);
            this.gbInGameControls.ResumeLayout(false);
            this.gbInGameControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHelp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gbPreGameControls;
        private System.Windows.Forms.GroupBox gbInGameControls;
        private System.Windows.Forms.Button btEndGame;
        private System.Windows.Forms.Panel pnBoard;
        private System.Windows.Forms.Panel pnBoardSize;
        private System.Windows.Forms.Label lbPlayerTurn;
        private System.Windows.Forms.TrackBar tbHelp;
        private System.Windows.Forms.Panel pnScoreKeeper;
    }
}