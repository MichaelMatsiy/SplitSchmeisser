using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IOperationService
    {
        Task CreateOperation(int ownerId, int groupId, double amount, string Description = "");

        Task<IList<OperationDTO>> GetOperationsAsync();

        Task DeleteAsync(OperationDTO dto);

        Task UpdateOperation(OperationDTO dto);

        Task<IList<OperationDTO>> GetAllOperationsByGroup(int groupId);

        Task<OperationDTO> GetOperationById(int id);
    }
}
