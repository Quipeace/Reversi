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
            this.lbBoardSize = new System.Windows.Forms.Label();
            this.tbBoardSize = new System.Windows.Forms.TrackBar();
            this.gbInGameControls = new System.Windows.Forms.GroupBox();
            this.btEndGame = new System.Windows.Forms.Button();
            this.pnBoard = new System.Windows.Forms.Panel();
            this.gbPreGameControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBoardSize)).BeginInit();
            this.gbInGameControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(7, 20);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 3;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // gbPreGameControls
            // 
            this.gbPreGameControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPreGameControls.Controls.Add(this.lbBoardSize);
            this.gbPreGameControls.Controls.Add(this.tbBoardSize);
            this.gbPreGameControls.Controls.Add(this.btStart);
            this.gbPreGameControls.Location = new System.Drawing.Point(12, 12);
            this.gbPreGameControls.Name = "gbPreGameControls";
            this.gbPreGameControls.Size = new System.Drawing.Size(260, 147);
            this.gbPreGameControls.TabIndex = 3;
            this.gbPreGameControls.TabStop = false;
            this.gbPreGameControls.Text = "New Game";
            // 
            // lbBoardSize
            // 
            this.lbBoardSize.AutoSize = true;
            this.lbBoardSize.Location = new System.Drawing.Point(118, 128);
            this.lbBoardSize.Name = "lbBoardSize";
            this.lbBoardSize.Size = new System.Drawing.Size(13, 13);
            this.lbBoardSize.TabIndex = 4;
            this.lbBoardSize.Text = "4";
            // 
            // tbBoardSize
            // 
            this.tbBoardSize.LargeChange = 4;
            this.tbBoardSize.Location = new System.Drawing.Point(6, 96);
            this.tbBoardSize.Maximum = 40;
            this.tbBoardSize.Minimum = 4;
            this.tbBoardSize.Name = "tbBoardSize";
            this.tbBoardSize.Size = new System.Drawing.Size(247, 45);
            this.tbBoardSize.SmallChange = 2;
            this.tbBoardSize.TabIndex = 2;
            this.tbBoardSize.TickFrequency = 2;
            this.tbBoardSize.Value = 4;
            this.tbBoardSize.ValueChanged += new System.EventHandler(this.tbBoardSize_ValueChanged);
            // 
            // gbInGameControls
            // 
            this.gbInGameControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInGameControls.Controls.Add(this.btEndGame);
            this.gbInGameControls.Location = new System.Drawing.Point(12, 12);
            this.gbInGameControls.Name = "gbInGameControls";
            this.gbInGameControls.Size = new System.Drawing.Size(260, 147);
            this.gbInGameControls.TabIndex = 4;
            this.gbInGameControls.TabStop = false;
            this.gbInGameControls.Text = "In Game";
            this.gbInGameControls.Visible = false;
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
            this.pnBoard.Click += new System.EventHandler(this.pnBoard_Click);
            // 
            // ReversiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 171);
            this.Controls.Add(this.gbInGameControls);
            this.Controls.Add(this.pnBoard);
            this.Controls.Add(this.gbPreGameControls);
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.gbPreGameControls.ResumeLayout(false);
            this.gbPreGameControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBoardSize)).EndInit();
            this.gbInGameControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gbPreGameControls;
        private System.Windows.Forms.GroupBox gbInGameControls;
        private System.Windows.Forms.Button btEndGame;
        private System.Windows.Forms.Panel pnBoard;
        private System.Windows.Forms.TrackBar tbBoardSize;
        private System.Windows.Forms.Label lbBoardSize;
    }
}