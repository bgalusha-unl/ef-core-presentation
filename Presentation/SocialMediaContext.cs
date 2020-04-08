using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    class SocialMediaContext : DbContext
    {
        public readonly string PATH = Path.Combine(
            Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData ),
            @"SeniorDesign\Presentation.sqlite"
        );

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlite( "Data Source=" + this.PATH + ";" );
        }
    }
}
