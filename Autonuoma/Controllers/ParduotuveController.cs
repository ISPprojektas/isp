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
	public class ParduotuveController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			TempData.Clear();
			var parduotuves = ParduotuveRepo.List();
			return View(parduotuves);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var ParduotuveEVM = new ParduotuveEditVM();
			PopulateLists(ParduotuveEVM);
			return View(ParduotuveEVM);
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
			ParduotuveEditVM ParduotuveEVM)
		{
			if(add != null)
			{
				var a = new ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM {
					DarboPradzia = TimeSpan.MinValue,
					DarboPabaiga = TimeSpan.MaxValue,
					FkDarboDiena = 1,
					FkParduotuve = ParduotuveEVM.Parduotuve.ID
				};
				ModelState.Clear();
				PopulateLists(ParduotuveEVM, a);
				return View(ParduotuveEVM);
			}
			if(remove != null)
			{
				if(TempData["pre"] != null)
				{
					ParduotuveEVM.RemoveValues = TempData["pre"] as List<int>;
					if(ParduotuveEVM.RemoveValues == null)
					{
						ParduotuveEVM.RemoveValues = (TempData["pre"] as int[]).ToList();
						if(ParduotuveEVM.RemoveValues == null)
							ParduotuveEVM.RemoveValues = new List<int>();
					}
				}
				ParduotuveEVM.RemoveValues.Add(remove.Value);
				TempData["pre"] = ParduotuveEVM.RemoveValues as List<int>;
				ParduotuveEVM.DarboDienos =
					ParduotuveEVM
						.DarboDienos
						.Where(it => !ParduotuveEVM.RemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(ParduotuveEVM);
				return View(ParduotuveEVM);
			}
			if(save != null)
			{
				var duplicate = ParduotuveRepo.Exists(ParduotuveEVM.Parduotuve.ID);
				if(duplicate)
				{
					ModelState.First(m => m.Key == "Parduotuve.ID").Value.Errors.Add("Duplikatas");
				}
				else
				{
					

					//form field validation passed?
					if( ModelState.IsValid )
					{
						ParduotuveEVM.RemoveValues.Clear();
						TempData.Remove("pre");
						ParduotuveRepo.Insert(ParduotuveEVM);
						foreach(var darboDiena in ParduotuveEVM.DarboDienos)
						{
							ParduotuvesDarboDienaRepo.Insert(darboDiena);
						}

						//save success, go back to the entity list
						return RedirectToAction("Index");
					}
				}
			}
			PopulateLists(ParduotuveEVM);
						return View(ParduotuveEVM);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(string id)
		{
			var ParduotuveEVM = ParduotuveRepo.Find(id);
			PopulateLists(ParduotuveEVM);

			return View(ParduotuveEVM);
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
			ParduotuveEditVM ParduotuveEVM)
		{
			if(add != null)
			{
				var a = new ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM {
					DarboPradzia = TimeSpan.MinValue,
					DarboPabaiga = TimeSpan.MaxValue,
					FkDarboDiena = 1,
					FkParduotuve = ParduotuveEVM.Parduotuve.ID
				};
				ModelState.Clear();
				PopulateLists(ParduotuveEVM, a);
				return View(ParduotuveEVM);
			}
			if(remove != null)
			{
				if(TempData["pre"] != null)
				{
					ParduotuveEVM.RemoveValues = TempData["pre"] as List<int>;
					TempData.Keep();
					if(ParduotuveEVM.RemoveValues == null)
					{
						ParduotuveEVM.RemoveValues = (TempData["pre"] as int[]).ToList();
						if(ParduotuveEVM.RemoveValues == null)
							ParduotuveEVM.RemoveValues = new List<int>();
					}
				}
				ParduotuveEVM.RemoveValues.Add(remove.Value);
				TempData["pre"] = ParduotuveEVM.RemoveValues as List<int>;
				TempData.Keep();
				ParduotuveEVM.DarboDienos =
					ParduotuveEVM
						.DarboDienos
						.Where(it => !ParduotuveEVM.RemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(ParduotuveEVM);
				return View(ParduotuveEVM);
			}
			if(save != null)
			{
				//form field validation passed?
				if( ModelState.IsValid )
				{
					ParduotuveEVM.RemoveValues.Clear();
					TempData.Remove("pre");
					ParduotuveRepo.Update(ParduotuveEVM);
					ParduotuvesDarboDienaRepo.DeleteForParduotuve(ParduotuveEVM.Parduotuve.ID);

					foreach(var darboDiena in ParduotuveEVM.DarboDienos)
					{
						ParduotuvesDarboDienaRepo.Insert(darboDiena);
					}

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
				else
				{
					PopulateLists(ParduotuveEVM, save: true);
					return View(ParduotuveEVM);
				}
			}
			throw new Exception("Should not reach here.");
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(string id)
		{
			var ParduotuveEVM = ParduotuveRepo.FindForDeletion(id);
			return View(ParduotuveEVM);
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
				ParduotuveRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var ParduotuveEVM = ParduotuveRepo.FindForDeletion(id);

				return View("Delete", ParduotuveEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateLists(
			ParduotuveEditVM ParduotuveEVM,
			ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM d = null,
			bool save = false)
		{
			//load entities for the select lists
			var miestai = MiestasRepo.List();
			var savaitesDienos = DarboDienaRepo.List();

			//build select lists
			ParduotuveEVM.Lists.Miestai =
				miestai.Select(it => {
					return
						new SelectListItem() {
							Value = it.Pavadinimas,
							Text = it.Pavadinimas
						};
				})
				.ToList();

			ParduotuveEVM.Lists.DarboDienos =
				savaitesDienos.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

			if(save)
			{
				ParduotuveEVM.DarboDienos = new List<ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM>();
			}
			else
			{
				if(TempData["pre"] != null)
				{
					TempData.Keep();
					ParduotuveEVM.RemoveValues = TempData["pre"] as List<int>;
					TempData.Keep();
					if(ParduotuveEVM.RemoveValues == null)
					{
						ParduotuveEVM.RemoveValues = (TempData["pre"] as int[]).ToList();
						if(ParduotuveEVM.RemoveValues == null)
							ParduotuveEVM.RemoveValues = new List<int>();
					}
				}
			}

			var darboDienos = ParduotuvesDarboDienaRepo.List();

			foreach(var darboDiena in darboDienos)
			{
				if(darboDiena.FkParduotuve == ParduotuveEVM.Parduotuve.ID && !ParduotuveEVM.RemoveValues.Contains(darboDiena.ID)
				&& ParduotuveEVM.DarboDienos.Where(it => it.ID == darboDiena.ID).ToList().Count == 0)
				{
					ParduotuveEVM.DarboDienos.Add(new ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM {
						ID = darboDiena.ID,
						DarboPradzia = darboDiena.DarboPradzia,
						DarboPabaiga = darboDiena.DarboPabaiga,
						FkDarboDiena = darboDiena.FkDarboDiena,
						FkParduotuve = darboDiena.FkParduotuve
					});
				}
			}
			if(d != null)
			{
				ParduotuveEVM.DarboDienos.Add(d);
			}
			ParduotuveEVM.DarboDienos = ParduotuveEVM.DarboDienos.OrderBy(it => it.FkDarboDiena).ToList();
		}
	}
}


