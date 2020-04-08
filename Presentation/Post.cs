using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Presentation
{
    [Table( "Post" )]
    class Post
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int PostId { get; set; }
        public User User { get; set; }
        [Required]
        public string Message { get; set; }
        public string ImageURL { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public int Likes { get; set; }


        public override string ToString()
        {
            return String.Format( "{0}: (User {1}, {2}) {3} Likes\n\t'{4}'", PostId, User.UserId, Timestamp, Likes, Message );
        }
    }
}
