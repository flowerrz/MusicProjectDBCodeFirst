using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicProjectDBCodeFirst.Controller;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;
using Moq;

namespace MusicProjectDBCodeFirst.Tests
{
    [TestClass]
    public class SongTests
    {
        //private MusicProjectDBContext context;
        
        //[SetUp]
        //public void SetUp()
        //{
        //    context = new MusicProjectDBContext();
        //}

        [TestMethod]
        public void ShouldAddSongToDatabase()
        {
            SongController sc = new SongController();
            AlbumController ac = new AlbumController();
            Song expected = new Song { SongName = "testSong", SongDuration = 3 };

            sc.AddSong("testSong", 3, ac.GetAll()[0]);
            List<Song> songs = sc.GetAll().ToList();

            CollectionAssert.Contains(songs, expected);
        }

        [TestMethod]
        public void ShouldDeleteSong()
        {
            SongController sc = new SongController();
            AlbumController ac = new AlbumController();
            Song expected = new Song { SongName = "testSong", SongDuration = 3 };

            sc.AddSong("testSong", 3, ac.GetAll()[0]);
            sc.DeleteSong("testSong");
            List<Song> songs = sc.GetAll().ToList();

            CollectionAssert.DoesNotContain(songs, expected);
        }
    }
}
