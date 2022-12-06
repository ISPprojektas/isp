using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model for 'Vaistai' entity.
	/// </summary>
	public class Vaistai
	{
        [DisplayName("ID")]
		[Required]
		public int ID { get; set; }
        
		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }

        [DisplayName("Išrašymo data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime IsrasymoData { get; set; }

        [DisplayName("Kaina")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
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