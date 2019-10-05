using SplitSchmeisser.DAL.Entities;
using System;
using System.Xml.Serialization;

namespace SplitSchmeisser.BLL.Models
{
    [Serializable, XmlRoot(ElementName = "Operation")]
    public class OperationDTO
    {
        [XmlIgnore]
        public int Id { get; set; }
        public UserDTO Owner { get; set; }
        [XmlIgnore]
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
