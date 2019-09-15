using SplitSchmeisser.BLL.Models;

namespace SplitSchmeisser.Web.Models
{
    public class OperationEditModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int GroupId { get; set; }

        public static OperationEditModel FromDTO(OperationDTO dto)
        {
            return new OperationEditModel
            {
                Id = dto.Id,
                Description = dto.Description,
                Amount = dto.Amount,
                GroupId = dto.GroupId
            };
        }
    }
}
