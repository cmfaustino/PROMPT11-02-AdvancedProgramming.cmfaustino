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

    public static class MetodosExtensao
    {
        public static IEnumerable<T> MeWhere<T>(this IEnumerable<T> enumerable, Func<T,bool> pred)
        {
            foreach (var objT in enumerable)
            {
                if (pred(objT))
                {
                    yield return objT;
                }
            }
        }

        public static IEnumerable<TRes> MeSelect<TSrc,TRes>(this IEnumerable<TSrc> enumerable, Func<TSrc, TRes> sel)
        {
            foreach (var objT in enumerable)
            {
                yield return sel(objT);
            }
        }

        public static IEnumerable<TRes> MeSelect<TSrc, TRes>(this IEnumerable<TSrc> enumerable, Func<TSrc, int, TRes> sel)
        {
            int indexInterno = 0;
            foreach (var objT in enumerable)
            {
                yield return sel(objT, indexInterno++);
            }
        }

        public static IEnumerable<T> MeConcat<T>(this IEnumerable<T> enumerable, IEnumerable<T> enum2)
        {
            foreach (var objT in enumerable)
            {
                yield return objT;
            }
            foreach (var objT in enum2)
            {
                yield return objT;
            }
        }

        public static T MeFirst<T>(this IEnumerable<T> enumerable)
        {
            foreach (var objT in enumerable)
            {
                return objT;
            }
            return default(T);
        }

        public static T MeFirst<T>(this IEnumerable<T> enumerable, Func<T,bool> pred)
        {
            foreach (var objT in enumerable)
            {
                if (pred(objT))
                {
                    return objT;
                }
            }
            return default(T);
        }

        public static T MeLast<T>(this IEnumerable<T> enumerable)
        {
            T objRes = default(T);
            foreach (var objT in enumerable)
            {
                objRes = objT;
            }
            return objRes;
        }

        public static T MeLast<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            T objRes = default(T);
            foreach (var objT in enumerable)
            {
                if (pred(objT))
                {
                    objRes = objT;
                }
            }
            return objRes;
        }

        public static IEnumerable<T> MeTake<T>(this IEnumerable<T> enumerable, int count)
        {
            int contadorInterno = 0;
            foreach (var objT in enumerable)
            {
                if ((contadorInterno++)<count)
                {
                    yield return objT;
                }
                else
                {
                    yield break;
                }
            }
        }

        public static IEnumerable<T> MeTakeWhile<T>(this IEnumerable<T> enumerable, Func<T,bool> pred)
        {
            foreach (var objT in enumerable)
            {
                if (pred(objT))
                {
                    yield return objT;
                }
                else
                {
                    yield break;
                }
            }
        }

        public static IEnumerable<T> MeTakeWhile<T>(this IEnumerable<T> enumerable, Func<T, int, bool> pred)
        {
            int indexInterno = 0;
            foreach (var objT in enumerable)
            {
                if (pred(objT, indexInterno++))
                {
                    yield return objT;
                }
                else
                {
                    break;
                }
            }
        }

        public static IEnumerable<T> MeSkip<T>(this IEnumerable<T> enumerable, int count)
        {
            int contadorInterno = 0;
            foreach (var objT in enumerable)
            {
                if ((contadorInterno++) >= count)
                {
                    yield return objT;
                }
                else
                {
                    //yield break;
                }
            }
        }

        public static IEnumerable<T> MeSkipWhile<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            bool analisarElementos = true;
            foreach (var objT in enumerable)
            {
                if(analisarElementos)
                {
                    if (pred(objT))
                    {
                    }
                    else
                    {
                        analisarElementos = false;
                        yield return objT;
                    }
                }
                else
                {
                    yield return objT;
                }
            }
        }

        public static IEnumerable<T> MeSkipWhile<T>(this IEnumerable<T> enumerable, Func<T, int, bool> pred)
        {
            int indexInterno = 0;
            bool analisarElementos = true;
            foreach (var objT in enumerable)
            {
                if (analisarElementos)
                {
                    if (!pred(objT, indexInterno++))
                    {
                        analisarElementos = false;
                        yield return objT;
                    }
                }
                else
                {
                    yield return objT;
                }
            }
        }

        public static IEnumerable<TRes> MeZip<TSrc1, TSrc2, TRes>(this IEnumerable<TSrc1> enumerable, IEnumerable<TSrc2> enum2, Func<TSrc1, TSrc2, TRes> mergeRes)
        {
            IEnumerator<TSrc1> enumerador1 = enumerable.GetEnumerator();
            IEnumerator<TSrc2> enumerador2 = enum2.GetEnumerator();
            while (enumerador1.MoveNext() && enumerador2.MoveNext())
            {
                yield return mergeRes(enumerador1.Current, enumerador2.Current);
            }
        }

        public static T MeAggregate<T>(this IEnumerable<T> enumerable, Func<T, T, T> accum)
        {
            IEnumerator<T> enumerador1 = enumerable.GetEnumerator();
            T objT;
            if (enumerador1.MoveNext()) // primeiro elemento
            {
                objT = enumerador1.Current;
                while (enumerador1.MoveNext()) // resto dos elementos acumulados
                {
                    objT = accum(objT, enumerador1.Current);
                }
            }
            else
            {
                objT = default(T);
            }
            return objT;
        }

        public static TAccum MeAggregate<T, TAccum>(this IEnumerable<T> enumerable, TAccum inicial, Func<TAccum, T, TAccum> accum)
        {
            IEnumerator<T> enumerador1 = enumerable.GetEnumerator();
            TAccum objTAccum = inicial;
            while (enumerador1.MoveNext()) // todos os elementos acumulados
            {
                objTAccum = accum(objTAccum, enumerador1.Current);
            }
            return objTAccum;
        }

        public static TRes MeAggregate<T, TAccum, TRes>(this IEnumerable<T> enumerable, TAccum inicial, Func<TAccum, T, TAccum> accum, Func<TAccum,TRes> sel)
        {
            IEnumerator<T> enumerador1 = enumerable.GetEnumerator();
            TAccum objTAccum = inicial;
            while (enumerador1.MoveNext()) // todos os elementos acumulados
            {
                objTAccum = accum(objTAccum, enumerador1.Current);
            }
            return sel(objTAccum);
        }

        public static IEnumerable<TRes> MeJoin<TSrcOut, TSrcIn, TKey, TRes>(this IEnumerable<TSrcOut> enumOut, IEnumerable<TSrcIn> enumIn, Func<TSrcOut, TKey> selOut, Func<TSrcIn, TKey> selIn, Func<TSrcOut, TSrcIn, TRes> mergeRes)
        {
            foreach (var objSrcOut in enumOut)
            {
                foreach (var objSrcIn in enumIn)
                {
                    if (EqualityComparer<TKey>.Default.Equals(selOut(objSrcOut), selIn(objSrcIn)))
                    {
                        yield return mergeRes(objSrcOut, objSrcIn);
                    }
                }
            }
        }

        public static IEnumerable<TRes> MeJoin<TSrcOut, TSrcIn, TKey, TRes>(this IEnumerable<TSrcOut> enumOut, IEnumerable<TSrcIn> enumIn, Func<TSrcOut, TKey> selOut, Func<TSrcIn, TKey> selIn, Func<TSrcOut, TSrcIn, TRes> mergeRes, IEqualityComparer<TKey> compKeys)
        {
            foreach (var objSrcOut in enumOut)
            {
                foreach (var objSrcIn in enumIn)
                {
                    if (compKeys.Equals(selOut(objSrcOut), selIn(objSrcIn)))
                    {
                        yield return mergeRes(objSrcOut, objSrcIn);
                    }
                }
            }
        }

        //Join
        //OrderBy/ThenBy
        //GroupBy

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
