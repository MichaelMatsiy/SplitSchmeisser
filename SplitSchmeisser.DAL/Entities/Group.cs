using SplitSchmeisser.DAL.Entities.Base;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class Group : BaseEntity
    {
        public Group()
        {
            this.Users = new List<User>();
            this.Operations = new List<Operation>();
        }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
