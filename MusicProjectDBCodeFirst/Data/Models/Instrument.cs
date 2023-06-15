using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicProjectDBCodeFirst.Data.Models
{
    public class Instrument
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string InstrumentName { get; set; }
        [Required]
        [MaxLength(50)]
        public string InstrumentType { get; set; }

        public override string ToString()
        {
            return $"{InstrumentName} - {InstrumentType}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;

            Instrument other = (Instrument)obj;

            return this.InstrumentName == other.InstrumentName && this.InstrumentType == other.InstrumentType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + InstrumentName.GetHashCode();
                hash = hash * 23 + InstrumentType.GetHashCode();
                hash = hash * 23 + Id.GetHashCode();
                return hash;
            }
        }
    }
}
