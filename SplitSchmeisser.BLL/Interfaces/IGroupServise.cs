using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Infrasructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IGroupServise
    {
        Task CreateGroup(string name, IList<int> userIds, double amount);

        void AddUserToGroup(int groupId, int userId);

        IList<GroupDTO> GetGroups();

        void UpdateGroup(GroupDTO group);

        Task<GroupDTO> GetGroupById(int id);
    }
}
