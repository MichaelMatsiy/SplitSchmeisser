using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IOperationService
    {
        Task CreateOperation(int ownerId, int groupId, double amount, string Description = "");

        IList<OperationDTO> GetOperations();

        Task DeleteAsync(int id);

        Task UpdateOperation(OperationDTO dto);

        IList<OperationDTO> GetAllOperationsByGroup(int groupId);

        Task<OperationDTO> GetOperationById(int id);

    }
}
