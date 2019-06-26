using SplitSchmeisser.DAL.Entities.Base;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
