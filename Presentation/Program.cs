using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation
{
    class Program
    {
        static int USERS = 1;
        static int POSTS_PER_USER = 5;

        static void Main( string[] args )
        {
            Database.CreateDatabase();
            Console.WriteLine( "Database available at {0}", Database.PATH );

            using ( var db = Database.GetContext() )
            {
                Filler.Populate( USERS, POSTS_PER_USER, db );

                User testUser = db.Users.First();
                List<Post> testUserPosts = db.Posts
                    .Where( p => p.UserId == testUser.UserId )
                    .ToList();
                
                Console.WriteLine( "\n{0}'s Posts:\n", testUser.Email );
                foreach ( Post post in testUserPosts )
                {
                    Console.WriteLine( post );
                }
            }
        }
    }
}
