namespace DayThree_FinancialPortal.Migrations
{
    using DayThree_FinancialPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Head Of Household"))
            {
                roleManager.Create(new IdentityRole { Name = "Head Of Household" });
            }
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }
            if (!context.Roles.Any(r => r.Name == "Guest"))
            {
                roleManager.Create(new IdentityRole { Name = "Guest" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //*****************************************//
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();
            //*****************************************//

            context.Households.AddOrUpdate(h => h.Name,
             new Household { Name = "Demo House #1", Greeting = "Welcome to the Demo House!", Created = DateTime.Now }
            );

            if (!context.Users.Any(u => u.Email == "DemoHOH@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {                          
                    UserName = "DemoHOH@Mailinator.com",
                    Email = "DemoHOH@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "HOH",
                    DisplayName = "DEMO HOH",
                    AvatarPath = "/images/user.png",
                    Biography = "I am a long time C#.Net Developer and an aspiring Cross-Platform Mobile Developer. In my free time I enjoy playing chess and building gadgets for the IoT."
                }, "Abc&123!");

                var user = userManager.FindByEmail("DemoHOH@Mailinator.com");
                user.HouseholdId = context.Households.Where(h => h.Name == "Demo House #1").FirstOrDefault().Id;
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Head Of Household");
            }

            if (!context.Users.Any(u => u.Email == "DemoMember@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {                                      
                    UserName = "DemoMember@Mailinator.com",
                    Email = "DemoMember@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Member",
                    DisplayName = "DEMO MEMBER",
                    AvatarPath = "/images/user.png",
                    Biography = "I am a long time C#.Net Developer and an aspiring Cross-Platform Mobile Developer. In my free time I enjoy playing chess and building gadgets for the IoT."

                }, "Abc&123!");

                var user = userManager.FindByEmail("DemoMember@Mailinator.com");
                user.HouseholdId = context.Households.Where(h => h.Name == "Demo House #1").FirstOrDefault().Id;
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Member");

            }

            var houseHoldId = context.Households.Where(h => h.Name == "Demo House #1").FirstOrDefault().Id;

            if (!context.Users.Any(u => u.Email == "JTwichell@CoreTechs.net"))
            {
                userManager.Create(new ApplicationUser
                {              
                    UserName = "JTwichell@CoreTechs.net",
                    Email = "JTwichell@CoreTechs.net",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "JTWICHELL",
                    AvatarPath = "/images/user.png",
                    Biography = "I am a long time C#.Net Developer and an aspiring Cross-Platform Mobile Developer. In my free time I enjoy playing chess and building gadgets for the IoT."

                }, "Abc&123!");

                var user = userManager.FindByEmail("JTwichell@CoreTechs.net");
                user.HouseholdId = houseHoldId;
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Admin");
            }

            context.Budgets.AddOrUpdate(b => b.Name,
                new Budget { Id = 100, Name = "Utilities", Created = DateTime.Now, CurrentBalance = 0, SpendingTarget = 500, HouseholdId = houseHoldId },
                new Budget { Id = 200, Name = "Automotive", Created = DateTime.Now, CurrentBalance = 0, SpendingTarget = 600, HouseholdId = houseHoldId },
                new Budget { Id = 300, Name = "Groceries", Created = DateTime.Now, CurrentBalance = 0, SpendingTarget = 400, HouseholdId = houseHoldId },
                new Budget { Id = 400, Name = "Entertainment", Created = DateTime.Now, CurrentBalance = 0, SpendingTarget = 200, HouseholdId = houseHoldId },
                new Budget { Id = 500, Name = "Clothing", Created = DateTime.Now, CurrentBalance = 0, SpendingTarget = 200, HouseholdId = houseHoldId }
            );

            context.BudgetItems.AddOrUpdate(b => b.Name,
                new BudgetItem { BudgetId = 100, Created = DateTime.Now, CurrentBalance = 0, Name = "Gas Bill" },
                new BudgetItem { BudgetId = 100, Created = DateTime.Now, CurrentBalance = 0, Name = "Water Bill" },
                new BudgetItem { BudgetId = 100, Created = DateTime.Now, CurrentBalance = 0, Name = "Electric Bill" },
                new BudgetItem { BudgetId = 200, Created = DateTime.Now, CurrentBalance = 0, Name = "Gasoline" },
                new BudgetItem { BudgetId = 200, Created = DateTime.Now, CurrentBalance = 0, Name = "Car Payment" },
                new BudgetItem { BudgetId = 200, Created = DateTime.Now, CurrentBalance = 0, Name = "Car Repairs" },

                new BudgetItem { BudgetId = 300, Created = DateTime.Now, CurrentBalance = 0, Name = "Harris Teeter" },
                new BudgetItem { BudgetId = 300, Created = DateTime.Now, CurrentBalance = 0, Name = "Food Lion" },
                new BudgetItem { BudgetId = 300, Created = DateTime.Now, CurrentBalance = 0, Name = "Publix" },
                new BudgetItem { BudgetId = 300, Created = DateTime.Now, CurrentBalance = 0, Name = "Sam's Club" },
                new BudgetItem { BudgetId = 300, Created = DateTime.Now, CurrentBalance = 0, Name = "Aldi's" },
                new BudgetItem { BudgetId = 300, Created = DateTime.Now, CurrentBalance = 0, Name = "Walmart" },
                new BudgetItem { BudgetId = 400, Created = DateTime.Now, CurrentBalance = 0, Name = "Movies" },
                new BudgetItem { BudgetId = 400, Created = DateTime.Now, CurrentBalance = 0, Name = "Dinner" },
                new BudgetItem { BudgetId = 400, Created = DateTime.Now, CurrentBalance = 0, Name = "Vacation" },

                new BudgetItem { BudgetId = 500, Created = DateTime.Now, CurrentBalance = 0, Name = "Walmart" },
                new BudgetItem { BudgetId = 500, Created = DateTime.Now, CurrentBalance = 0, Name = "Target" },
                new BudgetItem { BudgetId = 500, Created = DateTime.Now, CurrentBalance = 0, Name = "Belk" },
                new BudgetItem { BudgetId = 500, Created = DateTime.Now, CurrentBalance = 0, Name = "Old Navy" },
                new BudgetItem { BudgetId = 500, Created = DateTime.Now, CurrentBalance = 0, Name = "Kohl's" }
            );
        }
    }
}
