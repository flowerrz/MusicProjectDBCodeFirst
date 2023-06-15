using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicProjectDBCodeFirst.Data.Models
{
    public class Performer
    {
        public Performer()
        {
            Albums = new HashSet<Album>();
        }

        public Performer(string name, int birthYear, RecordLabel label)
        {
            PerformerName = name;
            BirthYear = birthYear;
            RecordLabel = label;
            RecordLabelId = label.Id;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string PerformerName { get; set; }
        public int BirthYear { get; set; }
        public int RecordLabelId { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual RecordLabel RecordLabel { get; set; }

        public override string ToString()
        {
            return $"{PerformerName}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;

            Performer other = (Performer)obj;

            return this.PerformerName == other.PerformerName && this.BirthYear == other.BirthYear;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + PerformerName.GetHashCode();
                hash = hash * 23 + BirthYear.GetHashCode();
                hash = hash * 23 + Id.GetHashCode();
                return hash;
            }
        }
    }
}
