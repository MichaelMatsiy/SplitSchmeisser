using SplitSchmeisser.BLL.Models;
using System.Xml.Serialization;
using System;

namespace SplitSchmeisser.Web.Models
{
    [Serializable, XmlRoot(ElementName = "OperationItem")]
    public class OperationViewModel
    {
        [XmlIgnore]
        public int Id { get; set; }
        public string OwnerName { get; set; }
        [XmlIgnore]
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

