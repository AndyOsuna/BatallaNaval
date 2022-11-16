using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatallaNaval
{
    static class Program
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
         * 
         * 
         * 
         * AL INICIO DEL PROGRAMA ELEGIR TAMAÑOS Y CANTIDAD DE BARCOS POR EQUIPO
         * 
         */


        /*  -----CODIGO DEL START GAME DEL PROGRAM.CS ANTERIOR 
         *  
         *   //Cantidad de barcos
            int shipNum = 30;
            //Tamaño del tablero
            int x = 10, y = 10;
            
            Battleship bs = new Battleship(x, y);
            bs.Setup(shipNum);
            
            bs.StartGame();
         *  
         *  
         */
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
