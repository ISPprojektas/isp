using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Modelis' entity used in creation and editing forms.
	/// </summary>
	public class DarbuotojasEditVM
	{
		/// <summary>
		/// Entity data
		/// </summary>
		public class DarbuotojasM
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

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Lytys { get; set; }
            public IList<SelectListItem> Miestai { get; set; }
            public IList<SelectListItem> Darbovietes { get; set; }
            public IList<SelectListItem> DarboDienos { get; set; }
		}

		/// <summary>
		/// Entity view.
		/// </summary>
		public DarbuotojasM Darbuotojas { get; set; } = new DarbuotojasM();
        public IList<DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM> DarboDienos { get; set; } = new List<DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM>();
        public IList<int> RemoveValues { get; set; } = new List<int>();
		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}