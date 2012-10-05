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
            this.nmBoardX = new System.Windows.Forms.NumericUpDown();
            this.nmBoardY = new System.Windows.Forms.NumericUpDown();
            this.btStart = new System.Windows.Forms.Button();
            this.gbPreGameControls = new System.Windows.Forms.GroupBox();
            this.gbInGameControls = new System.Windows.Forms.GroupBox();
            this.btEndGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardY)).BeginInit();
            this.gbPreGameControls.SuspendLayout();
            this.gbInGameControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // nmBoardX
            // 
            this.nmBoardX.Location = new System.Drawing.Point(6, 121);
            this.nmBoardX.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nmBoardX.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmBoardX.Name = "nmBoardX";
            this.nmBoardX.Size = new System.Drawing.Size(120, 20);
            this.nmBoardX.TabIndex = 1;
            this.nmBoardX.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nmBoardY
            // 
            this.nmBoardY.Location = new System.Drawing.Point(132, 121);
            this.nmBoardY.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nmBoardY.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmBoardY.Name = "nmBoardY";
            this.nmBoardY.Size = new System.Drawing.Size(121, 20);
            this.nmBoardY.TabIndex = 2;
            this.nmBoardY.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
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
            this.gbPreGameControls.Controls.Add(this.btStart);
            this.gbPreGameControls.Controls.Add(this.nmBoardX);
            this.gbPreGameControls.Controls.Add(this.nmBoardY);
            this.gbPreGameControls.Location = new System.Drawing.Point(13, 12);
            this.gbPreGameControls.Name = "gbPreGameControls";
            this.gbPreGameControls.Size = new System.Drawing.Size(259, 147);
            this.gbPreGameControls.TabIndex = 3;
            this.gbPreGameControls.TabStop = false;
            this.gbPreGameControls.Text = "New Game";
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
            // ReversiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 173);
            this.Controls.Add(this.gbInGameControls);
            this.Controls.Add(this.gbPreGameControls);
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.Load += new System.EventHandler(this.ReversiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardY)).EndInit();
            this.gbPreGameControls.ResumeLayout(false);
            this.gbInGameControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nmBoardY;
        private System.Windows.Forms.NumericUpDown nmBoardX;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gbPreGameControls;
        private System.Windows.Forms.GroupBox gbInGameControls;
        private System.Windows.Forms.Button btEndGame;
    }
}