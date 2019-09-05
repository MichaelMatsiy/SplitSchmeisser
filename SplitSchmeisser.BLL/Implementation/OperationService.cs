using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;

namespace SplitSchmeisser.BLL.Implementation
{
    public class OperationService : IOperationService
    {
        private IGenericRepository<Operation> operationRepository;

        private IUserService userService;

        public OperationService(
           IGenericRepository<Operation> operationRepository,
           IUserService userService)
        {
            this.operationRepository = operationRepository;
            this.userService = userService;
        }

        public IList<OperationDTO> GetOperations()
        {
            return this.operationRepository.GetAll()
               .ToList()
               .Select(x => OperationDTO.FromEntity(x))
               .ToList();
        }

        public IDictionary<string, double> GetUsersDebtByGroup(GroupDTO groupDto)
        {
            IDictionary<string, double> userDebts = new Dictionary<string, double>();

            foreach (var item in groupDto.Users)
            {
                var debt = this.userService.GetUserDebsByGroup(item.Id, groupDto.Id);

                if(debt > 0) userDebts.Add(item.Name, debt);
            }

            return userDebts;
        }

        public IList<OperationDTO> GetAllUsersOperations(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateOperation(int ownerId, int groupId, double amount, string Description = "")
        {
            var operation = new Operation
            {
                Amount = amount,
                OwnerId = ownerId,
                GroupId = groupId,
                Description = Description,
                DateOfLoan = DateTime.Now
            };

            await this.operationRepository.Insert(operation);
        }

        public async Task UpdateOperation(int id) {

            var a = await this.operationRepository.GetById(id);

            a.DateOfLoan = a.DateOfLoan.AddDays(1);

            await this.operationRepository.UpdateAsync(a);
        }

        public async Task DeleteAsync(int id)
        {
            await this.operationRepository.Delete(id);
        }
    }
}
