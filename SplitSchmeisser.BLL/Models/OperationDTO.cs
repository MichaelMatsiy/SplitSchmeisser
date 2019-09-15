using SplitSchmeisser.DAL.Entities;
using System;

namespace SplitSchmeisser.BLL.Models
{
    public class OperationDTO
    {
        public int Id { get; set; }
        public UserDTO Owner { get; set; }
        public int GroupId { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public static OperationDTO FromEntity(Operation operation)
        {
            return new OperationDTO
            {
                Id = operation.Id,
                Owner = UserDTO.FromEntity(operation.Owner),
                GroupId = operation.GroupId,
                DateOfLoan = operation.DateOfLoan,
                Amount = operation.Amount,
                Description = operation.Description
            };
        }
    }
}
