using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiGame
    {
        public int gridSize;
        public int[] boardSize;
        public int[,] board;

        public ReversiGame(int[] boardSize)
        {
            this.boardSize = boardSize;
            this.board = new int[boardSize[0], boardSize[1]];

            this.gridSize = 500 / boardSize[0];
        }
    }
}
