using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SplitSchmeisser.Web.Models
{
    public class GroupListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> UserIDs { get; set; }

        public static GroupListModel FromDTO(GroupDTO dto)
        {
            return new GroupListModel
            {
                Id = dto.Id,
                Name = dto.Name,
                UserIDs = dto.Users.Select(x => x.Id).ToList()
            };
        }

    }
}
