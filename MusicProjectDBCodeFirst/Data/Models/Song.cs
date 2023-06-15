using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicProjectDBCodeFirst.Data.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string SongName { get; set; }
        public double SongDuration { get; set; }
        public int? AlbumId { get; set; }

        public virtual Album Album { get; set; }

        public override string ToString()
        {
            TimeSpan duration = TimeSpan.FromMinutes(SongDuration);
            string formattedDuration = string.Format("{0:D1}:{1:D2}", duration.Minutes, duration.Seconds);
            return $"{SongName} - {formattedDuration}";
        }

        // Override the Equals method to compare properties instead of references
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Song other = (Song)obj;
            return SongName == other.SongName && SongDuration == other.SongDuration;
        }

        // Override the GetHashCode method to use the same properties used in Equals method
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + SongName.GetHashCode();
                hash = hash * 23 + SongDuration.GetHashCode();
                return hash;
            }
        }
    }
}
