using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKEFishFries.Models
{
    public class Parish
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string WebsiteURL { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
        public Boolean RecieveComments { get; set; }
        public List<Event>listOfEvents { get; set; }

        [ForeignKey("People")]
        public int? AdminPersonId { get; set; }
        public People People { get; set; }

    }
}