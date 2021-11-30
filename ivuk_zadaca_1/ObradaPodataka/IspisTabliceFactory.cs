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
                default: return null;
            }
        }
    }
}
