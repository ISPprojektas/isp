using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
	/// <summary>
	/// Model of 'SutartiesBusena' entity.
	/// </summary>
	public class SutartiesBusena
	{
		[DisplayName("ID")]
		[Required]
		public int ID { get; set; }
		
		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }
	}
}