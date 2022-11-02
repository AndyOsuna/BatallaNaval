using BatallaNavalLogica.Entities;
using System;

namespace BatallaNavalLogica
{
    class Program
    {
        static void Main(string[] args)
        {
            int shipNum = 50, x = 50, y = 30;
            Battleship bs = new Battleship(x, y);

            bs.Setup(shipNum);
            bs.board.Show();
        }
    }
}
