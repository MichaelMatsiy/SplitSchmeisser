using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.DAL.Entities
{
    public class UserGroup
    {
        public User User { get; set; }
        public Group Group { get; set; }

        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}
