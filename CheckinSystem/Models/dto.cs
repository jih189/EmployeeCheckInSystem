using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckinSystem.Models
{
    public class dto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeOfDay { set; get; }
    }
}