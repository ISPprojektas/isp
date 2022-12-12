using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
	/// <summary>
	/// Model of 'Modelis' entity.
	/// </summary> 
	public class Parduotuves
	{
		[DisplayName("ID")]
        [Required]
		public string ID { get; set; }

		[DisplayName("Pavadinimas")]
        [Required]
		public string Pavadinimas { get; set; }

		[DisplayName("Adresas")]
        [Required]
        public string Adresas { get; set; }


        [DisplayName("Darbo laikas")]
        public string Darbo_laikas { get; set; }

        [DisplayName("Nuotrauka")]
        public string NuotraukaLink { get; set; }

        [DisplayName("Pašto kodas")]
        [EmailAddress]
        public string Pašto_kodas { get; set; }

        //Miestas
        [DisplayName("Miestas")]
        [Required]
        public string Miestas { get; set; }
	}
}