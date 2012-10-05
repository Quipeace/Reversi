using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiGame
    {
        int[,] board;

        public ReversiGame(int[] bordSize)
        {
            board = new int[bordSize[0], bordSize[1]];

        }
    }
}
