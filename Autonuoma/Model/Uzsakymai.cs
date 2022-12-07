using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class Uzsakymai
    {
        [DisplayName("Užsakymo laikas")]
		[Required]
        public DateTime UzsakymoLaikas { get; set; }

        [DisplayName("Užsakymo kaina")]
		[Required]
        public double UzsakymoKaina { get; set; }

        [DisplayName("Apmokėjimo laikas")]
        public DateTime ApmokejimoLaikas { get; set; }

        [DisplayName("Nuolaida")]
        public double Nuolaida { get; set; }

        [DisplayName("Būsena")]
		[Required]
        public int Busena { get; set; }

        [DisplayName("Parduotuvė")]
		[Required]
        public string BusenaToString { get; set; }

        [DisplayName("ID")]
		[Required]
        public int pk_Id { get; set; }

        [DisplayName("Naudotojas")]
		[Required]
        public int fk_Naudotojas { get; set; }

        [DisplayName("Parduotuvė")]
		[Required]
        public int fk_Parduotuve { get; set; }

        [DisplayName("Naudotojas")]
		[Required]
        public string fk_NaudotojasToString { get; set; }

        [DisplayName("Parduotuvė")]
		[Required]
        public string fk_ParduotuveToString { get; set; }

        public List<UzsakymoPrekes> uzsakymoPrekes = new List<UzsakymoPrekes>();
        public List<UzsakymoBusenos> busenos = new List<UzsakymoBusenos>();

        public class ListsM
		{
			public IList<SelectListItem> Busenos { get; set; }
		}
        public ListsM Lists { get; set; } = new ListsM();
    }
}
