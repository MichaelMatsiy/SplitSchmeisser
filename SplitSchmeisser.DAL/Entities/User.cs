using SplitSchmeisser.DAL.Entities.Base;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Groups = new List<Group>();
            this.Operations = new List<Operation>();
        }
        public string Name { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
