using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiGame
    {
        public const int MAX_BOARDSIZE = 20;
        public const int MAX_PLAYERS = 2;

        public const int STONE_EMPTY = -3;
        public const int STONE_BESTMOVE = -2;
        public const int STONE_VALID = -1;
        public const int PLAYER_1 = 1;
        public const int PLAYER_2 = 2;

        public ReversiPlayer[] players = new ReversiPlayer[MAX_PLAYERS + 1];
        public int currentPlayer;
        public int validOptions;
        public int newValidOptions;
        public int oldValidOptions;
        public bool gameEnded = false;

        public Brush validMoveBrush;
        public Brush bestMoveBrush;

        public int validMoves;
        public double gridSize;
        public int[] boardSize;
        public int[,] board;
        public int[,] drawnBoard;

        public ReversiGame(int[] boardSize)
        {
            this.boardSize = boardSize;
            this.board = new int[boardSize[0], boardSize[1]];
            this.drawnBoard = new int[boardSize[0], boardSize[1]];
            if (boardSize[0] > boardSize[1])
            {
                this.gridSize = 500 / (double)boardSize[0]; // TODO: schalen?
            }
            else
            {
                this.gridSize = 500 / (double)boardSize[1];
            }

            for (int x = 0; x < boardSize[0]; x++)
            {
                for (int y = 0; y < boardSize[1]; y++)
                {
                    this.board[x, y] = STONE_EMPTY;
                }
            }

            this.currentPlayer = PLAYER_1;
            this.players[PLAYER_1] = new ReversiPlayer(new HatchBrush(HatchStyle.LargeCheckerBoard, Color.Black, Color.FromArgb(255, 70, 70, 70)));
            this.players[PLAYER_2] = new ReversiPlayer(new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.FromArgb(255, 220, 220, 220)));

            this.validMoveBrush = new SolidBrush(Color.FromArgb(150, 150, 150, 150));
            this.bestMoveBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        }

        public void setInitialStones()
        {
            int posXFirst = boardSize[0] / 2;
            int posYFirst = boardSize[1] / 2;
            board[posXFirst, posYFirst] = PLAYER_1;
            board[posXFirst, posYFirst - 1] = PLAYER_2;
            board[posXFirst - 1, posYFirst] = PLAYER_2;
            board[posXFirst - 1, posYFirst - 1] = PLAYER_1;

            refreshValidMoves();
        }

        public void processTurn(int[] clickPos)
        {
            int gridValue = board[clickPos[0], clickPos[1]];

            if (gridValue == STONE_VALID)                           // Als de positie een geldige zet is voor deze speler
            {
                board[clickPos[0], clickPos[1]] = currentPlayer;    // Huidige positie heeft nu de steen van de speler
                players[currentPlayer].stones++;

                finishTurn(clickPos[0], clickPos[1]);

                currentPlayer++;                                    // Volgende speler
                if (currentPlayer > MAX_PLAYERS)                    // Als deze speler niet meedoet, terug naar de eerste
                {
                    currentPlayer = PLAYER_1;
                }

                if (newValidOptions == 0)
                {
                    oldValidOptions = 0;
                }
                else
                {
                    oldValidOptions = validOptions;
                }
                validOptions = 0;

                refreshValidMoves();                                // Geldige zetten voor de nieuwe speler uitrekenen
                newValidOptions = validOptions;
                if (validOptions == 0)
                {
                    if (oldValidOptions == 0)
                    {
                        gameEnded = true;
                    }
                    else
                    {
                        currentPlayer++;
                        if (currentPlayer > MAX_PLAYERS)                     // Als deze speler niet meedoet, terug naar de eerste
                        {
                            currentPlayer = PLAYER_1;
                        }
                        refreshValidMoves();
                    }
                }
            }
        }

        private void finishTurn(int x, int y)
        {
            if (isLeftValid(x, y, false))
            {
                isLeftValid(x, y, true);
            }
            if (isRightValid(x, y, false))
            {
                isRightValid(x, y, true);
            }
            if (isUpValid(x, y, false))
            {
                isUpValid(x, y, true);
            }
            if (isDownValid(x, y, false))
            {
                isDownValid(x, y, true);
            }
            if (isLeftUpValid(x, y, false))
            {
                isLeftUpValid(x, y, true);
            }
            if (isRightUpValid(x, y, false))
            {
                isRightUpValid(x, y, true);
            }
            if (isLeftDownValid(x, y, false))
            {
                isLeftDownValid(x, y, true);
            }
            if (isRightDownValid(x, y, false))
            {
                isRightDownValid(x, y, true);
            }
        }

        public void refreshValidMoves()
        {
            for (int x = 0; x < boardSize[0]; x++)      // voor alle kolommen
            {
                for (int y = 0; y < boardSize[1]; y++)  // voor alle rijen
                {
                    if (board[x, y] > 0)                // als het een daadwerkelijke steen is
                    {
                        checkValidMovesAround(x, y);          // Mogelijke zetten rond deze steen berekenen

                        if (checkValidMovesAround(x, y) == true)
                        {
                            validMoves++;
                        }
                    }
                }
            }
        }

        private Boolean checkValidMovesAround(int x, int y)
        {
            for (int i = -1; i <= 1; i++)                   // x-offset
            {
                if (x + i < 0 || x + i >= boardSize[0])     // Als de huidige positie buiten het bord valt, deze berekening overslaan
                {
                    continue;
                }
                for (int n = -1; n <= 1; n++)               // y-offset
                {
                    if (y + n < 0 || y + n >= boardSize[1]) // Als de huidige positie buiten het bord valt, deze berekening overslaan
                    {
                        continue;
                    }
                    if (board[x + i, y + n] > 0)            // Deze positie is al bezet, doorgaan
                    {
                        continue;
                    }
                    else
                    {
                        if (isValidMove(x + i, y + n))      // Deze zet is voor de huidige speler een geldige zet
                        {
                            board[x + i, y + n] = STONE_VALID;
                            validOptions++;
                        }
                        else                                // Zo niet, is de positie leeg (geen hint-steen, geblokte achtergrond)
                        {
                            board[x + i, y + n] = STONE_EMPTY;
                        }
                    }
                }
            }
            return true;
        }

        private Boolean isValidMove(int x, int y)
        {
           if (isLeftValid(x, y, false))
           {
               return true;
           }
           else if (isRightValid(x, y, false))
           {
               return true;
           }
           else if (isUpValid(x, y, false))
           {
               return true;
           }
           else if (isDownValid(x, y, false))
           {
               return true;
           }
           else if (isLeftUpValid(x, y, false))
           {
               return true;
           }
           else if (isRightUpValid(x, y, false))
           {
               return true;
           }
           else if (isLeftDownValid(x, y, false))
           {
               return true;
           }
           else if (isRightDownValid(x, y, false))
           {
               return true;
           }
           else
           {
               return false;
           }
        }

        private bool isLeftValid(int x, int y, bool setStone)
        {
            for (int i = x - 1; i >= 0; i--)                         // Naar links
            {
                int currentStone = board[i, y];

                if (i == x - 1 && currentStone == currentPlayer)    // Als de steen op de deze positie al van de speler is, geen geldige zet
                {
                    break;
                }
                else if (currentStone < 0)                          // Lijn onderbroken door lege positie, geen geldige zet
                {
                    break;
                }
                else if (currentStone == currentPlayer)             // Als de eerste steen niet van de speler was, maar daarna wel is het een geldige zet
                {
                    return true;
                }
                else if (setStone)                                  // Als setStone true is tussenliggende stenen al zetten
                {
                    setStoneAt(i, y, currentStone);
                }
            }
            return false;
        }
        private bool isRightValid(int x, int y, bool setStone)
        {
            for (int i = x + 1; i < boardSize[0]; i++)              // Naar rechts
            {
                int currentStone = board[i, y];
                if (i == x + 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(i, y, currentStone);
                }
            }
            return false;
        }
        private bool isUpValid(int x, int y, bool setStone)
        {
            for (int i = y - 1; i >= 0; i--)                         // Omhoog
            {
                int currentStone = board[x, i];
                if (i == y - 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x, i, currentStone);
                }
            }
            return false;
        }
        private bool isDownValid(int x, int y, bool setStone)
        {
            for (int i = y + 1; i < boardSize[1]; i++)              // Naar beneden
            {
                int currentStone = board[x, i];
                if (i == y + 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x, i, currentStone);
                }
            }
            return false;
        }
        private bool isLeftUpValid(int x, int y, bool setStone)
        {
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++) 
            {
                if (x - i < 0 || y - i < 0)                             // Buiten het bord, geen geldige zet
                {
                    break;
                }
                int currentStone = board[x - i, y - i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x - i, y - i, currentStone);
                }
            }
            return false;
        }
        private bool isRightUpValid(int x, int y, bool setStone)
        {
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++) 
            {
                if (x + i >= boardSize[0] || y - i < 0)
                {
                    break;
                }
                int currentStone = board[x + i, y - i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x + i, y - i, currentStone);
                }
            }
            return false;
        }
        private bool isLeftDownValid(int x, int y, bool setStone)
        {
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++) 
            {
                if (x - i < 0 || y + i >= boardSize[1])
                {
                    break;
                }
                int currentStone = board[x - i, y + i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x - i, y + i, currentStone);
                }
            }
            return false;
        }
        private bool isRightDownValid(int x, int y, bool setStone)
        {
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++)
            {
                if (x + i >= boardSize[0] || y + i >= boardSize[1])
                {
                    break;
                }
                int currentStone = board[x + i, y + i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x + i, y + i, currentStone);
                }
            }
            return false;
        }

        private void setStoneAt(int x, int y, int stoneAt)
        {
            board[x, y] = currentPlayer;
            players[currentPlayer].stones++;
            players[stoneAt].stones--;
        }
    }
}
