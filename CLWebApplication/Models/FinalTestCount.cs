using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{
    public class FinalTestCount
    {
        public FinalTestCountHeader Header { get; set; }
        public FinalTestCountItem[] Items { get; set; }
    }

    public class FinalTestCountHeader
    {
        public int TotalCount { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
    }

    public class FinalTestCountItem
    {
        public int TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
