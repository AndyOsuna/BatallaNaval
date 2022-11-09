using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Ship
    {
        public PartShip[] life { get; set; }
        public int x { get; set; }
        public int y { get; set; }
         /* Coordenadas dentro del tablero al que pertenezca */
        public int size { get; set; }
        /* Su tamaño o largo: entre 2 y 5 */
        public bool orientation { get; set; }
        /* true: vertical 
           false: horizontal*/

        /* Por defecto, todos los valores en 0*/
        public Ship()
        {
            size = 0;
            x = 0;
            y = 0;
            orientation = false;
        }

        public Ship(int x, int y, int size, bool orientation)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.orientation = orientation;
            Set();
        }
        public void Set()
        {
            life = new PartShip[size];
            for (int i = 0; i < size; i++)
            {
                life[i] = new PartShip();
                life[i].life = true;
            }

            if (orientation)    // Vertical
                for (int i = 0; i < size; i++)
                {
                    life[i].x = x;
                    life[i].y = y + i;
                }
            else                // Horizontal
                for(int i = 0; i < size; i++)
                {
                    life[i].x = x + i;
                    life[i].y = y;
                }
        }
        public bool Hit(int x, int y)
        {
            /* Actualiza el arrays de vidas */
            foreach (PartShip l in life)
                if (l.x == x && l.y == y)
                    l.life = false;

            /* La funcion devuelve 'true' cuando el barco es hundido*/
            foreach (PartShip l in life)
                if (l.life) return false;
            return true;
        }
    }
}
