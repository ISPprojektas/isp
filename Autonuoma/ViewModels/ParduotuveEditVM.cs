using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Modelis' entity used in creation and editing forms.
	/// </summary>
	public class ParduotuveEditVM
	{
		/// <summary>
		/// Entity data
		/// </summary>
		public class ParduotuveM
		{
			[DisplayName("ID")]
			[MinLength(11)]
            [MaxLength(11)]
            [Required]
            public string ID { get; set; }

            [DisplayName("Pavadinimas")]
            [Required]
            public string Pavadinimas { get; set; }

            [DisplayName("Adresas")]
            [Required]
            public string Adresas { get; set; }

            #nullable enable 

            [DisplayName("Telefonas")]  
            [Phone]
            public string? Telefonas { get; set; }

            [DisplayName("Elektroninis paštas")]
            [EmailAddress]
            public string? EPastas { get; set; }
            
            #nullable disable

            //Miestas
            [DisplayName("Miestas")]
            [Required]
            public string FkMiestas { get; set; }
	    }

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Miestai { get; set; }
			public IList<SelectListItem> DarboDienos { get; set; }
		}

		/// <summary>
		/// Entity view.
		/// </summary>
		public ParduotuveM Parduotuve { get; set; } = new ParduotuveM();
		public IList<ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM> DarboDienos { get; set; } = new List<ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM>();
        public IList<int> RemoveValues { get; set; } = new List<int>();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}