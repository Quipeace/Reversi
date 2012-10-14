﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Reversi
{
    public partial class ReversiForm : Form
    {
        private bool drawMoveSelector = false;
        private int[] boardSizeMoveSelectorPos = new int[2];
        private int[] boardSizeSelectorPos = { 8, 8 };
        private ReversiGame currentGame;
        private Graphics currentGraphics;
        private int helpClicked;

        public ReversiForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            pnBoardSize.Paint += drawBoardSize;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            currentGame = new ReversiGame(boardSizeSelectorPos);
            helpClicked = 1;
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

        private void btEndGame_Click(object sender, EventArgs e)
        {
            this.gbPreGameControls.Visible = true;
            this.gbInGameControls.Visible = false;

            this.Size = new Size(540, 210);
        }

        private void drawBoardSize(object obj, PaintEventArgs paintEventArgs)
        {
            Graphics graphics = paintEventArgs.Graphics;

            int gridWidth = pnBoardSize.Width - 1;
            int gridSize = (gridWidth / ReversiGame.MAX_BOARDSIZE);                                // (Panel width / number of fields)

            Console.WriteLine("boardsize; " + boardSizeSelectorPos[0]);

            int endX = (int)(boardSizeSelectorPos[0] * gridSize);                                  // Geselecteerde boardsize
            int endY = (int)(boardSizeSelectorPos[1] * gridSize);
            graphics.FillRectangle(Brushes.Gray, 0, 0, endX, endY);

            if (drawMoveSelector)
            {
                endX = (int)(boardSizeMoveSelectorPos[0] * gridSize);                              // Huidige selectie
                endY = (int)(boardSizeMoveSelectorPos[1] * gridSize);
                graphics.FillRectangle(Brushes.White, 0, 0, endX, endY);
            }

            for (double x = 0; x < ReversiGame.MAX_BOARDSIZE; x++)
            {
                Point startPoint = new Point((int)(x * gridSize), 0);
                Point endPoint = new Point((int)(x * gridSize), 500);

                graphics.DrawLine(Pens.LightGray, startPoint, endPoint);
            }
            for (double y = 0; y < ReversiGame.MAX_BOARDSIZE; y++)
            {
                Point startPoint = new Point(0, (int)(y * gridSize));
                Point endPoint = new Point(500, (int)(y * gridSize));

                graphics.DrawLine(Pens.LightGray, startPoint, endPoint);
            }
            graphics.DrawLine(Pens.LightGray, gridWidth, 0, gridWidth, gridWidth);
            graphics.DrawLine(Pens.LightGray, 0, gridWidth, gridWidth, gridWidth);

        }


        private void drawStones()
        {
            for (int x = 0; x < currentGame.boardSize[0]; x++)
            {
                for (int y = 0; y < currentGame.boardSize[1]; y++)
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
                                if (helpClicked % 2 == 0)
                                    currentGraphics.FillEllipse(currentGame.validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                                else
                                    setEmptyStone(x, y, circleX, circleY);
                                break;
                            case ReversiGame.STONE_EMPTY:
                                setEmptyStone(x, y, circleX, circleY);
                                break;
                            case ReversiGame.PLAYER_1:
                                currentGraphics.FillEllipse(currentGame.players[ReversiGame.PLAYER_1].brush, circleX + (gridSizeInt / 12), circleY + (gridSizeInt / 12), gridSizeInt - (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 6));
                                break;
                            case ReversiGame.PLAYER_2:
                                currentGraphics.FillEllipse(currentGame.players[ReversiGame.PLAYER_2].brush, circleX + (gridSizeInt / 12), circleY + (gridSizeInt / 12), gridSizeInt - (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 6));
                                break;
                        }

                        currentGame.drawnBoard[x, y] = stoneAtPos;
                    }
                }
            }
        }

        private void setEmptyStone(int x, int y, int startX, int startY)
        {
            if (x % 2 == 0)
            {
                if (y % 2 == 0)
                {
                    currentGraphics.FillRectangle(Brushes.Green, startX, startY, (int) currentGame.gridSize, (int) currentGame.gridSize);
                }
                else
                {
                    currentGraphics.FillRectangle(Brushes.LightGreen, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                }
            }
            else
            {
                if (y % 2 != 0)
                {
                    currentGraphics.FillRectangle(Brushes.Green, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                }
                else
                {
                    currentGraphics.FillRectangle(Brushes.LightGreen, startX, startY, (int) currentGame.gridSize, (int) currentGame.gridSize);
                }
            }
        }

        private void pnBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int[] gridPos = {(int)(e.X / currentGame.gridSize), (int)(e.Y / currentGame.gridSize)};

            currentGame.processTurn(gridPos);

            lbStoneWhite.Text = currentGame.players[ReversiGame.PLAYER_2].stones.ToString();
            lbStoneBlack.Text = currentGame.players[ReversiGame.PLAYER_1].stones.ToString();
            if (currentGame.currentPlayer == ReversiGame.PLAYER_1)
            {
                lbPlayerTurn.Text = "Zwart is aan de beurt";
            }
            else
            {
                lbPlayerTurn.Text = "Wit is aan de beurt";
            }
            if (((currentGame.players[ReversiGame.PLAYER_1].stones + currentGame.players[ReversiGame.PLAYER_2].stones) == (currentGame.boardSize[0] * currentGame.boardSize[1])) || currentGame.gameEnded == true)
            {
                if (currentGame.players[ReversiGame.PLAYER_1].stones > currentGame.players[ReversiGame.PLAYER_2].stones)
                {
                    lbPlayerTurn.Text = "Zwart heeft gewonnen";
                }
                else if (currentGame.players[ReversiGame.PLAYER_1].stones == currentGame.players[ReversiGame.PLAYER_2].stones)
                {
                    lbPlayerTurn.Text = "Gelijkspel";
                }
                else
                {
                    lbPlayerTurn.Text = "Wit heeft gewonnen";
                }
            }
            drawStones();
        }

        private void pnBoardSize_MouseClick(object sender, MouseEventArgs e)
        {
            boardSizeSelectorPos[0] = boardSizeMoveSelectorPos[0];
            boardSizeSelectorPos[1] = boardSizeMoveSelectorPos[1];
            pnBoardSize.Invalidate();
        }

        private void pnBoardSize_MouseMove(object sender, MouseEventArgs e)
        {
            drawMoveSelector = true;

            int gridWidth = pnBoardSize.Width - 1;
            int gridSize = (gridWidth / ReversiGame.MAX_BOARDSIZE);                                // (Panel width / number of fields)

            int newBoardSizeX = (int) (e.X / gridSize) + 1;
            int newBoardSizeY = (int)(e.Y / gridSize) + 1;

            if ((newBoardSizeX != boardSizeMoveSelectorPos[0] || newBoardSizeY != boardSizeMoveSelectorPos[1]) && newBoardSizeX >= 4 && newBoardSizeY >= 4)
            {
                boardSizeMoveSelectorPos[0] = newBoardSizeX;
                boardSizeMoveSelectorPos[1] = newBoardSizeY;
                pnBoardSize.Invalidate();
            }
        }

        private void pnBoardSize_MouseLeave(object sender, EventArgs e)
        {
            drawMoveSelector = false;
            pnBoardSize.Invalidate();
        }

        private void btInGameHelp_Click(object sender, EventArgs e)
        {
            helpClicked++;
            Console.WriteLine("CLICK: " + helpClicked);
            Invalidate();
        }
    }
}
