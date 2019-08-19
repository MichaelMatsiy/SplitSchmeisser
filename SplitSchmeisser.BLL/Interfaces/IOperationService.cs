using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IOperationService
    {
        void MakeOperation(int ownerId, int groupId, double amount, string Description = "");

        IList<OperationDTO> GetAll();

        IList<OperationDTO> GetAllUsersOperations(int userId);

        IDictionary<string, double> GetUsersDebtByGroup(GroupDTO groupDto);
    }
}
