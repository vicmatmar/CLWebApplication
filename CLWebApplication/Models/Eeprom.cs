using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class Eeprom
    {
        public Eeprom()
        {
            BoardRevision = new HashSet<BoardRevision>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BoardRevision> BoardRevision { get; set; }
    }
}
