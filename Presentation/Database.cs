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
            Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData ),
            @"SeniorDesign\Presentation.sqlite"
        );

        private static string USER_TABLE_SQL = @"CREATE TABLE IF NOT EXISTS User (
            UserId INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
            Email VARCHAR(255) NOT NULL,
            Password VARCHAR(255) NOT NULL)";
        private static string POST_TABLE_SQL = @"CREATE TABLE IF NOT EXISTS Post (
            PostId INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
            UserId INTEGER REFERENCES User (UserId) NOT NULL,
            Message VARCHAR(255) NOT NULL,
            Timestamp DATETIME NOT NULL,
            Likes INTEGER NOT NULL)";

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
                SQLiteCommand postCmd = new SQLiteCommand( POST_TABLE_SQL, conn );
                userCmd.ExecuteNonQuery();
                postCmd.ExecuteNonQuery();
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

        public static void InsertPost( Post p, SQLiteConnection conn )
        {
            string sql = @"INSERT INTO Post 
                (UserId, Message, Timestamp, Likes) VALUES
                (@UserId, @Message, @Timestamp, @Likes)";
            using ( SQLiteCommand cmd = new SQLiteCommand( sql, conn ) )
            {
                cmd.Parameters.AddWithValue( "@UserId", p.UserId );
                cmd.Parameters.AddWithValue( "@Message", p.Message );
                cmd.Parameters.AddWithValue( "@Timestamp", p.Timestamp );
                cmd.Parameters.AddWithValue( "@Likes", p.Likes );
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

        public static List<Post> ReadAllPosts( SQLiteConnection conn )
        {
            List<Post> posts = new List<Post>();
            string sql = "SELECT * FROM Post";
            using ( SQLiteCommand cmd = new SQLiteCommand( sql, conn ) )
            {
                using ( SQLiteDataReader reader = cmd.ExecuteReader() )
                {
                    while ( reader.Read() )
                    {
                        posts.Add( new Post
                        {
                            PostId = Int32.Parse( reader[ "PostId" ].ToString() ),
                            UserId = Int32.Parse( reader[ "UserId" ].ToString() ),
                            Message = reader[ "Message" ].ToString(),
                            Timestamp = DateTime.Parse( reader[ "Timestamp" ].ToString() ),
                            Likes = Int32.Parse( reader[ "Likes" ].ToString() ),
                        } );
                    }
                }
            }
            return posts;
        }
    }
}
