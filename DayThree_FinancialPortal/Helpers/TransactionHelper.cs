using DayThree_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Helpers
{
    public class TransactionHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static int GetHouseholdIdFromTransaction(int bankId)
        {            
            return db.BankAccounts.Find(bankId).HouseholdId;
        }
    }
}