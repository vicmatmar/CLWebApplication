using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class UserSession
    {
        public UserSession()
        {
            TestSession = new HashSet<TestSession>();
        }

        public int Id { get; set; }
        public int TesterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ApplicationName { get; set; }

        public Tester Tester { get; set; }
        public ICollection<TestSession> TestSession { get; set; }
    }
}
