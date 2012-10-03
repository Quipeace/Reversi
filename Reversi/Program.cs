using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new ReversiForm());
        }
    }
}
