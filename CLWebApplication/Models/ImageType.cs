using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class ImageType
    {
        public ImageType()
        {
            Firmware = new HashSet<Firmware>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Firmware> Firmware { get; set; }
    }
}
