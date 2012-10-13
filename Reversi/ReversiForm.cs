using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            pnBoardSize.Paint += drawBoardSize;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            currentGame = new ReversiGame((int)4);

            this.gbInGameControls.Visible = true;
            this.gbPreGameControls.Visible = false;

            this.Size = new Size(540, 725);
            pnBoard.Size = new Size(500, 500);

            currentGraphics = pnBoard.CreateGraphics();
            currentGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            drawStones();

            currentGame.setInitialStones();

            drawStones();
        }

        private void drawBoardSize(object obj, PaintEventArgs paintEventArgs)
        {
            Graphics graphics = paintEventArgs.Graphics;
            int gridSize = (120 / 20);                                // (Panel width / number of fields)
            for (double x = 0; x < 20; x++)
            {
                Point startPoint = new Point((int)(x * gridSize), 0);
                Point endPoint = new Point((int)(x * gridSize), 500);

                graphics.DrawLine(Pens.LightGray, startPoint, endPoint);
            }
            for (double y = 0; y < 20; y++)
            {
                Point startPoint = new Point(0, (int)(y * gridSize));
                Point endPoint = new Point(500, (int)(y * gridSize));

                graphics.DrawLine(Pens.LightGray, startPoint, endPoint);
            }
            graphics.DrawLine(Pens.LightGray, 120, 0, 120, 120);
            graphics.DrawLine(Pens.LightGray, 0, 120, 120, 120);
            /*
                        for (int x = 0; x < currentGame.boardSize; x++)
                        {
                            for (int y = 0; y < currentGame.boardSize; y++)
                            {
                                int stoneAtPos = currentGame.board[x, y];
                                int drawnStoneAtPos = currentGame.drawnBoard[x, y];
                                int circleX = (int)(x * 20) + 2;
                                int circleY = (int)(y * 20) + 2;

                                int gridSizeInt = (int)currentGame.gridSize;

                                if (drawnStoneAtPos != stoneAtPos)
                                {
                                    switch (stoneAtPos)
                                    {
                                        case ReversiGame.STONE_VALID:
                                            currentGraphics.FillEllipse(Brushes.Red, circleX + (gridSizeInt / 8), circleY + (gridSizeInt / 8), gridSizeInt - (gridSizeInt / 4), gridSizeInt - (gridSizeInt / 4));
                                            break;
                                        case ReversiGame.STONE_EMPTY:
                                            setEmptyStone(x, y, circleX, circleY);
                                            break;
                                        case ReversiGame.STONE_BLACK:
                                            currentGraphics.FillEllipse(Brushes.White, circleX, circleY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                                            break;
                                        case ReversiGame.STONE_WHITE:
                                            currentGraphics.FillEllipse(Brushes.Black, circleX, circleY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                                            break;
                                    }

                                    currentGame.drawnBoard[x, y] = stoneAtPos;
                                }
                            }

                        }
             *  */
        }

        private void btEndGame_Click(object sender, EventArgs e)
        {
            this.gbPreGameControls.Visible = true;
            this.gbInGameControls.Visible = false;

            this.Size = new Size(300, 210);
        }

        private void drawStones()
        {
            for (int x = 0; x < currentGame.boardSize; x++)
            {
                for (int y = 0; y < currentGame.boardSize; y++)
                {
                    int stoneAtPos = currentGame.board[x, y];
                    int drawnStoneAtPos = currentGame.drawnBoard[x, y];
                    int circleX = (int) (x * currentGame.gridSize) + 2;
                    int circleY = (int) (y * currentGame.gridSize) + 2;

                    int gridSizeInt = (int)currentGame.gridSize;

                    if (drawnStoneAtPos != stoneAtPos)
                    {
                        switch (stoneAtPos)
                        {
                            case ReversiGame.STONE_VALID:
                                currentGraphics.FillEllipse(Brushes.Red, circleX + (gridSizeInt / 8), circleY + (gridSizeInt / 8), gridSizeInt - (gridSizeInt / 4), gridSizeInt - (gridSizeInt / 4));
                                break;
                            case ReversiGame.STONE_EMPTY:
                                setEmptyStone(x, y, circleX, circleY);
                                break;
                            case ReversiGame.STONE_BLACK:
                                currentGraphics.FillEllipse(Brushes.White, circleX, circleY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                                break;
                            case ReversiGame.STONE_WHITE:
                                currentGraphics.FillEllipse(Brushes.Black, circleX, circleY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                                break;
                        }

                        currentGame.drawnBoard[x, y] = stoneAtPos;
                    }
                }
            }
        }

        private void setEmptyStone(int x, int y, int circleX, int circleY)
        {
            if (x % 2 == 0)
            {
                if (y % 2 == 0)
                {
                    currentGraphics.FillRectangle(Brushes.Green, circleX, circleY, (int) currentGame.gridSize, (int) currentGame.gridSize);
                }
                else
                {
                    currentGraphics.FillRectangle(Brushes.LightGreen, circleX, circleY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                }
            }
            else
            {
                if (y % 2 != 0)
                {
                    currentGraphics.FillRectangle(Brushes.Green, circleX, circleY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                }
                else
                {
                    currentGraphics.FillRectangle(Brushes.LightGreen, circleX, circleY, (int) currentGame.gridSize, (int) currentGame.gridSize);
                }
            }
        }

        private void pnBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int[] gridPos = {(int)(e.X / currentGame.gridSize), (int)(e.Y / currentGame.gridSize)};

            currentGame.processTurn(gridPos);

            lbStoneWhite.Text = "" + currentGame.players[ReversiGame.PLAYER_1].stones;
            lbStoneBlack.Text = "" + currentGame.players[ReversiGame.PLAYER_2].stones;
            if (currentGame.currentPlayer == ReversiGame.PLAYER_1)
            {
                lbPlayerTurn.Text = "Wit is aan de beurt";
            }
            else
            {
                lbPlayerTurn.Text = "Zwart is aan de beurt";
            }
            if ((currentGame.players[ReversiGame.PLAYER_1].stones + currentGame.players[ReversiGame.PLAYER_2].stones) == (currentGame.boardSize * currentGame.boardSize))
            {
                gameOver();
            }
            drawStones();
        }

        public void gameOver()
        {
            if (currentGame.players[ReversiGame.PLAYER_1].stones > currentGame.players[ReversiGame.PLAYER_2].stones)
            {
                lbPlayerTurn.Text = "Wit heeft gewonnen";
            }
            else if (currentGame.players[ReversiGame.PLAYER_1].stones == currentGame.players[ReversiGame.PLAYER_2].stones)
            {
                lbPlayerTurn.Text = "Gelijkspel";
            }
            else
            {
                lbPlayerTurn.Text = "Zwart heeft gewonnen";
            }
        }

        private void pnBoardSize_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pnBoardSize_MouseMove(object sender, MouseEventArgs e)
        {
            //int[] gridPos = { (int)(e.X / currentGame.gridSize), (int)(e.Y / currentGame.gridSize) };

        }
    }
}
