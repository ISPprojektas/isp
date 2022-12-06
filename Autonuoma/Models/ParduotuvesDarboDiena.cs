using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model for 'GyvunuDydis' entity.
	/// </summary>
	public class ParduotuvesDarboDiena
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

		public string FkDarboDienaToString { get; set; }

        [DisplayName("Parduotuvė")]
		[Required]
		public string FkParduotuve { get; set; }

		public string FkParduotuveToString { get; set; }
	}
}