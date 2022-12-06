using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels.StoresReport
{
	/// <summary>
	/// View model for single contract in a report.
	/// </summary>
	
	public class Ataskaita
	{
		public class Parduotuve
		{
			[DisplayName("Gyvūno ID")]
			[MinLength(11)]
			[MaxLength(11)]
			[Required]
			public string GyvunoID { get; set; }
			
			[DisplayName("Gyvūno vardas")]
			[Required]
			public string GyvunoVardas { get; set; }

			[DisplayName("Gyvūno gimimo data")]
			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
			[Required]
			public DateTime? GimimoData { get; set; }

			[DisplayName("Gyvūno svoris")]
			[Required]
			[DisplayFormat(DataFormatString = "{0:n2} kg", ApplyFormatInEditMode = true)]
			public double Svoris { get; set; }

			[DisplayName("Gyvūno lytis")]
			[Required]
			public string FkLytisToString { get; set; }
			public int FkLytis { get; set; }

			[DisplayName("Gyvūno rūšis")]
			[Required]
			public string FkRusisToString { get; set; }
			public int FkRusis { get; set; }

			[DisplayName("Gyvūno dydis")]
			[Required]
			public string FkDydisToString { get; set; }
			public int FkDydis { get; set; }

			[DisplayName("Parduotuvės ID")]
			public string FkParduotuve { get; set; }
			
			[DisplayName("Parduotuvės pavadinimas")]
			public string FkParduotuveToString { get; set; }

			[DisplayName("Kaina")]
			[Required]
			[DataType(DataType.Currency)]
			public double Kaina { get; set; }

			#nullable enable

			[DisplayName("Pardavimo sutarties kodas")]
			public string? FkSutartis { get; set; }

			[DisplayName("Kliento kodas")]
			public string? FkKlientas { get; set; }

			[DisplayName("Kliento vardas")]
			public string? FkKlientasToString { get; set; }

			#nullable disable

			[DataType(DataType.Currency)]
			[DisplayName("Vaistų suma")]
			public double SumaVaistu { get; set; }
			[DataType(DataType.Currency)]
			[DisplayName("Vakcinacijų suma")]
			public double SumaVakcinaciju { get; set; }
			[DisplayName("Vaistų kiekis")]
			public int KiekisVaistu { get; set; }
			[DisplayName("Vakcinacijų kiekis")]
			public int KiekisVakcinaciju { get; set; }
			[DataType(DataType.Currency)]
			public double BendraSumaVaistu { get; set; }
			[DataType(DataType.Currency)]
			public double BendraSumaVakcinaciju { get; set; }
			public int BendraKiekisVaistu { get; set; }
			public int BendraKiekisVakcinaciju { get; set; }
			[DisplayName("Dienos")]
			public int Dienos { get; set; }
		}

		/// <summary>
		/// View model for whole report.
		/// </summary>
		public class Report
		{
			[DataType(DataType.DateTime)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
			public DateTime? DateFrom { get; set; }

			[DataType(DataType.DateTime)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
			public DateTime? DateTo { get; set; }

			public int? Rusis { get; set; }
			#nullable enable
			public string? RusisToString { get; set; }
			#nullable disable

			[DataType(DataType.Currency)]
			public double? KainaFrom { get; set; }
			[DataType(DataType.Currency)]
			public double? KainaTo { get; set; }

			public Dictionary<string, List<Parduotuve>> Parduotuves { get; set; }

			[DataType(DataType.Currency)]
			public double VisoSumaVaistu { get; set; }
			[DataType(DataType.Currency)]
			public double VisoSumaVakcinaciju { get; set; }
			public int VisoKiekisVaistu { get; set; }
			public int VisoKiekisVakcinaciju { get; set; }
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
		public Report report { get; set; } = new Report();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}
}