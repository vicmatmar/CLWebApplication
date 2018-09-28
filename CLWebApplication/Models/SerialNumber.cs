using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class SerialNumber
    {
        public int SerialNumberId { get; set; }
        public int SerialNumber1 { get; set; }
        public int ProductId { get; set; }
        public int EuiId { get; set; }
        public DateTime CreateDate { get; set; }
        public int TesterId { get; set; }
        public DateTime? UpdateDate { get; set; }

        public EuiList Eui { get; set; }
        public Product Product { get; set; }
        public Tester Tester { get; set; }
    }
}
