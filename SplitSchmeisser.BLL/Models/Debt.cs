using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.BLL.Models
{
    [Serializable]
    public class Debt
    {
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
}
