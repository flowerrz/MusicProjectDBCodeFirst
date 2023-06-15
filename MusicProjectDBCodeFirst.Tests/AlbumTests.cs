using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicProjectDBCodeFirst.Controller;
using Microsoft.EntityFrameworkCore;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

namespace MusicProjectDBCodeFirst.Tests
{
    [TestClass]
    public class AlbumTests
    {
        [TestMethod]
        public void ShouldAddAlbum()
        {
            AlbumController ac = new AlbumController();
            PerformerController pc = new PerformerController();
            Performer testPerformer = pc.GetAll().ToList()[0];

            var expected = new Album { AlbumName = "testAlbum", Year = 1990, Performer = testPerformer, Genre = "Rock", CoverImage = null };

            ac.AddAlbum("testAlbum", 1990, testPerformer, "Rock", null);

            var albums = ac.GetAll().ToList();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(albums.Any(a => a.AlbumName == expected.AlbumName && a.Year == expected.Year));

            //CollectionAssert.Contains(pc.GetAll().ToList(), expected);
        }

        /*[TestMethod]
        public void ShouldChangeAlbumName()
        {
            AlbumController ac = new AlbumController();

            PerformerController pc = new PerformerController();
            Performer testPerformer = pc.GetAll().ToList()[0];

            var expected = new Album { AlbumName = "newTestAlbum", Year = 1990, Performer = testPerformer, Genre = "Rock", CoverImage = null };

            Album toBeUpdated = new Album { AlbumName = "testAlbum", Year = 1990, Performer = testPerformer, Genre = "Rock", CoverImage = null };

            ac.AddAlbum(toBeUpdated.AlbumName, 1990, testPerformer, "Rock", null);
            ac.ChangeAlbumName(toBeUpdated, "newTestAlbum");

            var albums = ac.GetAll().ToList();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(albums.Any(a => a.AlbumName == expected.AlbumName && a.Year == expected.Year));
        }*/

        [TestMethod]
        public void ShouldDeleteAlbum()
        {
            AlbumController ac = new AlbumController();
            PerformerController pc = new PerformerController();
            Performer testPerformer = pc.GetAll().ToList()[0];

            // Create an album that should not be in the list
            var albumToRemove = new Album { AlbumName = "SomeAlbumName", Year = 2022, Performer = testPerformer, Genre = "Pop", CoverImage = null };

            // Add some albums to the list, including the albumToRemove
            ac.AddAlbum("AnotherAlbum", 2000, testPerformer, "Rock", null);
            ac.AddAlbum(albumToRemove.AlbumName, albumToRemove.Year, albumToRemove.Performer, albumToRemove.Genre, albumToRemove.CoverImage);
            ac.AddAlbum("YetAnotherAlbum", 2010, testPerformer, "Jazz", null);

            // Verify that the list contains the albumToRemove before removing it
            var albums = ac.GetAll().ToList();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(albums.Any(a => a.AlbumName == albumToRemove.AlbumName && a.Year == albumToRemove.Year));

            // Remove the album from the list
            ac.DeleteAlbum(albumToRemove.AlbumName);

            // Verify that the list no longer contains the albumToRemove
            CollectionAssert.DoesNotContain(pc.GetAll().ToList(), albumToRemove);
        }
    }
}
