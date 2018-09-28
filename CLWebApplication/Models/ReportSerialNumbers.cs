using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{
    public class ReportSerialNumber
    {
        public int SerialNumber { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string Eui { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
