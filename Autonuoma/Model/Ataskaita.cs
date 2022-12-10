using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
	/// <summary>
	/// View model for single contract in a report.
	/// </summary>
	
	public class Ataskaita
	{

		/// <summary>
		/// View model for whole report.
		/// </summary>
		public class Filters
		{
			[DataType(DataType.DateTime)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
			public DateTime? DateFrom { get; set; }

			[DataType(DataType.DateTime)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
			public DateTime? DateTo { get; set; }

			[DataType(DataType.Currency)]
			public double? KainaFrom { get; set; }
			[DataType(DataType.Currency)]
			public double? KainaTo { get; set; }
			public string Apmoketas { get; set; }
		}
		public Ataskaita()
		{

		}

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Rusys { get; set; }
		}

		/// <summary>
		/// Entity view.
		/// </summary>
		public Filters filters { get; set; } = new Filters();
		//public Ataskaita ataskaita {get;set;} = new Ataskaita();
		public List<Uzsakymai> uzsakymai = new List<Uzsakymai>();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}