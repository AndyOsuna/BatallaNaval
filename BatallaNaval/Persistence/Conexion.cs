using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BatallaNaval.Persistence
{
    class Conexion
    {
        static String cadena = @"Data Source = BatallaNavalDB.db";
       public  static SQLiteConnection Connection = new SQLiteConnection(Conexion.cadena);
        public static void OpenConexion()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();
        }
    
        public static void CloseConexion()
        {
            Connection.Close();
        }


        public static SQLiteConnection Connections
        {
            set { Connection = value; }
            get { return Connection; }
        }


    }
}
