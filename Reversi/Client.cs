using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Reversi
{
    class Client
    {
        public static Boolean isConnected = false;  // Client verbonden ja/nee
        private static ReversiForm currentForm;     // Huidige form
        public static TcpClient tcpClient;          // Daadwerkelijke client
        public static bool runClient = false;       // Client draaien-toestemming

        private static StreamReader reader;
        private static StreamWriter writer;

        public static void start(string hostName, ReversiForm form)
        {
            if (isConnected)                        // Als is verbonden, nieuwe clients 
            {
                return;
            }

            currentForm = form; 
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(hostName, 1337);      // CLient verbinden met hostname op port leet
         
                currentForm.Invoke(new ReversiForm.startGameCallback(currentForm.startGame));   // start game button laten "klikken"

                Stream stream = tcpClient.GetStream();  // Stream ophalen
                onClientConnected(stream);              // Verder afhandelen
            }
            catch (SocketException)
            {
            }
        }

        private static void onClientConnected(Stream stream)        // Zelfde als onServerConnected
        {
            isConnected = true;

            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            writeMessage("START:" + currentForm.boardSizeSelectorPos[0] + "," + currentForm.boardSizeSelectorPos[1]);   // Start-bericht naar server
            
            read();

            writer.Close();
            reader.Close();
            stream.Close();
            tcpClient.Close();

            isConnected = false;
        }

        private static void read()                                  // Zelfde als in server
        {
            while (runClient)
            {
                try
                {
                    String input = reader.ReadLine();

                    if (input != null)
                    {
                        if (input.Contains("START:"))
                        {
                            input = input.Substring(6);
                            string[] coordinates = input.Split(',');
                            int x = int.Parse(coordinates[0]);
                            int y = int.Parse(coordinates[1]);
                        }
                        else if (input.Contains("MOVE@"))
                        {
                            input = input.Substring(5);
                            string[] coordinates = input.Split(',');
                            int x = int.Parse(coordinates[0]);
                            int y = int.Parse(coordinates[1]);

                            currentForm.handleBoardMouseClick(x, y);
                        }
                        else if (input.Equals("ENDGAME"))
                        {
                            runClient = false;
                        }
                    }
                    else
                    {
                        runClient = false;
                    }
                }
                catch (IOException)
                {
                    runClient = false;
                }
            }
        }

        public static void writeMessage(string message)     // Bericht naar andere partij sturen
        {
            writer.WriteLine(message);
        }
    }
}
