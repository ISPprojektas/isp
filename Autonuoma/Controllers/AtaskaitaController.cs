using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Model;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;
using Rotativa.AspNetCore;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers
{
	/// <summary>
	/// Controller for working with 'Modelis' entity.
	/// </summary>
	public class AtaskaitaController : Controller
	{


		[HttpPost]
		public ActionResult PDF(Ataskaita ataskaita)
		{
			ataskaita.uzsakymai = UzsakymaiRepo.List();
			if(ataskaita.filters.DateFrom != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.UzsakymoLaikas >= ataskaita.filters.DateFrom).ToList();
			}
			if(ataskaita.filters.DateTo != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.UzsakymoLaikas <= ataskaita.filters.DateTo).ToList();
			}
			if(ataskaita.filters.KainaFrom != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.GalineKaina() >= ataskaita.filters.KainaFrom).ToList();
			}
			if(ataskaita.filters.KainaTo != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.GalineKaina() <= ataskaita.filters.KainaTo).ToList();
			}
			if(ataskaita.filters.Apmoketas == "Apmoketas")
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.BusenaToString != "Sukurtas").ToList();
			}
			if(ataskaita.filters.Apmoketas == "Neapmoketas")
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.BusenaToString == "Sukurtas").ToList();
			}
			return new Rotativa.AspNetCore.ViewAsPdf("Report", ataskaita)
			{
				PageSize = Rotativa.AspNetCore.Options.Size.A4,
				FileName = "Report.pdf"
			};
		}

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Generate()
		{
			var a = new Ataskaita();
			a.uzsakymai = UzsakymaiRepo.List();
			return View(a);
		}

		#nullable enable
		[HttpPost]
		public ActionResult Report(Ataskaita ataskaita)
		{
			ataskaita.uzsakymai = UzsakymaiRepo.List();
			if(ataskaita.filters.DateFrom != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.UzsakymoLaikas >= ataskaita.filters.DateFrom).ToList();
			}
			if(ataskaita.filters.DateTo != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.UzsakymoLaikas <= ataskaita.filters.DateTo).ToList();
			}
			if(ataskaita.filters.KainaFrom != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.GalineKaina() >= ataskaita.filters.KainaFrom).ToList();
			}
			if(ataskaita.filters.KainaTo != null)
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.GalineKaina() <= ataskaita.filters.KainaTo).ToList();
			}
			if(ataskaita.filters.Apmoketas == "Apmoketas")
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.BusenaToString != "Sukurtas").ToList();
			}
			if(ataskaita.filters.Apmoketas == "Neapmoketas")
			{
				ataskaita.uzsakymai = ataskaita.uzsakymai.Where(u => u.BusenaToString == "Sukurtas").ToList();
			}
			return View(ataskaita);
		}
		#nullable disable

		public void PopulateLists(Ataskaita a)
		{
			//load entities for the select lists
            var rusys = GyvunuRusisRepo.List();

           	a.Lists.Rusys =
				rusys.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();
		}
	}
}
