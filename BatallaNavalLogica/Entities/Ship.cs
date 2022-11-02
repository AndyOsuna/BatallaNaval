using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BatallaNavalLogica.Entities
{
    class Ship
    {
        public int size { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int orientation { get; set; }
        /* 0: horizontal
         * 1: vertical */

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
