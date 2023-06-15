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
    public class InstrumentTests
    {
        [TestMethod]
        public void ShouldAddInstrument()
        {
            InstrumentController ic = new InstrumentController();
            var expected = new Instrument { InstrumentName = "testInstrument", InstrumentType = "testType" };

            ic.AddInstrument("testInstrument", "testType");

            CollectionAssert.Contains(ic.GetAll().ToList(), expected);
        }

        [TestMethod]
        public void ShouldDeleteInstrument()
        {
            InstrumentController ic = new InstrumentController();
            var expected = new Instrument { InstrumentName = "testInstrument1", InstrumentType = "testType" };

            ic.AddInstrument("testInstrument1", "testType");
            ic.DeleteInstrument("testInstrument1");

            CollectionAssert.DoesNotContain(ic.GetAll().ToList(), expected);
        }
    }
}
