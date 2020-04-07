using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    class Program
    {
        static int USERS = 1;
        static int POSTS_PER_USER = 0;

        static void Main( string[] args )
        {
            Database.CreateDatabase();
            Console.WriteLine( "Database available at {0}", Database.PATH );

            using ( var db = Database.GetContext() )
            {
                Filler.Populate( USERS, POSTS_PER_USER, db );

                User testUser = db.Users
                    .Include( u => u.Posts )
                    .First();

                Post newPost = new Post
                {
                    PostId = 0, // so EF Core can tell that this object is new
                    User = testUser,
                    Likes = 99,
                    Message = "Now is the time for all good men to come to the aid of their country."
                };

                testUser.Posts.Add( newPost );
                db.Users.Update( testUser );
                db.SaveChanges();

                Console.WriteLine( "\n{0}'s Posts:\n", testUser.Email );
                foreach ( Post post in db.Posts.Where( p => p.User == testUser ) )
                {
                    Console.WriteLine( post );
                }
            }
        }
    }
}
