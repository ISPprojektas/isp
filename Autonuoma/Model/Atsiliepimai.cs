using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class Atsiliepimai
    {
        [DisplayName("Autorius")]
        [Required]
        public string Autorius { get; set; }
        [DisplayName("Tekstas")]
        [Required]
        public string Atsiliepimo_Tekstas { get; set; }
        [DisplayName("Atsiliepimo data")]
        public DateTime Data { get; set; }
        [DisplayName("Įvertinimas (nuo 1 iki 10)")]
        [Required]
        public int Ivertinimas { get; set; }
        [DisplayName("Nuoroda į nuotrauką")]
        public string Nuotraukos_Link { get; set; }
        [DisplayName("Komentaro ID")]
        public int pk_Id { get; set; }
        [DisplayName("Prekės ID")]
        public int fk_Preke { get; set; }

        public string DataF {
            get {
                return Data.ToString("yyyy/MM/dd");
            }
        }
    }
}
