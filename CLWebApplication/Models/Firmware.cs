using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class Firmware
    {
        public Firmware()
        {
            TestSession = new HashSet<TestSession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long FileVersion { get; set; }
        public int? ImageTypeId { get; set; }
        public string Owner { get; set; }
        public int ChipTypeId { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Release { get; set; }

        public ChipType ChipType { get; set; }
        public ImageType ImageType { get; set; }
        public ICollection<TestSession> TestSession { get; set; }
    }
}
