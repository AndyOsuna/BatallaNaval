using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Board
    {
        public int cols { get; set; }
        public int rows { get; set; }
        private List<Ship> ships { get; set; }
        /* Lista de barcos que pertenecen al barco */
        public bool[,] board { get; set; }
        /* board guarda cada ubicación, guardando si cada celda está ocupada o no por un barco */
        public List<Ship> GetShips() => ships;

        public Board()
        {
            ships = new List<Ship>();
        }
        public Board(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            ships = new List<Ship>();
            board = new bool[cols, rows];
            set0();
        }
        public void set0()
        {
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    board[i, j] = false;
                }
            }
        }
        public void addShip(Ship s)
        {
            /* Para el caso en el que no quepa el barco en el tablero:
                * Cuando la coordenada está cerca de un borde del tablero, y por el tamaño sobresale de él.
                * Lo que hacemos es correrlo hacia adentro, según la orientacion. Para eso utilizamos la variable 'offset'.
                * Sabemos que sobresale del tablero cuando su coordenada (x ó y) mas el tamaño, 
                * son mayores al tamaño del tablero en dicha dimensión (x ó y).
                offset = barco.coordenada + barco.tamaño - tablero. */

            if (s.orientation == 1)
            {   // 1 = Orientacion Vertical

                int offset = 0;
                if (s.y + s.size > rows) offset = s.y + s.size - rows;

                for (int i = 0; i < s.size; i++)
                {
                    board[s.x, s.y + i - offset] = true;
                }
            }
            else
            {   // 0 = Orientacion Horizontal
                int offset = 0;
                if (s.x + s.size > cols) offset = s.x + s.size - cols;
                /* Igual que el caso vertical */
                for (int i = 0; i < s.size; i++)
                {
                    board[s.x + i - offset, s.y] = true;
                }
            }
            ships.Add(s);
        }
        public void DestroyShip()
        {

        }
        public void Show()
        {
            char c = 'A';
            for(int i = 0; i < rows; i++)
            {
                Console.Write(c++);
                for(int j = 0; j < cols; j++)
                {
                    if (board[j, i]) Console.Write("|X");
                    else Console.Write("| ");
                }
                Console.WriteLine("|");
            }
            Console.Write("  ");
            for (int i = 0; i < cols; i++)
            {
                Console.Write($"{i + 1} ");
            }
        }
    }
}
