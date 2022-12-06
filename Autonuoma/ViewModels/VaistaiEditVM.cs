using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Modelis' entity used in creation and editing forms.
	/// </summary>
	public class VaistaiEditVM
	{
		/// <summary>
		/// Entity data
		/// </summary>
		public class VaistaiM
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

            [DisplayName("Kaina")]
            [Required]
            [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
            public double Kaina { get; set; }

            [DisplayName("Darbuotojas")]
            [Required]
            public string FkDarbuotojas { get; set; }

            [DisplayName("Gyvūnasss")]
            public string FkGyvunas { get; set; }
	    }

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Gyvunai { get; set; }
            public IList<SelectListItem> Darbuotojai { get; set; }
		}

		/// <summary>
		/// Entity view.
		/// </summary>
		public VaistaiM Vaistai { get; set; } = new VaistaiM();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}