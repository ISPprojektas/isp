using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers
{
	/// <summary>
	/// Controller for working with 'Modelis' entity.
	/// </summary>
	public class DarbuotojoDarboDienaController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			var darbuotojoDarboDienos = DarbuotojoDarboDienaRepo.List();
			return View(darbuotojoDarboDienos);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var DarbuotojoDarboDienaEVM = new DarbuotojoDarboDienaEditVM();
			PopulateSelections(DarbuotojoDarboDienaEVM);
			return View(DarbuotojoDarboDienaEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(DarbuotojoDarboDienaEditVM DarbuotojoDarboDienaEVM)
		{
			//form field validation passed?
			if( ModelState.IsValid )
			{
				DarbuotojoDarboDienaRepo.Insert(DarbuotojoDarboDienaEVM);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			PopulateSelections(DarbuotojoDarboDienaEVM);
			return View(DarbuotojoDarboDienaEVM);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(int id)
		{
			var DarbuotojoDarboDienaEVM = DarbuotojoDarboDienaRepo.Find(id);
			
			PopulateSelections(DarbuotojoDarboDienaEVM);
			return View(DarbuotojoDarboDienaEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>
		/// <param name="autoEvm">Entity model filled with latest data.</param>
		/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(DarbuotojoDarboDienaEditVM DarbuotojoDarboDienaEVM)
		{
			//form field validation passed?
			if( ModelState.IsValid )
			{
				DarbuotojoDarboDienaRepo.Update(DarbuotojoDarboDienaEVM);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			PopulateSelections(DarbuotojoDarboDienaEVM);
			return View(DarbuotojoDarboDienaEVM);
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(int id)
		{
			var DarbuotojoDarboDienaEVM = DarbuotojoDarboDienaRepo.FindForDeletion(id);
			return View(DarbuotojoDarboDienaEVM);
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
				DarbuotojoDarboDienaRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var DarbuotojoDarboDienaEVM = DarbuotojoDarboDienaRepo.FindForDeletion(id);

				return View("Delete", DarbuotojoDarboDienaEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateSelections(DarbuotojoDarboDienaEditVM DarbuotojoDarboDienaEVM)
		{
			//load entities for the select lists
			var darbo_dienos = DarboDienaRepo.List();
            var darbuotojai = DarbuotojasRepo.List();

			//build select lists
			DarbuotojoDarboDienaEVM.Lists.DarboDienos =
				darbo_dienos.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

			DarbuotojoDarboDienaEVM.Lists.Darbuotojai =
				darbuotojai.Select(it => {
					return
						new SelectListItem() {
							Value = it.AsmensKodas,
							Text = it.Vardas + " " + it.Pavarde
						};
				})
				.OrderBy(it => it.Text)
				.ToList();
		}
	}
}
