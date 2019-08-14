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
        public string UserName { get; set; }

        public string UserPassword { get; set; }

        [NotMapped]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
