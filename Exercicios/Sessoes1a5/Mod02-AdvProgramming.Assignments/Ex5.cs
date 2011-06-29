using System;
using System.Linq;
using Mod02_AdvProgramming.Data;

namespace Mod02_AdvProgramming.Assignments
{
    using System.Collections.Generic;

    public class Ex5
    {
        public class CountryWithCities
        {
            public string Country { get; set; }
            public IEnumerable<string> Cities { get; set; }
        }

        public class CustomerOrders
        {
            public string Customer { get; set; }
            public int NumOrders { get; set; }
            public decimal TotalSales { get; set; }
        }

        public class TotalsByCountry
        {
            public string Country { get; set; }
            public int NumCustomers { get; set; }
            public decimal TotalSales { get; set; }
        }

        public enum PeriodRange
        {
            Year = 12,
            Semester = 6,
            Trimester = 3,
            Month = 1
        }

        public class TotalsByCountryByPeriod : TotalsByCountry
        {
            public PeriodRange PeriodRange { get; set; }
        }

        public static IEnumerable<string> CustomerCountriesSorted()
        {
            // TODO
            throw new NotImplementedException();
        }

        public static IEnumerable<CountryWithCities> CustomerCountriesWithCitiesSortedByCountry()
        {
            // TODO
            throw new NotImplementedException();
        }

        public static IEnumerable<CustomerOrders> CustomerWithNumOrdersSortedByNumOrdersDescending()
        {
            // TODO
            throw new NotImplementedException();
        }

        public static IEnumerable<TotalsByCountry> TotalsByCountrySortedByCountry()
        {
            // TODO
            throw new NotImplementedException();
        }

        public static IEnumerable<TotalsByCountryByPeriod> TotalsByCountryByPeriodSortedByCountry(PeriodRange periodRange)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
