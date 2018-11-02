using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using DayThree_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using DayThree_FinancialPortal.Helpers;

namespace DayThree_FinancialPortal.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            var newTransaction = new Transaction
            {                
                BankAccountId = id             
            };
                      
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name");           
            return View(newTransaction);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankAccountId,TransactionTypeId,BudgetItemId,Description,Amount,Type")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var houseId = db.BankAccounts.Find(transaction.BankAccountId).HouseholdId;
                transaction.Created = DateTime.Now;
                transaction.EnteredById = User.Identity.GetUserId();
                db.Transactions.Add(transaction);
                db.SaveChanges();


                //Adjust my Bank Account Balance based on the Transaction type and amount
                BankAccountHelper.AdjustBalance(transaction.Id);

                //Adjust my Budget Balance based on the Transaction type and amount
                BudgetHelper.AdjustBalance(transaction.Id);

                //Adjust my BudgetItem Balance based on the Transaction type and amount
                BudgetItemHelper.AdjustBalance(transaction.Id);
          
                return RedirectToAction("Dashboard", "Households", new { id = houseId });
            }
            
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);         
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FirstName", transaction.EnteredById);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankAccountId,EnteredById,BudgetItemId,Description,Created,Amount,Reconciled,ReconciledAmount,Type,IsDeleted,Deleted")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FirstName", transaction.EnteredById);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
