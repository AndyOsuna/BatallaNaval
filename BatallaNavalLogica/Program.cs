using BatallaNavalLogica.Entities;
using System;

namespace BatallaNavalLogica
{
    class Program
    {
        static void Main(string[] args)
        {
            int shipNum = 500, x = 100, y = 45;

            Battleship bs = new Battleship(x, y);

            bs.Setup(shipNum);
            bs.board.ShowShips();
        }
    }
}
