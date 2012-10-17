using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Reversi
{
    class ReversiGame
    {
        public const int MAX_BOARDSIZE = 20;    // Maximum grootte
        public const int MAX_PLAYERS = 2;       // Max aantal spelers

        public const int HELP_OFF = 0;          // Helpstanden
        public const int HELP_MILD = 1;
        public const int HELP_FULL = 2;

        public const int STONE_EMPTY = -3;      // Verschillende soorten "stenen"
        public const int STONE_BESTMOVE = -2;
        public const int STONE_VALID = -1;
        public const int PLAYER_1 = 1;
        public const int PLAYER_2 = 2;

        private List<Point> bestMoves = new List<Point>();
        private int newBestMoveStones = 0;
        private int bestMoveStones = 0;

        public ReversiPlayer[] players = new ReversiPlayer[MAX_PLAYERS + 1];    // Spelers beginnen op postitie 1
        public int currentPlayer;                                               // Huidige speler
        public int helpMode;                                                    // Huidige helpmodus
        public bool gameEnded;                                                  // True als het spel afgelopen is

        public double gridSize;                                                 // De grootte van één veldje
        public int[] boardSize;                                                 // Grootte van het bord x en y
        public int[,] board;                                                    // Array om het speelbord bij te houden
        public int[,] drawnBoard;                                               // Array voor het daadwerkelijk getekende bord

        public ReversiGame(int[] boardSize)
        {
            this.boardSize = boardSize;
            this.board = new int[boardSize[0], boardSize[1]];
            this.drawnBoard = new int[boardSize[0], boardSize[1]];
            this.gameEnded = false;
            if (boardSize[0] > boardSize[1])                                    // Gridsize berekenen op basis van de grootste
            {
                this.gridSize = 500 / (double)boardSize[0];
            }
            else
            {
                this.gridSize = 500 / (double)boardSize[1];
            }

            for (int x = 0; x < boardSize[0]; x++)                              // Bord vullen met empty
            {
                for (int y = 0; y < boardSize[1]; y++)
                {
                    this.board[x, y] = STONE_EMPTY;
                }
            }

            this.currentPlayer = PLAYER_1;                                      // Huidige speler is PLAYER_1
            this.players[PLAYER_1] = new ReversiPlayer();                       // Nieuwe spelers aanmaken
            this.players[PLAYER_2] = new ReversiPlayer();
        }

        public void setInitialStones()
        {
            int posXFirst = boardSize[0] / 2;                                   // Vier stenen zetten
            int posYFirst = boardSize[1] / 2;
            board[posXFirst, posYFirst] = PLAYER_1;
            board[posXFirst, posYFirst - 1] = PLAYER_2;
            board[posXFirst - 1, posYFirst] = PLAYER_2;
            board[posXFirst - 1, posYFirst - 1] = PLAYER_1;

            refreshValidMoves();                                                // Valide moves uitrekenen
        }

        public void processTurn(int[] clickPos) 
        {
            int stoneAt = board[clickPos[0], clickPos[1]];        // Steen op deze positie

            if (stoneAt == STONE_VALID || stoneAt == STONE_BESTMOVE)  // Als de positie een geldige zet is voor deze speler
            {
                board[clickPos[0], clickPos[1]] = currentPlayer;  // Huidige positie heeft nu de steen van de speler
                players[currentPlayer].stones++;                  // Aantal stenen ééntje ophogen

                finishTurn(clickPos[0], clickPos[1]);             // Beurt afmaken

                currentPlayer++;                                  // Volgende speler
                if (currentPlayer > MAX_PLAYERS)
                {
                    currentPlayer = PLAYER_1;
                }

                refreshValidMoves();                              // Valide moves uitrekenen voor nieuwe speler
                if (!players[currentPlayer].hasValidMoves)        // Als deze speler geen moves heeft
                {
                    currentPlayer++;
                    if (currentPlayer > MAX_PLAYERS)              // Volgende speler
                    {
                        currentPlayer = PLAYER_1;
                    }
                    refreshValidMoves();                          
                    if (!players[currentPlayer].hasValidMoves)    // Als deze speler ook geen zetten heeft
                    {
                        int victoriousPlayer = -1;                // Beginnen met geen winnaar
                        int numStones = players[PLAYER_1].stones;
                        for (int i = 2; i <= MAX_PLAYERS; i++)          // Check voor gelijkspel
                        {
                            if (numStones != players[i].stones)         // Als het aantal stenen niet overeen komt, geen gelijkspel
                            {
                                victoriousPlayer = PLAYER_1;
                            }
                        }
                        if (victoriousPlayer != -1)                     // Als geen gelijkspel, kijk wie gewonnen heeft
                        {
                            int mostStones = -1;
                            for (int i = 1; i <= MAX_PLAYERS; i++)
                            {
                                if (players[i].stones > mostStones)     // Deze speler heeft het "record", en is tot nu toe de winnaar
                                {
                                    mostStones = players[i].stones;
                                    victoriousPlayer = i;
                                }
                            }
                        }
                        
                        for(int x = 0; x < boardSize[0]; x++)           // Bord vullen
                        {
                            for (int y = 0; y < boardSize[1]; y++)
                            {
                                if (victoriousPlayer == -1)             // Als gelijkspel, om en om stenen voor alle spelers zetten
                                {
                                    for(int i = 1; i <= MAX_PLAYERS; i++)
                                    {
                                        if(x % i == 0)
                                        {
                                            board[x, y] = i;
                                        }
                                    }
                                }
                                else                                    // Geen gelijkspel, bord vullen met steen van winnaar
                                {
                                    board[x, y] = victoriousPlayer;
                                }
                            }
                        }
                        gameEnded = true;
                    }
                }
            }
        }

        private void finishTurn(int x, int y)   // Beurt afmaken                         
        {
            if (isLeftValid(x, y, false))       // Als deze richting valide is
            {
                isLeftValid(x, y, true);        // tekenen
            }
            if (isRightValid(x, y, false))
            {
                isRightValid(x, y, true);
            }
            if (isUpValid(x, y, false))
            {
                isUpValid(x, y, true);
            }
            if (isDownValid(x, y, false))
            {
                isDownValid(x, y, true);
            }
            if (isLeftUpValid(x, y, false))
            {
                isLeftUpValid(x, y, true);
            }
            if (isRightUpValid(x, y, false))
            {
                isRightUpValid(x, y, true);
            }
            if (isLeftDownValid(x, y, false))
            {
                isLeftDownValid(x, y, true);
            }
            if (isRightDownValid(x, y, false))
            {
                isRightDownValid(x, y, true);
            }
        }

        public void refreshValidMoves()                 // Valide moves uitrekenen voor huidige speler
        {
            bestMoveStones = 0;
            bestMoves.Clear();

            players[currentPlayer].hasValidMoves = false;
            for (int x = 0; x < boardSize[0]; x++)      // voor alle kolommen
            {
                for (int y = 0; y < boardSize[1]; y++)  // voor alle rijen
                {
                    if (board[x, y] > 0)                // als het een daadwerkelijke steen is
                    {
                        checkValidMovesAround(x, y);    // Mogelijke zetten rond deze steen berekenen
                    }
                }
            }

            foreach(Point point in bestMoves)
            {
                board[point.X, point.Y] = STONE_BESTMOVE;
            }
        }

        private void checkValidMovesAround(int x, int y)
        {
            for (int i = -1; i <= 1; i++)                   // x-offset
            {
                if (x + i < 0 || x + i >= boardSize[0])     // Als de huidige positie buiten het bord valt, deze berekening overslaan
                {
                    continue;
                }
                for (int n = -1; n <= 1; n++)               // y-offset
                {
                    if (y + n < 0 || y + n >= boardSize[1]) // Als de huidige positie buiten het bord valt, deze berekening overslaan
                    {
                        continue;
                    }
                    if (board[x + i, y + n] > 0)            // Deze positie is al bezet, doorgaan
                    {
                        continue;
                    }
                    else
                    {
                        if (isValidMove(x + i, y + n))      // Deze zet is voor de huidige speler een geldige zet
                        {
                            board[x + i, y + n] = STONE_VALID;
                            players[currentPlayer].hasValidMoves = true;
                        }
                        else                                // Zo niet, is de positie leeg (geen hint-steen, geblokte achtergrond)
                        {
                            board[x + i, y + n] = STONE_EMPTY;
                        }
                    }
                }
            }
        }

        private Boolean isValidMove(int x, int y)           // Uitrekenen of deze zet valide zou zijn
        {
           bool isValid = false;
           newBestMoveStones = 0;

           if (isLeftValid(x, y, false))                    // Als één richting valid is, is de move geldig
           {
               isValid = true;
           }
           if (isRightValid(x, y, false))
           {
               isValid = true;
           }
           if (isUpValid(x, y, false))
           {
               isValid = true;
           }
           if (isDownValid(x, y, false))
           {
               isValid = true;
           }
           if (isLeftUpValid(x, y, false))
           {
               isValid = true;
           }
           if (isRightUpValid(x, y, false))
           {
               isValid = true;
           }
           if (isLeftDownValid(x, y, false))
           {
               isValid = true;
           }
           if (isRightDownValid(x, y, false))
           {
               isValid = true;
           }

           if (isValid)
           {
               if (newBestMoveStones > bestMoveStones)
               {
                   bestMoveStones = newBestMoveStones;
                   bestMoves.Clear();
                   bestMoves.Add(new Point(x, y));
               }
               else if (newBestMoveStones == bestMoveStones)
               {
                   bestMoves.Add(new Point(x, y));
               }
           }

           return isValid;
        }

        private bool isLeftValid(int x, int y, bool setStone)
        {
            int iterations = 0;
            for (int i = x - 1; i >= 0; i--)                         // Naar links
            {
                int currentStone = board[i, y];

                if (i == x - 1 && currentStone == currentPlayer)    // Als de steen op de deze positie al van de speler is, geen geldige zet
                {
                    break;
                }
                else if (currentStone < 0)                          // Lijn onderbroken door lege positie, geen geldige zet
                {
                    break;
                }
                else if (currentStone == currentPlayer)             // Als de eerste steen niet van de speler was, maar daarna wel is het een geldige zet
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)                                  // Als setStone true is tussenliggende stenen al zetten
                {
                    setStoneAt(i, y, currentStone);
                }
                iterations++;
            }
            return false;
        }
        private bool isRightValid(int x, int y, bool setStone)
        {
            int iterations = 0;
            for (int i = x + 1; i < boardSize[0]; i++)              // Naar rechts
            {
                int currentStone = board[i, y];
                if (i == x + 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(i, y, currentStone);
                }
                iterations++;
            }
            return false;
        }
        private bool isUpValid(int x, int y, bool setStone)
        {
            int iterations = 0;
            for (int i = y - 1; i >= 0; i--)                         // Omhoog
            {
                int currentStone = board[x, i];
                if (i == y - 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x, i, currentStone);
                }
                iterations++;
            }
            return false;
        }
        private bool isDownValid(int x, int y, bool setStone)
        {
            int iterations = 0;
            for (int i = y + 1; i < boardSize[1]; i++)              // Naar beneden
            {
                int currentStone = board[x, i];
                if (i == y + 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x, i, currentStone);
                }
                iterations++;
            }
            return false;
        }
        private bool isLeftUpValid(int x, int y, bool setStone)         // Links omhoog
        {
            int iterations = 0;
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++) 
            {
                if (x - i < 0 || y - i < 0)                    
                {
                    break;
                }
                int currentStone = board[x - i, y - i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x - i, y - i, currentStone);
                }
                iterations++;
            }
            return false;
        }
        private bool isRightUpValid(int x, int y, bool setStone)        // Rechts omhoog
        {
            int iterations = 0;
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++) 
            {
                if (x + i >= boardSize[0] || y - i < 0)
                {
                    break;
                }
                int currentStone = board[x + i, y - i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x + i, y - i, currentStone);
                }
                iterations++;
            }
            return false;
        }
        private bool isLeftDownValid(int x, int y, bool setStone)       // Links naar beneden
        {
            int iterations = 0;
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++) 
            {
                if (x - i < 0 || y + i >= boardSize[1])
                {
                    break;
                }
                int currentStone = board[x - i, y + i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x - i, y + i, currentStone);
                }
                iterations++;
            }
            return false;
        }
        private bool isRightDownValid(int x, int y, bool setStone)      // Rechts naar beneden
        {
            int iterations = 0;
            for (int i = 1; i < boardSize[0] && i < boardSize[1]; i++)
            {
                if (x + i >= boardSize[0] || y + i >= boardSize[1])
                {
                    break;
                }
                int currentStone = board[x + i, y + i];
                if (i == 1 && currentStone == currentPlayer)
                {
                    break;
                }
                else if (currentStone < 0)
                {
                    break;
                }
                else if (currentStone == currentPlayer)
                {
                    newBestMoveStones += iterations;
                    return true;
                }
                else if (setStone)
                {
                    setStoneAt(x + i, y + i, currentStone);
                }
                iterations++;
            }
            return false;
        }

        private void setStoneAt(int x, int y, int stoneAt)          // Steen op deze positie wijzigen
        {
            board[x, y] = currentPlayer;
            players[currentPlayer].stones++;                        // Aantal stenen van deze speler ophogen
            if (stoneAt > 0)
            {
                players[stoneAt].stones--;                          // Aantal stenen van geslagen speler verlagen
            }
        }
    }
}
