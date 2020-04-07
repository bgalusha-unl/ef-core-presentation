using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation
{
    class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public override string ToString()
        {
            return String.Format( "{0}: {1}", UserId, Email );
        }
    }
}
