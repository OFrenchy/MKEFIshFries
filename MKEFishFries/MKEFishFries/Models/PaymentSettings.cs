using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKEFishFries.Models
{
    public class PaymentSettings
    {
        public string StripePrivateKey {get; set;}
        public string StripePublicKey { get; set; }
    }
}