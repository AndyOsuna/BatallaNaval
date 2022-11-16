using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Battleship
    {
        public Board board1 { get; set; } // Jugador 1
        public Board board2 { get; set; } // Jugador 2 (PC)

        public Battleship(int x, int y)
        {
            board1 = new Board(x, y);
            board2 = new Board(x, y);
        }

        public void Setup(int numShips)
        {
            for (int i = 0; i < numShips; i++)
            {
                board1.addShip();
                board2.addShip();
            }
        }
        public void StartGame()
        {
            int i;
            //Random r = new Random();
            for (i = 0; board1.CheckLivies() && board2.CheckLivies(); i++)
            {
                Console.Clear();
                /* Turnos pares: turno del Jugador 1 */
                if (i % 2 == 0)
                {
                    Console.WriteLine("Jugador 1");
                    board2.ShowShoots();
                    board1.Show();
                    board2.Shoot(); // se dispara sobre el tablero 2
                }
                /* Turnos impares: Jugador 2 */
                else
                {
                    Console.WriteLine("Jugador 2");
                    board1.ShowShoots();
                    board2.Show();
                    board1.Shoot();
                }
            }
            if (i % 2 == 0)
                Console.WriteLine("Gano jugador 2");
            else
                Console.WriteLine("Gano jugador 1");
        }

        public void ShowThisShips(List<Ship> ships)
        {
            Board tmpBoard = new Board(board1.cols, board1.rows);
            tmpBoard.ships = ships;
            tmpBoard.Show();
            
            Console.ReadLine();
        }public void ShowThisShips(Ship ship)
        {
            Board tmpBoard = new Board(board1.cols, board1.rows);
            tmpBoard.ships.Add(ship);
            tmpBoard.Show();
            
            Console.ReadLine();
        }
    }
}
