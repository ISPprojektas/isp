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
	public class UzsakymasController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			var uzsakymai = UzsakymaiRepo.List();
			return View(uzsakymai);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(KlientasEditVM klientasEVM)
		{
			return View();
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		/*public ActionResult Edit(int id)
		{
			
		}*/

		public ActionResult Edit(int id, int? save, Uzsakymai Uzsakymas)
		{
			if(save != null)
			{
				//form field validation passed?
				if( ModelState.IsValid )
				{
					UzsakymaiRepo.Update(Uzsakymas);

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
				else
				{
					PopulateLists(Uzsakymas);
					return View(Uzsakymas);
				}
			}
			else
			{
				var uzsakymas = UzsakymaiRepo.Find(id);
				PopulateLists(uzsakymas);

				return View(uzsakymas);
			}
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(int id)
		{
			var UzsakymaiEVM = UzsakymaiRepo.Find(id);
			return View(UzsakymaiEVM);
		}

		/// <summary>
		/// This is invoked when deletion is confirmed in deletion form
		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view on error, redirects to Index on success.</returns>
		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			//try deleting, this will fail if foreign key constraint fails
			try
			{
				UzsakymaiRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var UzsakymaiEVM = UzsakymaiRepo.Find(id);

				return View("Delete", UzsakymaiEVM);
			}
		}

		public void PopulateLists(Uzsakymai uzsakymai)
		{
			var busenos = UzsakymuBusenosRepo.List();

			uzsakymai.Lists.Busenos =
				busenos.Select(it => {
					return
						new SelectListItem() {
							Value = it.Id.ToString(),
							Text = it.Pavadinimas
						};
				}).ToList();
		}
	}
}