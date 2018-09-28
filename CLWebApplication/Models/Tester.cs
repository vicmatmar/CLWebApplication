using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class Tester
    {
        public Tester()
        {
            SerialNumber = new HashSet<SerialNumber>();
            UserSession = new HashSet<UserSession>();
        }

        public string Name { get; set; }
        public int? Pin { get; set; }
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public bool? Active { get; set; }

        public ICollection<SerialNumber> SerialNumber { get; set; }
        public ICollection<UserSession> UserSession { get; set; }
    }
}
