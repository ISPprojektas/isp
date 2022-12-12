using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Model;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers
{
	/// <summary>
	/// Controller for working with 'Modelis' entity.
	/// </summary>
	public class KomentarasController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create(int id)
		{
			var komentaras = new Atsiliepimai();
			komentaras.Data = DateTime.Now;
			komentaras.fk_Preke = id;
			return View(komentaras);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(
			int? save,
			Atsiliepimai komentaras)
		{			
			if(save != null)
			{
				AtsiliepimaiRepo.Insert(komentaras);
			}
			return RedirectToAction("IndexOne","Prekes",new { id = komentaras.fk_Preke.ToString()});
		}
	}
}
