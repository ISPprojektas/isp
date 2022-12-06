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
	public class DarbuotojasController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			TempData.Clear();
			var gyvunai = DarbuotojasRepo.List();
			return View(gyvunai);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var DarbuotojasEVM = new DarbuotojasEditVM();
			PopulateLists(DarbuotojasEVM);
			return View(DarbuotojasEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(
			int? save, 
			int? add, int? remove,
			DarbuotojasEditVM DarbuotojasEVM)
		{
			if(add != null)
			{
				var a = new DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM {
					DarboPradzia = TimeSpan.MinValue,
					DarboPabaiga = TimeSpan.MaxValue,
					FkDarboDiena = 1,
					FkDarbuotojas = DarbuotojasEVM.Darbuotojas.AsmensKodas
				};
				ModelState.Clear();
				PopulateLists(DarbuotojasEVM, a);
				return View(DarbuotojasEVM);
			}
			if(remove != null)
			{
				if(TempData["dre"] != null)
				{
					DarbuotojasEVM.RemoveValues = TempData["dre"] as List<int>;
					if(DarbuotojasEVM.RemoveValues == null)
					{
						DarbuotojasEVM.RemoveValues = (TempData["dre"] as int[]).ToList();
						if(DarbuotojasEVM.RemoveValues == null)
							DarbuotojasEVM.RemoveValues = new List<int>();
					}
				}
				DarbuotojasEVM.RemoveValues.Add(remove.Value);
				TempData["dre"] = DarbuotojasEVM.RemoveValues as List<int>;
				DarbuotojasEVM.DarboDienos =
					DarbuotojasEVM
						.DarboDienos
						.Where(it => !DarbuotojasEVM.RemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(DarbuotojasEVM);
				return View(DarbuotojasEVM);
			}
			if(save != null)
			{
				var duplicate = DarbuotojasRepo.Exists(DarbuotojasEVM.Darbuotojas.AsmensKodas);
				if(duplicate)
				{
					ModelState.First(m => m.Key == "Darbuotojas.AsmensKodas").Value.Errors.Add("Duplikatas");
				}
				else
				{
					
					if( ModelState.IsValid )
					{
						DarbuotojasEVM.RemoveValues.Clear();
						TempData.Remove("dre");
						DarbuotojasRepo.Insert(DarbuotojasEVM);
						foreach(var darboDiena in DarbuotojasEVM.DarboDienos)
						{
							DarbuotojoDarboDienaRepo.Insert(darboDiena);
						}


						//save success, go back to the entity list
						return RedirectToAction("Index");
					}
				}
				//form field validation passed?
				
			}
			PopulateLists(DarbuotojasEVM);
			return View(DarbuotojasEVM);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(string id)
		{
			var DarbuotojasEVM = DarbuotojasRepo.Find(id);
			PopulateLists(DarbuotojasEVM);

			return View(DarbuotojasEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>
		/// <param name="autoEvm">Entity model filled with latest data.</param>
		/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(
			string id, int? save,
			int? add, int? remove,
			DarbuotojasEditVM DarbuotojasEVM)
		{
			if(add != null)
			{
				var a = new DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM {
					DarboPradzia = TimeSpan.MinValue,
					DarboPabaiga = TimeSpan.MaxValue,
					FkDarboDiena = 1,
					FkDarbuotojas = DarbuotojasEVM.Darbuotojas.AsmensKodas
				};
				ModelState.Clear();
				PopulateLists(DarbuotojasEVM, a);
				return View(DarbuotojasEVM);
			}
			if(remove != null)
			{
				if(TempData["dre"] != null)
				{
					DarbuotojasEVM.RemoveValues = TempData["dre"] as List<int>;
					TempData.Keep();
					if(DarbuotojasEVM.RemoveValues == null)
					{
						DarbuotojasEVM.RemoveValues = (TempData["dre"] as int[]).ToList();
						if(DarbuotojasEVM.RemoveValues == null)
							DarbuotojasEVM.RemoveValues = new List<int>();
					}
				}
				DarbuotojasEVM.RemoveValues.Add(remove.Value);
				TempData["dre"] = DarbuotojasEVM.RemoveValues as List<int>;
				TempData.Keep();
				DarbuotojasEVM.DarboDienos =
					DarbuotojasEVM
						.DarboDienos
						.Where(it => !DarbuotojasEVM.RemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(DarbuotojasEVM);
				return View(DarbuotojasEVM);
			}
			if(save != null)
			{
				//form field validation passed?
				if( ModelState.IsValid )
				{
					DarbuotojasEVM.RemoveValues.Clear();
					TempData.Remove("dre");
					DarbuotojasRepo.Update(DarbuotojasEVM);
					DarbuotojoDarboDienaRepo.DeleteForDarbuotojas(DarbuotojasEVM.Darbuotojas.AsmensKodas);

					foreach(var darboDiena in DarbuotojasEVM.DarboDienos)
					{
						DarbuotojoDarboDienaRepo.Insert(darboDiena);
					}

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
				else
				{
					PopulateLists(DarbuotojasEVM, save: true);
					return View(DarbuotojasEVM);
				}
			}
			throw new Exception("Should not reach here.");
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(string id)
		{
			var DarbuotojasEVM = DarbuotojasRepo.FindForDeletion(id);
			return View(DarbuotojasEVM);
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
				DarbuotojasRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var DarbuotojasEVM = DarbuotojasRepo.FindForDeletion(id);

				return View("Delete", DarbuotojasEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateLists(
			DarbuotojasEditVM DarbuotojasEVM,
			DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM d = null,
			bool save = false)
		{
			//load entities for the select lists
			var miestai = MiestasRepo.List();
			var lytys = LytisRepo.List();
			var parduotuves = ParduotuveRepo.List();
			var savaitesDienos = DarboDienaRepo.List();

			//build select lists
			DarbuotojasEVM.Lists.Miestai =
				miestai.Select(it => {
					return
						new SelectListItem() {
							Value = it.Pavadinimas,
							Text = it.Pavadinimas
						};
				})
				.ToList();

			DarbuotojasEVM.Lists.Lytys =
				lytys.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

			DarbuotojasEVM.Lists.Darbovietes =
				parduotuves.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

			DarbuotojasEVM.Lists.DarboDienos =
				savaitesDienos.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.OrderBy(it => it.Value)
				.ToList();

			if(save)
			{
				DarbuotojasEVM.DarboDienos = new List<DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM>();
			}
			else
			{
				if(TempData["dre"] != null)
				{
					TempData.Keep();
					DarbuotojasEVM.RemoveValues = TempData["dre"] as List<int>;
					TempData.Keep();
					if(DarbuotojasEVM.RemoveValues == null)
					{
						DarbuotojasEVM.RemoveValues = (TempData["dre"] as int[]).ToList();
						if(DarbuotojasEVM.RemoveValues == null)
							DarbuotojasEVM.RemoveValues = new List<int>();
					}
				}
			}

			var darboDienos = DarbuotojoDarboDienaRepo.List();

			foreach(var darboDiena in darboDienos)
			{
				if(darboDiena.FkDarbuotojas == DarbuotojasEVM.Darbuotojas.AsmensKodas && !DarbuotojasEVM.RemoveValues.Contains(darboDiena.ID)
				&& DarbuotojasEVM.DarboDienos.Where(it => it.ID == darboDiena.ID).ToList().Count == 0)
				{
					DarbuotojasEVM.DarboDienos.Add(new DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM {
						ID = darboDiena.ID,
						DarboPradzia = darboDiena.DarboPradzia,
						DarboPabaiga = darboDiena.DarboPabaiga,
						FkDarboDiena = darboDiena.FkDarboDiena,
						FkDarbuotojas = darboDiena.FkDarbuotojas
					});
				}
			}
			if(d != null)
			{
				DarbuotojasEVM.DarboDienos.Add(d);
			}
			DarbuotojasEVM.DarboDienos = DarbuotojasEVM.DarboDienos.OrderBy(it => it.FkDarboDiena).ToList();
		}
	}
}

