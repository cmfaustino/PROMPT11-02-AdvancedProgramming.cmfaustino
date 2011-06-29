using System;
using System.Configuration;
using System.Linq;
using Mod02_AdvProgramming.Data;

namespace Mod02_AdvProgramming.Assignments {
    using System.Collections.Generic;

    public class Ex5 {
        private static string ToPeriod(DateTime d, PeriodRange p)
        {
            return  d.Year.ToString() + (d.Month-1) / (int)p; 
        }


        public class CountryWithCities {
            public string Country { get; set; }
            public IEnumerable<string> Cities { get; set; }
        }

        public class CustomerOrders {
            public string Customer { get; set; }
            public int NumOrders { get; set; }
            public decimal TotalSales { get; set; }
        }

        public class TotalsByCountry {
            public string Country { get; set; }
            public int NumCustomers { get; set; }
            public decimal TotalSales { get; set; }
        }

        public enum PeriodRange {
                Year = 12, 
                Semester = 6, 
                Trimester = 3, 
                Month = 1
            }

        public class TotalsByCountryByPeriod : TotalsByCountry
        {
            public PeriodRange PeriodRange { get; set; }
            public string Period { get; set; }

        }

        public static IEnumerable<string> CustomerCountriesSorted() {
            return SampleData.LoadCustomersFromXML().Select(c => c.Country).Distinct().OrderBy(c => c);
        }

        public static IEnumerable<CountryWithCities> CustomerCountriesWithCitiesSortedByCountry()
        {
            return SampleData.LoadCustomersFromXML().GroupBy(c => c.Country).
                Select(g => new CountryWithCities { Country = g.Key, Cities = g.Select(c=> c.City).Distinct().OrderBy(city => city) }).OrderBy(cwc => cwc.Country);
        }


        public static IEnumerable<CustomerOrders> CustomerWithNumOrdersSortedByNumOrdersDescending() {
            return SampleData.LoadCustomersFromXML().
                Select(c => new CustomerOrders { Customer = c.Name, NumOrders = c.Orders.Length, TotalSales = c.Orders.Sum(o => o.Total) }).
                OrderByDescending(co => co.NumOrders).ThenBy(co => co.TotalSales);
        }

        public static IEnumerable<TotalsByCountry> TotalsByCountrySortedByCountry() {
            return SampleData.LoadCustomersFromXML().GroupBy(c => c.Country).
                Select(g => new TotalsByCountry { Country = g.Key, NumCustomers = g.Count(), TotalSales = g.SelectMany(c => c.Orders).Sum(o => o.Total) })
                .OrderBy(cwc => cwc.Country);
        }
        
        public static IEnumerable<TotalsByCountryByPeriod> TotalsByCountryByPeriodSortedByCountry(PeriodRange periodRange)
        {
            //var query =  from c in SampleData.LoadCustomersFromXML()
            //       group c by c.Country into g
            //       select 
            //       select new TotalsByCountryByPeriod { Country = g.Key, NumCustomers = g.Count(), TotalSales = g.SelectMany(c => c.Orders).Sum(o => o.Total) }

            return SampleData.LoadCustomersFromXML().GroupBy(c => c.Country).
                SelectMany(g => g.SelectMany(c => c.Orders, (c, o) => new {Customer = c, Order = o}).GroupBy(
                        co => ToPeriod(co.Order.OrderDate, periodRange))
                        ,(g, period) => new TotalsByCountryByPeriod {
                                  Country = g.Key,
                                  NumCustomers = period.Select(co => co.Customer).Distinct().Count(),
                                  PeriodRange = periodRange,
                                  Period = period.Key, 
                                  TotalSales = period.Sum(co => co.Order.Total)
                              }).OrderBy(cwc => cwc.Country).ThenBy(cwc => cwc.Period);
        }
    }
}