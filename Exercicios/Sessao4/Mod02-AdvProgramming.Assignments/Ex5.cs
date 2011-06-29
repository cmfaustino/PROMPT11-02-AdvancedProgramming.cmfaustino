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
            return SampleData.LoadCustomersFromXML().Select(c => c.Country).Distinct().OrderBy(x => x);
        }

        public static IEnumerable<CountryWithCities> CustomerCountriesWithCitiesSortedByCountry()
        {
            //// TODO
        //    var countryList = SampleData.LoadCustomersFromXML().Select(c => c.Country).Distinct();
            

        //    foreach (var c in countryList)
        //    {
        //        CountryWithCities cwc = new CountryWithCities();
        //        cwc.Country = c;
        //        cwc.Cities = SampleData.LoadCustomersFromXML().Where(x => x.Country == c).Select(y => y.City).Distinct().OrderBy(w => w);
        //        yield return cwc;
        //    }
        //

            var cust = SampleData.LoadCustomersFromXML();

            //foreach (var country in cust.GroupBy(x => x.Country))
            //{
            //    CountryWithCities cwc = new CountryWithCities();
            //    cwc.Country = country.Key;
            //    cwc.Cities = country.Select(q => q.City).Distinct().OrderBy(w => w);
            //        //cust.Where(x => x.Country = country).Select(y => y.City).OrderBy();
                
            //    yield return cwc;
            //}
            return cust.GroupBy(x => x.Country).Select(x =>
                                                           {
                                                               CountryWithCities cwc = new CountryWithCities();
                                                               cwc.Country = x.Key;
                                                               cwc.Cities = x.Select(y => y.City).Distinct().OrderBy(w => w);
                                                               return cwc;
                                                           });
        }


        public static IEnumerable<CustomerOrders> CustomerWithNumOrdersSortedByNumOrdersDescending()
        {

            var cust = SampleData.LoadCustomersFromXML();

            return cust.Select(c => new CustomerOrders
                                        {
                                            Customer = c.CustomerID,
                                            NumOrders = c.Orders.Count(),
                                            TotalSales = c.Orders.Select(o => o.Total).Sum()
                                        });


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

    public static class MetodosExtensao //adicionada de Sessao1a4
    {
        public static IEnumerable<TRes> MeSelectMany<TSrc, TRes>(this IEnumerable<TSrc> enumerable, Func<TSrc, IEnumerable<TRes>> sel)
        {
            foreach (var objT in enumerable)
            {
                foreach (var src in sel(objT))
                {
                    yield return src;
                }
            }
        }
    }

}
