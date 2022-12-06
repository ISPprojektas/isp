namespace Autonuoma.Model
{
    public class Atsiliepimai
    {
        public string Autorius { get; set; }
        public string Atsiliepimo_Tekstas { get; set; }
        public DateTime Data { get; set; }
        public int Ivertinimas { get; set; }
        public string Nuotraukos_Link { get; set; }
        public int pk_Id { get; set; }
        public int fk_Preke { get; set; }
    }
}
