using System.Collections;

namespace Mod02_AdvProgramming.Assignments
{
    using System;
    using System.Collections.Generic;

    public class Ex3
    {
        public class FibonacciSequence : IEnumerable<int>
        {
            public FibonacciSequence()
            {
                throw new NotImplementedException();
            }

            public FibonacciSequence(int limit)
            {
                throw new NotImplementedException();
            }

            #region Implementation of IEnumerable

            public IEnumerator<int> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }
    }
}
