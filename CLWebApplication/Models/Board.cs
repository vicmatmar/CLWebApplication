using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class Board
    {
        public Board()
        {
            BoardRevision = new HashSet<BoardRevision>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int ChipTypeId { get; set; }

        public ChipType ChipType { get; set; }
        public ICollection<BoardRevision> BoardRevision { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
