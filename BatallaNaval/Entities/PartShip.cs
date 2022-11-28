using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNaval.Entities
{
    class PartShip
    {
        public PartShip(int x, int y, bool life)
        {
            this.x = x;
            this.y = y;
            this.life = life;
        }
        public PartShip() { }

        public int x { get; set; }
        public int y { get; set; }
        public bool life { get; set; }
    }
}
