namespace Mod02_AdvProgramming.Assignments {
    using System;
    using System.Collections.Generic;

    public class Ex1 {


        public static List<Prm> LessThan<Prm>(ICollection<Prm?> col, Prm r)
            where Prm : struct, IComparable<Prm>
        {
            List<Prm> list = new List<Prm>();
            if (col != null)
                foreach (var prm in  col)
                {
                    if (prm != null)

                        if (prm.Value.CompareTo(r) < 0) 
                        {
                            list.Add(prm.Value);
                        }
                }
            return list;

        }

    }
}