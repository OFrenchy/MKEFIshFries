using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MKEFishFries.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public int ParishId {get; set;}
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        public int Price { get; set; }
        public string FoodDescription { get; set; }
        public string CarryOutOption { get; set; }

        [ForeignKey("People")]
        public int? SponserPersonId { get; set; }
        public People People { get; set; }
    }
}