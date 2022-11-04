using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Board
    {

        /* Tamaño del tablero */
        public int cols { get; set; }
        public int rows { get; set; }
        /* Lista de barcos que pertenecen al barco */
        public List<Ship> ships { get; set; }
        /* board guarda cada ubicación, guardando si cada celda está ocupada o no por un barco */
        public char[,] board { get; set; }

        public Board()
        {
            ships = new List<Ship>();
            board = new char[cols, rows];
            SetBoard0();
        }
        public Board(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            ships = new List<Ship>();
            board = new char[cols, rows];
            SetBoard0();
        }
        public void SetBoard0()
        {
            for (int j = 0; j < rows; j++)
                for (int i = 0; i < cols; i++)
                    board[i, j] = ' ';
        }
        public void addShip()
        {
            Random r = new Random();
            Ship s = new Ship();
            do
            {
                s = new Ship(r.Next(cols), r.Next(rows), r.Next(2, 6), r.Next(2));

                //Console.Write($"{i}: {s.x}-{s.y}. size: {s.size} ");
                //if (s.orientation == 1) Console.WriteLine("Vertical");
                //else Console.WriteLine("Horizontal");

                /* Correccion de coordenada del barco, cuando sobresale del tablero por el borde inferior y derecho.
                 * Para el caso en el que no quepa el barco en el tablero:
                    * Cuando la coordenada está cerca de un borde del tablero, y por el tamaño sobresale de él.
                    * Lo que hacemos es correrlo hacia adentro, según la orientacion. Para eso utilizamos la variable 'offset'.
                    * Sabemos que sobresale del tablero cuando su coordenada (x ó y) mas el tamaño, 
                    * son mayores al tamaño del tablero en dicha dimensión (x ó y).
                    offset = barco.coordenada + barco.tamaño - tablero. 
                */
                // 1 = Orientacion Vertical
                if (s.orientation == 1)
                {   
                    int offset = 0;
                    if (s.y + s.size > rows) offset = s.y + s.size - rows;
                    s.y -= offset;
                }
                // 0 = Orientacion Horizontal
                else
                {   
                    int offset = 0;
                    if (s.x + s.size > cols) offset = s.x + s.size - cols;
                    s.x -= offset;
                }
            }
            while (CheckSuperposition(s, ships));
            /* Corta el bucle cuando ya no se superponen el nuevo barco con los que ya estaban */
            ships.Add(s); /* Se añade el barco a la lista */

            /* Se actualiza la matriz del tablero. */
            char c = 'O';
            for (int i = 0; i < s.size; i++)
            {
                if (s.orientation == 1) board[s.x, s.y + i] = c;
                else board[s.x + i, s.y] = c;
            }
            c++;
        }
        public void FireIn(int x, int y)
        {
            /*
             * Recibe un disparo en (x,y).
             */
            if (board[x, y] != ' ')
            {
                Console.WriteLine("Fuego");
            }
            else
            {
                Console.WriteLine("Agua");
            }
        }
        public bool CheckSuperposition(Ship ship2, List<Ship> ships)
        {
            foreach (Ship ship1 in ships)
            {
                //List<Ship> xd = new List<Ship>();
                //xd.Add(s); xd.Add(ship);
                //ShowThisShips(xd);

                if (ship1.orientation == 1)
                {
                    if (ship2.orientation == 1)
                    {
                        if (ship1.x == ship2.x &&
                            ship1.y + ship1.size >= ship2.y &&
                            ship1.y <= ship2.y + ship2.size)
                            return true;
                    }
                    else
                    {
                        if (ship1.x >= ship2.x &&
                            ship1.x <= ship2.x + ship2.size &&
                            ship2.y >= ship1.y &&
                            ship2.y <= ship1.y + ship1.size)
                            return true;
                    }
                }
                else
                {
                    if (ship2.orientation == 1)
                    {
                        if (ship2.x >= ship1.x &&
                            ship2.x <= ship1.x + ship1.size &&
                            ship1.y >= ship2.y &&
                            ship1.y <= ship2.y + ship2.size)
                            return true;
                    }
                    else
                    {
                        if (ship1.y == ship2.y &&
                            ship1.x + ship1.size >= ship2.x &&
                            ship1.x <= ship2.x + ship2.size)
                            return true;
                    }
                }
            }
            return false;
        }

        public void Show()
        {
            char c = 'A';
            for(int i = 0; i < rows; i++)
            {
                Console.Write(c++);
                for(int j = 0; j < cols; j++)
                {
                    Console.Write("|" + board[j, i]);
                }
                Console.WriteLine("|");
            }
            Console.Write("  ");
            for (int i = 0; i < cols; i++)
            {
                Console.Write($"{i + 1} ");
            }
            Console.WriteLine();
        }
    }
}
