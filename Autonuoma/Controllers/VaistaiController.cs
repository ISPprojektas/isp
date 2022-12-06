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
	public class VaistaiController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			var vaistai = VaistaiRepo.List();
			return View(vaistai);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var VaistaiEVM = new VaistaiEditVM();
			PopulateSelections(VaistaiEVM);
			return View(VaistaiEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(VaistaiEditVM VaistaiEVM)
		{
			//form field validation passed?
			if( ModelState.IsValid )
			{
				VaistaiRepo.Insert(VaistaiEVM);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			PopulateSelections(VaistaiEVM);
			return View(VaistaiEVM);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(int id)
		{
			var VaistaiEVM = VaistaiRepo.Find(id);
			
			PopulateSelections(VaistaiEVM);
			return View(VaistaiEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>
		/// <param name="autoEvm">Entity model filled with latest data.</param>
		/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(VaistaiEditVM VaistaiEVM)
		{
			//form field validation passed?
			if( ModelState.IsValid )
			{
				VaistaiRepo.Update(VaistaiEVM);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			PopulateSelections(VaistaiEVM);
			return View(VaistaiEVM);
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(int id)
		{
			var VaistaiEVM = VaistaiRepo.FindForDeletion(id);
			return View(VaistaiEVM);
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
				VaistaiRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var VaistaiEVM = VaistaiRepo.FindForDeletion(id);

				return View("Delete", VaistaiEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateSelections(VaistaiEditVM VaistaiEVM)
		{
			//load entities for the select lists
            var darbuotojai = DarbuotojasRepo.List();
            var gyvunai = GyvunasRepo.List();

			//build select lists
			VaistaiEVM.Lists.Darbuotojai =
				darbuotojai.Select(it => {
					return
						new SelectListItem() {
							Value = it.AsmensKodas,
							Text = it.Vardas + " " + it.Pavarde
						};
				})
				.OrderBy(it => it.Text)
				.ToList();

            VaistaiEVM.Lists.Gyvunai =
				gyvunai.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID,
							Text = it.Vardas
						};
				})
				.OrderBy(it => it.Text)
				.ToList();
		}
	}
}
