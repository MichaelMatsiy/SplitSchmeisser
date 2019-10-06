using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;

namespace SplitSchmeisser.BLL.Services
{
    public class OperationService : IOperationService
    {
        private readonly IGenericRepository<Operation> operationRepository;
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

        public async Task CreateOperationAsync(int ownerId, int groupId, double amount, string Description = "")
        {
            await this.operationRepository.InsertAsync(new Operation
            {
                Amount = amount,
                OwnerId = ownerId,
                GroupId = groupId,
                Description = Description,
                DateOfLoan = DateTime.Now
            });
        }

        public async Task UpdateOperationAsync(OperationDTO dto)
        {
            var operation = await this.operationRepository.GetByIdAsync(dto.Id);
            operation.Description = dto.Description;
            operation.Amount = dto.Amount;

            await this.operationRepository.UpdateAsync(operation);
        }

        public async Task<OperationDTO> GetOperationByIdAsync(int id)
        {
            var operation = await this.operationRepository.GetByIdAsync(id);

            return OperationDTO.FromEntity(operation);
        }

        public async Task DeleteAsync(OperationDTO dto)
        {
            await this.operationRepository.DeleteAsync(dto.Id);

            var group = await this.groupService.GetGroupByIdAsync(dto.GroupId);

            if (group.Operations.Count == 0) {
                await this.groupService.DeleteAsync(dto.GroupId);
            }
        }

        public async Task<IList<OperationDTO>> GetAllOperationsByGroupAsync(int groupId)
        {
            return await this.operationRepository.GetAll()
               .Where(x => x.GroupId == groupId)
               .Select(x => OperationDTO.FromEntity(x))
               .ToListAsync();
        }
    }
}
