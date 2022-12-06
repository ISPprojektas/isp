namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class Naudotojai
    {
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public string ElPastas { get; set; }
        public string Slaptazodis { get; set; }
        public DateOnly GimimoData { get; set; }
        public string Slapyvardis { get; set; }
        public string KortelesNr { get; set; }
        public string NuotraukosLink { get; set; }
        public string TelNr { get; set; }
        public string Miestas { get; set; }
        public DateOnly RegistracijosData { get; set; }
        public int Tipas { get; set; }
        public int Privilegijos { get; set; }
        public int pk_Id { get; set; }
    }
}
