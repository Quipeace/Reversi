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

            this.Size = new Size(540, 725);
            pnBoard.Size = new Size(500, 500);

            drawGrid();
        }

        private void btEndGame_Click(object sender, EventArgs e)
        {
            this.gbPreGameControls.Visible = true;
            this.gbInGameControls.Visible = false;

            this.Size = new Size(300, 210);
        }

        private void drawGrid()
        {
            Graphics currentGraphics = pnBoard.CreateGraphics();
            currentGraphics.Clear(Color.White);

            for (int x = 1; x < currentGame.boardSize[0]; x++)
            {
                Point startPoint = new Point(x * currentGame.gridSize, 0);
                Point endPoint = new Point(x * currentGame.gridSize, 500);

                currentGraphics.DrawLine(Pens.Black, startPoint, endPoint); 
            }
            for (int y = 1; y < currentGame.boardSize[1]; y++)
            {
            }
        }
    }
}
