using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class ProductionSite
    {
        public ProductionSite()
        {
            EuiList = new HashSet<EuiList>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool LoadRangeTest { get; set; }
        public bool? RunIct { get; set; }
        public bool RunRangeTest { get; set; }
        public bool LoadApplication { get; set; }
        public bool ForceChannel { get; set; }
        public bool Erase { get; set; }
        public bool EnableFirmwareChange { get; set; }

        public ICollection<EuiList> EuiList { get; set; }
    }
}
