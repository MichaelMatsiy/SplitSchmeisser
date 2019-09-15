using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IGroupService
    {
        Task CreateGroup(string name, IList<int> userIds, double amount);

        Task AddUserToGroup(int groupId, int userId);

        IList<GroupDTO> GetGroups();

        Task UpdateGroup(GroupDTO dto);

        Task<GroupDTO> GetGroupById(int id);

        Task Delete(int id);

    }
}
