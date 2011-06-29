namespace Mod02_AdvProgramming.Assignments
{
    using System;
    public static class Ex2
    {
        public class Fan {
            public string Label  { get; set; }
            public string Slogan { get; set; }
        }

        public static Func<Fan>[] GenerateFans_old(string[] clubs)
        {
            if (clubs == null) return null;

            var fans = new Func<Fan>[clubs.Length];
            int idx = 0;

            foreach (string club in clubs) {
                string label = "Fan of club \"" + club;
                //fans[idx++] = () => new Fan { Label = label, Slogan = club.ToUpper() + "!!!" };
                string clubtemp = club.ToUpper() + "!!!";
                fans[idx++] = () => new Fan { Label = label, Slogan = clubtemp };
            }

            return fans;
        }
        public static Fan funcao_auxiliar(this string club)
        {
            Fan f = new Fan();
            f.Label = "Fan of club \"" + club;
            f.Slogan = club.ToUpper() + "!!!";
            return f;
        }
        public static Func<Fan>[] GenerateFans(string[] clubs)
        {
            if (clubs == null) return null;

            var fans = new Func<Fan>[clubs.Length];
            int idx = 0;

            foreach (string club in clubs)
            {
                string label = "Fan of club \"" + club;
                ////fans[idx++] = () => new Fan { Label = label, Slogan = club.ToUpper() + "!!!" };
                //string clubtemp = club.ToUpper() + "!!!";
                //fans[idx++] = () => new Fan { Label = label, Slogan = clubtemp };
                fans[idx++] = club.funcao_auxiliar;
            }

            return fans;
        }
    }
}