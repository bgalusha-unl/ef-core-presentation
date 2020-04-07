using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace Presentation
{
    class Database
    {
        public static string PATH = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData ),
            @"SeniorDesign\Presentation.sqlite"
        );

        private static string USER_TABLE_SQL = @"CREATE TABLE IF NOT EXISTS User (
            UserId INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
            Email VARCHAR(255) NOT NULL,
            Password VARCHAR(255) NOT NULL)";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection( "Data Source=" + Database.PATH + ";" );
        }

        public static void CreateDatabase()
        {
            Directory.CreateDirectory( Path.GetDirectoryName( Database.PATH ) );
            SQLiteConnection.CreateFile( Database.PATH );
            using ( SQLiteConnection conn = GetConnection() )
            {
                conn.Open();
                SQLiteCommand userCmd = new SQLiteCommand( USER_TABLE_SQL, conn );
                userCmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void InsertUser( User u, SQLiteConnection conn )
        {
            string sql = "INSERT INTO User (Email, Password) VALUES (@Email, @Password)";
            using ( SQLiteCommand cmd = new SQLiteCommand( sql, conn ) )
            {
                cmd.Parameters.AddWithValue( "@Email", u.Email );
                cmd.Parameters.AddWithValue( "@Password", u.Password );
                cmd.ExecuteNonQuery();
            }
        }

        public static List<User> ReadAllUsers( SQLiteConnection conn )
        {
            List<User> users = new List<User>();
            string sql = "SELECT * FROM User";
            using ( SQLiteCommand cmd = new SQLiteCommand( sql, conn ) )
            {
                using ( SQLiteDataReader reader = cmd.ExecuteReader() )
                {
                    while ( reader.Read() )
                    {
                        users.Add( new User
                        {
                            UserId = Int32.Parse( reader[ "UserId" ].ToString() ),
                            Email = reader[ "Email" ].ToString(),
                            Password = reader[ "Password" ].ToString(),
                        } );
                    }
                }
            }
            return users;
        }
    }
}
