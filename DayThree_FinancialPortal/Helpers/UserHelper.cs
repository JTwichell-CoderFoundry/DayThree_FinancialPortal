using DayThree_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DayThree_FinancialPortal.Helpers
{
    public class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetAvatar()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId == null)
                return WebConfigurationManager.AppSettings["DefaultUserAvatarPath"];
            return db.Users.Find(userId).AvatarPath;
        }

        public static string GetFullName()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId == null)
                return WebConfigurationManager.AppSettings["DefaultUserFullName"];
            return db.Users.Find(userId).FullName;
        }

       

    }
}