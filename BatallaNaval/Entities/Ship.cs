using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNaval.Entities
{
    class Ship
    {
        public PartShip[] parts { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        /* Coordenadas dentro del tablero al que pertenezca */
        public int size { get; set; }
        /* Su tamaño o largo: entre 2 y 5 */
        public bool orientation { get; set; }
        /* true: vertical 
           false: horizontal */

        public Ship(int x, int y, int size, bool orientation)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.orientation = orientation;
            SetParts();
        }
        public Ship(int x, int y, bool orientation, PartShip[] parts)
        {
            this.x = x;
            this.y = y;
            this.size = parts.Length;
            this.orientation = orientation;
            this.parts = parts;

        }
        public void SetParts()
        {
            parts = new PartShip[size];
            for (int i = 0; i < size; i++)
            {
                parts[i] = new PartShip();
                parts[i].life = true;
            }

            for (int i = 0; i < size; i++)
                if (orientation)    // Vertical
                {
                    parts[i].x = x;
                    parts[i].y = y + i;
                }
                else                // Horizontal
                {
                    parts[i].x = x + i;
                    parts[i].y = y;
                }
        }
        public bool Hit(int x, int y)
        {
            /* Actualiza el arrays de vidas */
            foreach (PartShip l in parts)
                if (l.x == x && l.y == y)
                    l.life = false;

            /* La funcion devuelve 'true' cuando el barco es hundido*/
            foreach (PartShip l in parts)
                if (l.life) return false;
            return true;
        }
    }
}
