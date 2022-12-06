using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Modelis' entity used in creation and editing forms.
	/// </summary>
	public class GyvunasEditVM
	{
		/// <summary>
		/// Entity data
		/// </summary>
		public class GyvunasM
		{
            [DisplayName("ID")]
            [MinLength(11)]
            [MaxLength(11)]
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

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Lytys { get; set; }
            public IList<SelectListItem> Rusys { get; set; }
            public IList<SelectListItem> Dydziai { get; set; }
            public IList<SelectListItem> Sutartys { get; set; }
            public IList<SelectListItem> Parduotuves { get; set; }
            public IList<SelectListItem> Darbuotojai { get; set; }
		}

		/// <summary>
		/// Entity view.
		/// </summary>
		public GyvunasM Gyvunas { get; set; } = new GyvunasM();

        public IList<AlergijaEditVM.AlergijaM> Alergijos { get; set; } = new List<AlergijaEditVM.AlergijaM>();
        public IList<VaistaiEditVM.VaistaiM> Vaistai { get; set; } = new List<VaistaiEditVM.VaistaiM>();
        public IList<VakcinacijaEditVM.VakcinacijaM> Vakcinacijos { get; set; } = new List<VakcinacijaEditVM.VakcinacijaM>();
        public IList<int> AlergijaRemoveValues { get; set; } = new List<int>();
        public IList<int> VaistaiRemoveValues { get; set; } = new List<int>();
        public IList<int> VakcinacijaRemoveValues { get; set; } = new List<int>();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}