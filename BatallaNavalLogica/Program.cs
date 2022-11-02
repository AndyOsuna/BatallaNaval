using BatallaNavalLogica.Entities;
using System;

namespace BatallaNavalLogica
{
    class Program
    {
        static void Main(string[] args)
        {
            int shipNum = 5, x = 10, y = 10;

            Battleship bs = new Battleship(x, y);

            bs.Setup(shipNum);
            bs.board.ShowShips();
        }
    }
}
