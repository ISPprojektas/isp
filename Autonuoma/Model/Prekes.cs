using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class Prekes
    {
        [DisplayName("ID")]
		[Required]
		public int ID { get; set; }
		
		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }

        [DisplayName("Kaina")]
        [Required][DisplayFormat(DataFormatString = "{0:#0.00}", ApplyFormatInEditMode =true)]
        public decimal Kaina { get; set; }

		[DisplayName("Aprašymas")]
		[Required]
		public string Aprasymas { get; set; }

        [DisplayName("Patinka paspaudimai")]
        [Required]
        public int Patinka_paspaudimai { get; set; }

		#nullable enable
		[DisplayName("Nuotrauka")]
		public string? Nuotrauka { get; set; }

        [DisplayName("Spalva")]
		public string? Spalva { get; set; }
        [DisplayName("Medžiaga")]
		public string? Medžiaga { get; set; }
        [DisplayName("Matmenys")]
		public string? Matmenys { get; set; }
        [DisplayName("Svoris")]
		public string? Svoris { get; set; }
        [DisplayName("Garantijos trukmė")]
		public string? Garantijos_trukmė { get; set; }
        [DisplayName("Rūbų dydis")]
		public int? dydis { get; set; }

		#nullable disable
        [DisplayName("Kategorija")]
		[Required]
		public string Kategorija { get; set; }

        [DisplayName("Gamybos vieta")]
		[Required]
		public int Gamybos_vieta { get; set; }
    }
}
