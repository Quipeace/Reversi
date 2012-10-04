using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class ReversiForm : Form
    {
        const int DEFAULT_GRID_SIZE = 100;

        private ReversiGame currentGame;

        public ReversiForm()
        {
            InitializeComponent();
        }

        private void ReversiForm_Load(object sender, EventArgs e)
        {

        }

        private void ReversiForm_Click(object sender, EventArgs e)
        {
        }

        private System.Windows.Forms.GroupBox gbControlsGame;
        private System.Windows.Forms.Button btEndGame;
        private System.Windows.Forms.Label lbGameInfo;
        private void btStart_Click(object sender, EventArgs e)
        {

            int[] boardSize = { (int)nmBoardX.Value, (int)nmBoardY.Value };
            currentGame = new ReversiGame(boardSize);

            this.gbControlsGame.Visible = true;
            this.gbControls.Visible = false;

            this.btEndGame.Location = new System.Drawing.Point(7, 20);
            this.btEndGame.Name = "btEndGame";
            this.btEndGame.Size = new System.Drawing.Size(75, 23);
            this.btEndGame.TabIndex = 3;
            this.btEndGame.Text = "End Game";
            this.btEndGame.UseVisualStyleBackColor = true;
            this.btEndGame.Click += new System.EventHandler(this.btEndGame_Click);

            this.lbGameInfo.Location = new System.Drawing.Point(7, 50);
            this.lbGameInfo.Name = "lbGameInfo";
            this.lbGameInfo.Size = new System.Drawing.Size(75, 50);
            this.lbGameInfo.Text = "hier moet komen te staan hoeveel stenen ieder heeft";

            this.gbControlsGame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbControlsGame.Location = new System.Drawing.Point(13, 12);
            this.gbControlsGame.Controls.Add(this.btEndGame);
            this.gbControlsGame.Controls.Add(this.lbGameInfo);
            this.gbControlsGame.Name = "gbControls";
            this.gbControlsGame.Size = new System.Drawing.Size(259, 147);
            this.gbControlsGame.TabIndex = 3;
            this.gbControlsGame.TabStop = false;
            this.gbControlsGame.Text = "End Game";
            this.Controls.Add(gbControlsGame);

            this.Size = new Size(this.Width + 200, this.Height + 200);
        }

        private void btEndGame_Click(object sender, EventArgs e)
        {
            this.gbControls.Visible = true;
            this.gbControlsGame.Visible = false;
            this.Size = new Size(300, 200);
        }
    }
}
