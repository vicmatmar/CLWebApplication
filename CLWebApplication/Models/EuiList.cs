using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class EuiList
    {
        public EuiList()
        {
            SerialNumber = new HashSet<SerialNumber>();
            TargetDevice = new HashSet<TargetDevice>();
        }

        public int Id { get; set; }
        public string Eui { get; set; }
        public int ProductionSiteId { get; set; }
        public string VendorEui { get; set; }

        public ProductionSite ProductionSite { get; set; }
        public ICollection<SerialNumber> SerialNumber { get; set; }
        public ICollection<TargetDevice> TargetDevice { get; set; }
    }
}
