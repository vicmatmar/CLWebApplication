using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class TargetDevice
    {
        public int Id { get; set; }
        public int IsaId { get; set; }
        public int? EuiId { get; set; }
        public int TestSessionId { get; set; }
        public int ResultId { get; set; }

        public EuiList Eui { get; set; }
        public InsightAdapter Isa { get; set; }
        public Result Result { get; set; }
        public TestSession TestSession { get; set; }
    }
}
