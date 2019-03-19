using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKEFishFries.Models
{
    public class VisitorActionsViewModel
    {
        //    public string SignUpForNotifications { get; set; }
        //    public string Comments { get; set; }
        //    public int DonationAmount { get; set; }
        //    public string CreditCardNumber { get; set; }
        //    public string Expiration { get; set; }
        //    public int SecurityNumber { get; set; }

        public Parish Parishes { get; set; }
        public Donation Donations { get; set; }
        public ContactList ContactList { get; set; }
        
    }

}