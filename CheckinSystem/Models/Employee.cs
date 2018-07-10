using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheckinSystem.Models
{
    public class Employee
    {
        [Required, Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}