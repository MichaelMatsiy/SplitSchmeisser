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

        void UpdateGroup(GroupDTO group);

        Task<GroupDTO> GetGroupById(int id);

        Task Delete(int id);

        Task<IList<OperationDTO>> GetAllOperationsByGroup(int groupId);
    }
}
