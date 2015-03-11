using System.Collections.Generic;
using System.Linq;
using InscribedCircles.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InscribedCircles.Tests
{
    [TestClass]
    public class CircleServiceTest
    {
        private CircleService _service;

        [TestInitialize]
        public void Init()
        {
            _service = new CircleService();
        }

        [TestMethod]
        public void CheckCirclesForNull()
        {
            var centers = _service.GetCirclesCenters(200, 100, 10, 1);
            Assert.IsNotNull(centers);
        }

        [TestMethod]
        public void CheckReceivingCircles()
        {
            var centers = _service.GetCirclesCenters(200, 100, 10, 1);
            Assert.IsTrue(centers.Any());
        }

        //When using for example NUnit I will make RowTests and specify TestCases for this method.
        //Here I didn't mentioned different combinations of wrong parameters
        //Executing method fails when pass there wrong radius or gap
        [TestMethod]
        public void CheckReceivingCirclesWithWrongParameters()
        {
            var centers = _service.GetCirclesCenters(-200, -100, -10, -1);
            Assert.IsFalse(centers.Any());
        }

        [TestMethod]
        public void CheckReceivingCirclesCount()
        {
            var centers = _service.GetCirclesCenters(20, 10, 5, 0);
            Assert.AreEqual(2, centers.Count());
        }

        [TestMethod]
        public void CheckCirclesReturnType()
        {
            var centers = _service.GetCirclesCenters(200, 100, 10, 1);
            Assert.IsInstanceOfType(centers, typeof(IEnumerable<Point>));
        }
    }
}
