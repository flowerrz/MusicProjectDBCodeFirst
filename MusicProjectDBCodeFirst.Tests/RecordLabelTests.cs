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
    public class RecordLabelTests
    {
        //private MusicProjectDBContext context;

        //[SetUp]
        //public void SetUp()
        //{
        //    context = new MusicProjectDBContext();
        //}

        [TestMethod]
        public void ShouldAddRecordLabel()
        {
            var mockRecordLabelController = new Mock<RecordLabelController>();

            mockRecordLabelController.Setup(x => x.GetAll()).Returns(new List<RecordLabel> { new RecordLabel { LabelName = "testLabel", CountryName = "testCountry" } });

            var expected = new RecordLabel { LabelName = "testLabel", CountryName = "testCountry" };

            mockRecordLabelController.Object.AddRecordLabel("testLabel", "testCountry");

            var actual = mockRecordLabelController.Object.GetAll().ToList();

            CollectionAssert.Contains(actual, expected);
        }

        [TestMethod]
        public void ShouldDeleteRecordLabel()
        {
            var recordLabelController = new RecordLabelController();

            var expected = new RecordLabel { LabelName = "testLabel", CountryName = "testCountry" };

            recordLabelController.AddRecordLabel("testLabel", "testCountry");
            recordLabelController.DeleteRecordLabel("testLabel");

            CollectionAssert.DoesNotContain(recordLabelController.GetAll().ToList(), expected);
        }
    }
}
