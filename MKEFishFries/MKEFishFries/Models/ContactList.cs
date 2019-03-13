using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MKEFishFries.Models
{
    public class ContactList
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("People")]
        //public int PeopleId { get; set; }
        //public People People { get; set; }
        //[ForeignKey("Parish")]
        //public int ParishId { get; set; }
        //public Parishes Parishes { get; set; }
    }
}