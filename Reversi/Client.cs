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
        public static Boolean isConnected = false;
        private static ReversiForm currentForm;
        public static TcpClient tcpClient;
        public static bool runClient = false;

        public static string waitingString = "";

        public static void start(string hostName, ReversiForm form)
        {
            if (isConnected)
            {
                return;
            }

            currentForm = form;
            tcpClient = new TcpClient();
            tcpClient.Connect(hostName, 1337);

            try
            {
                Stream stream = tcpClient.GetStream();

                form.btStart.Invoke(new ReversiForm.startGameCallback(form.startGame));

                onClientConnected(stream);
            }
            catch (SocketException)
            {

            }

        }

        private static void onClientConnected(Stream stream)
        {
            isConnected = true;
            Console.WriteLine("CLIENT CONNECTED");

            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            waitingString = "";

            Thread readerT = new Thread(() => readerThread(reader));
            readerT.Start();
            Thread writerT = new Thread(() => writerThread(writer));
            writerT.Start();

            readerT.Join();
            writerT.Join();

            writer.Close();
            reader.Close();
            stream.Close();
            tcpClient.Close();

            Console.WriteLine("CLIENT CLOSED");
            isConnected = false;
        }

        private static void readerThread(StreamReader reader)
        {
            while (runClient)
            {
                try
                {
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
                            runClient = false;
                        }
                    }
                    else
                    {
                        runClient = false;
                    }

                    Thread.Sleep(100);
                }
                catch (IOException)
                {
                    runClient = false;
                }
            }
        }

        private static void writerThread(StreamWriter writer)
        {
            while (runClient)
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
