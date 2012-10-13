using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiPlayer
    {
        public Brush brush;
        public int stones;

        public ReversiPlayer(Brush brush)
        {
            this.brush = brush;
            this.stones = 2;
        }
    }
}
