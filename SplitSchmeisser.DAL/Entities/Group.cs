using SplitSchmeisser.DAL.Entities.Base;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class Group : BaseEntity
    {
        public Group()
        {
            this.UserGroups = new List<UserGroup>();
            this.Operations = new List<Operation>();
        }
        public string GroupName { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
