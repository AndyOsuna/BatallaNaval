using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNaval.Entities
{
    class IA
    {
        public List<PartShip> destroyedPartShips;
        bool lastOrientacion;
        int lastSentido;

        public IA()
        {
            destroyedPartShips = new List<PartShip>();
        }

        public int[] Shoot(char[,] board)
        {
            Random r = new Random();
            int cols = board.GetLength(0);
            int rows = board.GetLength(1);
            int x = 0, y = 0;
            
            // Si aún no le ha pegado a ningun barco
            if (destroyedPartShips.Count == 0)
            {
                x = r.Next(cols);
                y = r.Next(rows);
                return new int[] { x, y };
            }

            // Si tiene al menos le pegó un disparo a un barco
            if (destroyedPartShips.Count > 0)
            {
                foreach (PartShip ps in destroyedPartShips)
                {
                    if (!ps.life)
                    {
                        lastOrientacion = true;
                        if (r.Next(2) == 0) lastOrientacion = false;

                        lastSentido = r.Next(2);
                        if (lastSentido == 0) lastSentido = -1;

                        if (lastOrientacion)
                        {
                            x = ps.x;
                            y = ps.y + lastSentido;
                        }
                        else
                        {
                            x = ps.x + lastSentido;
                            y = ps.y;
                        }
                        return new int[] { x, y };
                    }
                }
            }
            return new int[] { x, y };
        }
        public void addDestroyedPart(int x, int y)
        { 
            /* Cuando le pegué a un barco, se añade un PartShip donde se pegó el disparo. */
            destroyedPartShips.Add(new PartShip()
            {
                x = x,
                y = y,
                life = false
            }); 
        }
    }
}
