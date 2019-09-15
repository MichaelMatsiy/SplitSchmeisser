using System;

namespace SplitSchmeisser.Web.Models
{
    public class OperationCreateModel
    {
        public int OwnerID { get; set; }
        public int GroupID { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
