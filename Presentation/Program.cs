using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation
{
    class Program
    {
        static int USERS = 30;
        static int POSTS_PER_USER = 30;

        static void Main( string[] args )
        {
            Database.CreateDatabase();
            Console.WriteLine( "Database available at {0}", Database.PATH );

            using ( var db = Database.GetContext() )
            {
                Filler.Populate( USERS, POSTS_PER_USER, db );

                // get 10 (or less) users that have a @yahoo.com email address
                var yahooUsers = db.Users
                    .Where( u => u.Email.EndsWith( "@yahoo.com" ) )
                    .Take( 10 );
                Console.WriteLine( "\nYahoo Users:" );
                foreach ( User user in yahooUsers )
                {
                    Console.WriteLine( user );
                }

                // find a sad post that only has 0 or 1 likes; prefer posts with 0 likes
                var sadPost = db.Posts
                    .Where( p => p.Likes < 2 )
                    .OrderBy( p => p.Likes )
                    .First();
                Console.WriteLine( "\nSad Post:" );
                Console.WriteLine( sadPost );
            }
        }
    }
}
