using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    class Program
    {
        static int USERS = 10;
        static int POSTS_PER_USER = 1;

        static void Main( string[] args )
        {
            using ( var db = new SocialMediaContext() )
            {
                Filler.Clear( db );     // clear out the database before populating
                Filler.Populate( USERS, POSTS_PER_USER, db );
                Console.WriteLine( "Database available at {0}", db.PATH );
                Console.WriteLine();

                var posts = db.Posts.ToList();
                foreach ( Post post in posts )
                {
                    Console.WriteLine(
                        "{0}: {1} likes, {2}\n\t'{3}'",
                        post.User.Name, 
                        post.Likes,
                        post.ImageURL,
                        post.Message
                    );
                }
            }
        }
    }
}
