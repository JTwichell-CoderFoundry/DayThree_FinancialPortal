using DayThree_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Helpers
{
    public class BankAccountHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void AdjustBalance(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);
            var transactionType = transaction.Type;
            var bankId = transaction.BankAccountId;

            var bankAccount = db.BankAccounts.Find(bankId);
            db.BankAccounts.Attach(bankAccount);

            switch(transactionType)
            {
                case TransactionType.Deposit:
                    bankAccount.CurrentBalance += transaction.Amount;
                    break;
                case TransactionType.Withdrawal:
                    bankAccount.CurrentBalance -= transaction.Amount;
                    break;
            }
            db.SaveChanges();           
        }
    }
}