using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;

namespace SplitSchmeisser.Web.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public IDictionary<string, double> Debts { get; set; }

        public static GroupViewModel FromDTO(GroupDTO dto)
        {
            return new GroupViewModel
            {
                GroupName = dto.Name,
                Debts = dto.UserDebts
            };
        }

    }
}