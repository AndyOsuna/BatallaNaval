using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Battleship
    {
        public Board board1 { get; set; }
        public Board board2 { get; set; }

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
            for(int i = 0; i < 100; i++)
            {
                /* Turnos pares */
                if (i % 2 == 0)
                {
                    
                }
                /* Turnos impares */
                else
                {

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
