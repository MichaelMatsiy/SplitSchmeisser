using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IUserServise
    {
        void GetUserDebsByGroup(int userId, int groupId);

        void GetUserDebsByGroupPerUrers(int userId, int groupId);

        IList<UserDTO> GetUsers();

        User GetCurrUser();
        //void GetUsersByGroup(int groupId);
    }
}
