using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }

        public string Name { get; set; }
        public DateTime Created { get; set; }
        public decimal CurrentBalance { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            this.Transactions = new HashSet<Transaction>();
        }

    }
}