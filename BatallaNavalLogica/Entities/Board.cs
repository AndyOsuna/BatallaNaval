using System;
using System.Collections.Generic;
using System.Text;
using TP2.Controller;

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

        public Board(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            ships = new List<Ship>();
            board = new char[cols, rows];
            SetBoard0();
        }
        private void SetBoard0()
        {
            for (int j = 0; j < rows; j++)
                for (int i = 0; i < cols; i++)
                    board[i, j] = ' ';
        }

        public bool CheckLivies()
        {
            /* Chequea barco por barco, si al menos uno tiene una vida. Cuando ninguno tenga vida, retorna False */
            foreach (Ship s in ships)
                foreach (PartShip l in s.parts)
                    if (l.life) return true;
            return false;
        }

        public void addShip()
        {
            Random r = new Random();
            Ship s;
            do
            {
                bool or = false;
                if (r.Next(2) == 1) or = true;

                s = new Ship(r.Next(cols), r.Next(rows), r.Next(2, 6), or);
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
                // TRUE = Orientacion Vertical
                if (s.orientation)
                {
                    int offset = 0;
                    if (s.y + s.size > rows) offset = s.y + s.size - rows;
                    s.y -= offset;
                }
                // FALSE = Orientacion Horizontal
                else
                {
                    int offset = 0;
                    if (s.x + s.size > cols) offset = s.x + s.size - cols;
                    s.x -= offset;
                }
                s.SetParts();
            }
            while (CheckSuperposition(s));
            /* Corta el bucle cuando ya no se superponen el nuevo barco con los que ya estaban */

            ships.Add(s); /* Se añade el barco a la lista */

            /* Se actualiza la matriz del tablero. */
            if (s.orientation)
            {
                for (int i = 0; i < s.size; i++)
                {
                    char c = 'O';
                    if (!s.parts[i].life) c = 'X';
                    board[s.x, s.y + i] = c;
                }
            }
            else
            {
                for (int i = 0; i < s.size; i++)
                {
                    char c = 'O';
                    if (!s.parts[i].life) c = 'X';
                    board[s.x + i, s.y] = c;
                }
            }
        }

        public bool CheckSuperposition(Ship ship2)
        {
            foreach (Ship ship1 in ships)
            {
                //List<Ship> xd = new List<Ship>();
                //xd.Add(s); xd.Add(ship);
                //ShowThisShips(xd);
                foreach (PartShip ps1 in ship1.parts)
                {
                    foreach (PartShip ps2 in ship2.parts)
                    {
                        if (ps1.x == ps2.x && ps1.y == ps2.y) return true;
                    }
                }
/*
                if (ship1.orientation)
                {
                    if (ship2.orientation)
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
                    if (ship2.orientation)
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
                }*/

            }
            return false;
        }

        public bool Shoot()
        {
            /*
             * Gestiona el disparo para el usuario
             */
            int x, y;
            Console.WriteLine("Ingrese coordenadas para disparar:");
            Console.Write("X: ");
            x = utils.ingresarIndice(cols);
            Console.Write("Y: ");
            y = utils.ingresarIndice(rows);

            /*
             * Recibe un disparo en (x,y).
             */
            switch (board[x, y])
            {
                // Le pega a un barco
                case 'O':
                    board[x, y] = 'X';
                    foreach (Ship s in ships)
                    {
                        if (s.Hit(x, y)) return true;
                    }
                    break;
                // Le pega al agua
                case ' ':
                    board[x, y] = 'a';
                    break;
                case 'X':
                    Console.WriteLine("Ya disparaste aca capo");
                    break;
            }
            return false;
        }

        public void Show()
        {
            /* Muestra el tablero en consola, con barcos en verde, disparos al agua en azul y disparos a barcos en rojo */
            char c = 'A';
            for(int y = 0; y < rows; y++)
            {
                Console.Write(c++);
                for (int x = 0; x < cols; x++)
                {
                    Console.Write("|");

                    if (board[x, y] == 'X') Console.ForegroundColor = ConsoleColor.Red;
                    else if (board[x, y] == 'a') Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(board[x, y]);
                    
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("|");
            }
            Console.Write("  ");
            for (int i = 0; i < cols; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }

        public void ShowShoots()
        {
            /* Muestra los disparos recibidos, ocultando los barcos. Utilizado para que el contrincante vea sus disparos. */
            char c = 'A';
            for(int y = 0; y < rows; y++)
            {
                Console.Write(c++);
                for (int x = 0; x < cols; x++)
                {
                    Console.Write("|");
                    if (board[x, y] == 'X') Console.ForegroundColor = ConsoleColor.Red;
                    else if (board[x, y] == 'a') Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    
                    if(board[x,y] == 'O') System.Console.Write(' ');
                    else Console.Write(board[x, y]);
                    
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("|");
            }
            Console.Write("  ");
            for (int i = 0; i < cols; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }
    }
}
