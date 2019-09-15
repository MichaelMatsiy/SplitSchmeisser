using SplitSchmeisser.BLL.Models;
using System;

namespace SplitSchmeisser.Web.Models
{
    public class OperationViewModel
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public int GroupId { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public static OperationViewModel FromDTO(OperationDTO operation)
        {
            return new OperationViewModel
            {
                Id = operation.Id,
                OwnerName = operation.Owner.Name,
                GroupId = operation.GroupId,
                DateOfLoan = operation.DateOfLoan,
                Amount = operation.Amount,
                Description = operation.Description
            };
        }
    }
}

