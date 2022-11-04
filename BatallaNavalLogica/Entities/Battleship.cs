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
            // board2.Show();
            Random r = new Random();
            for(int i = 0; i < 1000; i++)
            {
                /* Turnos pares: turno del Jugador 1 */
                if (i % 2 == 0)
                {
                    board1.Show();
                    board2.Shoot();
                    board2.ShowShoots();
                }
                /* Turnos impares */
                else
                {
                    board1.FireIn(r.Next(board2.cols),r.Next(board2.rows));
                }
            }
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
