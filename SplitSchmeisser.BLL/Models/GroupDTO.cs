using SplitSchmeisser.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SplitSchmeisser.BLL.Models
{
    public class GroupDTO
    {
        public GroupDTO() {
            this.UserDebts = new Dictionary<string, double>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<UserDTO> Users { get; set; }

        public IDictionary<string, double> UserDebts { get; set; }

        public static GroupDTO FromEntity(Group group)
        {
            return new GroupDTO
            {
                Id = group.Id,
                Name = group.GroupName,
                Users = group.Users.Select(x => UserDTO.FromEntity(x)).ToList()
            };
        }
    }
}
