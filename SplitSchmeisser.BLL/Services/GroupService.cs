using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGenericRepository<Group> groupRepository;
        private readonly IGenericRepository<User> userRepository;

        private readonly IUserService userService;

        public GroupService(
            IGenericRepository<Group> groupRepository,
            IGenericRepository<User> userRepository,
            IUserService userService)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.userService = userService;
        }

        public async Task AddUserToGroupAsync(int groupId, int userId)
        {
            var group = await groupRepository.GetByIdAsync(groupId);
            var user = await this.userRepository.GetByIdAsync(userId);

            group.Users.Add(user);

            await groupRepository.UpdateAsync(group);
        }

        public async Task CreateGroupAsync(string name, IList<int> userIds, double amount, string description)
        {
            var users = await this.userRepository.GetAll()
                .Where(x => userIds.Contains(x.Id))
                .ToListAsync();

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
                Description = description
            };

            group.Operations.Add(operation);

            await this.groupRepository.InsertAsync(group);
        }

        public async Task UpdateGroupAsync(GroupDTO dto)
        {
            var group = await this.groupRepository.GetByIdAsync(dto.Id);
            group.Name = dto.Name;

            await this.groupRepository.UpdateAsync(group);
        }

        public async Task<IList<GroupDTO>> GetGroupsAsync()
        {
            var groups = await this.groupRepository.GetAll().ToListAsync();

            return groups.Select(x => GroupDTO.FromEntity(x)).ToList();
        }

        public async Task<GroupDTO> GetGroupByIdAsync(int id)
        {
            var group = await this.groupRepository.GetByIdAsync(id);

            if (group == null) return null;

            var groupDto = GroupDTO.FromEntity(group);
            groupDto.UserDebts = this.userService.GetUserDebtsByGroupPerUrersAsync(groupDto);

            return groupDto;
        }

        public async Task DeleteAsync(int id)
        {
            await this.groupRepository.DeleteAsync(id);
        }
    }
}
