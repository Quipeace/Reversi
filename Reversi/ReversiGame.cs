using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiGame
    {
        public const int STONE_VALID = -1;
        public const int STONE_EMPTY = 0;
        public const int STONE_BLUE = 1;
        public const int STONE_RED = 2;

        public const int PLAYER_1 = 1;
        public const int PLAYER_2 = 2;

        public int currentPlayer;
        public int maxPlayers;

        public double gridSize;
        public int boardSize;
        public int[,] board;

        public ReversiGame(int boardSize)
        {
            this.boardSize = boardSize;
            this.board = new int[boardSize, boardSize];

            this.gridSize = 500 / (double) boardSize ;

            currentPlayer = PLAYER_1;
            maxPlayers = PLAYER_2;

            int posXFirst = boardSize / 2;
            int posYFirst = boardSize / 2;
            board[posXFirst, posYFirst] = STONE_BLUE;
            board[posXFirst, posYFirst - 1] = STONE_RED;
            board[posXFirst - 1, posYFirst] = STONE_RED;
            board[posXFirst - 1, posYFirst - 1] = STONE_BLUE;

            initValidMoves();
        }

        public void processTurn(int[] clickPos)
        {
            int gridValue = board[clickPos[0], clickPos[1]];
            if (gridValue == STONE_VALID && (gridValue != STONE_BLUE || gridValue != STONE_RED))
            {
                board[clickPos[0], clickPos[1]] = currentPlayer;

                currentPlayer++;
                if (currentPlayer > maxPlayers)
                {
                    currentPlayer = PLAYER_1;
                }

                initValidMoves();
            }
        }

        public void initValidMoves()
        {
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    if (board[x, y] == STONE_BLUE || board[x, y] == STONE_RED)
                    {
                        checkValidMoves(x, y);
                    }
                }
            }
        }

        private Boolean checkValidMoves(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                if (x + i < 0 || x + i >= boardSize)
                {
                    continue;
                }

                for (int n = -1; n <= 1; n++)
                {
                    if (y + n < 0 || y + n >= boardSize)
                    {
                        continue;
                    }

                    if (board[x + i, y + n] == STONE_BLUE || board[x + i, y + n] == STONE_RED)
                    {
                        continue;
                    }
                    else
                    {
                        if (isValidMove(x + i, y + n))
                        {
                            board[x + i, y + n] = STONE_VALID;
                        }
                        else
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
            Boolean leftHorizValid = false;
            for (int i = x; i > 0; i--)
            {
                int currentStone = board[i, y];
                if (i == x - 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    leftHorizValid = true;
                    break;
                }
            }

            Boolean rightHorizValid = false;
            for (int i = x; i < boardSize; i++)
            {
                int currentStone = board[i, y];
                if (i == x + 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    rightHorizValid = true;
                    break;
                }
            }

            Boolean bottomVerticalValid = false;
            for (int i = y; i < boardSize; i++)
            {
                int currentStone = board[x, i];
                if (i == y + 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    bottomVerticalValid = true;
                    break;
                }
            }

            Boolean topVerticalValid = false;
            for (int i = y; i > 0; i--)
            {
                int currentStone = board[x, i];
                if (i == y - 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    topVerticalValid = true;
                    break;
                }
            }

            if (leftHorizValid || rightHorizValid || bottomVerticalValid || topVerticalValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
