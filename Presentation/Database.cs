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

        public static SocialMediaContext GetContext()
        {
            return new SocialMediaContext( Database.PATH );
        }

        public static void CreateDatabase()
        {
            Directory.CreateDirectory( Path.GetDirectoryName( Database.PATH ) );
            SQLiteConnection.CreateFile( Database.PATH );
            using ( var conn = new SQLiteConnection( "Data Source=" + Database.PATH + ";" ) )
            {
                conn.Open();
                SQLiteCommand userCmd = new SQLiteCommand( USER_TABLE_SQL, conn );
                SQLiteCommand postCmd = new SQLiteCommand( POST_TABLE_SQL, conn );
                userCmd.ExecuteNonQuery();
                postCmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
