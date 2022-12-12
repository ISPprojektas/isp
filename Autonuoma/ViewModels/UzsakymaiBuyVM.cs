using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class UzsakymaiBuyVM
    {
        public class UzsakymaiM {
             [DisplayName("Būsena")]
            public int Busena { get; set; }

            [DisplayName("ID")]
            public int pk_Id { get; set; }
            
            [DisplayName("Mokėjimo metodas")]
            public bool payment_method_selected { get; set; }

            [DisplayName("Kortelės Nr.")]
            [Required]
            public string korteles_nr { get; set; }

            [DisplayName("Parduotuvė")]
            [Required]
            public int fk_Parduotuve { get; set; }

            public List<UzsakymoPrekes> uzsakymoPrekes = new List<UzsakymoPrekes>();
            public List<UzsakymoBusenos> busenos = new List<UzsakymoBusenos>();
        }
       

        public class ListsM
		{
			public IList<SelectListItem> Busenos { get; set; }
            public IList<SelectListItem> Parduotuves { get; set; }
		}
        public ListsM Lists { get; set; } = new ListsM();
        
		/// <summary>
		/// Entity view.
		/// </summary>
		public UzsakymaiM Uzsakymai { get; set; } = new UzsakymaiM();
    }
}
