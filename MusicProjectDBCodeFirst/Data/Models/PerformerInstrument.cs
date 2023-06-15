using System;
using System.Collections.Generic;
using System.Text;

namespace MusicProjectDBCodeFirst.Data.Models
{
    public class PerformerInstrument
    {
        public int? PerformerId { get; set; }
        public int? InstrumentId { get; set; }

        public virtual Performer Performer { get; set; }
        public virtual Instrument Instrument{ get; set; }
    }
}
