using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.Web.Models
{
    public class CreateOperationModel
    {
        public int OwnerID { get; set; }
        public int GroupID { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
