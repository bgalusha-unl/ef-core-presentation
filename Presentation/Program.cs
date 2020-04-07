using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation
{
    class Program
    {
        static int USERS = 3;
        static int POSTS_PER_USER = 3;

        static void Main( string[] args )
        {
            Database.CreateDatabase();
            Console.WriteLine( "Database available at {0}", Database.PATH );

            List<User> users = null;
            List<Post> posts = null;
            using ( var db = Database.GetContext() )
            {
                Filler.Populate( USERS, POSTS_PER_USER, db );
                users = db.Users.ToList();
                posts = db.Posts.ToList();
            }
            
            Console.WriteLine();
            foreach ( User u in users )
            {
                Console.WriteLine( u );
            }

            Console.WriteLine();
            foreach ( Post p in posts )
            {
                Console.WriteLine( p );
            }
        }
    }
}
