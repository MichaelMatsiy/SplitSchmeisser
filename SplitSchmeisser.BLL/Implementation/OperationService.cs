using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly IGroupService groupService;

        public OperationService(
           IGenericRepository<Operation> operationRepository,
           IGroupService groupService)
        {
            this.operationRepository = operationRepository;
            this.groupService = groupService;
        }

        public async Task<IList<OperationDTO>> GetOperationsAsync()
        {
            var operations = await this.operationRepository.GetAll().ToListAsync();
            return operations.Select(x => OperationDTO.FromEntity(x)).ToList();
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

        public async Task DeleteAsync(OperationDTO dto)
        {
            await this.operationRepository.Delete(dto.Id);

            var group = await this.groupService.GetGroupById(dto.GroupId);

            if (group.Operations.Count == 0) {
                await this.groupService.Delete(dto.GroupId);
            }
        }

        public async Task<IList<OperationDTO>> GetAllOperationsByGroup(int groupId)
        {
            return await this.operationRepository.GetAll()
               .Where(x => x.GroupId == groupId)
               .Select(x => OperationDTO.FromEntity(x))
               .ToListAsync();
        }
    }
}
