using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Battleship
    {
        public Board board { get; set; }

        public Battleship(int x, int y)
        {
            board = new Board(x, y);
        }

        public void Setup(int numShips)
        {
            Random r = new Random();

            for (int i = 0; i < numShips; i++)
            {
                Ship s = new Ship();
                bool x = true;
                while (x)
                {
                    // Ship(x, y, size, orientation)
                    s = new Ship(r.Next(0, board.cols), r.Next(0, board.rows), r.Next(2, 6), r.Next(2));
                    x = CheckCollision(s, board.GetShips());
                }
                Console.Write($"{i}: {s.x}-{s.y}. size: {s.size} ");
                if (s.orientation == 1) Console.WriteLine("Vertical");
                else Console.WriteLine("Horizontal");
                board.addShip(s);
            }
        }

        public bool CheckCollision(Ship ship, List<Ship> ships)
        {
            foreach (Ship s in ships)
            {
                // TA MAL
                for (int i = 0; i < s.size; i++)
                {
                    for (int j = 0; j < ship.size; j++)
                    {
                        if ((s.x + i) == (ship.x + j) && (s.y + i) == (ship.y + j))
                        {
                            Console.WriteLine("XD");
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
