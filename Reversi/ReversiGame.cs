using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiGame
    {
        public const int STONE_RED = 1;
        public const int STONE_BLUE = 2;

        public double gridSize;
        public int[] boardSize;
        public int[,] board;

        public ReversiGame(int[] boardSize)
        {
            this.boardSize = boardSize;
            this.board = new int[boardSize[0], boardSize[1]];

            this.gridSize = 500 / (double) boardSize[0] ;

            int posXFirst = boardSize[0] / 2;
            int posYFirst = boardSize[1] / 2;

            board[posXFirst, posYFirst] = STONE_BLUE;
            board[posXFirst, posYFirst - 1] = STONE_RED;
            board[posXFirst - 1, posYFirst] = STONE_RED;
            board[posXFirst - 1, posYFirst - 1] = STONE_BLUE;
        }
    }
}
