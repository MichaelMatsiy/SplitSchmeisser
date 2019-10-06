using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IUserService
    {
        List<Debt> GetUserDebtsByGroupPerUrers(GroupDTO groupId);

        Task<IList<UserDTO>> GetUsers();

        User GetCurrUser();

        Task CreateUserAsync(string name, string password);

        ValueTask<bool> ValidateUserAsync(string userNmae, string password);

        ValueTask<bool> CheckUserNameAsync(string userNmae);
    }
}
