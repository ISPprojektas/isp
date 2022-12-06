using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using StoresReport = Org.Ktu.Isk.P175B602.Autonuoma.ViewModels.StoresReport;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers
{
	/// <summary>
	/// Controller for producing reports.
	/// </summary>
	public class AtaskaitaController : Controller
	{
		/// <summary>
		/// Produces contracts report.
		/// </summary>
		/// <param name="dateFrom">Starting date. Can be null.</param>
		/// <param name="dateTo">Ending date. Can be null.</param>
		/// <returns>Report view.</returns>
		#nullable enable
		public ActionResult Report(StoresReport.Ataskaita.Report? report)
		{
			StoresReport.Ataskaita ataskaita = new StoresReport.Ataskaita();
			ataskaita.report = new StoresReport.Ataskaita.Report();
			ataskaita.report.DateFrom = report?.DateFrom;
			ataskaita.report.DateTo = report?.DateTo?.AddHours(23).AddMinutes(59).AddSeconds(59); //move time of end date to end of day

			ataskaita.report.KainaFrom = report?.KainaFrom;
			ataskaita.report.KainaTo = report?.KainaTo;

			ataskaita.report.Rusis = report?.Rusis;

			ataskaita.report.Parduotuves = AtaskaitaRepo.GetStores(ataskaita.report.DateFrom, ataskaita.report.DateTo, ataskaita.report.Rusis, ataskaita.report.KainaFrom, ataskaita.report.KainaTo);

			foreach (var item in ataskaita.report.Parduotuves)
			{
				foreach(var parduotuve in item.Value) 
				{
					ataskaita.report.VisoSumaVaistu += parduotuve.SumaVaistu;
					ataskaita.report.VisoKiekisVaistu += parduotuve.KiekisVaistu;
					ataskaita.report.VisoSumaVakcinaciju += parduotuve.SumaVakcinaciju;
					ataskaita.report.VisoKiekisVakcinaciju += parduotuve.KiekisVakcinaciju;
				}
				
			}
			PopulateLists(ataskaita);

			return View(ataskaita);
		}
		#nullable disable

		public void PopulateLists(StoresReport.Ataskaita ataskaita)
		{
			//load entities for the select lists
            var rusys = GyvunuRusisRepo.List();

           	ataskaita.Lists.Rusys =
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
