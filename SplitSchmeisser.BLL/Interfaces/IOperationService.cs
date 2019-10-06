using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IOperationService
    {
        Task CreateOperationAsync(int ownerId, int groupId, double amount, string Description = "");

        Task<IList<OperationDTO>> GetOperationsAsync();

        Task DeleteAsync(OperationDTO dto);

        Task UpdateOperationAsync(OperationDTO dto);

        Task<IList<OperationDTO>> GetAllOperationsByGroupAsync(int groupId);

        Task<OperationDTO> GetOperationByIdAsync(int id);
    }
}
