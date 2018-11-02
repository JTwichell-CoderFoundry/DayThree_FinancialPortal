using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string EnteredById { get; set; }
        public int BudgetItemId { get; set; }

        public string Description { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }
        public bool Reconciled { get; set; }
        public decimal ReconciledAmount { get; set; }
        public TransactionType Type { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }

    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        AdjustmentUp,
        AdjustmentDown
    }
}