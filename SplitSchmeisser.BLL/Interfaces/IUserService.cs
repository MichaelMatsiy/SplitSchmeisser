using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IUserService
    {
        ValueTask<double> GetUserDebtsByGroup(int userId, int groupId);

        Task<List<CommonLogic.Debt>> GetUserDebtsByGroupPerUrers(GroupDTO groupId);

        IList<UserDTO> GetUsers();

        User GetCurrUser();

        Task CreateUserAsync(string name, string password);

        bool ValidateUser(string userNmae, string password);

        bool CheckUserName(string userNmae);
    }
}
