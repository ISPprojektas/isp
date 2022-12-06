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
	public class SutartisController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			var pardavimo_sutartys = SutartisRepo.List();
			return View(pardavimo_sutartys);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var SutartisEVM = new SutartisEditVM();
			PopulateSelections(SutartisEVM);
			return View(SutartisEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(SutartisEditVM SutartisEVM)
		{
			var duplicate = SutartisRepo.Exists(SutartisEVM.Sutartis.ID);
			if(duplicate)
			{
				ModelState.First(m => m.Key == "Sutartis.ID").Value.Errors.Add("Duplikatas");
			}
			else
			{
				//form field validation passed?
				if( ModelState.IsValid )
				{
					SutartisRepo.Insert(SutartisEVM);

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
			}

			//form field validation failed, go back to the form
			PopulateSelections(SutartisEVM);
			return View(SutartisEVM);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(string id)
		{
			var SutartisEVM = SutartisRepo.Find(id);
			PopulateSelections(SutartisEVM);

			return View(SutartisEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>
		/// <param name="autoEvm">Entity model filled with latest data.</param>
		/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(string id, SutartisEditVM SutartisEVM)
		{
			//form field validation passed?
			if( ModelState.IsValid )
			{
				SutartisRepo.Update(SutartisEVM);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			PopulateSelections(SutartisEVM);
			return View(SutartisEVM);
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(string id)
		{
			var SutartisEVM = SutartisRepo.FindForDeletion(id);
			return View(SutartisEVM);
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
				SutartisRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var SutartisEVM = SutartisRepo.FindForDeletion(id);

				return View("Delete", SutartisEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateSelections(SutartisEditVM SutartisEVM)
		{
			//load entities for the select lists
			var darbuotojai = DarbuotojasRepo.List();
			var klientai = KlientasRepo.List();
			var gyvunai = GyvunasRepo.List();
			var busenos = SutartiesBusenaRepo.List();

			//build select lists
			SutartisEVM.Lists.Darbuotojai =
				darbuotojai.Select(it => {
					return
						new SelectListItem() {
							Value = it.AsmensKodas,
							Text = it.Vardas + " " + it.Pavarde
						};
				})
				.ToList();

			SutartisEVM.Lists.Klientai =
				klientai.Select(it => {
					return
						new SelectListItem() {
							Value = it.AsmensKodas,
							Text = it.Vardas + " " + it.Pavarde
						};
				})
				.ToList();

			SutartisEVM.Lists.Gyvunai =
				gyvunai.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Vardas
						};
				})
				.ToList();

			SutartisEVM.Lists.Busenos =
				busenos.Select(it => {
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
