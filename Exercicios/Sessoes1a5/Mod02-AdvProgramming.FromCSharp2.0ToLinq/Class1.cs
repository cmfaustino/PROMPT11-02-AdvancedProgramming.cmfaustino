// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;

namespace Mod02_AdvProgramming_FromCSharp2_0ToLinq {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>


    class Filter<T> : IEnumerable<T>
    {
        IEnumerable<T> enumerable;
        Predicate<T> pred;

        public Filter(IEnumerable<T> ie, Predicate<T> p)
        {
            enumerable = ie;
            pred = p;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FilterEnumerator<T>(enumerable.GetEnumerator(), pred);
        }

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    class FilterEnumerator<T> : IEnumerator<T>
    {
        IEnumerator<T> enumerator;
        Predicate<T> pred;

        public FilterEnumerator(IEnumerator<T> ie, Predicate<T> p)
        {
            enumerator = ie;
            pred = p;
        }

        public void Dispose()
        {
            enumerator.Dispose();
        }
        public bool MoveNext()
        {
            bool b;
            while ((b = enumerator.MoveNext()) && pred(enumerator.Current) == false) ;
            return b;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception><filterpriority>2</filterpriority>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        public T Current
        {
            get { return enumerator.Current; }
        }
    }




    public class Class1 {
        

        public IEnumerable<T> Filter<T>(IEnumerable<T> seq, Predicate<T> pred)
        {
            List<T> returnSeq = new List<T>();
            foreach(T elem in seq)
            {
                if(pred(elem))
                {
                    returnSeq.Add(elem);
                }
            }
            return returnSeq;
        }


        public static IEnumerable<T> FilterLazyYeld<T>(IEnumerable<T> ie, Predicate<T> pred)
        {
            foreach (T t in ie) { if (pred(t)) yield return t; }
        }


        public IEnumerable<T> FilterLazy<T>(IEnumerable<T> seq, Predicate<T> pred)
        {
            return new Filter<T>(seq, pred);
        } 



        public void UsingFilter()
        {
            int[] ai = {1, 2, 3, 4, 5, 6, 7};
            int max = 10;

            IEnumerable<int> newSeq = Filter(ai, i => i < max && i % 2 == 0);

            IEnumerator<int> enumer = newSeq.GetEnumerator();

            enumer.MoveNext();

        }

        bool checkParity(int i)
        {
            return i%2 == 0;
        }


        static Action m1(int x)
        {
            int y = x + 10;
            Action a =  () => Console.WriteLine("x:{0} - Y: {1}", x++, y++);
            x += 20;
            return a;

        }

        void callM1()
        {
            Action ma = m1(5);
            Action mb = m1(8);

            ma();
            mb();
            ma();
            mb();


        }

    }
}
