using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Implementation
{
    public class GroupService : IGroupService
    {
        private IGenericRepository<Group> groupRepository;
        private IGenericRepository<User> userRepository;
        private IGenericRepository<Operation> operationRepository;

        private IUserService userService;
        private IOperationService operationService;

        public GroupService(
            IGenericRepository<Group> groupRepository,
            IGenericRepository<User> userRepository,
            IGenericRepository<Operation> operationRepository,
            IUserService userService,
            IOperationService operationService)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.operationRepository = operationRepository;
            this.userService = userService;
            this.operationService = operationService;
        }

        public async Task AddUserToGroup(int groupId, int userId)
        {
            var group = groupRepository.GetById(groupId).Result;
            var user = await this.userRepository.GetById(userId);

            group.Users.Add(user);

            await groupRepository.UpdateAsync(group);
        }

        public async Task CreateGroup(string name, IList<int> userIds, double amount)
        {
            var users = this.userRepository.GetAll().Where(x => userIds.Contains(x.Id)).ToList();
            var currUser = this.userService.GetCurrUser();

            var group = new Group
            {
                Name = name,
                Users = users
            };

            var operation = new Operation
            {
                Amount = amount,
                DateOfLoan = DateTime.Now,
                OwnerId = currUser.Id,
                Group = group,
                Owner = currUser,
                Description = "description"
            };


            group.Operations.Add(operation);
            group.Users.Add(currUser);

            await this.groupRepository.Insert(group);
        }

        public void UpdateGroup(GroupDTO group)
        {
            this.groupRepository.UpdateAsync(new Group { Id = group.Id, Name = group.Name });
        }

        public IList<GroupDTO> GetGroups()
        {
            return this.groupRepository.GetAll()
                .ToList()
                .Select(x => GroupDTO.FromEntity(x))
                .ToList();
        }

        public async Task<GroupDTO> GetGroupById(int id)
        {
            var group = await this.groupRepository.GetById(id);
            var groupDto = GroupDTO.FromEntity(group);

            groupDto.UserDebts = this.operationService.GetUsersDebtByGroup(groupDto);

            return groupDto;
        }

        public async Task Delete(int id)
        {
            await this.groupRepository.Delete(id);
        }

        public async Task<IList<OperationDTO>> GetAllOperationsByGroup(int groupId)
        {

            var ops = this.operationRepository.GetAll()
               .ToList()
               .Select(x => OperationDTO.FromEntity(x))
               .ToList()
               .Where(x => x.Group.Id == groupId)
               .ToList();

            foreach (var op in ops)
            {
                op.Group = await this.GetGroupById(groupId);
            }

            return ops;
        }

    }
}
