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
        private Graphics currentGraphics;

        public ReversiForm()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            currentGame = new ReversiGame((int)tbBoardSize.Value);

            this.gbInGameControls.Visible = true;
            this.gbPreGameControls.Visible = false;

            this.Size = new Size(540, 725);
            pnBoard.Size = new Size(500, 500);

            currentGraphics = pnBoard.CreateGraphics();
            currentGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            drawGrid();
            drawStones();
        }

        private void btEndGame_Click(object sender, EventArgs e)
        {
            this.gbPreGameControls.Visible = true;
            this.gbInGameControls.Visible = false;

            this.Size = new Size(300, 210);
        }

        private void drawGrid()
        {
            currentGraphics.Clear(Color.White);

            for (double x = 1; x < currentGame.boardSize; x++)
            {
                Point startPoint = new Point((int) (x * currentGame.gridSize), 0);
                Point endPoint = new Point((int) (x * currentGame.gridSize), 500);

                currentGraphics.DrawLine(Pens.Black, startPoint, endPoint); 
            }
            for (double y = 1; y < currentGame.boardSize; y++)
            {
                Point startPoint = new Point(0, (int)(y * currentGame.gridSize));
                Point endPoint = new Point(500, (int)(y * currentGame.gridSize));

                currentGraphics.DrawLine(Pens.Black, startPoint, endPoint); 
            }
        }

        private void drawStones()
        {
            for (int x = 0; x < currentGame.boardSize; x++)
            {
                for (int y = 0; y < currentGame.boardSize; y++)
                {
                    int currentPosition = currentGame.board[x, y];
                    if (currentPosition != 0)
                    {
                        int circleX = (int) (x * currentGame.gridSize);
                        int circleY = (int) (y * currentGame.gridSize);

                        switch (currentPosition)
                        {
                        case ReversiGame.STONE_VALID:
                                currentGraphics.FillEllipse(Brushes.Black, circleX, circleY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                                break;
                        case ReversiGame.STONE_BLUE:
                                currentGraphics.FillEllipse(Brushes.Blue, circleX, circleY, (int) currentGame.gridSize, (int) currentGame.gridSize);
                            break;
                        case ReversiGame.STONE_RED:
                                currentGraphics.FillEllipse(Brushes.Red, circleX, circleY, (int) currentGame.gridSize, (int) currentGame.gridSize);
                            break;
                        }
                    }
                }
            }
        }

        private void tbBoardSize_ValueChanged(object sender, EventArgs e)
        {
            lbBoardSize.Text = tbBoardSize.Value.ToString();
            if (tbBoardSize.Value % 2 != 0)
            {
                tbBoardSize.Value += 1;
            }
        }

        private void pnBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int[] gridPos = {(int)(e.X / currentGame.gridSize), (int)(e.Y / currentGame.gridSize)};

            currentGame.processTurn(gridPos);

            drawStones();
        }
    }
}
