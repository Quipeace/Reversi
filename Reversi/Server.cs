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
    class Server
    {
        public static bool runServer = false;       // Of de server mag draaien
        private static bool runWorkers = false;     // Of eventuele workers mogen draaien
        public static Boolean isConnected = false;  // Server is verbonden ja/nee
        private static ReversiForm currentForm;     // Form dat behandeld moet worden
        public static TcpListener listener;         // Huidige listener

        private static StreamReader reader;         // Voor lezen
        private static StreamWriter writer;         // Voor schrijven

        public static void start(ReversiForm form)
        {
            currentForm = form;

            while (runServer)
            {
                listener = new TcpListener(1337);       // Deprecated.. Lokaal adres filteren gaat wellicht wat out-of-scope..?
                listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);    // Mag reused worden
                listener.Start();                       // Listener starten

                try
                {
                    Socket socket = listener.AcceptSocket();                                        // Verbinding accepteren

                    onServerConnected(socket);                                                      // Server is verbonden, verder laten afhandelen
                }
                catch (SocketException)
                {
                }

                listener.Stop();
            }
            runWorkers = false;
        }

        private static void onServerConnected(Socket socket)
        {   
            isConnected = true;                         // Server is verbonden

            Stream stream = new NetworkStream(socket);  // Stream ophalen en "splitsen"
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;                    // Meteen schrijven


            currentForm.Invoke(new ReversiForm.startGameCallback(currentForm.startGame));   // Start game button laten "klikken"

            read();                                     // Lees-loop

            writer.Close();                             // Netjes afsluiten
            reader.Close();
            stream.Close();
            socket.Close();

            isConnected = false;                        // Niet meer verbonden
        }

        private static void read()
        {
            runWorkers = true;                          // Worker mag draaien
            while (runWorkers)
            {
                try
                {
                    String input = reader.ReadLine();

                    if (input != null)
                    {
                        if (input.StartsWith("START:"))         // De client heeft een spel gestart met coordinaten gescheiden door een comma
                        {
                            input = input.Substring(6);         // "START:" gedeelte weghalen
                            string[] size = input.Split(',');   // Overgebleven string splitsen op de komma
                            currentForm.boardSizeSelectorPos[0] = int.Parse(size[0]);   // Boardsize zetten in het form
                            currentForm.boardSizeSelectorPos[1] = int.Parse(size[1]);
                            currentForm.Invoke(new ReversiForm.startGameCallback(currentForm.startGame));   // Start game button laten "klikken"
                        }
                        else if (input.StartsWith("MOVE@"))     // De client doet een zet met een muisklik op de coordinaten door een komma gescheiden
                        {
                            input = input.Substring(5);         // Zelfde verhaal als hierboven
                            string[] coordinates = input.Split(',');
                            int x = int.Parse(coordinates[0]);
                            int y = int.Parse(coordinates[1]);

                            currentForm.handleBoardMouseClick(x, y);
                        }
                        else if (input.Equals("ENDGAME"))       // De client heeft het spel gestopt, stop deze worker en verbreek de verbinding
                        {
                            runWorkers = false;
                        }
                    }
                    else
                    {
                        runWorkers = false;             // Null = worker stoppen
                    }
                }
                catch (IOException)
                {
                    runWorkers = false;                 // IOException, worker stoppen
                }
            }
        }

        public static void writeMessage(string message) // Bericht naar ander partij laten sturen
        {
            writer.WriteLine(message);
        }
    }
}
