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
        public static bool runServer = false;
        private static bool runWorkers = false;
        public static Boolean isConnected = false;
        private static ReversiForm currentForm;
        public static TcpListener listener;

        public static string waitingString = "";

        public static void start(ReversiForm form)
        {
            currentForm = form;

            while (runServer)
            {
                listener = new TcpListener(1337);       // Deprecated.. Lokaal adres filteren gaat wellicht wat out-of-scope...
                listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                listener.Start();

                try
                {
                    Console.WriteLine("SERVER CONNECTING");
                    Socket socket = listener.AcceptSocket();

                    currentForm.btStart.Invoke(new ReversiForm.startGameCallback(form.startGame));

                    onServerConnected(socket);
                }
                catch (SocketException e)
                {
                }

                listener.Stop();
            }
            runWorkers = false;
        }

        private static void onServerConnected(Socket socket)
        {
            isConnected = true;
            Console.WriteLine("SERVER CONNECTED");

            Stream stream = new NetworkStream(socket);
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            waitingString = "";
            runWorkers = true;

            Thread readerT = new Thread(() => readerThread(reader));
            readerT.Start();
            Thread writerT = new Thread(() => writerThread(writer));
            writerT.Start();

            readerT.Join();
            writerT.Join();

            writer.Close();
            reader.Close();
            stream.Close();
            socket.Close();

            Console.WriteLine("SERVER CLOSED");
            isConnected = false;
        }

        private static void readerThread(StreamReader reader)
        {
            while (runWorkers)
            {
                try
                {
                    Console.WriteLine("SERVER WAIT FOR IN");
                    String input = reader.ReadLine();

                    Console.WriteLine("SERVER INPUT: " + input);

                    if (input != null)
                    {
                        if (input.Contains("MOVE@"))
                        {
                            input = input.Substring(5);
                            string[] coordinates = input.Split(',');
                            int x = int.Parse(coordinates[0]);
                            int y = int.Parse(coordinates[1]);

                            currentForm.handleBoardMouseClick(x, y);
                        }
                        else if (input.Equals("ENDGAME"))
                        {
                            waitingString = "ENDGAME";
                            runWorkers = false;
                        }
                    }
                    else
                    {
                        runWorkers = false;
                    }

                    Thread.Sleep(100);
                }
                catch (IOException)
                {
                    runServer = false;
                }
            }
        }

        private static void writerThread(StreamWriter writer)
        {
            while (runWorkers)
            {
                if (waitingString.Length != 0)
                {
                    writer.WriteLine(waitingString);
                    Console.WriteLine("SERVER SEND: " + waitingString);
                    waitingString = "";
                }
                Thread.Sleep(100);
            }
        }
    }
}
