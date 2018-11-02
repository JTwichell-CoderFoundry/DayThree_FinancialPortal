using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DayThree_FinancialPortal.Helpers;
using DayThree_FinancialPortal.Models;
using DayThree_FinancialPortal.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DayThree_FinancialPortal.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles ="Head Of Household")]
        public ActionResult Invite(int id)
        {
            var invitation = new InvitationViewModel
            {
                HouseholdId = id
            };

            return View(invitation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InviteAsync(InvitationViewModel invitation)
        {
            //Look for other active invitations for the email we just sent an invitation to and mark it/them as invalid
            foreach (var invite in db.Invitations.Where(i => i.Email == invitation.Email && i.IsValid).ToList())
            {
                db.Invitations.Attach(invite);
                invite.IsValid = false;
                //db.SaveChanges();
            }

            //Here is where all the work will be done to create an invitation
            var newInvitation = new Invitation
            {
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(3),
                HouseholdId = invitation.HouseholdId,
                Email = invitation.Email,
                IsValid = true,
                Code = Guid.NewGuid()
            };
         
            db.Invitations.Add(newInvitation);
            db.SaveChanges();

            //Here is where I will create the email invitation
            try
            {                             
                var callbackUrl = Url.Action("Accept", "Households", new { email = newInvitation.Email, code = newInvitation.Code }, protocol: Request.Url.Scheme);
                var body = "This is an invitation to join my Household on The Financial Portal Web Application. Please click <a href=\"" + callbackUrl + 
                           "\">here</a> to begin the acceptance process and Register as a new user.";

                var email = new MailMessage("Financial Portal<Jason@Twichell.com>", invitation.Email)
                {
                    Subject = "You have been invited to join a Household...",
                    Body = body,
                    IsBodyHtml = true
                };

                var svc = new PersonalEmail();
                await svc.SendAsync(email);               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return RedirectToAction("Dashboard", new { id = invitation.HouseholdId });
        }

        public ActionResult Accept(string email, Guid code)
        {
            var errors = new StringBuilder();

            var invitation = db.Invitations.FirstOrDefault(i => i.Code == code);
            if(invitation.IsAccepted)
            {
                errors.Append("This Invitation has already been accepted.");
            }
            if(DateTime.Now > invitation.Expires)
            {
                errors.Append("This invitation has expired");
            }
            if (!invitation.IsValid)
            {
                errors.Append("This invitation is invalid");
            }

            if (errors.ToString() != string.Empty)
            {
                TempData["ErrorMsg"] = errors.ToString();  
                return View();
            }
            
            var accept = new AcceptViewModel
            {
                Email = email,
                Code = code
            };
            return View(accept);
        }
  
        [Authorize]
        public ActionResult Dashboard(string tab)
        {
            ViewBag.ActiveTab = tab;
            var userId = User.Identity.GetUserId();
            if (userId == null)
                return RedirectToAction("Lobby", "Home");

            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return RedirectToAction("Lobby", "Home");

            ViewBag.BankAccountId = new SelectList(db.BankAccounts.Where(b => b.HouseholdId == houseId).ToList());

            return View(db.Households.Find(houseId));
        }


        // POST: Households/Create
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Greeting")] Household household)
        {
            if (ModelState.IsValid)
            {
                household.Created = DateTime.Now;
                db.Households.Add(household);
                db.SaveChanges();

                //Update User record to reflect the new House Id
                var user = db.Users.Find(User.Identity.GetUserId());
                db.Users.Attach(user);
                user.HouseholdId = household.Id;
                db.SaveChanges();

                //Add Head Of Household role to the User
                roleHelper.AddUserToRole(user.Id, "Head Of Household");

                return RedirectToAction("Dashboard");
            }

            return View(household);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Greeting,Created,IsDeleted,Deleted")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
