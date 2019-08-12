using SplitSchmeisser.DAL.Entities.Base;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.UserGroups = new List<UserGroup>();
        }
        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
