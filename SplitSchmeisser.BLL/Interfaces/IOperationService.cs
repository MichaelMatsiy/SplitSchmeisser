using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IOperationService
    {
        void MakeOperation(int ownerId, int groupId, double amount, string Description = "");

        IList<OperationDTO> GetOperations();

        IList<OperationDTO> GetAllUsersOperations(int userId);

        IDictionary<string, double> GetUsersDebtByGroup(GroupDTO groupDto);
    }
}
