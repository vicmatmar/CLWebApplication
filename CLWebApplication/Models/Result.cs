using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class Result
    {
        public Result()
        {
            TargetDevice = new HashSet<TargetDevice>();
            TestSession = new HashSet<TestSession>();
        }

        public int Id { get; set; }
        public string Text { get; set; }

        public ICollection<TargetDevice> TargetDevice { get; set; }
        public ICollection<TestSession> TestSession { get; set; }
    }
}
