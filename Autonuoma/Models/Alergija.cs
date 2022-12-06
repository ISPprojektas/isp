using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model for 'Alergija' entity.
	/// </summary>
	public class Alergija
	{
		[DisplayName("ID")]
		[Required]
		public int ID { get; set; }

		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }

        [DisplayName("Nustatymo data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime NustatymoData { get; set; }

        [DisplayName("Darbuotojas")]
        [Required]
        public string FkDarbuotojas { get; set; }

        public string FkDarbuotojasToString { get; set; }

        [DisplayName("Gyvūnas")]
        [Required]
        public string FkGyvunas { get; set; }

        public string FkGyvunasToString { get; set; }
	}
}