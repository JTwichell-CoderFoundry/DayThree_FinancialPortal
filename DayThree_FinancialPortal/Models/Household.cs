using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name="Welcome Message")]
        public string Greeting { get; set; }

        public DateTime Created { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        public Household()
        {
            this.BankAccounts = new HashSet<BankAccount>();
            this.Members = new HashSet<ApplicationUser>();
            this.Budgets = new HashSet<Budget>();
            this.Invitations = new HashSet<Invitation>();
        }
    }
}