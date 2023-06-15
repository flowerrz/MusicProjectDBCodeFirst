using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicProjectDBCodeFirst.Data.Models
{
    public class RecordLabel
    {
        public RecordLabel()
        {
            Performers = new HashSet<Performer>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string LabelName { get; set; }
        [MaxLength(50)]
        public string CountryName { get; set; }

        public virtual ICollection<Performer> Performers { get; set; }

        public override string ToString()
        {
            return $"{LabelName} - {CountryName}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;

            RecordLabel other = (RecordLabel)obj;

            return this.LabelName == other.LabelName && this.CountryName == other.CountryName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + LabelName.GetHashCode();
                hash = hash * 23 + CountryName.GetHashCode();
                return hash;
            }
        }
    }
}
