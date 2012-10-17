using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reversi
{
    public class BufferedPanel : Panel      //  Double-buffered panel om flikkeren te voorkomen
    {
        public BufferedPanel()
        {
            this.DoubleBuffered = true;
        }
    }
}
