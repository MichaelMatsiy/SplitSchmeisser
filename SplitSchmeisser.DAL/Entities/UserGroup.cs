using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.DAL.Entities
{
    public class UserGroup
    {
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }

        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}
