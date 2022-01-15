using ivuk_zadaca_3.ObradaPodataka.Factory;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public class IspisTabliceFactory
    {
        public IIspisTabliceVisitable DohvatiIspis(string argument, string[] mogucnost)
        {
            switch (argument)
            {
                case "T": return new IspisT(mogucnost);
                case "S": return new IspisS(mogucnost);
                case "K": return new IspisK(mogucnost);
                case "R": return new IspisR(mogucnost);
                case "D": {
                    IspisD ispis = new IspisD(mogucnost);
                    new Semafor(ispis);
                    return ispis;
                }
                case "SU": return new IspisSU(mogucnost);
                case "GR": return new IspisGR(mogucnost);
                case "IG": return new IspisIG(mogucnost);
                case "IK": return new IspisIK(mogucnost);
                case "IR": return new IspisIR(mogucnost);
                case "VR": return new IspisVR(mogucnost);
                default: return null;
            }
        }
    }
}
