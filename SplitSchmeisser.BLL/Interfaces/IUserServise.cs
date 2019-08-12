using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IUserServise
    {
        void GetUserDebsByGroup(int userId, int groupId);

        void GetUserDebsByGroupPerUrers(int userId, int groupId);

        IList<UserDTO> GetUsers();

        User GetCurrUser();
        //void GetUsersByGroup(int groupId);

        void CreateUserAsync(string name, string password);

        bool ValidateUser(string userNmae, string password);
    }
}
