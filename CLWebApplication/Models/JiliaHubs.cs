using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class JiliaHubs
    {
        public int Id { get; set; }
        public string Bid { get; set; }
        public string Mac { get; set; }
        public string Uid { get; set; }
        public string Activation { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
