using SplitSchmeisser.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace SplitSchmeisser.DAL.Entities
{
    public class Operation : BaseEntity
    {
        public virtual User Owner { get; set; }
        public int OwnerId { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

    }
}
