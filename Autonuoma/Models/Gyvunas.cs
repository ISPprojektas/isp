using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model of 'Klientas' entity.
	/// </summary>
	public class Gyvunas
	{
		[DisplayName("ID")]
		[MinLength(11)]
		[MaxLength(11)]
		[Key]
		[Required]
		public string ID { get; set; }
		
		[DisplayName("Vardas")]
		[Required]
		public string Vardas { get; set; }

		[DisplayName("Gimimo data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime? GimimoData { get; set; }

        [DisplayName("Svoris")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Svoris { get; set; }

		[DisplayName("Lytis")]
		[Required]
		public int FkLytis { get; set; }

        [DisplayName("Rūšis")]
		[Required]
		public int FkRusis { get; set; }

        [DisplayName("Dydis")]
		[Required]
		public int FkDydis { get; set; }

        /*#nullable enable

        [DisplayName("Pardavimo sutarties kodas")]
		public string? FkSutartis { get; set; }

        #nullable disable*/

        [DisplayName("Parduotuvė")]
		public string FkParduotuve { get; set; }

        [DisplayName("Kaina")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double Kaina { get; set; }
	}
}