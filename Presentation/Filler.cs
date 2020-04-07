using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Presentation
{
    class Filler
    {
        public static User RandomUser()
        {
            Bogus.Faker faker = new Bogus.Faker( "en" );
            return new User
            {
                UserId = 0,
                Email = faker.Internet.Email(),
                Password = faker.Internet.Password(),
            };
        }

        public static Post RandomPost( User u )
        {
            Bogus.Faker faker = new Bogus.Faker( "en" );
            return new Post
            {
                PostId = 0,
                UserId = u.UserId,
                Message = faker.Hacker.Phrase(), 
                Timestamp = faker.Date.Soon(),
                Likes = faker.Random.Number( 50 ),
            };
        }

        public static void Populate( int users, int userPosts, SocialMediaContext db )
        {
            for ( int i = 1; i <= users; i++ )
            {
                User randomUser = Filler.RandomUser();
                db.Users.Add( randomUser );
            }
            // need to save changes here to commit UserIds
            db.SaveChanges();
            
            foreach ( User u in db.Users )
            {
                for ( int i = 0; i < userPosts; i++ )
                {
                    Post randomPost = Filler.RandomPost( u );
                    db.Posts.Add( randomPost );
                }
            }
            // save again to commit PostIds
            db.SaveChanges();
        }
    }
}
