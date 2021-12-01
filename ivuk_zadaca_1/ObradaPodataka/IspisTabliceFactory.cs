namespace ivuk_zadaca_2.ObradaPodataka
{
    public class IspisTabliceFactory
    {
        public IspisTablice DohvatiIspis(string argument)
        {
            switch (argument)
            {
                case "T": return new IspisT();
                case "S": return new IspisS();
                case "K": return new IspisK();
                case "R": return new IspisR();
                case "D": {
                    IspisD ispis = new IspisD();
                    new Semafor(ispis);
                    return ispis;
                }
                default: return null;
            }
        }
    }
}
