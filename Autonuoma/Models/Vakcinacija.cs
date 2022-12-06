using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model for 'Vakcinacija' entity.
	/// </summary>
	public class Vakcinacija
	{
		[DisplayName("ID")]
		[Required]
		public int ID { get; set; }
		
		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }

        [DisplayName("Vakcinacijos data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime NustatymoData { get; set; }

		[DisplayName("Kaina")]
		[Required]
		public double Kaina { get; set; }

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