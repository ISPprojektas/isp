using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Modelis' entity used in creation and editing forms.
	/// </summary>
	public class SutartisEditVM
	{
		/// <summary>
		/// Entity data
		/// </summary>
		public class SutartisM
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
        public int Busena { get; set; }

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

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Busenos { get; set; }
            public IList<SelectListItem> Darbuotojai { get; set; }
            public IList<SelectListItem> Klientai { get; set; }
            public IList<SelectListItem> Gyvunai { get; set; }
		}

		/// <summary>
		/// Entity view.
		/// </summary>
		public SutartisM Sutartis { get; set; } = new SutartisM();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}