using System;
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

        private IUserService userService;

        public OperationService(
           IGenericRepository<Operation> operationRepository,
           IUserService userService)
        {
            this.operationRepository = operationRepository;
            this.userService = userService;
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

        public IDictionary<string, double> GetUsersDebtByGroup(GroupDTO groupDto)
        {
            IDictionary<string, double> userDebts = new Dictionary<string, double>();

            foreach (var item in groupDto.Users)
            {
                var debt = this.userService.GetUserDebsByGroup(item.Id, groupDto.Id);

                userDebts.Add(item.Name, Math.Max(0, debt));
            }

            return userDebts;
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
