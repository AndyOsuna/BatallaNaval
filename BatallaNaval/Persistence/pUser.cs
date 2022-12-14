using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace BatallaNaval.Persistence
{
    class pUser
    {
        public static bool authLogin(String User, String Password)
        {
            bool auth = false;
            String password = EncoderMD5.Encode (Password);
            String user = User;
            string dbUser = "";
            string dbPassword = "";

            Conexion.OpenConexion();
            //query para verificar usuario y contraseña
            String query = @"SELECT username, password FROM  user WHERE username = @username AND password = @password";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter ("@username", user));
            cmd.Parameters.Add(new SQLiteParameter ("@password", password));
            SQLiteDataReader dr = cmd.ExecuteReader();                       //tobi estuvo aqui
            while (dr.Read())
            {
                dbUser = dr.GetString(0);
                dbPassword = dr.GetString(1);

            }
            if (dbUser == user && dbPassword == password)
            {
                auth = true;
            }

            return auth;
        }
        public static bool regUser(String name, String lastName, String username, String password, String Email)
        {
            bool result = true;
            string Password = EncoderMD5.Encode(password);
            //query para  registrar nuevo usuario
            Conexion.OpenConexion();
            string query = "INSERT INTO user (name, lastname, username, email, password) VALUES (@name, @lastname,@username,@email,@password)";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter("@name", name));
            cmd.Parameters.Add(new SQLiteParameter("@lastname", lastName));
            cmd.Parameters.Add(new SQLiteParameter("@username", username));
            cmd.Parameters.Add(new SQLiteParameter("@password", Password));
            cmd.Parameters.Add(new SQLiteParameter("@email", Email));
            if (cmd.ExecuteNonQuery()<1)
            {
                result = false;
            }
            return result;
        }
        public static bool verifyUsername(String username)
        {
            bool exist = false;

            //query para verificar existencia 
            return exist;
        }
    }
}
