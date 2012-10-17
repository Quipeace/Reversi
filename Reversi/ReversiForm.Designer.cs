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
            this.btStartServer = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.tbConnect = new System.Windows.Forms.TextBox();
            this.gbInGameControls = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHelp = new System.Windows.Forms.TrackBar();
            this.btEndGame = new System.Windows.Forms.Button();
            this.pnBoardSize = new Reversi.BufferedPanel();
            this.pnBoard = new Reversi.BufferedPanel();
            this.pnScoreKeeper = new Reversi.BufferedPanel();
            this.gbPreGameControls.SuspendLayout();
            this.gbInGameControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(6, 19);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(81, 121);
            this.btStart.TabIndex = 3;
            this.btStart.Text = "Start Hotseat Multiplayer";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // gbPreGameControls
            // 
            this.gbPreGameControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPreGameControls.Controls.Add(this.btStartServer);
            this.gbPreGameControls.Controls.Add(this.btConnect);
            this.gbPreGameControls.Controls.Add(this.tbConnect);
            this.gbPreGameControls.Controls.Add(this.pnBoardSize);
            this.gbPreGameControls.Controls.Add(this.btStart);
            this.gbPreGameControls.Location = new System.Drawing.Point(12, 12);
            this.gbPreGameControls.Name = "gbPreGameControls";
            this.gbPreGameControls.Size = new System.Drawing.Size(500, 147);
            this.gbPreGameControls.TabIndex = 3;
            this.gbPreGameControls.TabStop = false;
            this.gbPreGameControls.Text = "New Game";
            // 
            // btStartServer
            // 
            this.btStartServer.Location = new System.Drawing.Point(289, 45);
            this.btStartServer.Name = "btStartServer";
            this.btStartServer.Size = new System.Drawing.Size(118, 20);
            this.btStartServer.TabIndex = 7;
            this.btStartServer.Text = "Start Server";
            this.btStartServer.UseVisualStyleBackColor = true;
            this.btStartServer.Click += new System.EventHandler(this.btStartServer_Click);
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(413, 19);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(81, 121);
            this.btConnect.TabIndex = 6;
            this.btConnect.Text = "Start Network Multiplayer";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // tbConnect
            // 
            this.tbConnect.Location = new System.Drawing.Point(289, 19);
            this.tbConnect.Name = "tbConnect";
            this.tbConnect.Size = new System.Drawing.Size(118, 20);
            this.tbConnect.TabIndex = 5;
            // 
            // gbInGameControls
            // 
            this.gbInGameControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInGameControls.Controls.Add(this.label3);
            this.gbInGameControls.Controls.Add(this.label2);
            this.gbInGameControls.Controls.Add(this.label1);
            this.gbInGameControls.Controls.Add(this.pnScoreKeeper);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Off";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(425, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "All";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(417, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mild";
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
            this.btEndGame.Size = new System.Drawing.Size(81, 121);
            this.btEndGame.TabIndex = 0;
            this.btEndGame.Text = "End Game";
            this.btEndGame.UseVisualStyleBackColor = true;
            this.btEndGame.Click += new System.EventHandler(this.btEndGame_Click);
            // 
            // pnBoardSize
            // 
            this.pnBoardSize.BackColor = System.Drawing.Color.Transparent;
            this.pnBoardSize.Location = new System.Drawing.Point(93, 19);
            this.pnBoardSize.Name = "pnBoardSize";
            this.pnBoardSize.Size = new System.Drawing.Size(121, 121);
            this.pnBoardSize.TabIndex = 4;
            this.pnBoardSize.Paint += new System.Windows.Forms.PaintEventHandler(this.pnBoardSize_Paint);
            this.pnBoardSize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnBoardSize_MouseClick);
            this.pnBoardSize.MouseLeave += new System.EventHandler(this.pnBoardSize_MouseLeave);
            this.pnBoardSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnBoardSize_MouseMove);
            // 
            // pnBoard
            // 
            this.pnBoard.Location = new System.Drawing.Point(12, 174);
            this.pnBoard.Name = "pnBoard";
            this.pnBoard.Size = new System.Drawing.Size(200, 100);
            this.pnBoard.TabIndex = 5;
            this.pnBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnBoard_Paint);
            this.pnBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnBoard_MouseClick);
            // 
            // pnScoreKeeper
            // 
            this.pnScoreKeeper.Location = new System.Drawing.Point(129, 19);
            this.pnScoreKeeper.Name = "pnScoreKeeper";
            this.pnScoreKeeper.Size = new System.Drawing.Size(252, 121);
            this.pnScoreKeeper.TabIndex = 7;
            this.pnScoreKeeper.Paint += new System.Windows.Forms.PaintEventHandler(this.drawScoreKeeper);
            // 
            // ReversiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 171);
            this.Controls.Add(this.gbPreGameControls);
            this.Controls.Add(this.pnBoard);
            this.Controls.Add(this.gbInGameControls);
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReversiForm_FormClosing);
            this.gbPreGameControls.ResumeLayout(false);
            this.gbPreGameControls.PerformLayout();
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
        private System.Windows.Forms.TrackBar tbHelp;
        private BufferedPanel pnBoardSize;
        private BufferedPanel pnScoreKeeper;
        private BufferedPanel pnBoard;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.TextBox tbConnect;
        private System.Windows.Forms.Button btStartServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}