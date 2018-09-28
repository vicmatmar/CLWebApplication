using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class InsightAdapter
    {
        public InsightAdapter()
        {
            TargetDevice = new HashSet<TargetDevice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }

        public ICollection<TargetDevice> TargetDevice { get; set; }
    }
}
