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
    public class PerformerTests
    {
        [TestMethod]
        public void ShouldAddPerformer()
        {
            PerformerController pc = new PerformerController();
            Performer expected = new Performer { PerformerName = "testPerformer", BirthYear = 1999 };
            RecordLabelController rlc = new RecordLabelController();

            RecordLabel label = rlc.GetLabelByName("Hansa Records");

            pc.AddPerformer(new Performer { PerformerName = "testPerformer", BirthYear = 1999, RecordLabel = label });
            
            CollectionAssert.Contains(pc.GetAll().ToList(), expected);
        }

        [TestMethod]
        public void ShouldChangePerformerName()
        {
            PerformerController pc = new PerformerController();
            Performer expected = new Performer { PerformerName = "newTestPerformer", BirthYear = 1999 };
            RecordLabelController rlc = new RecordLabelController();

            RecordLabel label = rlc.GetLabelByName("Hansa Records");

            pc.AddPerformer(new Performer { PerformerName = "testPerformer", BirthYear = 1999, RecordLabel = label });
            Performer performer = pc.GetPerformerByName("testPerformer");

            pc.ChangeName(performer, "newTestPerformer");

            CollectionAssert.Contains(pc.GetAll().ToList(), expected);
        }

        [TestMethod]
        public void ShouldChangePerformerLabel()
        {
            PerformerController pc = new PerformerController();
            RecordLabelController rlc = new RecordLabelController();

            RecordLabel newLabel = rlc.GetLabelByName("Polar Music");
            Performer expected = new Performer { PerformerName = "testPerformer", BirthYear = 1999, RecordLabel =  newLabel};
            RecordLabel label = rlc.GetLabelByName("Hansa Records");

            pc.AddPerformer(new Performer { PerformerName = "testPerformer", BirthYear = 1999, RecordLabel = label });
            Performer performer = pc.GetPerformerByName("testPerformer");

            pc.UpdateLabel(performer, newLabel);

            CollectionAssert.Contains(pc.GetAll().ToList(), expected);
        }

        [TestMethod]
        public void ShouldDeletePerformer()
        {
            PerformerController pc = new PerformerController();
            Performer expected = new Performer { PerformerName = "testPerformer1", BirthYear = 1999 };
            RecordLabelController rlc = new RecordLabelController();

            RecordLabel label = rlc.GetLabelByName("Hansa Records");

            pc.AddPerformer(new Performer { PerformerName = "testPerformer1", BirthYear = 1999, RecordLabel = label });
            pc.DeletePerformer("testPerformer1");

            CollectionAssert.DoesNotContain(pc.GetAll().ToList(), expected);
        }
    }
}
