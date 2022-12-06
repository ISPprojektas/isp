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
	public class KlientasController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			var klientai = KlientasRepo.List();
			return View(klientai);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var klientasEVM = new KlientasEditVM();
			PopulateSelections(klientasEVM);
			return View(klientasEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(KlientasEditVM klientasEVM)
		{
			var duplicate = KlientasRepo.Exists(klientasEVM.Klientas.AsmensKodas);
			if(duplicate)
			{
				ModelState.First(m => m.Key == "Klientas.AsmensKodas").Value.Errors.Add("Duplikatas");
			}
			else
			{
				//form field validation passed?
				if( ModelState.IsValid )
				{
					KlientasRepo.Insert(klientasEVM);

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
			}
			//form field validation failed, go back to the form
			PopulateSelections(klientasEVM);
			return View(klientasEVM);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(string id)
		{
			var klientasEVM = KlientasRepo.Find(id);
			PopulateSelections(klientasEVM);

			return View(klientasEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>
		/// <param name="autoEvm">Entity model filled with latest data.</param>
		/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(string id, KlientasEditVM klientasEVM)
		{
			//form field validation passed?
			if( ModelState.IsValid )
			{
				KlientasRepo.Update(klientasEVM);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			PopulateSelections(klientasEVM);
			return View(klientasEVM);
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(string id)
		{
			var klientasEVM = KlientasRepo.FindForDeletion(id);
			return View(klientasEVM);
		}

		/// <summary>
		/// This is invoked when deletion is confirmed in deletion form
		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view on error, redirects to Index on success.</returns>
		[HttpPost]
		public ActionResult DeleteConfirm(string id)
		{
			//try deleting, this will fail if foreign key constraint fails
			try
			{
				KlientasRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var klientasEVM = KlientasRepo.FindForDeletion(id);

				return View("Delete", klientasEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateSelections(KlientasEditVM klientasEVM)
		{
			//load entities for the select lists
			var miestai = MiestasRepo.List();
			var lytys = LytisRepo.List();

			//build select lists
			klientasEVM.Lists.Miestai =
				miestai.Select(it => {
					return
						new SelectListItem() {
							Value = it.Pavadinimas,
							Text = it.Pavadinimas
						};
				})
				.ToList();

			klientasEVM.Lists.Lytys =
				lytys.Select(it => {
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
