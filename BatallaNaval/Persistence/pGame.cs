using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatallaNaval.Entities;

namespace BatallaNaval.Persistence
{
    class pGame
    {
       

        public static void SinglePlayerSave(Board board)
        {
            Conexion.OpenConexion();
            int boardId = BoardSave(board);
            string query = "INSERT INTO game (gamemodeId, playerId,boardEnemyId) VALUES (@gamemodeId, @playerId, @boardEnemyId)";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter("@gamemodeId", 1));
            cmd.Parameters.Add(new SQLiteParameter("@playerId", Program.IdentifiedUser));
            cmd.Parameters.Add(new SQLiteParameter("@boardEnemyId", boardId));
            cmd.ExecuteNonQuery();
        }
        public static void PlayerVsIASave(Board playerBoard, Board IABoard)
        {
            Conexion.OpenConexion();
            int playerBoardId = BoardSave(playerBoard);
            int IABoardId = BoardSave(IABoard);
            string query = "INSERT INTO game (gamemodeId, playerId, boardPlayerId, boardEnemyId) VALUES (@gamemodeId, @playerId, @boardPlayerId, @boardEnemyId)";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter("@gamemodeId", 2));
            cmd.Parameters.Add(new SQLiteParameter("@playerId", Program.IdentifiedUser));
            cmd.Parameters.Add(new SQLiteParameter("@boardEnemyId", IABoardId));
            cmd.Parameters.Add(new SQLiteParameter("@boardPlayerId", playerBoardId));
            cmd.ExecuteNonQuery();

        }
        public static int BoardSave(Board board)
        {
            int boardId = -1;
            //Insertar el tablero
            string query = "INSERT INTO board (cols, rows) VALUES (@cols, @rows)";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter("@cols", board.cols));
            cmd.Parameters.Add(new SQLiteParameter("@rows", board.rows));
            cmd.ExecuteNonQuery();
            //recuperar el id autogenerado por la BD 
            string query2 = "SELECT max(id) FROM board";
            SQLiteCommand cmd2 = new SQLiteCommand(query2, Conexion.Connection);
            SQLiteDataReader dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                boardId = dr.GetInt32(0);
            }

            //Insertar los ships 
            foreach (Ship s in board.ships)
            {
                string query3 = "INSERT INTO ship (boardId, x, y, orientation) VALUES (@boardid, @x, @y,@orientation)";
                //Cambiar booleano por entero 0 o 1 ya que SQLite no tiene bool 
                int orientation; if (s.orientation == true) orientation = 1; else orientation = 0;

                SQLiteCommand cmd3 = new SQLiteCommand(query3, Conexion.Connection);
                cmd3.Parameters.Add(new SQLiteParameter("@boardid", boardId));
                cmd3.Parameters.Add(new SQLiteParameter("@x", s.x));
                cmd3.Parameters.Add(new SQLiteParameter("@y", s.y));
                cmd3.Parameters.Add(new SQLiteParameter("@orientation", orientation));
                cmd3.ExecuteNonQuery();


                int shipId = -1;


                //Recupera id del barco insertado en la BD 
                string query4 = "SELECT max(id) FROM ship";
                SQLiteCommand cmd4 = new SQLiteCommand(query4, Conexion.Connection);
                SQLiteDataReader dr2 = cmd4.ExecuteReader();
                while (dr2.Read())
                {
                    shipId = dr2.GetInt32(0);
                }
                //Insertar los "Partships" de dicho barco 
                foreach (PartShip ps in s.parts)
                {
                    string query5 = "INSERT INTO partship (shipId, x, y, life) VALUES (@shipId, @x, @y,@life)";
                    //Cambiar booleano por entero 0 o 1 ya que SQLite no tiene bool 
                    int life; if (ps.life == true) life = 1; else life = 0;

                    SQLiteCommand cmd5 = new SQLiteCommand(query5, Conexion.Connection);
                    cmd5.Parameters.Add(new SQLiteParameter("@shipId", shipId));
                    cmd5.Parameters.Add(new SQLiteParameter("@x", ps.x));
                    cmd5.Parameters.Add(new SQLiteParameter("@y", ps.y));
                    cmd5.Parameters.Add(new SQLiteParameter("@life", life));
                    cmd5.ExecuteNonQuery();
                }


            }
            //Insertar los disparos fallidos 
            if (board.water_drops != null)
            {
                foreach (int[] w in board.water_drops)
                {
                    string query6 = "INSERT INTO water_drop (boardId, x,y) VALUES (@boardID,@x, @y)";
                    SQLiteCommand cmd6 = new SQLiteCommand(query6, Conexion.Connection);
                    cmd6.Parameters.Add(new SQLiteParameter("@boardID", boardId));
                    cmd6.Parameters.Add(new SQLiteParameter("@x", w[0]));
                    cmd6.Parameters.Add(new SQLiteParameter("@y", w[1]));
                    cmd6.ExecuteNonQuery();
                }
            }
            return boardId;
        }

        public static void SinglePlayerLoad(int gameId)
        {

            Conexion.OpenConexion();
            int enemyBoardId = -1;
            string query = "SELECT * FROM game WHERE id= @gameId";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter("@gameId", gameId));
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                enemyBoardId = dr.GetInt32(4);
            }


            Board b = LoadBoard(enemyBoardId);
            Battleship.boardEnemy = b; 
        }
        public static void PlayerVsIALoad(int gameId)
        {
            Conexion.OpenConexion();
            int enemyBoardId = -1;
            int allyBoardId = -1;
            string query = "SELECT * FROM game WHERE id= @gameId";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter("@gameId", gameId));
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                enemyBoardId = dr.GetInt32(4);
                allyBoardId = dr.GetInt32(3);
            }


            Board a = LoadBoard(allyBoardId);
            Board b = LoadBoard(enemyBoardId);
            Battleship.boardPlayer = a;
            Battleship.boardEnemy = b;
        }

        public static Board LoadBoard(int boardId)
        {
            
            List<Ship> ships = new List<Ship>();
            List<int> shipsId = new List<int>();
            string query = @"SELECT id from ship where boardId = @boardId";

            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.Connection);
            cmd.Parameters.Add(new SQLiteParameter("@boardId", boardId));
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                shipsId.Add( dr.GetInt32(0));
            }


            
            foreach(int i in shipsId)
            {
                List<PartShip> partShips = new List<PartShip>();
                string query2 = @"SELECT * FROM board b JOIN ship s
                           ON b.id= s.boardId
                           JOIN partship ps 
                           ON ps.shipId = s.id WHERE b.id=@boardId AND s.id= @shipId";
                SQLiteCommand cmd2 = new SQLiteCommand(query2, Conexion.Connection);
                cmd2.Parameters.Add(new SQLiteParameter("@boardId", boardId));
                cmd2.Parameters.Add(new SQLiteParameter("@shipId", i));
                SQLiteDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    

                    bool life;
                    if (dr2.GetInt32(12) == 1) life = true; else life = false;
                    partShips.Add(new PartShip(dr2.GetInt32(10),dr2.GetInt32(11), life ));
                }


                //***************

                string queryships = @"SELECT * FROM board b JOIN ship s
                           ON b.id= s.boardId
                            WHERE b.id=@boardId AND s.id= @shipId";
                SQLiteCommand commandShips = new SQLiteCommand(queryships, Conexion.Connection);
                commandShips.Parameters.Add(new SQLiteParameter("@boardId", boardId));
                commandShips.Parameters.Add(new SQLiteParameter("@shipId", i));
                SQLiteDataReader drships = commandShips.ExecuteReader();

                while (drships.Read())
                {
                    bool orientation;

                    if (drships.GetInt32(7) == 1) orientation = true; else orientation = false;
                    ships.Add(new Ship(drships.GetInt32(5), drships.GetInt32(6), orientation, partShips.ToArray()));

                }



                }

            //Recuperar tiros al agua 
            List<int[]> water_drops = new List<int[]>();
            string query3 = "SELECT x,y FROM water_drop where boardId = @boardId";
            SQLiteCommand cmd3 = new SQLiteCommand(query3, Conexion.Connection);
            cmd3.Parameters.Add(new SQLiteParameter("@boardId", boardId));
            SQLiteDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                water_drops.Add(new int[] { dr3.GetInt32(0), dr3.GetInt32(1)});
            }

            //***************

            string queryboart = @"SELECT *FROM board Where id= @boardId";
            Board b = new Board(0,0);
            SQLiteCommand cmdboard = new SQLiteCommand(queryboart, Conexion.Connection);
            cmdboard.Parameters.Add(new SQLiteParameter("@boardId", boardId));
            SQLiteDataReader drboard = cmdboard.ExecuteReader();
            while (drboard.Read())
            {
                b = new Board(drboard.GetInt32(1), drboard.GetInt32(2));
                
            }


            foreach (int[] w in water_drops)
            {
                b.board[w[0], w[1]] = 'a';
            }

            //Si trae el objeto bien de la bd pero no 
            foreach(Ship s in ships)
            {
                Console.WriteLine($"{s.x} - {s.y}");
                b.addShip(s);
            }
            return b;
        }
    }
}
