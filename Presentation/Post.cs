using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation
{
    class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public int Likes { get; set; }

        public override string ToString()
        {
            return String.Format( "{0}: (User {1}, {2}) {3} Likes\n\t'{4}'", PostId, UserId, Timestamp, Likes, Message );
        }
    }
}
