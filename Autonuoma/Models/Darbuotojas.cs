using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model of 'Darbuotojas' entity.
	/// </summary>
	public class Darbuotojas
	{
		[DisplayName("Asmens kodas")]
		[MinLength(11)]
		[MaxLength(11)]
		[Required]
		public string AsmensKodas { get; set; }
		
		[DisplayName("Vardas")]
		[Required]
		public string Vardas { get; set; }

		[DisplayName("Pavardė")]
		[Required]
		public string Pavarde { get; set; }

		#nullable enable

		[DisplayName("Telefonas")]
		public string? Telefonas { get; set; }

		[DisplayName("Elektroninis paštas")]
		[EmailAddress]
		public string? EPastas { get; set; }

		#nullable disable

		[DisplayName("Adresas")]
		[Required]
		public string Adresas { get; set; }

		[DisplayName("Gimimo data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime GimimoData { get; set; }

		[DisplayName("Pasamdymo data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime PasamdymoData { get; set; }

		[DisplayName("Pareigos")]
		[Required]
		public string Pareigos { get; set; }

		[DisplayName("Lytis")]
		[Required]
		public int Lytis { get; set; }

		[DisplayName("Miestas")]
		[Required]
		public string FkMiestas { get; set; }

		[DisplayName("Darbovietė")]
		[Required]
		public string FkParduotuve { get; set; }
	}
}