using System;
using System.Collections.Generic;

namespace Presentation
{
    class Program
    {
        static void Main( string[] args )
        {
            Database.CreateDatabase();
            Console.WriteLine( "Database available at {0}", Database.PATH );
            Console.WriteLine();

            List<User> users = null;
            using ( var conn = Database.GetConnection(  ) )
            {
                conn.Open();
                Filler.Populate( 5, conn );
                users = Database.ReadAllUsers( conn );
                conn.Close();
            }

            foreach ( User u in users )
            {
                Console.WriteLine( u );
            }
        }
    }
}
