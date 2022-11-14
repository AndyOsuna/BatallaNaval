using BatallaNavalLogica.Entities;
using System;

namespace BatallaNavalLogica
{
    class Program
    {
        /* 
         * Entidades: 
         *  Ship (barco)
         *  Board (tablero)
         *  Battleship (batalla naval)
         */
        /*
         * El programa debe crear dos tableros, para la persona y para la PC. Debe gestionar los turnos
         * Debe gestionar los disparos.
         * La inteligencia disparará aleatoriamente. Cuando le pegue a un barco, deberá disparar por 
         * esa zona donde pegó, hasta undir el o los barcos dados.
         */
        static void Main(string[] args)
        {
            //Cantidad de barcos
            int shipNum = 8;
            //Tamaño del tablero
            int x = 10, y = 10;
            
            Battleship bs = new Battleship(x, y);
            bs.Setup(shipNum);

            bs.StartGame();
        }
    }
}
