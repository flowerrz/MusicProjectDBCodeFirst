using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicProjectDBCodeFirst.Data.Models
{
    public class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string AlbumName { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        public int? PerformerId { get; set; }
        public byte[] CoverImage { get; set; }

        public virtual Performer Performer { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public override string ToString()
        {
            return $"{AlbumName} - {Year} - {Performer.PerformerName}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;

            Album other = (Album)obj;

            return this.AlbumName == other.AlbumName && this.Year == other.Year;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + AlbumName.GetHashCode();
                hash = hash * 23 + Year.GetHashCode();
                return hash;
            }
        }
    }
}
