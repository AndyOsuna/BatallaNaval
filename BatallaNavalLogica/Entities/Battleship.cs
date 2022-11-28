using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNaval.Entities
{
    class Battleship
    {
        public static Board boardPlayer { get; set; } // Jugador 1
        public static Board boardEnemy { get; set; } // Jugador 2 (PC)
        private static IA ia;

        public static void Setup(int x, int y)
        {
            boardPlayer = new Board(x, y);
            boardEnemy = new Board(x, y);
            ia = new IA();
            boardPlayer.addShip(2);
            boardPlayer.addShip(3);
            boardPlayer.addShip(3);
            boardPlayer.addShip(4);
            boardPlayer.addShip(5);

            boardEnemy.addShip(2);
            boardEnemy.addShip(3);
            boardEnemy.addShip(3);
            boardEnemy.addShip(4);
            boardEnemy.addShip(5);
        }
        
        public static void StartGame()
        {
            string[] ops = { "1. Jugador solo", "2. Contra PC", "3. Customizar juego" };
            int op = 1;
            //int op = utils.CreateMenu("modo de juego", ops);
            switch(op)
            {
                case 0:
                    PlayerAlone();
                    break;
                case 1:
                    TwoPlayers();
                    break;
                case 2:
                    Customize();
                    break;
            }
        }
        
        public static void PlayerAlone()
        {
            while (boardEnemy.CheckLivies())
            {
                Console.Clear();
                boardEnemy.ShowShoots();
                int x, y;
                Console.WriteLine("Ingrese coordenadas para disparar:");
                Console.Write("X: ");
                x = utils.ingresarIndice(boardEnemy.cols);
                Console.Write("Y: ");
                y = utils.ingresarIndice(boardEnemy.rows);
                boardEnemy.Shoot(x, y);
            }
            Console.WriteLine("Terminaste el juego");
        }

        public static void TwoPlayers()
        {
            int i;
            //Random r = new Random();
            for (i = 0; boardPlayer.CheckLivies() && boardEnemy.CheckLivies(); i++)
            {
                Console.Clear();
                /* Turnos pares: turno del Jugador 1 */
                if (i % 2 == 0)
                {
                    Console.WriteLine("Jugador 1");
                    boardPlayer.Show();
                    boardEnemy.ShowShoots();
                    Random r = new Random();
                    int x = r.Next(boardEnemy.cols);
                    int y = r.Next(boardEnemy.rows);
                    boardEnemy.Shoot(x, y); // se dispara sobre el tablero 2
                }
                /* Turnos impares: Jugador 2 (PC) */
                else
                {
                    //Console.WriteLine("Jugador 2");
                    ia.checkDestroyedShip();
                    int[] shoot = ia.Shoot(boardPlayer.boardShoots());

                    if(boardPlayer.Shoot(shoot[0], shoot[1]))
                        ia.addDestroyedPart(shoot[0], shoot[1]);

                }
            }
            if (i % 2 == 0)
                Console.WriteLine("Gano jugador 2");
            else
                Console.WriteLine("Gano jugador 1");
        }

        public static void Customize()
        {
            Console.WriteLine("Ingrese cantidad de columnas para el tablero:");
            int x = utils.ingresarInt();
            Console.WriteLine("Ingrese cantidad de filas para el tablero:");
            int y = utils.ingresarInt();

            boardPlayer = new Board(x, y);
            boardEnemy = new Board(x, y);

            int[] cantShips = new int[4];
            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine("Ingrese la cantidad de barcos de tamaño "+(i+2)+":");
                cantShips[i] = utils.ingresarInt();
            }
        }

        public static void ShowThisShips(List<Ship> ships)
        {
            Board tmpBoard = new Board(boardPlayer.cols, boardPlayer.rows);
            tmpBoard.ships = ships;
            tmpBoard.Show();

            Console.ReadLine();
        }
        public static void ShowThisShips(Ship ship)
        {
            Board tmpBoard = new Board(boardPlayer.cols, boardPlayer.rows);
            tmpBoard.ships.Add(ship);
            tmpBoard.Show();
            
            Console.ReadLine();
        }
    }
}
