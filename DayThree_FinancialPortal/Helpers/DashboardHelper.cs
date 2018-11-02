using DayThree_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Helpers
{
    public class DashboardHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static int GetHouseholdMemberCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Users.Where(u => u.HouseholdId == householdId).Count();
        }

        public static int GetHouseholdBankCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.BankAccounts.Where(u => u.HouseholdId == householdId).Count();
        }

        public static int GetHouseholdTransactionCount()
        {           
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Households.Find(householdId).BankAccounts.SelectMany(t => t.Transactions).Count();   
        }

        public static int GetHouseholdBudgetCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Households.Find(householdId).Budgets.Count();
        }

        public static int GetHouseholdBudgetItemnCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            return db.Households.Find(householdId).Budgets.SelectMany(b => b.BudgetItems).Count();
        }

    }
}