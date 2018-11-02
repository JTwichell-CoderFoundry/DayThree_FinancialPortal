using DayThree_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayThree_FinancialPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Landing()
        {
            return View();
        }

        [Authorize]
        public ActionResult Lobby()
        {
            var homelessUsers = db.Users.Where(u => u.HouseholdId == null).ToList();
            return View(homelessUsers);
        }
    }
}