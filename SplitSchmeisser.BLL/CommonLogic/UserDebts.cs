using System.Collections.Generic;
using SplitSchmeisser.BLL.Interfaces;
using System;

namespace SplitSchmeisser.BLL.CommonLogic
{
    [Serializable]
    public class Debt {
        public Debt() { }

        public Debt(string borrower, double amount, string debtor)
        {
            this.Borrower = borrower;
            this.Amount = amount;
            this.Debtor = debtor;
        }
        public string Borrower { get; set; }
        public double Amount { get; set; }
        public string Debtor { get; set; }
    }

    [Serializable]
    public class UserDebts
    {
        private IUserService userService;

        private List<Debt> debts;

        public UserDebts()
        {
            this.debts = new List<Debt>();
        }

        public void Add(string borrower, double amount, string debtor)
        {
            debts.Add(new Debt(borrower, amount, debtor));
        }

        public List<Debt> GetDebts()
        {
            return debts;
        }

        public UserDebts(List<Debt> debts)
        {
            this.debts = debts;
        }

        public IEnumerator<Debt> GetEnumerator()
        {
            foreach (var d in debts)
            {
                yield return d;
            }
        }

    }
}
