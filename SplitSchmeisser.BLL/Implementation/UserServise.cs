using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitSchmeisser.BLL.Implementation
{
    public class UserServise : IUserServise
    {
        private IGenericRepository<Operation> operationRepository;
        private IGenericRepository<Group> groupRepository;


        public UserServise(IGenericRepository<Group> groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        public void GetUserDebsByGroup(int userId, int groupId)
        {
            var group = this.groupRepository.GetById(groupId).Result;
            var memberCount = group.UserGroups.Count;
            var operations = group.Operations
                .ToList();

            var userPayments = operations.Where(x => x.Owner.Id == userId)
                .Sum(x => x.Amount);

            var otherPayments = operations.Where(x => x.Owner.Id != userId)
                .Sum(x => x.Amount);

            var debt = userPayments - otherPayments;
        }

        public void GetUserDebsByGroupPerUrers(int userId, int groupId)
        {
            var group = this.groupRepository.GetById(groupId).Result;
            var memberCount = group.UserGroups.Count;
            var operations = group.Operations
                .ToList();

            var userPayments = operations.Where(x => x.Owner.Id == userId)
                .Sum(x => x.Amount);

            var otherOperations = operations.Where(x => x.Owner.Id != userId).GroupBy(x => x.Owner).ToList();

            var debt = otherOperations.ToDictionary(x => x.Key, x => (x.Sum(s => s.Amount) / memberCount) - (userPayments/ memberCount));
        }

        public void TestSum()
        {
            var user = 3;

            var group = new List<UserDebt>
            {
                new UserDebt { id = 1, amount = 900 },
                new UserDebt { id = 2, amount = 120 },
                new UserDebt { id = 3, amount = 0}
            };

            var userAmount = group.FirstOrDefault(x => x.id == user).amount;

            var otherAmounts = group.Where(x=>x.id != user).GroupBy(x => x.id);

            var debt = otherAmounts.ToDictionary(x => x.Key, x => (x.Sum(s => s.amount) / 3) - userAmount/3);

            var i = 0;
        }

        public class UserDebt
        {
            public int id { get; set; }

            public int amount { get; set; }
        }
    }
}
