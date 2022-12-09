using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
	/// <summary>
	/// Model of 'Klientas' entity.
	/// </summary>
	public class PrekesListVM
	{
		[DisplayName("ID")]
		[Required]
		public int ID { get; set; }
		
		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }

        [DisplayName("Kaina")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double Kaina { get; set; }

		[DisplayName("Aprašymas")]
		[Required]
		public string Aprasymas { get; set; }

        [DisplayName("Patinka paspaudimai")]
        [Required]
        public int Patinka_paspaudimai { get; set; }

		#nullable enable
		[DisplayName("Nuotrauka")]
		public string? Nuotrauka { get; set; }

        [DisplayName("Spalva")]
		public string? Spalva { get; set; }
        [DisplayName("Medžiaga")]
		public string? Medžiaga { get; set; }
        [DisplayName("Matmenys")]
		public string? Matmenys { get; set; }
        [DisplayName("Svoris")]
		public string? Svoris { get; set; }
        [DisplayName("Garantijos trukmė")]
		public string? Garantijos_trukmė { get; set; }
        [DisplayName("Rūbų dydis")]
		public string? dydis { get; set; }

		#nullable disable
        [DisplayName("Kategorija")]
		[Required]
		public string Kategorija { get; set; }

        [DisplayName("Gamybos vieta")]
		[Required]
		public string Gamybos_vieta { get; set; }
		[DisplayName("Savybės")]
		public List<string> Savybes {
			get {
				List<string> arr = new List<string>();
				if (!String.IsNullOrEmpty(Spalva) && Spalva[Spalva.Length-1] != ' ')
					arr.Add(Spalva);
				if (!String.IsNullOrEmpty(Medžiaga) && Medžiaga[Medžiaga.Length-1] != ' ')
					arr.Add(Medžiaga);
				if (!String.IsNullOrEmpty(Matmenys) && Matmenys[Matmenys.Length-1] != ' ')
					arr.Add(Matmenys);
				if (!String.IsNullOrEmpty(Svoris) && Svoris[Svoris.Length-1] != ' ')
					arr.Add(Svoris);
				if (!String.IsNullOrEmpty(Garantijos_trukmė) && Garantijos_trukmė[Garantijos_trukmė.Length-1] != ' ')
					arr.Add(Garantijos_trukmė);
				if (!String.IsNullOrEmpty(dydis) && dydis[dydis.Length-1] != ' ')
					arr.Add(dydis);
				return arr;
			}
		}
	}
}