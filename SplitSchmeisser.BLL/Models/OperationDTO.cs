using SplitSchmeisser.DAL.Entities;
using System;

namespace SplitSchmeisser.BLL.Models
{
    public class OperationDTO
    {
        public int Id { get; set; }
        public UserDTO Owner { get; set; }
        public GroupDTO Group { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public static OperationDTO FromEntity(Operation operation)
        {
            return new OperationDTO
            {
                Id = operation.Id,
                Owner = UserDTO.FromEntity(operation.Owner),
                Group = GroupDTO.FromEntity(operation.Group),
                DateOfLoan = operation.DateOfLoan,
                Amount = operation.Amount,
                Description = operation.Description
            };
        }
    }
}
