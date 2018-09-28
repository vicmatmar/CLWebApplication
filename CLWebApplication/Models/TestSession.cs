using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class TestSession
    {
        public TestSession()
        {
            TargetDevice = new HashSet<TargetDevice>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BoardRevisionId { get; set; }
        public int FirmwareId { get; set; }
        public int UserSessionId { get; set; }
        public int ResultId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public BoardRevision BoardRevision { get; set; }
        public Firmware Firmware { get; set; }
        public Product Product { get; set; }
        public Result Result { get; set; }
        public UserSession UserSession { get; set; }
        public ICollection<TargetDevice> TargetDevice { get; set; }
    }
}
