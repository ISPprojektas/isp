using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Klientas' entity.
	/// </summary>
	public class SutartisListVM
	{
		[DisplayName("ID")]
        [MinLength(11)]
        [MaxLength(11)]
		[Required]
		public string ID { get; set; }
		
		[DisplayName("Data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime? Data { get; set; }

        [DisplayName("Kaina")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Kaina { get; set; }

        [DisplayName("Busena")]
        [Required]
        public string Busena { get; set; }

        [DisplayName("Darbuotojas")]
        [Required]
		public string FkDarbuotojas { get; set; }

        [DisplayName("Klientas")]
        [Required]
		public string FkKlientas { get; set; }

        [DisplayName("Gyvūnas")]
        [Required]
		public string FkGyvunas { get; set; }
	}
}