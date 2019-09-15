﻿using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Implementation
{
    public class UserService : IUserService
    {
        private IGenericRepository<Group> groupRepository;
        private IGenericRepository<User> userRepository;

        public UserService(IGenericRepository<Group> groupRepository,
            IGenericRepository<User> userRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
        }

        public IList<UserDTO> GetUsers()
        {
            return this.userRepository.GetAll().ToList()
                .Select(x => UserDTO.FromEntity(x))
                .ToList();
        }

        public async ValueTask<double> GetUserDebtsByGroup(int userId, int groupId)
        {
            var group = await groupRepository.GetById(groupId);

            var memberCount = group.Users.Count();
            var operations = group.Operations
                .ToList();

            var userPayments = operations.Where(x => x.Owner.Id == userId)
                .Sum(x => x.Amount);

            var otherPayments = operations.Where(x => x.Owner.Id != userId)
                .Sum(x => x.Amount);

            return (otherPayments / memberCount) - (userPayments / memberCount);
        }

        public async Task GetUserDebtsByGroupPerUrers(int userId, int groupId)
        {
            var group = await this.groupRepository.GetById(groupId);
            //var memberCount = group.UserGroups.Count;
            //var operations = group.Operations
            //    .ToList();

            //var userPayments = operations.Where(x => x.Owner.Id == userId)
            //    .Sum(x => x.Amount);

            //var otherOperations = operations.Where(x => x.Owner.Id != userId).GroupBy(x => x.Owner).ToList();

            //var debt = otherOperations.ToDictionary(x => x.Key, x => (x.Sum(s => s.Amount) / memberCount) - (userPayments/ memberCount));
        }

        //test
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

        }

        public class UserDebt
        {
            public int id { get; set; }

            public int amount { get; set; }
        }

        public User GetCurrUser()
        {
            return this.userRepository.GetAll().First(x => x.Name == CurrentUserService.UserName);
        }

        public async Task CreateUserAsync(string name, string password)
        {
            await this.userRepository.Insert(new User()
            {
                Name = name,
                Password = password
            });
        }

        public bool ValidateUser(string userName, string password) {
            return this.userRepository.GetAll()
                .FirstOrDefault(x => x.Name == userName && x.Password == password) 
                != null;
        }

        public bool CheckUserName(string userName)
        {
            return this.userRepository.GetAll()
                .FirstOrDefault(x => x.Name == userName)
                == null;
        }
    }
}
