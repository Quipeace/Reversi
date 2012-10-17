using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    static class Program
    {
        public static string hostName;

        static void Main()
        {
            hostName = Dns.GetHostName();
            Application.EnableVisualStyles();
            Application.Run(new ReversiForm());
        }
    }
}
