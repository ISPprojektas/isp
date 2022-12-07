namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class UzsakymoPrekes
    {
        public int PrekesKiekis { get; set; }
        public int fk_Preke { get; set; }
        public int fk_Uzsakymas { get; set; }
        public double VienetoKaina { get; set; }
        public double Kaina { get; set; }
        public string fk_PrekeToString { get; set; }
    }
}
