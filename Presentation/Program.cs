using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    class Program
    {
        static int USERS = 10;
        static int POSTS_PER_USER = 3;

        static void Main( string[] args )
        {
            Database.CreateDatabase();
            Console.WriteLine( "Database available at {0}", Database.PATH );

            using ( var db = Database.GetContext() )
            {
                Filler.Populate( USERS, POSTS_PER_USER, db );

                var posts = db.Posts.ToList();
                foreach ( Post post in posts )
                {
                    Console.WriteLine( "\n{0} Wrote: '{1}'", post.User.Name, post.Message );
                }
            }
        }
    }
}
