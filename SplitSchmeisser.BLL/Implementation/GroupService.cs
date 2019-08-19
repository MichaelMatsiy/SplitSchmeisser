﻿using SplitSchmeisser.BLL.Interfaces;
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
        private IOperationService operationService;

        public GroupService(
            IGenericRepository<Group> groupRepository,
            IGenericRepository<User> userRepository,
            IUserService userService,
            IOperationService operationService)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.userService = userService;
            this.operationService = operationService;
        }

        public async Task AddUserToGroup(int groupId, int userId)
        {
            var group = groupRepository.GetById(groupId).Result;
            var user =  await this.userRepository.GetById(userId);

            group.Users.Add(user);

            await groupRepository.UpdateAsync(group);
        }

        public async Task CreateGroup(string name, IList<int> userIds, double amount)
        {
            var users = this.userRepository.GetAll().Where(x => userIds.Contains(x.Id)).ToList();
            var currUser = this.userService.GetCurrUser();

            var operation = new Operation
            {
                Amount = amount,
                DateOfLoan = DateTime.Now,
                OwnerId = currUser.Id
            };

            var group = new Group {
                GroupName = name,
                Users = users
            };

            group.Operations.Add(operation);
            group.Users.Add(currUser);

            await this.groupRepository.Insert(group);
        }

        public void UpdateGroup(GroupDTO group)
        {
            this.groupRepository.UpdateAsync(new Group { Id = group.Id, GroupName = group.Name });
        }

        public IList<GroupDTO> GetGroups()
        {
            var result = this.groupRepository.GetAll()
                .ToList()
                .Select(x => GroupDTO.FromEntity(x))
                .ToList();

            return result;
        }

        public async Task<GroupDTO> GetGroupById(int id)
        {
            var group = await this.groupRepository.GetById(id);
            var groupDto = GroupDTO.FromEntity(group);

            groupDto.UserDebts = this.operationService.GetUsersDebtByGroup(groupDto);

            return groupDto;
        }
    }
}
