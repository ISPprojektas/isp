using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Modelis' entity used in creation and editing forms.
	/// </summary>
	public class DarbuotojoDarboDienaEditVM
	{
		/// <summary>
		/// Entity data
		/// </summary>
		public class DarbuotojoDarboDienaM
		{
			[DisplayName("ID")]
			[Required]
			public int ID { get; set; }

            [DisplayName("Darbo pradžia")]
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
            [Required]
            public TimeSpan DarboPradzia { get; set; }

            [DisplayName("Darbo pabaiga")]
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
            [Required]
            public TimeSpan DarboPabaiga { get; set; }

            [DisplayName("Savaitės diena")]
            [Required]
            public int FkDarboDiena { get; set; }

            [DisplayName("Darbuotojas")]
            [Required]
            public string FkDarbuotojas { get; set; }
	    }

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> DarboDienos { get; set; }
            public IList<SelectListItem> Darbuotojai { get; set; }
		}

		/// <summary>
		/// Entity view.
		/// </summary>
		public DarbuotojoDarboDienaM DarbuotojoDarboDiena { get; set; } = new DarbuotojoDarboDienaM();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}