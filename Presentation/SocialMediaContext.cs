using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    class SocialMediaContext : DbContext
    {
        private readonly string SQLitePath = null;
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public SocialMediaContext( string sqlitePath )
        {
            this.SQLitePath = sqlitePath;
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            if ( !optionsBuilder.IsConfigured )
            {
                optionsBuilder.UseSqlite( "Data Source=" + this.SQLitePath + ";" );
            }
        }
    }
}
