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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btEndGame = new System.Windows.Forms.Button();
            this.pnBoard = new System.Windows.Forms.Panel();
            this.gbPreGameControls.SuspendLayout();
            this.gbInGameControls.SuspendLayout();
            this.pnBoard.SuspendLayout();
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
            this.gbInGameControls.Controls.Add(this.label2);
            this.gbInGameControls.Controls.Add(this.label1);
            this.gbInGameControls.Controls.Add(this.btEndGame);
            this.gbInGameControls.Location = new System.Drawing.Point(176, 104);
            this.gbInGameControls.Name = "gbInGameControls";
            this.gbInGameControls.Size = new System.Drawing.Size(615, 147);
            this.gbInGameControls.TabIndex = 4;
            this.gbInGameControls.TabStop = false;
            this.gbInGameControls.Text = "In Game";
            this.gbInGameControls.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
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
            this.pnBoard.Controls.Add(this.gbInGameControls);
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
            this.ClientSize = new System.Drawing.Size(639, 437);
            this.Controls.Add(this.pnBoard);
            this.Controls.Add(this.gbPreGameControls);
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.gbPreGameControls.ResumeLayout(false);
            this.gbInGameControls.ResumeLayout(false);
            this.gbInGameControls.PerformLayout();
            this.pnBoard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gbPreGameControls;
        private System.Windows.Forms.GroupBox gbInGameControls;
        private System.Windows.Forms.Button btEndGame;
        private System.Windows.Forms.Panel pnBoard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnBoardSize;
    }
}