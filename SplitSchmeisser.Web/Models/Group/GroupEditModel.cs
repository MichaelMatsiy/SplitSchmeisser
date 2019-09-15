using SplitSchmeisser.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.Web.Models
{
    public class GroupEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static GroupEditModel FromDTO(GroupDTO dto)
        {
            return new GroupEditModel
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
