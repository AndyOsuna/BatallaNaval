﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BatallaNaval.Entities
{
    class IA
    {
        /* Esta lista será para cada barco. Cuando se concluya con un barco, se reseteará */
        public List<PartShip> destroyedPartShips;
        public bool shootPreviously; // Si en el anterior turno le pegó a un barco
        public bool orientChanged;
        public bool sentChanged;
        public bool orientFinded;
        public bool lastOrientation; 
        public int lastSentido;

        public IA()
        {
            reset();
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
                do
                {
                    x = r.Next(cols);
                    y = r.Next(rows);
                } 
                while (board[x, y] == 'a' || board[x, y] == 'X');
                /* Este bucle se asegura de no disparar varias veces en el mismo lugar */

                return new int[] { x, y };
            }

            // Si tiene al menos le pegó un disparo a un barco
            if (destroyedPartShips.Count > 0)
            {
                if (destroyedPartShips.Count > 1) orientFinded = true;

                var ps = destroyedPartShips[destroyedPartShips.Count-1];

                if (!ps.life)
                {
                    if (!orientFinded && !shootPreviously)
                    {
                        switchDirection();
                    }
                    else
                    {
                        if (!shootPreviously)
                        {
                            /* Volver al primer disparo y continuar por el otro lado */
                            PartShip p = destroyedPartShips[0];
                            destroyedPartShips = new List<PartShip>();
                            destroyedPartShips.Add(p);
                            ps = p;
                            switchDirection();
                        }
                    }

                    if (lastOrientation)
                    {
                        x = ps.x;
                        if (ps.y + lastSentido < 0 || ps.y + lastSentido > (rows - 1)) switchDirection();
                        y = ps.y + lastSentido;
                    }
                    else
                    {
                        if (ps.x + lastSentido < 0 || ps.x + lastSentido > (cols - 1)) switchDirection();
                        x = ps.x + lastSentido;
                        y = ps.y;
                    }

                    shootPreviously = false;
                    return new int[] { x, y };
                }
            }
            return new int[] { x, y };
        }
        public void switchDirection()
        {
            if (!sentChanged)
            {
                if (lastSentido == -1) lastSentido = 1;
                else lastSentido = -1;
                sentChanged = true;
            }
            else
            {
                if (!orientChanged && !orientFinded)
                {
                    if (lastOrientation) lastOrientation = false;
                    else lastOrientation = true;
                    orientChanged = true;
                    sentChanged = false;
                }
            }
        }
        public void checkDestroyedShip()
        {
            if (!shootPreviously && orientFinded) destroyedPartShips = new List<PartShip>();
        }
        public void reset()
        {
            destroyedPartShips = new List<PartShip>();

            shootPreviously = false;
            orientChanged = false;
            sentChanged = false;
            orientFinded = false;

            lastOrientation = false;
            lastSentido = -1;
        }
        public void addDestroyedPart(int x, int y)
        { 
            /* Cuando le pegue a un barco, se añade un PartShip donde cayó el disparo. */
            destroyedPartShips.Add(new PartShip()
            {
                x = x,
                y = y,
                life = false
            });
            shootPreviously = true;
        }
    }
}
