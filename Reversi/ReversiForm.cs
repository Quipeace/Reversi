using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Reversi
{
    public partial class ReversiForm : Form
    {
        Bitmap boardBitmap;

        private Brush gridGreenLight;
        private Brush gridGreenDark;

        private Brush validMoveBrush;
        private Brush bestMoveBrush;

        private bool drawMoveSelector = false;
        private int[] boardSizeMoveSelectorPos = new int[2];
        private int[] boardSizeSelectorPos = { 8, 8 };

        private int currentPlayerIndicatorTransparency = 0;

        private Graphics boardBitmapGraphics;
        private ReversiGame currentGame;

        Thread currentPlayerIndicatorThread;
        Boolean animateCurrentPlayerIndicator = true;

        public ReversiForm()
        {
            this.InitializeComponent();

            this.validMoveBrush = new SolidBrush(Color.FromArgb(150, 150, 150, 150));
            this.bestMoveBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
            this.gridGreenLight = new SolidBrush(Color.FromArgb(106, 206, 0));
            this.gridGreenDark = new SolidBrush(Color.FromArgb(83, 160, 0));

            this.BackColor = Color.FromArgb(93, 181, 0);
        }

        private void ReversiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.animateCurrentPlayerIndicator = false;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            this.gbInGameControls.Visible = true;
            this.gbPreGameControls.Visible = false;
            this.Size = new Size(540, 725);
            pnBoard.Size = new Size(500, 500);
            this.boardBitmap = new Bitmap(this.Width, this.Height);
            tbHelp.Value = ReversiGame.HELP_OFF;

            boardBitmapGraphics = Graphics.FromImage(boardBitmap);

            currentPlayerIndicatorThread = new Thread(updateCurrentPlayerThread);
            animateCurrentPlayerIndicator = true;
            currentPlayerIndicatorThread.Start();

            currentGame = new ReversiGame(boardSizeSelectorPos);
            currentGame.helpMode = tbHelp.Value;
           
            drawStones();                       // Standaard grid tekenen

            currentGame.setInitialStones();     // Stenen plaatsen

            drawStones();                       // Stenen tekenen
        }

        private void btEndGame_Click(object sender, EventArgs e)
        {
            this.animateCurrentPlayerIndicator = false;

            this.gbPreGameControls.Visible = true;
            this.gbInGameControls.Visible = false;

            this.Size = new Size(540, 210);
        }

        private void pnBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int[] gridPos = {(int)(e.X / currentGame.gridSize), (int)(e.Y / currentGame.gridSize)};

            currentGame.processTurn(gridPos);

            if (((currentGame.players[ReversiGame.PLAYER_1].stones + currentGame.players[ReversiGame.PLAYER_2].stones) == (currentGame.boardSize[0] * currentGame.boardSize[1])) || currentGame.gameEnded == true)
            {
                if (currentGame.players[ReversiGame.PLAYER_1].stones > currentGame.players[ReversiGame.PLAYER_2].stones)
                {
                    //lbPlayerTurn.Text = "Zwart heeft gewonnen";
                }
                else if (currentGame.players[ReversiGame.PLAYER_1].stones == currentGame.players[ReversiGame.PLAYER_2].stones)
                {
                   // lbPlayerTurn.Text = "Gelijkspel";
                }
                else
                {
                   // lbPlayerTurn.Text = "Wit heeft gewonnen";
                }
            }
            drawStones();
            pnScoreKeeper.Invalidate();
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

        private void tbHelp_ValueChanged(object sender, EventArgs e)
        {
            currentGame.helpMode = tbHelp.Value;
            toggleHelpStones();
        }

        private void pnBoard_Paint(object sender, PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.DrawImage(boardBitmap, 0, 0);
        }

        private void drawBoardSizeSelector(object obj, PaintEventArgs paintEventArgs)
        {
            Graphics graphics = paintEventArgs.Graphics;

            int gridWidth = pnBoardSize.Width - 1;
            int gridSize = (gridWidth / ReversiGame.MAX_BOARDSIZE);                                // (Panel width / number of fields)

            Console.WriteLine("boardsize; " + boardSizeSelectorPos[0]);

            int endX = (int)(boardSizeSelectorPos[0] * gridSize);                                  // Geselecteerde boardsize
            int endY = (int)(boardSizeSelectorPos[1] * gridSize);
            graphics.FillRectangle(Brushes.LightGray, 0, 0, endX, endY);

            if (drawMoveSelector)
            {
                endX = (int)(boardSizeMoveSelectorPos[0] * gridSize);                              // Huidige selectie
                endY = (int)(boardSizeMoveSelectorPos[1] * gridSize);
                graphics.FillRectangle(Brushes.White, 0, 0, endX, endY);
            }

            Pen linePen = new Pen(gridGreenDark, 1);

            for (double x = 0; x < ReversiGame.MAX_BOARDSIZE; x++)
            {
                Point startPoint = new Point((int)(x * gridSize), 0);
                Point endPoint = new Point((int)(x * gridSize), 500);

                graphics.DrawLine(linePen, startPoint, endPoint);
            }
            for (double y = 0; y < ReversiGame.MAX_BOARDSIZE; y++)
            {
                Point startPoint = new Point(0, (int)(y * gridSize));
                Point endPoint = new Point(500, (int)(y * gridSize));

                graphics.DrawLine(linePen, startPoint, endPoint);
            }
            graphics.DrawLine(linePen, gridWidth, 0, gridWidth, gridWidth);
            graphics.DrawLine(linePen, 0, gridWidth, gridWidth, gridWidth);
        }

        private void updateCurrentPlayerThread()
        {
            Boolean increase = true;
            while (animateCurrentPlayerIndicator)
            {
                if (increase)
                {
                    currentPlayerIndicatorTransparency += 20;
                }
                else
                {
                    currentPlayerIndicatorTransparency -= 20;
                }

                if (currentPlayerIndicatorTransparency > 255)
                {
                    increase = false;
                    currentPlayerIndicatorTransparency = 255;
                }
                else if(currentPlayerIndicatorTransparency < 0)
                {
                    increase = true;
                    currentPlayerIndicatorTransparency = 0;
                }

                pnScoreKeeper.Invalidate();
                Thread.Sleep(40);
            }
        }
        private void drawScoreKeeper(object obj, PaintEventArgs paintEventArgs)
        {
            Graphics graphics = paintEventArgs.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle blackStoneRect = new Rectangle(20, 20, pnScoreKeeper.Height - 40, pnScoreKeeper.Height - 40);
            Rectangle whiteStoneRect = new Rectangle(pnScoreKeeper.Height + 20, 20, pnScoreKeeper.Height - 40, pnScoreKeeper.Height - 40);
            Brush brushStoneBlack = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.Black, Color.FromArgb(255, 70, 70, 70));
            Brush brushStoneWhite = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.FromArgb(255, 220, 220, 220));
            graphics.FillEllipse(brushStoneBlack, blackStoneRect);
            graphics.FillEllipse(brushStoneWhite, whiteStoneRect);

            Pen circlePen;
            switch (currentGame.currentPlayer)
            {
                case ReversiGame.PLAYER_1:
                    circlePen = new Pen(Color.FromArgb(currentPlayerIndicatorTransparency, 255, 255, 255), 5);
                    graphics.DrawEllipse(circlePen, blackStoneRect);
                    break;
                case ReversiGame.PLAYER_2:
                    circlePen = new Pen(Color.FromArgb(currentPlayerIndicatorTransparency, 0, 0, 0), 5);
                    graphics.DrawEllipse(circlePen, whiteStoneRect);
                    break;
            }
                        
            if (currentGame.players[ReversiGame.PLAYER_1] != null)
            {
                Font font = new Font("Tahoma", 30);

                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Center;

                graphics.DrawString(currentGame.players[ReversiGame.PLAYER_1].stones.ToString(), font, Brushes.White, blackStoneRect, stringFormat);
                graphics.DrawString(currentGame.players[ReversiGame.PLAYER_2].stones.ToString(), font, Brushes.Black, whiteStoneRect, stringFormat);
            }
        }

        private void drawStones()
        {
            for (int x = 0; x < currentGame.boardSize[0]; x++)
            {
                for (int y = 0; y < currentGame.boardSize[1]; y++)
                {
                    int stoneAtPos = currentGame.board[x, y];
                    int drawnStoneAtPos = currentGame.drawnBoard[x, y];
                    int circleX = (int)(x * currentGame.gridSize) + 2;
                    int circleY = (int)(y * currentGame.gridSize) + 2;

                    int gridSizeInt = (int)currentGame.gridSize;

                    if (drawnStoneAtPos != stoneAtPos)
                    {
                        switch (stoneAtPos)
                        {
                            case ReversiGame.STONE_VALID:
                                if (currentGame.helpMode == ReversiGame.HELP_MILD)
                                {
                                    lock (boardBitmapGraphics)
                                    {
                                        boardBitmapGraphics.FillEllipse(validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                                    }
                                }
                                else if (currentGame.helpMode == ReversiGame.HELP_FULL)
                                {
                                    //currentGraphics.FillEllipse(currentGame.validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                                    // BEST MOVE
                                }
                                else
                                {
                                    setEmptyField(x, y, circleX, circleY);
                                }
                                break;
                            case ReversiGame.STONE_EMPTY:
                                setEmptyField(x, y, circleX, circleY);
                                break;
                            case ReversiGame.PLAYER_1:
                                Thread drawStoneThread = new Thread(() => drawStone(x, y, circleX, circleY, gridSizeInt, stoneAtPos));
                                drawStoneThread.Start();
                                break;
                            case ReversiGame.PLAYER_2:
                                drawStoneThread = new Thread(() => drawStone(x, y, circleX, circleY, gridSizeInt, stoneAtPos));
                                drawStoneThread.Start();
                                break;
                        }

                        currentGame.drawnBoard[x, y] = stoneAtPos;

                    }
                }
            }
            pnBoard.Invalidate();
        }

        private void drawStone(int x, int y, int circleX, int circleY, int gridSizeInt, int stoneAtPos)
        {
            int transparency = 0;
            switch (stoneAtPos)
            {
                case ReversiGame.PLAYER_1:
                    while (transparency < 255)
                    {
                        Brush brushStoneBlack = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.FromArgb(transparency, 0, 0, 0), Color.FromArgb(transparency, 70, 70, 70));
                        lock (boardBitmapGraphics)
                        {
                            boardBitmapGraphics.FillEllipse(brushStoneBlack, circleX + (gridSizeInt / 12), circleY + (gridSizeInt / 12), gridSizeInt - (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 6));
                        }
                        pnBoard.Invalidate();
                        transparency += 10;
                        Thread.Sleep(30);
                    }
                    break;
                case ReversiGame.PLAYER_2:
                    while (transparency < 255)
                    {
                        Brush brushStoneWhite = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.FromArgb(transparency, 255, 255, 255), Color.FromArgb(transparency, 220, 220, 220));
                        lock (boardBitmapGraphics)
                        {
                            boardBitmapGraphics.FillEllipse(brushStoneWhite, circleX + (gridSizeInt / 12), circleY + (gridSizeInt / 12), gridSizeInt - (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 6));
                        }
                        
                        pnBoard.Invalidate();
                        transparency += 10;
                        Thread.Sleep(30);
                    }
                    break;
            }
        }

        private void toggleHelpStones()
        {
            for (int x = 0; x < currentGame.boardSize[0]; x++)
            {
                for (int y = 0; y < currentGame.boardSize[1]; y++)
                {
                    int stoneAtPos = currentGame.board[x, y];
                    int circleX = (int)(x * currentGame.gridSize) + 2;
                    int circleY = (int)(y * currentGame.gridSize) + 2;

                    int gridSizeInt = (int)currentGame.gridSize;

                    if (stoneAtPos == ReversiGame.STONE_VALID)
                    {
                        if (currentGame.helpMode == ReversiGame.HELP_MILD)
                        {
                            lock (boardBitmapGraphics)
                            {
                                Console.WriteLine("HI");
                                boardBitmapGraphics.FillEllipse(validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                            }
                        }
                        else if (currentGame.helpMode == ReversiGame.HELP_FULL)
                        {
                            //currentGraphics.FillEllipse(currentGame.validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                            // BEST MOVE
                        }
                        else
                        {
                            setEmptyField(x, y, circleX, circleY);
                        }
                    }
                    currentGame.drawnBoard[x, y] = stoneAtPos;
                }
            }

            pnBoard.Invalidate();
        }

        private void setEmptyField(int x, int y, int startX, int startY)
        {
            if (x % 2 == 0)
            {
                if (y % 2 == 0)
                {
                    lock (boardBitmapGraphics)
                    {
                        boardBitmapGraphics.FillRectangle(gridGreenLight, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
                else
                {
                    lock (boardBitmapGraphics)
                    {
                        boardBitmapGraphics.FillRectangle(gridGreenDark, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
            }
            else
            {
                if (y % 2 != 0)
                {
                    lock (boardBitmapGraphics)
                    {
                        boardBitmapGraphics.FillRectangle(gridGreenLight, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
                else
                {
                    lock (boardBitmapGraphics)
                    {
                        boardBitmapGraphics.FillRectangle(gridGreenDark, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
            }
        }
    }
}
