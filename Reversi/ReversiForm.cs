using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Reversi
{
    public partial class ReversiForm : Form
    {
        private Brush brushGridGreenLight;                      // Brushes
        private Brush brushGridGreenDark;
        private Brush validMoveBrush;
        private Brush bestMoveBrush;

        private bool drawMoveSelector = false;                  // Boolean voor het tekenen witte boardsize selectie
        private int[] boardSizeMoveSelectorPos = new int[2];    // Geselecteerde boardsize
        public int[] boardSizeSelectorPos = { 8, 8 };          // Geklikte boardsize

        private int currentPlayerIndicatorTransparency = 0;     // Transparantie van het indicator-circeltje
        private bool animateCurrentPlayerIndicator = true;      // Animeren van indicator-circle

        private Bitmap boardBitmap;                             // Speelbord-bitmap
        private Graphics boardBitmapGraphics;                   // Graphics voor het tekenen bitmap

        private ReversiGame currentGame;                        // Huidige spel

        public ReversiForm()
        {
            this.InitializeComponent();

            this.validMoveBrush = new SolidBrush(Color.FromArgb(255, 90, 150, 150));
            this.bestMoveBrush = new SolidBrush(Color.FromArgb(255, 255, 225, 30));
            this.brushGridGreenLight = new SolidBrush(Color.FromArgb(106, 206, 0));
            this.brushGridGreenDark = new SolidBrush(Color.FromArgb(83, 160, 0));

            this.BackColor = Color.FromArgb(93, 181, 0);

            this.tbConnect.Text = Program.hostName;             // Hostname ophalen
        }

        private void ReversiForm_FormClosing(object sender, FormClosingEventArgs e)     // Als het scherm gesloten wordt, netjes afsluiten
        {
            this.animateCurrentPlayerIndicator = false;         // Stoppen met animeren

            Server.runServer = false;                           // Server/client stoppen
            Client.runClient = false;
            if (Server.listener != null)
            {
                Server.listener.Stop();                         // Eventueel exception forceren, om alsnog te sluiten.
            }
            if (Client.tcpClient != null)
            {
                Client.tcpClient.Close();
            }
        }

        // ----------
        // Server/Client starter
        // ----------
        private void btConnect_Click(object sender, EventArgs e)        // Een klik op deze button start de client met de ingetypte hostname
        {
            if (!Client.runClient)
            {
                Client.runClient = true;
                Thread clientT = new Thread(() => Client.start(tbConnect.Text, this));
                clientT.Start();
            }
        }
        private void btStartServer_Click(object sender, EventArgs e)    // Server starten, als deze nog niet gestart is
        {
            if (!Server.runServer)
            {
                Server.runServer = true;
                Thread server = new Thread(() => Server.start(this));
                server.Start();
            }
        }

        // ----------
        // Start game button, met callback voor starten vanuit server/client
        // ----------
        public delegate void startGameCallback();                           // Delegate om vanuit de server/client te kunnen starten
        public void startGame()
        {
            btStart.PerformClick();
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            this.gbInGameControls.Visible = true;                           // Game controls zichtbaar maken
            this.gbPreGameControls.Visible = false;
            this.Size = new Size(540, 725);                                 // Form groter maken
            pnBoard.Size = new Size(500, 500);
            this.boardBitmap = new Bitmap(500, 500);                        // Bitmap aanmaken
            tbHelp.Value = ReversiGame.HELP_OFF;                            // Help standaard uitzetten

            boardBitmapGraphics = Graphics.FromImage(boardBitmap);          // Graphics-object om te tekenen
            animateCurrentPlayerIndicator = true;
            Thread currentPlayerIndicatorThread = new Thread(currentPlayerIndicatorAnimator);   // Indicator-animator starten
            currentPlayerIndicatorThread.Start();

            currentGame = new ReversiGame(boardSizeSelectorPos);
            currentGame.helpMode = tbHelp.Value;
           
            drawStones();                       // Standaard grid tekenen
            currentGame.setInitialStones();     // Stenen plaatsen
            drawStones();                       // Veranderde stenen tekenen
        }

        // ----------
        // Game stoppen
        // ----------
        private void btEndGame_Click(object sender, EventArgs e)    // Spel gestopt
        {
            this.animateCurrentPlayerIndicator = false;             // Animatie stoppen

            this.gbPreGameControls.Visible = true;                  // New game zichtbaar
            this.gbInGameControls.Visible = false;

            this.Size = new Size(540, 210);                         // Size terugzetten
                
            if (Client.isConnected)                                 // Client/server stoppen
            {
                Client.writeMessage("ENDGAME");
            }
            else if(Server.isConnected)
            {
                Server.writeMessage("ENDGAME");
            }
        }

        // ----------
        // Een klik op het speelveld, vanuit de UI en server/client
        // ----------
        private void pnBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (Client.isConnected && currentGame.currentPlayer == ReversiGame.PLAYER_1)        // Server is P1, beurt is niet van client, dus return
            {
                return;
            }
            else if (Server.isConnected && currentGame.currentPlayer == ReversiGame.PLAYER_2)   // Idem, maar dan andersom
            {
                return;
            }

            if (Client.isConnected)                                 // Zet doorsturen naar tweede partij
            {
                Client.writeMessage("MOVE@" + e.X + "," + e.Y);
            }
            else if(Server.isConnected)
            {
                Server.writeMessage("MOVE@" + e.X + "," + e.Y);
            }

            handleBoardMouseClick(e.X, e.Y);                        // Mouseclick doorsturen naar andere functie, die evt. ook vanaf client/server aangeroepen kan worden
        }
        public void handleBoardMouseClick(int x, int y)
        {
            int[] gridPos = { (int)(x / currentGame.gridSize), (int)(y / currentGame.gridSize) };   // Muisklik omzetten naar grid-coordinaten

            currentGame.processTurn(gridPos);                       // Zet laten uitrekenen
            drawStones();                                           // Veranderde stenen tekenen

            if (currentGame.gameEnded)                              // Als het spel klaar is, stoppen met animeren
            {
                animateCurrentPlayerIndicator = false;
            }

            pnScoreKeeper.Invalidate();                             // Score netjes updaten
        }


        // ----------
        // Boardsize-selector handlers
        // ----------
        private void pnBoardSize_MouseClick(object sender, MouseEventArgs e)
        {
            boardSizeSelectorPos[0] = boardSizeMoveSelectorPos[0];  // Als er geklikt, de geselecteerde size vastleggen
            boardSizeSelectorPos[1] = boardSizeMoveSelectorPos[1];                              
        }
        private void pnBoardSize_MouseMove(object sender, MouseEventArgs e)
        {
            drawMoveSelector = true;                                // Muis wordt bewogen, teken selectie

            int gridWidth = pnBoardSize.Width - 1;
            int gridSize = (gridWidth / ReversiGame.MAX_BOARDSIZE);

            int newBoardMoveSizeX = (int) (e.X / gridSize) + 1;     // Nieuwe selectie
            int newBoardMoveSizeY = (int)(e.Y / gridSize) + 1;

            // Als de selectie anders is dan de eerder vastgelegde selectie, en groter of gelijk is aan vier, tekenen
            if ((newBoardMoveSizeX != boardSizeMoveSelectorPos[0] || newBoardMoveSizeY != boardSizeMoveSelectorPos[1]) && newBoardMoveSizeX >= 4 && newBoardMoveSizeY >= 4)
            {
                boardSizeMoveSelectorPos[0] = newBoardMoveSizeX;
                boardSizeMoveSelectorPos[1] = newBoardMoveSizeY;
                pnBoardSize.Invalidate();
            }
        }
        private void pnBoardSize_MouseLeave(object sender, EventArgs e) // Muis uit de selector, selectie niet tekenen
        {
            drawMoveSelector = false;
            pnBoardSize.Invalidate();
        }

        private void tbHelp_ValueChanged(object sender, EventArgs e)    // Help-modus aangepast
        {
            currentGame.helpMode = tbHelp.Value;
            toggleHelpStones();                                         // Helpstenen aanpassen
        }

        private void pnBoard_Paint(object sender, PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.DrawImage(boardBitmap, 0, 0);       // Bitmap naar de panel tekenen
        }

        private void pnBoardSize_Paint(object obj, PaintEventArgs paintEventArgs)   // Tekenen van de selector
        {
            Graphics graphics = paintEventArgs.Graphics;                

            int gridWidth = pnBoardSize.Width - 1;
            int gridSize = (gridWidth / ReversiGame.MAX_BOARDSIZE);

            int endX = (int)(boardSizeSelectorPos[0] * gridSize);           // De vastgelegde boardsize in het grijs
            int endY = (int)(boardSizeSelectorPos[1] * gridSize);
            graphics.FillRectangle(Brushes.LightGray, 0, 0, endX, endY);

            if (drawMoveSelector)                                           // Als de selectie getekend moet worden
            {
                endX = (int)(boardSizeMoveSelectorPos[0] * gridSize);       // Huidige selectie in het wit
                endY = (int)(boardSizeMoveSelectorPos[1] * gridSize);
                graphics.FillRectangle(Brushes.White, 0, 0, endX, endY);
            }

            Pen linePen = new Pen(brushGridGreenDark, 1);                   // Dunne pen voor het tekenen van het grid
            for (double x = 0; x < ReversiGame.MAX_BOARDSIZE; x++)          // Verticale lijnen tekenen
            {
                Point startPoint = new Point((int)(x * gridSize), 0);
                Point endPoint = new Point((int)(x * gridSize), 500);

                graphics.DrawLine(linePen, startPoint, endPoint);
            }
            for (double y = 0; y < ReversiGame.MAX_BOARDSIZE; y++)          // Horizontale lijnen tekenen
            {
                Point startPoint = new Point(0, (int)(y * gridSize));
                Point endPoint = new Point(500, (int)(y * gridSize));

                graphics.DrawLine(linePen, startPoint, endPoint);
            }
            graphics.DrawLine(linePen, gridWidth, 0, gridWidth, gridWidth); // Twee missende lijnen (rechts en onder)
            graphics.DrawLine(linePen, 0, gridWidth, gridWidth, gridWidth);
        }

        private void currentPlayerIndicatorAnimator()               // Functie voor het animeren van de indicator
        {
            Boolean increaseTransparency = true;                    // Increase verhoogt transparantie, niet increase verlaagt.
            while (animateCurrentPlayerIndicator)
            {
                if (increaseTransparency)
                {
                    currentPlayerIndicatorTransparency += 20;       // Transparantie 20 hoger
                    if (currentPlayerIndicatorTransparency > 255)   // Hoger dan het maximum, richting omdraaien en zetten op 255
                    {
                        increaseTransparency = false;
                        currentPlayerIndicatorTransparency = 255;
                    }
                } 
                else                                                // Idem voor het verlagen van transparantie
                {
                    currentPlayerIndicatorTransparency -= 20;
                    if(currentPlayerIndicatorTransparency < 0)
                    {
                        increaseTransparency = true;
                        currentPlayerIndicatorTransparency = 0;
                    }
                }
                pnScoreKeeper.Invalidate();                         // Opnieuw tekenen met huidige waarden
                Thread.Sleep(40);                                   // Slapen (ongeveer 25 FPS)
            }
            currentPlayerIndicatorTransparency = 0;                 // Niet meer animeren, transparantie volledig en opnieuw tekenen
            pnScoreKeeper.Invalidate();
        }
        private void drawScoreKeeper(object obj, PaintEventArgs paintEventArgs)
        {
            Graphics graphics = paintEventArgs.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;     // Grote cirkel, dus netjes tekenen

            Rectangle blackStoneRect = new Rectangle(20, 20, pnScoreKeeper.Height - 40, pnScoreKeeper.Height - 40);                         // Rectangle voor zwart
            Rectangle whiteStoneRect = new Rectangle(pnScoreKeeper.Height + 20, 20, pnScoreKeeper.Height - 40, pnScoreKeeper.Height - 40);  // Zelfde voor wit
            Brush brushStoneBlack = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.Black, Color.FromArgb(255, 70, 70, 70));             // Dezelfde brush als de steen zelf
            Brush brushStoneWhite = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.FromArgb(255, 220, 220, 220));
            graphics.FillEllipse(brushStoneBlack, blackStoneRect);  // Cirkel tekenen in het aangegeven rechthoek
            graphics.FillEllipse(brushStoneWhite, whiteStoneRect);

            Pen circlePen;
            switch (currentGame.currentPlayer)
            {
                case ReversiGame.PLAYER_1:
                    circlePen = new Pen(Color.FromArgb(currentPlayerIndicatorTransparency, 255, 255, 255), 5);  // Cirkel om de indicator-steen van deze speler tekenen
                    graphics.DrawEllipse(circlePen, blackStoneRect);
                    break;
                case ReversiGame.PLAYER_2:
                    circlePen = new Pen(Color.FromArgb(currentPlayerIndicatorTransparency, 0, 0, 0), 5);        // Zelfde voor P2
                    graphics.DrawEllipse(circlePen, whiteStoneRect);
                    break;
            }
                        
            if (currentGame.players[ReversiGame.PLAYER_1] != null)
            {
                Font font = new Font("Tahoma", 30);                     // Groot lettertype

                StringFormat stringFormat = new StringFormat();         // Stringformat, voor het centreren van de score
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Center;

                graphics.DrawString(currentGame.players[ReversiGame.PLAYER_1].stones.ToString(), font, Brushes.White, blackStoneRect, stringFormat);    // Score tekenen in vierkant
                graphics.DrawString(currentGame.players[ReversiGame.PLAYER_2].stones.ToString(), font, Brushes.Black, whiteStoneRect, stringFormat);
            }
        }

        private void drawStones()
        {
            for (int x = 0; x < currentGame.boardSize[0]; x++)
            {
                for (int y = 0; y < currentGame.boardSize[1]; y++)
                {
                    int stoneAtPos = currentGame.board[x, y];               // De steen op het speelbord
                    int drawnStoneAtPos = currentGame.drawnBoard[x, y];     // De steen op het getekende bord
                    int circleX = (int)(x * currentGame.gridSize) + 2;      // Positie van steen 
                    int circleY = (int)(y * currentGame.gridSize) + 2;

                    int gridSizeInt = (int)currentGame.gridSize;            // Gridsize omzetten naar int om veelvuldige cast te vermijden

                    if (drawnStoneAtPos != stoneAtPos)                      // Als de steen op het speelbord niet overeenkomt met met het getekende bord, tekenen
                    {
                        switch (stoneAtPos)
                        {
                            case ReversiGame.STONE_VALID:                               // Valid "steen"
                                if (currentGame.helpMode == ReversiGame.HELP_FULL || currentGame.helpMode == ReversiGame.HELP_MILD)
                                {
                                    lock (boardBitmapGraphics)
                                    {
                                        boardBitmapGraphics.FillEllipse(validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                                    }
                                }
                                else
                                {
                                    setEmptyField(x, y, circleX, circleY);
                                }
                                break;
                            case ReversiGame.STONE_BESTMOVE:
                                if (currentGame.helpMode == ReversiGame.HELP_FULL) // Help staat volledig aan, beste steen 
                                {
                                    lock (boardBitmapGraphics)                          // Lock op graphics, andere threads moeten wachten
                                    {
                                        boardBitmapGraphics.FillEllipse(bestMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                                    }
                                }
                                else if (currentGame.helpMode == ReversiGame.HELP_MILD) // Help staat uit, leeg vakje tekenen
                                {
                                    lock (boardBitmapGraphics)
                                    {
                                        boardBitmapGraphics.FillEllipse(validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                                    }
                                }
                                else
                                {
                                    setEmptyField(x, y, circleX, circleY);
                                }
                                break;
                            case ReversiGame.STONE_EMPTY:                               // Leeg veld gebruiken
                                setEmptyField(x, y, circleX, circleY);
                                break;
                            case ReversiGame.PLAYER_1:                                  // Steen van speler één laten tekenen
                                Thread drawStoneThread = new Thread(() => drawStone(x, y, circleX, circleY, gridSizeInt, stoneAtPos));
                                drawStoneThread.Start();
                                break;
                            case ReversiGame.PLAYER_2:                                  // Steen van speler twee
                                drawStoneThread = new Thread(() => drawStone(x, y, circleX, circleY, gridSizeInt, stoneAtPos));
                                drawStoneThread.Start();
                                break;
                        }
                        currentGame.drawnBoard[x, y] = stoneAtPos;
                    }
                }
            }
            pnBoard.Invalidate();       // Bitmap laten tekenen
        }

        private void drawStone(int x, int y, int circleX, int circleY, int gridSizeInt, int stoneAtPos)
        {
            int transparency = 0;      // Volledig transparant beginnen
            switch (stoneAtPos)
            {
                case ReversiGame.PLAYER_1:
                    while (transparency < 255)          // Op laten lopen tot niet transparant
                    {
                        Brush brushStoneBlack = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.FromArgb(transparency, 0, 0, 0), Color.FromArgb(transparency, 70, 70, 70));  // Brush op basis van transparantie
                        lock (boardBitmapGraphics)      // Locken
                        {
                            boardBitmapGraphics.FillEllipse(brushStoneBlack, circleX + (gridSizeInt / 12), circleY + (gridSizeInt / 12), gridSizeInt - (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 6));
                        }
                        pnBoard.Invalidate();           // Laten tekenen
                        transparency += 10;             // Transparantie ophogen
                        Thread.Sleep(30);               // Slapen
                    }
                    break;
                case ReversiGame.PLAYER_2:              // Zelfde voor speler twee
                    while (transparency < 255)
                    {
                        Brush brushStoneWhite = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.FromArgb(transparency, 255, 255, 255), Color.FromArgb(transparency, 220, 220, 220));
                        lock (boardBitmapGraphics)
                        {
                            boardBitmapGraphics.FillEllipse(brushStoneWhite, circleX + (gridSizeInt / 12), circleY + (gridSizeInt / 12), gridSizeInt - (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 6));
                        }
                        
                        pnBoard.Invalidate();
                        transparency += 10;
                        Thread.Sleep(30);
                    }
                    break;
            }
        }

        private void toggleHelpStones()             // Helpstenen aan/uit zetten, voor het grootste deel gelijk aan drawStones
        {
            for (int x = 0; x < currentGame.boardSize[0]; x++)
            {
                for (int y = 0; y < currentGame.boardSize[1]; y++)
                {
                    int stoneAtPos = currentGame.board[x, y];
                    int circleX = (int)(x * currentGame.gridSize) + 2;
                    int circleY = (int)(y * currentGame.gridSize) + 2;

                    int gridSizeInt = (int)currentGame.gridSize;

                    if (stoneAtPos == ReversiGame.STONE_VALID)
                    {
                        if (currentGame.helpMode == ReversiGame.HELP_FULL || currentGame.helpMode == ReversiGame.HELP_MILD)
                        {
                            lock (boardBitmapGraphics)
                            {
                                boardBitmapGraphics.FillEllipse(validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                            }
                        }
                        else
                        {
                            setEmptyField(x, y, circleX, circleY);
                        }
                    }
                    else if (stoneAtPos == ReversiGame.STONE_BESTMOVE)
                    {
                        if (currentGame.helpMode == ReversiGame.HELP_FULL) // Help staat volledig aan, beste steen 
                        {
                            lock (boardBitmapGraphics)                          // Lock op graphics, andere threads moeten wachten
                            {
                                boardBitmapGraphics.FillEllipse(bestMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                            }
                        }
                        else if (currentGame.helpMode == ReversiGame.HELP_MILD) // Help staat uit, leeg vakje tekenen
                        {
                            lock (boardBitmapGraphics)
                            {
                                boardBitmapGraphics.FillEllipse(validMoveBrush, circleX + (gridSizeInt / 6), circleY + (gridSizeInt / 6), gridSizeInt - (gridSizeInt / 3), gridSizeInt - (gridSizeInt / 3));
                            }
                        }
                        else
                        {
                            setEmptyField(x, y, circleX, circleY);
                        }
                    }
                    currentGame.drawnBoard[x, y] = stoneAtPos;
                }
            }
            pnBoard.Invalidate();
        }

        private void setEmptyField(int x, int y, int startX, int startY)
        {
            if (x % 2 == 0)                     // Geblokt patroon
            {
                if (y % 2 == 0)     
                {
                    lock (boardBitmapGraphics)  // Lock voor tekenen
                    {
                        boardBitmapGraphics.FillRectangle(brushGridGreenLight, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
                else
                {
                    lock (boardBitmapGraphics)
                    {
                        boardBitmapGraphics.FillRectangle(brushGridGreenDark, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
            }
            else
            {
                if (y % 2 != 0)
                {
                    lock (boardBitmapGraphics)
                    {
                        boardBitmapGraphics.FillRectangle(brushGridGreenLight, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
                else
                {
                    lock (boardBitmapGraphics)
                    {
                        boardBitmapGraphics.FillRectangle(brushGridGreenDark, startX, startY, (int)currentGame.gridSize, (int)currentGame.gridSize);
                    }
                }
            }
        }
    }
}
