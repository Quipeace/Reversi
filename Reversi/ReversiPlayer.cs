using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiPlayer
    {
        public int stones;
        public Boolean hasValidMoves;

        public ReversiPlayer()
        {
            this.stones = 2;
            this.hasValidMoves = true;
        }
    }
}
