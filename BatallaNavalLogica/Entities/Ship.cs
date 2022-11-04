using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Ship
    {

        public int size { get; set; }
        /* Su tamaño o largo: entre 2 y 5 */
        public int x { get; set; }
        public int y { get; set; }
         /* Coordenadas dentro del tablero al que pertenezca */
        public int orientation { get; set; }
        /* 0: horizontal
         * 1: vertical */

        /* Por defecto, todos los valores en 0*/
        public Ship()
        {
            size = 0;
            x = 0;
            y = 0;
            orientation = 0;
        }

        public Ship(int x, int y, int size, int orientation)
        {
            this.size = size;
            this.x = x;
            this.y = y;
            this.orientation = orientation;
        }
    }
}
