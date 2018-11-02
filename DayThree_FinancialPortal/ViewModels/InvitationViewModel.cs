using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.ViewModels
{
    public class InvitationViewModel
    {
        public int HouseholdId { get; set; }

        [Required, Display(Name = "Email Address"), EmailAddress]
        public string Email { get; set; }
    }
}