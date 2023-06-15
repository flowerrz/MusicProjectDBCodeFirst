using System;
using System.Collections.Generic;
using System.Text;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using System.Windows.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MusicProjectDBCodeFirst.Controller
{
    public class AlbumController
    {
        private MusicProjectDBContext context;

        public AlbumController()
        {
            context = new MusicProjectDBContext();
        }

        public void AddAlbum(string albumName, int year, Performer performer, string genre, byte[] coverImage)
        {
            if (performer == null)
                MessageBox.Show("Този изпълнител не съществува");
            else
            {
                context.Albums.Add(new Album
                {
                    AlbumName = albumName,
                    Year = year,
                    PerformerId = performer.Id,
                    Genre = genre,
                    CoverImage = coverImage
                });
            }

            context.SaveChanges();
        }

        public List<Album> GetAlbumsByPerformer(string performerName)
        {
            return context.Albums.Include(p => p.Performer).Include(s => s.Songs).Where(x => x.Performer.PerformerName == performerName).ToList();
        }

        public Album GetAlbumByName(string albumName)
        {
            return context.Albums.Include(p => p.Performer).Include(s => s.Songs).FirstOrDefault(x => x.AlbumName == albumName);
        }

        public Album GetAlbumBySong(string song)
        {
            return context.Albums.FirstOrDefault(a => a.Songs.Any(s => s.SongName == song));
        }

        public List<Album> GetAlbumsByGenre(string genre)
        {
            return context.Albums.Include(p => p.Performer).Include(s => s.Songs).Where(x => x.Genre == genre).ToList();
        }

        public virtual List<Album> GetAll()
        {
            return context.Albums.Include(p => p.Performer).ToList();
        }

        public void DeleteAlbum(string name)
        {
            var albumToDelete = context.Albums.Include(a => a.Songs).FirstOrDefault(a => a.AlbumName == name);
            if (albumToDelete != null)
            {
                context.RemoveRange(albumToDelete.Songs);
                context.Remove(albumToDelete);
                context.SaveChanges();
            }
        }

        public void ChangeAlbumName(Album album, string newName)
        {
            album.AlbumName = newName;
            context.SaveChanges();
        }
    }
}
