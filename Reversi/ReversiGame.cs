using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiGame
    {
        public const int STONE_EMPTY = -2;
        public const int STONE_VALID = -1;
        public const int STONE_BLUE = 1;
        public const int STONE_RED = 2;

        public const int MAX_PLAYERS = 2;
        public const int PLAYER_1 = 1;
        public const int PLAYER_2 = 2;


        public ReversiPlayer[] players = new ReversiPlayer[MAX_PLAYERS + 1];
        public int currentPlayer;

        public double gridSize;
        public int boardSize;
        public int[,] board;

        public ReversiGame(int boardSize)
        {
            this.boardSize = boardSize;
            this.board = new int[boardSize, boardSize];

            this.gridSize = 500 / (double) boardSize ;

            currentPlayer = PLAYER_1;
            players[PLAYER_1] = new ReversiPlayer();
            players[PLAYER_2] = new ReversiPlayer();


            int posXFirst = boardSize / 2;
            int posYFirst = boardSize / 2;
            board[posXFirst, posYFirst] = STONE_BLUE;
            board[posXFirst, posYFirst - 1] = STONE_RED;
            board[posXFirst - 1, posYFirst] = STONE_RED;
            board[posXFirst - 1, posYFirst - 1] = STONE_BLUE;

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
                if (currentPlayer > MAX_PLAYERS)                     // Als deze speler niet meedoet, terug naar de eerste
                {
                    currentPlayer = PLAYER_1;
                }

                refreshValidMoves();                                // Geldige zetten voor de nieuwe speler uitrekenen
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
            for (int x = 0; x < boardSize; x++)         // voor alle kolommen
            {
                for (int y = 0; y < boardSize; y++)     // voor alle rijen
                {
                    if (board[x, y] > 0)                // als het een daadwerkelijke steen is
                    {
                        checkValidMovesAround(x, y);          // Mogelijke zetten rond deze steen berekenen
                    }
                }
            }
        }

        private Boolean checkValidMovesAround(int x, int y)
        {
            for (int i = -1; i <= 1; i++)                   // x-offset
            {
                for (int n = -1; n <= 1; n++)               // y-offset
                {
                    try
                    {
                        if (board[x + i, y + n] > 0)            // Deze positie is al bezet, doorgaan
                        {
                            continue;
                        }
                    }
                    catch(IndexOutOfRangeException)
                    {
                        continue;
                    }

                    if (isValidMove(x + i, y + n))      // Deze zet is voor de huidige speler een geldige zet
                    {
                        board[x + i, y + n] = STONE_VALID;
                    }
                    else                                // Zo niet, is de positie leeg (geen hint-steen)
                    {
                        board[x + i, y + n] = STONE_EMPTY;                      
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
            for (int i = x + 1; i < boardSize; i++)                 // Naar rechts
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
            for (int i = y + 1; i < boardSize; i++)                 // Naar beneden
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
            for (int i = 1; i < boardSize; i++)                     // Links-omhoog
            {
                if (x - i < 0 || y - i < 0)                         // Buiten het bord, geen geldige zet
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
            for (int i = 1; i < boardSize; i++)                     // Rechts-omhoog
            {
                if (x + i >= boardSize || y - i < 0)
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
            for (int i = 1; i < boardSize; i++)                     // Links-naar beneden
            {
                if (x - i < 0 || y + i >= boardSize)
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
            for (int i = 1; i < boardSize; i++)                     // Rechts-naar beneden
            {
                if (x + i >= boardSize || y + i >= boardSize)
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
