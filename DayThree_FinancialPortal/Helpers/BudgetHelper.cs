using DayThree_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Helpers
{
    public class BudgetHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        public static void AdjustBalance(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);           
            var transactionType = transaction.Type;
            var budgetItem = db.BudgetItems.Find(transaction.BudgetItemId);
            var budget = db.Budgets.Find(budgetItem.BudgetId);
            db.Budgets.Attach(budget);

            switch (transactionType)
            {
                case TransactionType.Deposit:
                    budget.CurrentBalance -= transaction.Amount;
                    break;
                case TransactionType.Withdrawal:
                    budget.CurrentBalance += transaction.Amount;
                    break;
                case TransactionType.AdjustmentUp:
                    break;
                case TransactionType.AdjustmentDown:
                    break;

            }
            db.SaveChanges();
        }
    }
}