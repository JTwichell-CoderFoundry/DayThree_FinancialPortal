using DayThree_FinancialPortal.Models;
using DayThree_FinancialPortal.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayThree_FinancialPortal.Controllers
{
    public class MorrisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [Authorize]
        public ActionResult GetBudgetDataForBarChart()
        {
            var budgetData = new List<MorrisBudgetBar>();

            //Now I need to go grab data that I want to be displayed in each bar on the chart
            //Specifically, the need for the TargetAmount and the CurrentBalance

            //Get the UserId...
            var userId = User.Identity.GetUserId();

            //Get the HouseId
            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return Content(JsonConvert.SerializeObject(budgetData), "application/json");

            foreach(var budget in db.Households.Find(houseId).Budgets.ToList())
            {
                budgetData.Add(new MorrisBudgetBar
                {
                    Label = budget.Name,
                    Target = budget.SpendingTarget,
                    Actual = budget.CurrentBalance,
                    BarColor = budget.CurrentBalance > budget.SpendingTarget ? "red" : "green";
                });         
            }

            return Content(JsonConvert.SerializeObject(budgetData), "application/json");
        }

        [Authorize]
        public ActionResult GetBudgetItemDataForBarChart()
        {
            var budgetItemData = new List<MorrisBudgetItemBar>();

            //Now I need to go grab data that I want to be displayed in each bar on the chart
            //Specifically, the need for the TargetAmount and the CurrentBalance

            //Get the UserId...
            var userId = User.Identity.GetUserId();

            //Get the HouseId
            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");

            foreach (var budget in db.Households.Find(houseId).Budgets.ToList())
            {
                foreach(var budgetItem in budget.BudgetItems.ToList())
                {
                    budgetItemData.Add(new MorrisBudgetItemBar
                    {
                        Label = budgetItem.Name,
                        Actual = budgetItem.CurrentBalance
                    });
                }
            }

            return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");
        }

        [Authorize]
        public ActionResult GetPositiveBudgetItemDataForBarChart()
        {
            var budgetItemData = new List<MorrisBudgetItemBar>();

            //Now I need to go grab data that I want to be displayed in each bar on the chart
            //Specifically, the need for the TargetAmount and the CurrentBalance

            //Get the UserId...
            var userId = User.Identity.GetUserId();

            //Get the HouseId
            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");

            foreach (var budget in db.Households.Find(houseId).Budgets.ToList())
            {
                foreach (var budgetItem in budget.BudgetItems.ToList())
                {
                    if(budgetItem.CurrentBalance > 0)
                    {
                        budgetItemData.Add(new MorrisBudgetItemBar
                        {
                            Label = budgetItem.Name,
                            Actual = budgetItem.CurrentBalance
                        });
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(budgetItemData), "application/json");
        }
    }
}