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
            return this.operationRepository.GetAll().ToList()
               .Select(x => OperationDTO.FromEntity(x))
               .ToList();
        }

        public async Task<IDictionary<string, double>> GetUsersDebtByGroup(GroupDTO groupDto)
        {
            IDictionary<string, double> userDebts = new Dictionary<string, double>();

            //Parallel.ForEach(groupDto.Users, async item =>
            //{
            //    var debt = await this.userService.GetUserDebtsByGroup(item.Id, groupDto.Id);

            //    if (debt > 0) userDebts.Add(item.Name, debt);
            //});

            foreach (var item in groupDto.Users)
            {
                var debt = await this.userService.GetUserDebtsByGroup(item.Id, groupDto.Id);

                if (debt > 0) userDebts.Add(item.Name, debt);
            }

            return userDebts;
        }

        public IList<OperationDTO> GetAllUsersOperations(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateOperation(int ownerId, int groupId, double amount, string Description = "")
        {
            await this.operationRepository.Insert(new Operation
            {
                Amount = amount,
                OwnerId = ownerId,
                GroupId = groupId,
                Description = Description,
                DateOfLoan = DateTime.Now
            });
        }

        public async Task UpdateOperation(OperationDTO dto)
        {
            var operation = await this.operationRepository.GetById(dto.Id);
            operation.Description = dto.Description;
            operation.Amount = dto.Amount;

            await this.operationRepository.UpdateAsync(operation);
        }

        public async Task<OperationDTO> GetOperationById(int id)
        {
            var operation = await this.operationRepository.GetById(id);

            return OperationDTO.FromEntity(operation);
        }

        public async Task DeleteAsync(int id)
        {
            await this.operationRepository.Delete(id);
        }

        public IList<OperationDTO> GetAllOperationsByGroup(int groupId)
        {
            var ops = operationRepository.GetAll()
               .ToList()
               .Select(x => OperationDTO.FromEntity(x))
               .ToList()
               .Where(x => x.GroupId == groupId)
               .ToList();

            return ops;
        }
    }
}
