namespace Mod02_AdvProgramming.Assignments {
    using System;
    using System.Collections.Generic;

    public class Ex4
	{
        
        public struct Pair<T, U>
		{
            public T t;
            public U u;

            public Pair(T t, U u)
            {
                // TODO: Complete member initialization
                this.t = t;
                this.u = u;
            }
            public static Pair<T, U> MakePair(T t, U u)
            {

                return new Pair<T,U>(t,u);


            }
        }

        public static IEnumerable<Pair<T, int>> CountRepeated<T>(IEnumerable<T> seq)
        {
            var result = new List<Pair<T, int>>();
            T bef = default(T);
            
            int count = 0;
            int i = 0;

            if (seq == null)
            {
                // return result;
               yield break;
                
            }

            foreach (var ix in seq)
            {
                if (i == 0)
                {
                    bef = ix;
                    count = 1; 
                }
                else
                {
                    if (ix.Equals(bef))
                    {
                        count++;
                    }
                    else
                    {
                        if (count > 1)
                            // result.Add(Pair<T,int>.MakePair(bef, count));
                            yield return (Pair<T, int>.MakePair(bef, count));
                        bef = ix;
                        count = 1;
                    }
                }

                i++;

            }
          //  if (count > 1)
           //     yield return (Pair<T, int>.MakePair(bef, count));
                //result.Add(Pair<T, int>.MakePair(bef, count));

            //return result;
            if (count > 1)
            {
                yield return (Pair<T, int>.MakePair(bef, count));
            }
           
        }
    }
}