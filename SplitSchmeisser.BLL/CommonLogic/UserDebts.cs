using System.Collections.Generic;
using System;
using SplitSchmeisser.BLL.Models;
using System.Collections;

namespace SplitSchmeisser.BLL.CommonLogic
{
    [Serializable]
    public class UserDebts : IEnumerable<Debt>
    {
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

        //public IEnumerator<Debt> GetEnumerator()
        //{
        //    foreach (var d in debts)
        //    {
        //        yield return d;
        //    }
        //}

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
