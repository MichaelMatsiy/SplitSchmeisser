using SplitSchmeisser.DAL.Entities.Base;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class Group : BaseEntity
    {
        public string GroupName { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
