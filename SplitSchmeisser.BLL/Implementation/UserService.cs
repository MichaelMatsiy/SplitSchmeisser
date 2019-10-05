using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.BLL.CommonLogic;
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


        private List<CommonLogic.Debt> ResolveDebts(UserDebts userDebts) {

            List<Debt> resolvedList = new List<Debt>();

            foreach (var item in userDebts)
            {
                var debts = userDebts.GetDebts().Where(x => x.Borrower == item.Debtor && x.Debtor == item.Borrower).ToList();

                if (debts.Count > 0 && debts.Count < 2)
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
                    else if (debts.First().Amount == item.Amount)
                    {
                        continue;
                    }
                }
                else if (debts.Count > 1) {

                    foreach (var d in debts) {
                        if (d.Amount > item.Amount)
                        {
                            d.Amount -= item.Amount;
                            resolvedList.Add(d);
                        }
                        else if (d.Amount < item.Amount)
                        {
                            item.Amount -= d.Amount;
                            resolvedList.Add(item);

                        }
                        else if (d.Amount == item.Amount)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    resolvedList.Add(item);
                }
            }

            return resolvedList;
        }

        public async Task<List<CommonLogic.Debt>> GetUserDebtsByGroupPerUrers(GroupDTO group)
        {
           return await Task.Run(() =>
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
                           userDebts.Add(u.Name, (userPayments / users.Count), d.Name);
                       }
                   }
               }

               return ResolveDebts(userDebts);
           });
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

        public bool ValidateUser(string userName, string password)
        {
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
