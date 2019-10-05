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

        private IUserService userService;

        public GroupService(
            IGenericRepository<Group> groupRepository,
            IGenericRepository<User> userRepository,
            IUserService userService)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.userService = userService;
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
            var users = this.userRepository.GetAll()
                .Where(x => userIds.Contains(x.Id))
                .ToList();

            var currUser = this.userService.GetCurrUser();

            users.Add(currUser);

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

            await this.groupRepository.Insert(group);
        }

        public async Task UpdateGroup(GroupDTO dto)
        {
            var group = await this.groupRepository.GetById(dto.Id);
            group.Name = dto.Name;

            await this.groupRepository.UpdateAsync(group);
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

            return GroupDTO.FromEntity(group);
        }

        public async Task Delete(int id)
        {
            await this.groupRepository.Delete(id);
        }

    }
}
