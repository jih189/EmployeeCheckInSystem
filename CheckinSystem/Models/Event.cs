using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheckinSystem.Models
{
    public class Event
    {
        [Required, Key]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan TimeOfDay { get; set; }
    }
}