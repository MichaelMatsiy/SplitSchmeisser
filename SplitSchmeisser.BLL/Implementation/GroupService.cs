
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Implementation
{
    public class GroupService : IGroupServise
    {
        private IGenericRepository<Group> groupRepository;
        private IGenericRepository<User> userRepository;


        public GroupService(
            IGenericRepository<Group> groupRepository,
            IGenericRepository<User> userRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
        }

        public void AddUserToGroup(int groupId, int userId)
        {
            var group = groupRepository.GetById(groupId).Result;

            group.UserGroups.Add(new UserGroup { GroupId = groupId, UserId = userId });

            groupRepository.Update(group);
        }

        public void CreateGroup(string name, IList<int> userIds)
        {
            var users = this.userRepository.GetAll().Where(x => userIds.Contains(x.Id));

            var group = new Group();
            group.GroupName = name;

            foreach (var user in users)
            {
                group.UserGroups.Add(new UserGroup { Group = group, User = user });
            }

            this.groupRepository.Insert(group);
        }

        public void UpdateGroup(GroupDTO group)
        {
            this.groupRepository.Update(new Group { Id = group.Id, GroupName = group.Name});
        }

        public IList<GroupDTO> GetGroups()
        {
            return this.groupRepository.GetAll()
                .Select(x => new GroupDTO
                {
                    Id = x.Id,
                    Name = x.GroupName
                })
                .ToList();
        }
    }
}
