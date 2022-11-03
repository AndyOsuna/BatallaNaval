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
                board.addShip();
            }
        }

        public void ShowThisShips(List<Ship> ships)
        {
            char[,] tmpBoard = new char[board.cols, board.rows];
            for (int i = 0; i < board.cols; i++) for (int j = 0; j < board.rows; j++) tmpBoard[i, j] = ' ';
            char indx = 'a';
            foreach(Ship s in ships)
            {
                for (int i = 0; i < s.size; i++)
                {
                    if(s.orientation==0) tmpBoard[s.x+i, s.y] = indx;
                    if(s.orientation==1) tmpBoard[s.x, s.y+i] = indx;
                }
                indx++;
            }
            Console.Clear();
            for(int j = 0; j < board.rows; j++)
            {
                for(int i = 0; i < board.cols; i++)
                {
                    Console.Write($"|{tmpBoard[i, j]}");
                }
                Console.WriteLine("|");
            }
            Console.ReadLine();
        }
    }
}
