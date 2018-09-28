using System;
using System.Collections.Generic;

namespace CLWebApp.Models
{
    public partial class BoardRevision
    {
        public BoardRevision()
        {
            TestSession = new HashSet<TestSession>();
        }

        public int Id { get; set; }
        public int Revision { get; set; }
        public string MfgTokens { get; set; }
        public bool? HasPa { get; set; }
        public int BoardId { get; set; }
        public int Eepromid { get; set; }
        public bool Release { get; set; }
        public bool BoostMode { get; set; }
        public bool ExternalPaalt { get; set; }
        public bool ExternalPabidirectional { get; set; }
        public byte BoostTxpower { get; set; }
        public bool AltTx { get; set; }
        public bool AltRx { get; set; }
        public int? ChipSelect { get; set; }
        public string Port { get; set; }
        public bool FlashTest { get; set; }
        public int? Bomrevision { get; set; }

        public Board Board { get; set; }
        public Eeprom Eeprom { get; set; }
        public ICollection<TestSession> TestSession { get; set; }
    }
}
