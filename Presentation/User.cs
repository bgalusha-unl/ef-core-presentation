using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Presentation
{
    [Table( "User" )]
    class User
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public List<Post> Posts { get; set; }

        public override string ToString()
        {
            return String.Format( "{0}: {1}", UserId, Email );
        }
    }
}
