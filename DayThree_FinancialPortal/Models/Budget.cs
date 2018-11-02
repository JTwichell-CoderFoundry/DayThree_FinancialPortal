using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int? HouseholdId { get; set; }

        public string Name { get; set; }
        public DateTime Created { get; set; }
        public decimal SpendingTarget { get; set; }
        public decimal CurrentBalance { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public Budget()
        {
            this.BudgetItems = new HashSet<BudgetItem>();
        }
    }
}