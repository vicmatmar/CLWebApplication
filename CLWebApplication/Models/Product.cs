using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class Product
    {
        public Product()
        {
            SerialNumber = new HashSet<SerialNumber>();
            TestSession = new HashSet<TestSession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ModelString { get; set; }
        public string Description { get; set; }
        public bool? Released { get; set; }
        public int BoardId { get; set; }
        public DateTime CreateDate { get; set; }
        public string SerialNumberCode { get; set; }
        public string ZigbeeModelString { get; set; }
        public int ExtensionId { get; set; }
        public string Sku { get; set; }
        public bool CurrentTest { get; set; }
        public int? ModelEncodingNumber { get; set; }
        public string ZplFile { get; set; }
        public bool EnableReadProtectOnDevice { get; set; }
        public string BoxLabelName { get; set; }

        public Board Board { get; set; }
        public ICollection<SerialNumber> SerialNumber { get; set; }
        public ICollection<TestSession> TestSession { get; set; }
    }
}
