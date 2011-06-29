namespace Mod02_AdvProgramming.Assignments.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using Mod02_AdvProgramming.Data;
    using System.Linq;

    [TestFixture]
    public class Ex5Tests
    {
        #region Setup AndTearDown methods

        [TestFixtureSetUp]
        void SetUpFixture()
        {
            
        }

        #endregion Setup AndTearDown methods

        #region Test methods

        [Test]
        public void CustomerCountriesSortedShouldReturn21Countries()
        {
            // Arrange

            // Act
            var countries = Ex5.CustomerCountriesSorted();

            // Assert
            Assert.AreEqual(21, countries.Count());
        }

        [Test]
        public void CustomerCountriesWithCitiesSortedByCountryShouldReturn21Countries()
        {
            var citiesSortedByCountry = Ex5.CustomerCountriesWithCitiesSortedByCountry();


            Assert.AreEqual(
                21
                , citiesSortedByCountry.Count());
        }

        [Test]
        public void CustomerCountriesWithCitiesSortedByCountryLisbonShouldContainLisbonOnly()
        {
            var citiesSortedByCountry = Ex5.CustomerCountriesWithCitiesSortedByCountry();


            Assert.AreEqual(
                1
                , citiesSortedByCountry.Where(x=>x.Country == "Portugal").Select(s=>s.Cities).Count());
            Assert.AreEqual(
                "Lisboa"
                , citiesSortedByCountry.Where(x => x.Country == "Portugal").Single().Cities.Single()
                );
        }

        

        // ...

        [Test]
        public void TotalsByCountrySortedByCountryFirstElementTest()
        {
            var totalsByCountry = Ex5.TotalsByCountrySortedByCountry();
            var firstElem = totalsByCountry.First();
            Assert.AreEqual("Argentina", firstElem.Country);
            Assert.AreEqual(3, firstElem.NumCustomers);
            Assert.AreEqual((decimal)8119.10, firstElem.TotalSales);
        }

        #endregion
    }
}
