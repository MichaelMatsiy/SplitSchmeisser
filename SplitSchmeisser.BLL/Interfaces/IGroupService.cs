using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IGroupService
    {
        Task CreateGroupAsync(string name, IList<int> userIds, double amount, string description);

        Task AddUserToGroupAsync(int groupId, int userId);

        Task<IList<GroupDTO>> GetGroupsAsync();

        Task UpdateGroupAsync(GroupDTO dto);

        Task<GroupDTO> GetGroupByIdAsync(int id);

        Task DeleteAsync(int id);

    }
}
