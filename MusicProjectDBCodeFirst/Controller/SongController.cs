using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace MusicProjectDBCodeFirst.Controller
{
    public class SongController
    {
        private MusicProjectDBContext context;

        public SongController()
        {
            context = new MusicProjectDBContext();
        }

        public void AddSong(string songName, double duration, Album album)
        {
            if (album != null)
            {
                context.Songs.Add(new Song
                {
                    SongName = songName,
                    SongDuration = duration,
                    AlbumId = album.Id
                });
                context.SaveChanges();
            }
            else
                MessageBox.Show("This album doesn't exist");
        }

        public Song GetSongByName(string songName)
        {
            return context.Songs.Include(a => a.Album).FirstOrDefault(s => s.SongName == songName);
        }

        public ICollection<Song> GetAll()
        {
            return context.Songs.ToList();
        }

        public void DeleteSong(string name)
        {
            var song = context.Songs.FirstOrDefault(y => y.SongName == name);
            if (song != null)
                context.Songs.Remove(song);
            else
                throw new MyException("This song doesn't exist");
            context.SaveChanges();
        }
    }
}
