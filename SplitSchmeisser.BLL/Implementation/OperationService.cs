using System.Collections.Generic;
using System.Linq;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;

namespace SplitSchmeisser.BLL.Implementation
{
    public class OperationService : IOperationService
    {
        private IGenericRepository<Operation> operationRepository;

        public OperationService(
           IGenericRepository<Operation> operationRepository)
        {
            this.operationRepository = operationRepository;
        }

        public IList<OperationDTO> GetAll()
        {

            //TODO: UserDTO and GroupDTO
            return this.operationRepository.GetAll()
               .Select(x => new OperationDTO
               {
                   Id = x.Id,
                   Amount = x.Amount,
                   DateOfLoan = x.DateOfLoan,
                   Description = x.Description,
                   Owner = new UserDTO(),
                   Group = new GroupDTO()

               })
               .ToList();
        }

        public IList<OperationDTO> GetAllUsersDebts(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IList<OperationDTO> GetAllUsersOperations(int userId)
        {
            throw new System.NotImplementedException();
        }

        public void MakeOperation(int ownerId, int groupId, double amount, string Description = "")
        {
            throw new System.NotImplementedException();
        }
    }
}
