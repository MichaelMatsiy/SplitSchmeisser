using SplitSchmeisser.DAL.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SplitSchmeisser.DAL.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Groups = new List<Group>();
        }
        public string Name { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
