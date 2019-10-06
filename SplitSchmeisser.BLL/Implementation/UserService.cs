using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.BLL.CommonLogic;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data.Entity;

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

        public async Task<IList<UserDTO>> GetUsers()
        {
            var users = await this.userRepository.GetAll().ToListAsync();

            return users.Select(x => UserDTO.FromEntity(x)).ToList();
        }

        private List<Debt> ResolveDebts(UserDebts userDebts)
        {

            List<Debt> resolvedList = new List<Debt>();

            foreach (var item in userDebts)
            {
                if (resolvedList.Any(x => x.Borrower == item.Debtor && x.Debtor == item.Borrower)) continue;

                var debts = userDebts.GetDebts().Where(x => x.Borrower == item.Debtor && x.Debtor == item.Borrower).ToList();

                if (debts.Count > 0)
                {
                    if (debts.First().Amount > item.Amount)
                    {
                        debts.First().Amount -= item.Amount;
                        resolvedList.Add(debts.First());
                    }
                    else if (debts.First().Amount < item.Amount)
                    {
                        item.Amount -= debts.First().Amount;
                        resolvedList.Add(item);

                    }
                }
                else
                {
                    resolvedList.Add(item);
                }
            }

            return resolvedList;
        }

        public List<Debt> GetUserDebtsByGroupPerUrers(GroupDTO group)
        {
            var userDebts = new UserDebts();

            var users = group.Users.ToList();

            foreach (var u in users)
            {
                var userPayments = group.Operations.Where(x => x.Owner.Id == u.Id)
                    .Sum(x => x.Amount);

                if ((userPayments / users.Count) > 0)
                {
                    var debtors = users.Where(x => x.Id != u.Id);

                    foreach (var d in debtors)
                    {
                        userDebts.Add(u.Name, Math.Round((userPayments / users.Count), 2), d.Name);
                    }
                }
            }

            return ResolveDebts(userDebts);
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

        public async ValueTask<bool> ValidateUserAsync(string userName, string password)
        {
            var user = await this.userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == userName && x.Password == password);
            return user != null;
        }

        public async ValueTask<bool> CheckUserNameAsync(string userName)
        {
            var user = await this.userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == userName);

            return user != null;
        }
    }
}
