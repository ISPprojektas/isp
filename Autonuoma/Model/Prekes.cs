namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class Prekes
    {
        public string Pavadinimas { get; set; }
        public double Kaina { get; set; }
        public string Aprasymas { get; set; }
        public int PatinkaPaspaudimai { get; set; }
        public string NuotraukaLink { get; set; }
        public string Spalva { get; set; }
        public string Medziaga { get; set; }
        public string Matmenys { get; set; }
        public string Svoris { get; set; }
        public int GarantijosTrukme { get; set; }
        public int Dydis { get; set; }
        public int Id { get; set; }
        public string fk_Kategorija { get; set; }
        public int fk_GamybosVieta { get; set; }
    }
}
