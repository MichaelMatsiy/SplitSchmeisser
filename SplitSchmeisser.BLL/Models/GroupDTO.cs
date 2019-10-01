using SplitSchmeisser.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using SplitSchmeisser.BLL.CommonLogic;

namespace SplitSchmeisser.BLL.Models
{
    public class GroupDTO
    {
        public GroupDTO() {
            this.UserDebts = new List<Debt>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserDTO> Users { get; set; }

        public List<OperationDTO> Operations { get; set; }

        public List<Debt> UserDebts { get; set; }

        public static GroupDTO FromEntity(Group group)
        {
            return new GroupDTO
            {
                Id = group.Id,
                Name = group.Name,
                Users = group.Users.Select(x => UserDTO.FromEntity(x)).ToList(),
                Operations = group.Operations.Select(x => OperationDTO.FromEntity(x)).ToList()
            };
        }
    }
}
