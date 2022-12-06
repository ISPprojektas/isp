namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class Uzsakymai
    {
        public DateTime UzsakymoLaikas { get; set; }
        public double UzsakymoKaina { get; set; }
        public DateTime ApmokejimoLaikas { get; set; }
        public double Nuolaida { get; set; }
        public int Busena { get; set; }
        public int pk_Id { get; set; }
        public int fk_Naudotojas { get; set; }
        public int fk_Parduotuve { get; set; }
    }
}
