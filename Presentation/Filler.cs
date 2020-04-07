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

        public static void Populate( int users, SQLiteConnection conn )
        {
            for ( int i = 1; i <= users; i++ )
            {
                User randomUser = Filler.RandomUser();
                Database.InsertUser( randomUser, conn );
            }
        }
    }
}
