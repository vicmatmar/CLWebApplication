using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class ChipType
    {
        public ChipType()
        {
            Board = new HashSet<Board>();
            Firmware = new HashSet<Firmware>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }

        public ICollection<Board> Board { get; set; }
        public ICollection<Firmware> Firmware { get; set; }
    }
}
