using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IGroupServise
    {
        void CreateGroup(string name, IList<int> userIds);

        void AddUserToGroup(int groupId, int userId);

        IList<GroupDTO> GetGroups();
    }
}
