using SplitSchmeisser.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class Operation : BaseEntity
    {
        public User Owner { get; set; }
        public Group Group { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Members { get; set; }
    }
}
