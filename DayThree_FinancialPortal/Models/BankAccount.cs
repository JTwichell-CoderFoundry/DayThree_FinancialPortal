using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }

        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        public DateTime Created { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            this.Transactions = new HashSet<Transaction>();
        }

    }
}