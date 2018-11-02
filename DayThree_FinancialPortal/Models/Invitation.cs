using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }

        public Guid Code { get; set; }
        public string Email { get; set; }

        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public DateTime? Accepted { get; set; }
        
        public bool IsAccepted { get; set; }
        public bool IsValid { get; set; }

        public virtual Household Household { get; set; }
    }
}