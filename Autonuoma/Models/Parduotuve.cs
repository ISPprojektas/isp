using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model of 'Modelis' entity.
	/// </summary> 
	public class Parduotuve
	{
		[DisplayName("ID")]
        [MinLength(11)]
        [MaxLength(11)]
        [Required]
		public string ID { get; set; }

		[DisplayName("Pavadinimas")]
        [Required]
		public string Pavadinimas { get; set; }

		[DisplayName("Adresas")]
        [Required]
        public string Adresas { get; set; }

        #nullable enable 

        [DisplayName("Telefonas")]  
        [Phone]
        public string? Telefonas { get; set; }

        [DisplayName("Elektroninis paštas")]
        [EmailAddress]
        public string? EPastas { get; set; }

        #nullable disable

        //Miestas
        [DisplayName("Miestas")]
        [Required]
        public string FkMiestas { get; set; }
	}
}