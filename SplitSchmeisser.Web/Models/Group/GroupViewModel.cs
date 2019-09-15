using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SplitSchmeisser.Web.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<OperationViewModel> Operations { get; set; }
        public IDictionary<string, double> Debts { get; set; }

        public static GroupViewModel FromDTO(GroupDTO dto)
        {
            return new GroupViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Debts = dto.UserDebts,
                Operations = dto.Operations.Select(x => OperationViewModel.FromDTO(x)).ToList()
            };
        }

    }
}