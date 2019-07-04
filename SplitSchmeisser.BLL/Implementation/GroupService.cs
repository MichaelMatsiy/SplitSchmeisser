
using Microsoft.EntityFrameworkCore;
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
    public class GroupService : IGroupServise
    {
        private IGenericRepository<Group> groupRepository;
        private IGenericRepository<User> userRepository;

        private IUserServise userServise;


        public GroupService(
            IGenericRepository<Group> groupRepository,
            IGenericRepository<User> userRepository,
             IUserServise userServise)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.userServise = userServise;
        }

        public void AddUserToGroup(int groupId, int userId)
        {
            var group = groupRepository.GetById(groupId).Result;

            group.UserGroups.Add(new UserGroup { GroupId = groupId, UserId = userId });

            groupRepository.Update(group);
        }

        public async Task CreateGroup(string name, IList<int> userIds, double amount)
        {
            var users = this.userRepository.GetAll().Where(x => userIds.Contains(x.Id));
            var currUser = this.userServise.GetCurrUser();

            var operation = new Operation
            {
                Amount = amount,
                DateOfLoan = DateTime.Now,
                OwnerId = currUser.Id
            };

            var group = new Group();
            group.GroupName = name;

            foreach (var user in users)
            {
                group.UserGroups.Add(new UserGroup { Group = group, UserId = user.Id });
            }

            group.Operations.Add(operation);

            await this.groupRepository.Insert(group);
        }

        public void UpdateGroup(GroupDTO group)
        {
            this.groupRepository.Update(new Group { Id = group.Id, GroupName = group.Name });
        }

        public IList<GroupDTO> GetGroups()
        {
            var result = this.groupRepository.GetAll()
                .Select(x => GroupDTO.FromEntity(x))
                .ToList();

            return result;
        }

        public async Task<GroupDTO> GetGroupById(int id)
        {
            var group = await this.groupRepository.GetById(id);

            return GroupDTO.FromEntity(group);
        }
    }
}
