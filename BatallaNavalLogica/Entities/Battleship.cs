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

            for(int i = 0; i < numShips; i++)
            {
                // Ship(x, y, size, orientation)
                Ship s = new Ship();
                bool x = true;
                while (x)
                {
                    s = new Ship(r.Next(0, board.cols), r.Next(0, board.rows), r.Next(2, 6), r.Next(2));
                    x = CheckCollision(s, board.GetShips());
                }
               board.addShip(s);
            }
        }

        public bool CheckCollision(Ship ship, List<Ship> ships)
        {
            foreach(Ship s in ships)
            {
                if(s.orientation == 1) // Vertical
                {

                }
            }

            return false;
        }
    }
}
