using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MKEFishFries.Models
{
    public class Donations
    {
        [Key]
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        //[ForeignKey("People")]
        //public int PesonId { get; set; }
        //public Person Person { get; set; }
    }
}