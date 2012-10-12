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
            this.lbStoneWhite = new System.Windows.Forms.Label();
            this.lbStoneBlack = new System.Windows.Forms.Label();
            this.lbColorWhite = new System.Windows.Forms.Label();
            this.lbColorBlack = new System.Windows.Forms.Label();
            this.btEndGame = new System.Windows.Forms.Button();
            this.pnBoard = new System.Windows.Forms.Panel();
            this.lbPlayerTurn = new System.Windows.Forms.Label();
            this.gbPreGameControls.SuspendLayout();
            this.gbInGameControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(6, 17);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 120);
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
            this.gbPreGameControls.Size = new System.Drawing.Size(615, 143);
            this.gbPreGameControls.TabIndex = 3;
            this.gbPreGameControls.TabStop = false;
            this.gbPreGameControls.Text = "New Game";
            // 
            // pnBoardSize
            // 
            this.pnBoardSize.BackColor = System.Drawing.Color.Transparent;
            this.pnBoardSize.Location = new System.Drawing.Point(87, 17);
            this.pnBoardSize.Name = "pnBoardSize";
            this.pnBoardSize.Size = new System.Drawing.Size(121, 121);
            this.pnBoardSize.TabIndex = 4;
            this.pnBoardSize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnBoardSize_MouseClick);
            this.pnBoardSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnBoardSize_MouseMove);
            // 
            // gbInGameControls
            // 
            this.gbInGameControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInGameControls.Controls.Add(this.lbPlayerTurn);
            this.gbInGameControls.Controls.Add(this.lbStoneWhite);
            this.gbInGameControls.Controls.Add(this.lbStoneBlack);
            this.gbInGameControls.Controls.Add(this.lbColorWhite);
            this.gbInGameControls.Controls.Add(this.lbColorBlack);
            this.gbInGameControls.Controls.Add(this.btEndGame);
            this.gbInGameControls.Location = new System.Drawing.Point(12, 12);
            this.gbInGameControls.Name = "gbInGameControls";
            this.gbInGameControls.Size = new System.Drawing.Size(615, 147);
            this.gbInGameControls.TabIndex = 4;
            this.gbInGameControls.TabStop = false;
            this.gbInGameControls.Text = "In Game";
            this.gbInGameControls.Visible = false;
            // 
            // lbStoneWhite
            // 
            this.lbStoneWhite.AutoSize = true;
            this.lbStoneWhite.Location = new System.Drawing.Point(62, 67);
            this.lbStoneWhite.Name = "lbStoneWhite";
            this.lbStoneWhite.Size = new System.Drawing.Size(13, 13);
            this.lbStoneWhite.TabIndex = 4;
            this.lbStoneWhite.Text = "2";
            // 
            // lbStoneBlack
            // 
            this.lbStoneBlack.AutoSize = true;
            this.lbStoneBlack.Location = new System.Drawing.Point(62, 45);
            this.lbStoneBlack.Name = "lbStoneBlack";
            this.lbStoneBlack.Size = new System.Drawing.Size(13, 13);
            this.lbStoneBlack.TabIndex = 3;
            this.lbStoneBlack.Text = "2";
            // 
            // lbColorWhite
            // 
            this.lbColorWhite.AutoSize = true;
            this.lbColorWhite.Location = new System.Drawing.Point(11, 67);
            this.lbColorWhite.Name = "lbColorWhite";
            this.lbColorWhite.Size = new System.Drawing.Size(26, 13);
            this.lbColorWhite.TabIndex = 2;
            this.lbColorWhite.Text = "Wit:";
            // 
            // lbColorBlack
            // 
            this.lbColorBlack.AutoSize = true;
            this.lbColorBlack.Location = new System.Drawing.Point(11, 45);
            this.lbColorBlack.Name = "lbColorBlack";
            this.lbColorBlack.Size = new System.Drawing.Size(37, 13);
            this.lbColorBlack.TabIndex = 1;
            this.lbColorBlack.Text = "Zwart:";
            // 
            // btEndGame
            // 
            this.btEndGame.Location = new System.Drawing.Point(8, 19);
            this.btEndGame.Name = "btEndGame";
            this.btEndGame.Size = new System.Drawing.Size(75, 23);
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
            // lbPlayerTurn
            // 
            this.lbPlayerTurn.AutoSize = true;
            this.lbPlayerTurn.Location = new System.Drawing.Point(11, 104);
            this.lbPlayerTurn.Name = "lbPlayerTurn";
            this.lbPlayerTurn.Size = new System.Drawing.Size(96, 13);
            this.lbPlayerTurn.TabIndex = 5;
            this.lbPlayerTurn.Text = "Wit is aan de beurt";
            // 
            // ReversiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 437);
            this.Controls.Add(this.gbInGameControls);
            this.Controls.Add(this.pnBoard);
            this.Controls.Add(this.gbPreGameControls);
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.gbPreGameControls.ResumeLayout(false);
            this.gbInGameControls.ResumeLayout(false);
            this.gbInGameControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gbPreGameControls;
        private System.Windows.Forms.GroupBox gbInGameControls;
        private System.Windows.Forms.Button btEndGame;
        private System.Windows.Forms.Panel pnBoard;
        private System.Windows.Forms.Label lbColorWhite;
        private System.Windows.Forms.Label lbColorBlack;
        private System.Windows.Forms.Panel pnBoardSize;
        private System.Windows.Forms.Label lbStoneWhite;
        private System.Windows.Forms.Label lbStoneBlack;
        private System.Windows.Forms.Label lbPlayerTurn;
    }
}