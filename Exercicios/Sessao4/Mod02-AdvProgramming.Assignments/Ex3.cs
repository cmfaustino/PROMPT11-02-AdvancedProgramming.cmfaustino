using System.Collections;

namespace Mod02_AdvProgramming.Assignments {
    using System;
    using System.Collections.Generic;

    public class Ex3 {
        public class FibonacciSequence : IEnumerable<int>
        {
            int l;
            public FibonacciSequence()
            {
                l = -1;
            }

            public FibonacciSequence(int limit)
            {
                l = limit;

            }

            #region Implementation of IEnumerable

            public IEnumerator<int> GetEnumerator()
            {
                if (l == -1)
                    for (int i = 0; ; i++)
                    {
                        yield return i;
                    }
                else
                    for (int i = 0; i < l; i++)
                    {
                        yield return i;
                    }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }
    }
}