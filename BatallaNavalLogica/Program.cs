using BatallaNaval.Entities;
using System;

namespace BatallaNaval
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
         * esa zona donde pegó, hasta undir los barcos dados.
         */
        static void Main(string[] args)
        {
            //Tamaño del tablero
            int x = 10, y = 10;

            Battleship.Setup(x, y);

            Battleship.StartGame();
        }
    }
}
