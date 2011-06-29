    namespace Mod02_AdvProgramming.Assignments {
    using System;
    using System.Collections.Generic;

    public class Ex1 {
        public static List<Prm> LessThan<Prm>(ICollection<Prm?> col, Prm r)
            // TODO 
            where Prm : struct , IComparable<Prm> { // adicionado IComparable<Prm>
            // TODO
            //throw new NotImplementedException();

            var listaRes = new List<Prm>();
            if (col != null)
                foreach (Prm? prm in col)
                {
                    if ((prm.HasValue) && (prm.GetValueOrDefault().CompareTo(r) < 0))
                        listaRes.Add(prm.Value);
                }
            return listaRes;

            }


    }
}