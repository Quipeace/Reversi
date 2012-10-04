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
            this.btEndGame = new System.Windows.Forms.Button();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.gbControlsGame = new System.Windows.Forms.GroupBox();
            this.lbGameInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardY)).BeginInit();
            this.gbControls.SuspendLayout();
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
            // btEndGame
            // 
            this.btEndGame.Location = new System.Drawing.Point(0, 0);
            this.btEndGame.Name = "btEndGame";
            this.btEndGame.Size = new System.Drawing.Size(75, 23);
            this.btEndGame.TabIndex = 0;
            // 
            // gbControls
            // 
            this.gbControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbControls.Controls.Add(this.btStart);
            this.gbControls.Controls.Add(this.nmBoardX);
            this.gbControls.Controls.Add(this.nmBoardY);
            this.gbControls.Location = new System.Drawing.Point(13, 12);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(259, 147);
            this.gbControls.TabIndex = 3;
            this.gbControls.TabStop = false;
            this.gbControls.Text = "New Game";
            // 
            // gbControlsGame
            // 
            this.gbControlsGame.Location = new System.Drawing.Point(0, 0);
            this.gbControlsGame.Name = "gbControlsGame";
            this.gbControlsGame.Size = new System.Drawing.Size(200, 100);
            this.gbControlsGame.TabIndex = 0;
            this.gbControlsGame.TabStop = false;
            // 
            // lbGameInfo
            // 
            this.lbGameInfo.Location = new System.Drawing.Point(0, 0);
            this.lbGameInfo.Name = "lbGameInfo";
            this.lbGameInfo.Size = new System.Drawing.Size(100, 23);
            this.lbGameInfo.TabIndex = 0;
            // 
            // ReversiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 173);
            this.Controls.Add(this.gbControls);
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.Load += new System.EventHandler(this.ReversiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBoardY)).EndInit();
            this.gbControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nmBoardY;
        private System.Windows.Forms.NumericUpDown nmBoardX;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gbControls;
    }
}