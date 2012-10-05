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

        private void btStart_Click(object sender, EventArgs e)
        {
            int[] boardSize = { (int)nmBoardX.Value, (int)nmBoardY.Value };
            currentGame = new ReversiGame(boardSize);

            this.gbInGameControls.Visible = true;
            this.gbPreGameControls.Visible = false;

            this.Size = new Size(this.Width + 200, this.Height + 200);
        }

        private void btEndGame_Click(object sender, EventArgs e)
        {
            this.gbPreGameControls.Visible = true;
            this.gbInGameControls.Visible = false;
            this.Size = new Size(300, 200);
        }
    }
}
