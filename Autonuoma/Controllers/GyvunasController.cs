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
	public class GyvunasController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			TempData.Clear();
			var gyvunai = GyvunasRepo.List();
			return View(gyvunai);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var GyvunasEVM = new GyvunasEditVM();
			PopulateLists(GyvunasEVM);
			return View(GyvunasEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(
			int? save, 
			int? aadd, int? aremove, 
			int? madd, int? mremove,
			int? vadd, int? vremove,
			GyvunasEditVM GyvunasEVM)
		{
			if(aadd != null)
			{
				var a = new AlergijaEditVM.AlergijaM {
					Pavadinimas = "",
					NustatymoData = DateTime.Now,
					FkDarbuotojas = null,
					FkGyvunas = null
				};
				ModelState.Clear();
				PopulateLists(GyvunasEVM, a);
				return View(GyvunasEVM);
			}
			if(madd != null)
			{
				var m = new VaistaiEditVM.VaistaiM {
					Pavadinimas = "",
					NustatymoData = DateTime.Now,
					Kaina = 0,
					FkDarbuotojas = null,
					FkGyvunas = GyvunasEVM.Gyvunas.ID
				};
				ModelState.Clear();
				PopulateLists(GyvunasEVM, m: m);
				return View(GyvunasEVM);
			}
			if(vadd != null)
			{
				var v = new VakcinacijaEditVM.VakcinacijaM {
					Pavadinimas = "",
					NustatymoData = DateTime.Now,
					Kaina = 0,
					FkDarbuotojas = null,
					FkGyvunas = GyvunasEVM.Gyvunas.ID
				};
				ModelState.Clear();
				PopulateLists(GyvunasEVM, v: v);
				return View(GyvunasEVM);
			}
			if(aremove != null)
			{
				if(TempData["are"] != null)
				{
					GyvunasEVM.AlergijaRemoveValues = TempData["are"] as List<int>;
					if(GyvunasEVM.AlergijaRemoveValues == null)
					{
						GyvunasEVM.AlergijaRemoveValues = (TempData["are"] as int[]).ToList();
						if(GyvunasEVM.AlergijaRemoveValues == null)
							GyvunasEVM.AlergijaRemoveValues = new List<int>();
					}
				}
				GyvunasEVM.AlergijaRemoveValues.Add(aremove.Value);
				TempData["are"] = GyvunasEVM.AlergijaRemoveValues as List<int>;
				GyvunasEVM.Alergijos =
					GyvunasEVM
						.Alergijos
						.Where(it => !GyvunasEVM.AlergijaRemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(GyvunasEVM);
				return View(GyvunasEVM);
			}
			if(mremove != null)
			{
				if(TempData["mre"] != null)
				{
					GyvunasEVM.VaistaiRemoveValues = TempData["mre"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.VaistaiRemoveValues == null)
					{
						GyvunasEVM.VaistaiRemoveValues = (TempData["mre"] as int[]).ToList();
						if(GyvunasEVM.VaistaiRemoveValues == null)
							GyvunasEVM.VaistaiRemoveValues = new List<int>();
					}
				}
				GyvunasEVM.VaistaiRemoveValues.Add(mremove.Value);
				TempData["mre"] = GyvunasEVM.VaistaiRemoveValues as List<int>;
				TempData.Keep();
				GyvunasEVM.Vaistai =
					GyvunasEVM
						.Vaistai
						.Where(it => !GyvunasEVM.VaistaiRemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(GyvunasEVM);
				return View(GyvunasEVM);
			}
			if(vremove != null)
			{
				if(TempData["vre"] != null)
				{
					GyvunasEVM.VakcinacijaRemoveValues = TempData["vre"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.VakcinacijaRemoveValues == null)
					{
						GyvunasEVM.VakcinacijaRemoveValues = (TempData["vre"] as int[]).ToList();
						if(GyvunasEVM.VakcinacijaRemoveValues == null)
							GyvunasEVM.VakcinacijaRemoveValues = new List<int>();
					}
				}
				GyvunasEVM.VakcinacijaRemoveValues.Add(vremove.Value);
				TempData["vre"] = GyvunasEVM.VakcinacijaRemoveValues as List<int>;
				TempData.Keep();
				GyvunasEVM.Vakcinacijos =
					GyvunasEVM
						.Vakcinacijos
						.Where(it => !GyvunasEVM.VakcinacijaRemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(GyvunasEVM);
				return View(GyvunasEVM);
			}
			if(save != null)
			{

				var duplicate = GyvunasRepo.Exists(GyvunasEVM.Gyvunas.ID);
				if(duplicate)
				{
					ModelState.First(m => m.Key == "Gyvunas.ID").Value.Errors.Add("Duplikatas");
				}
				else
				{
					foreach(var alergija in GyvunasEVM.Alergijos)
					{
						alergija.FkGyvunas = GyvunasEVM.Gyvunas.ID;
						TryValidateModel(GyvunasEVM.Alergijos);
					}
					foreach(var vaistai in GyvunasEVM.Vaistai)
					{
						vaistai.FkGyvunas = GyvunasEVM.Gyvunas.ID;
						TryValidateModel(GyvunasEVM.Vaistai);
					}
					foreach(var vakcinacija in GyvunasEVM.Vakcinacijos)
					{
						vakcinacija.FkGyvunas = GyvunasEVM.Gyvunas.ID;
						TryValidateModel(GyvunasEVM.Vakcinacijos);
					}
					TryValidateModel(GyvunasEVM);
					//form field validation passed?
					if( ModelState.IsValid )
					{
						GyvunasEVM.AlergijaRemoveValues.Clear();
						GyvunasEVM.VakcinacijaRemoveValues.Clear();
						GyvunasEVM.VaistaiRemoveValues.Clear();
						TempData.Remove("are");
						TempData.Remove("mre");
						TempData.Remove("vre");

						GyvunasRepo.Insert(GyvunasEVM);
						foreach(var alergija in GyvunasEVM.Alergijos)
						{
							AlergijaRepo.Insert(alergija);
						}
						foreach(var vaistai in GyvunasEVM.Vaistai)
						{
							VaistaiRepo.Insert(vaistai);
						}
						foreach(var vakcinacija in GyvunasEVM.Vakcinacijos)
						{
							VakcinacijaRepo.Insert(vakcinacija);
						}

						//save success, go back to the entity list
						return RedirectToAction("Index");
					}
				}
			}
			var errors =ModelState.Select(x=>x.Value.Errors).Where(y=>y.Count>0).ToList();
			PopulateLists(GyvunasEVM);
			return View(GyvunasEVM);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(string id)
		{
			var GyvunasEVM = GyvunasRepo.Find(id);
			PopulateLists(GyvunasEVM);

			return View(GyvunasEVM);
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
			int? aadd, int? aremove, 
			int? madd, int? mremove,
			int? vadd, int? vremove, 
			GyvunasEditVM GyvunasEVM)
		{
			if(aadd != null)
			{
				var a = new AlergijaEditVM.AlergijaM {
					Pavadinimas = "",
					NustatymoData = DateTime.Now,
					FkDarbuotojas = null,
					FkGyvunas = GyvunasEVM.Gyvunas.ID
				};
				ModelState.Clear();
				PopulateLists(GyvunasEVM, a);
				return View(GyvunasEVM);
			}
			if(madd != null)
			{
				var m = new VaistaiEditVM.VaistaiM {
					Pavadinimas = "",
					NustatymoData = DateTime.Now,
					Kaina = 0,
					FkDarbuotojas = null,
					FkGyvunas = GyvunasEVM.Gyvunas.ID
				};
				ModelState.Clear();
				PopulateLists(GyvunasEVM, null, m);
				return View(GyvunasEVM);
			}
			if(vadd != null)
			{
				var v = new VakcinacijaEditVM.VakcinacijaM {
					Pavadinimas = "",
					NustatymoData = DateTime.Now,
					Kaina = 0,
					FkDarbuotojas = null,
					FkGyvunas = GyvunasEVM.Gyvunas.ID
				};
				ModelState.Clear();
				PopulateLists(GyvunasEVM, null, null, v);
				return View(GyvunasEVM);
			}
			if(aremove != null)
			{
				if(TempData["are"] != null)
				{
					GyvunasEVM.AlergijaRemoveValues = TempData["are"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.AlergijaRemoveValues == null)
					{
						GyvunasEVM.AlergijaRemoveValues = (TempData["are"] as int[]).ToList();
						if(GyvunasEVM.AlergijaRemoveValues == null)
							GyvunasEVM.AlergijaRemoveValues = new List<int>();
					}
				}
				GyvunasEVM.AlergijaRemoveValues.Add(aremove.Value);
				TempData["are"] = GyvunasEVM.AlergijaRemoveValues as List<int>;
				TempData.Keep();
				GyvunasEVM.Alergijos =
					GyvunasEVM
						.Alergijos
						.Where(it => !GyvunasEVM.AlergijaRemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(GyvunasEVM);
				return View(GyvunasEVM);
			}
			if(mremove != null)
			{
				if(TempData["mre"] != null)
				{
					GyvunasEVM.VaistaiRemoveValues = TempData["mre"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.VaistaiRemoveValues == null)
					{
						GyvunasEVM.VaistaiRemoveValues = (TempData["mre"] as int[]).ToList();
						if(GyvunasEVM.VaistaiRemoveValues == null)
							GyvunasEVM.VaistaiRemoveValues = new List<int>();
					}
				}
				GyvunasEVM.VaistaiRemoveValues.Add(mremove.Value);
				TempData["mre"] = GyvunasEVM.VaistaiRemoveValues as List<int>;
				TempData.Keep();
				GyvunasEVM.Vaistai =
					GyvunasEVM
						.Vaistai
						.Where(it => !GyvunasEVM.VaistaiRemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(GyvunasEVM);
				return View(GyvunasEVM);
			}
			if(vremove != null)
			{
				if(TempData["vre"] != null)
				{
					GyvunasEVM.VakcinacijaRemoveValues = TempData["vre"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.VakcinacijaRemoveValues == null)
					{
						GyvunasEVM.VakcinacijaRemoveValues = (TempData["vre"] as int[]).ToList();
						if(GyvunasEVM.VakcinacijaRemoveValues == null)
							GyvunasEVM.VakcinacijaRemoveValues = new List<int>();
					}
				}
				GyvunasEVM.VakcinacijaRemoveValues.Add(vremove.Value);
				TempData["vre"] = GyvunasEVM.VakcinacijaRemoveValues as List<int>;
				TempData.Keep();
				GyvunasEVM.Vakcinacijos =
					GyvunasEVM
						.Vakcinacijos
						.Where(it => !GyvunasEVM.VakcinacijaRemoveValues.Contains(it.ID))
						.ToList();
				
				ModelState.Clear();
				
				PopulateLists(GyvunasEVM);
				return View(GyvunasEVM);
			}
			if(save != null)
			{
				//form field validation passed?
				if( ModelState.IsValid )
				{
					GyvunasEVM.AlergijaRemoveValues.Clear();
					GyvunasEVM.VaistaiRemoveValues.Clear();
					GyvunasEVM.VakcinacijaRemoveValues.Clear();
					TempData.Remove("are");
					TempData.Remove("mre");
					TempData.Remove("vre");
					GyvunasRepo.Update(GyvunasEVM);
					AlergijaRepo.DeleteForGyvunas(GyvunasEVM.Gyvunas.ID);
					VaistaiRepo.DeleteForGyvunas(GyvunasEVM.Gyvunas.ID);
					VakcinacijaRepo.DeleteForGyvunas(GyvunasEVM.Gyvunas.ID);

					foreach(var alergija in GyvunasEVM.Alergijos)
					{
						AlergijaRepo.Insert(alergija);
					}
					foreach(var vaistai in GyvunasEVM.Vaistai)
					{
						VaistaiRepo.Insert(vaistai);
					}
					foreach(var vakcinacija in GyvunasEVM.Vakcinacijos)
					{
						VakcinacijaRepo.Insert(vakcinacija);
					}

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
				else
				{
					PopulateLists(GyvunasEVM, save: true);
					return View(GyvunasEVM);
				}
			}
			throw new Exception("Should not reach here.");
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(string id)
		{
			var GyvunasEVM = GyvunasRepo.FindForDeletion(id);
			return View(GyvunasEVM);
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
				GyvunasRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var GyvunasEVM = GyvunasRepo.FindForDeletion(id);

				return View("Delete", GyvunasEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateLists(
			GyvunasEditVM GyvunasEVM,
			AlergijaEditVM.AlergijaM a = null, VaistaiEditVM.VaistaiM m = null, VakcinacijaEditVM.VakcinacijaM v = null,
			bool save = false)
		{
			//load entities for the select lists
			var lytys = LytisRepo.List();
            var rusys = GyvunuRusisRepo.List();
            var dydziai = GyvunuDydisRepo.List();
            //var sutartys = 
            var parduotuves = ParduotuveRepo.List();
			var darbuotojai = DarbuotojasRepo.List();

			//build select lists
			GyvunasEVM.Lists.Lytys =
				lytys.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

            GyvunasEVM.Lists.Rusys =
				rusys.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

            GyvunasEVM.Lists.Dydziai =
				dydziai.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

            GyvunasEVM.Lists.Parduotuves =
				parduotuves.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

			GyvunasEVM.Lists.Darbuotojai =
				darbuotojai.Select(it => {
					return
						new SelectListItem() {
							Value = it.AsmensKodas.ToString(),
							Text = it.Vardas + " " + it.Pavarde
						};
				})
				.OrderBy(it => it.Text)
				.ToList();

			if(save)
			{
				GyvunasEVM.Alergijos = new List<AlergijaEditVM.AlergijaM>();
				GyvunasEVM.Vaistai = new List<VaistaiEditVM.VaistaiM>();
				GyvunasEVM.Vakcinacijos = new List<VakcinacijaEditVM.VakcinacijaM>();
			}
			else
			{
				if(TempData["are"] != null)
				{
					TempData.Keep();
					GyvunasEVM.AlergijaRemoveValues = TempData["are"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.AlergijaRemoveValues == null)
					{
						GyvunasEVM.AlergijaRemoveValues = (TempData["are"] as int[]).ToList();
						if(GyvunasEVM.AlergijaRemoveValues == null)
							GyvunasEVM.AlergijaRemoveValues = new List<int>();
					}
				}
				if(TempData["mre"] != null)
				{
					GyvunasEVM.VaistaiRemoveValues = TempData["mre"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.VaistaiRemoveValues == null)
					{
						GyvunasEVM.VaistaiRemoveValues = (TempData["mre"] as int[]).ToList();
						if(GyvunasEVM.VaistaiRemoveValues == null)
							GyvunasEVM.VaistaiRemoveValues = new List<int>();
					}
				}
				if(TempData["vre"] != null)
				{
					GyvunasEVM.VakcinacijaRemoveValues = TempData["vre"] as List<int>;
					TempData.Keep();
					if(GyvunasEVM.VakcinacijaRemoveValues == null)
					{
						GyvunasEVM.VakcinacijaRemoveValues = (TempData["vre"] as int[]).ToList();
						if(GyvunasEVM.VakcinacijaRemoveValues == null)
							GyvunasEVM.VakcinacijaRemoveValues = new List<int>();
					}
				}
			}

			var alergijos = AlergijaRepo.List();
			var vaistai = VaistaiRepo.List();
			var vakcinacijos = VakcinacijaRepo.List();

			foreach(var alergija in alergijos)
			{
				if(alergija.FkGyvunas == GyvunasEVM.Gyvunas.ID && !GyvunasEVM.AlergijaRemoveValues.Contains(alergija.ID)
				&& GyvunasEVM.Alergijos.Where(it => it.ID == alergija.ID).ToList().Count == 0)
				{
					GyvunasEVM.Alergijos.Add(new AlergijaEditVM.AlergijaM {
						ID = alergija.ID,
						Pavadinimas = alergija.Pavadinimas,
						NustatymoData = alergija.NustatymoData,
						FkDarbuotojas = alergija.FkDarbuotojas,
						FkGyvunas = GyvunasEVM.Gyvunas.ID
					});
				}
			}
			if(a != null)
			{
				GyvunasEVM.Alergijos.Add(a);
			}
			foreach(var vaistas in vaistai)
			{
				if(vaistas.FkGyvunas == GyvunasEVM.Gyvunas.ID && !GyvunasEVM.VaistaiRemoveValues.Contains(vaistas.ID)
				&& GyvunasEVM.Vaistai.Where(it => it.ID == vaistas.ID).ToList().Count == 0)
				{
					GyvunasEVM.Vaistai.Add(new VaistaiEditVM.VaistaiM {
						ID = vaistas.ID,
						Pavadinimas = vaistas.Pavadinimas,
						NustatymoData = vaistas.IsrasymoData,
						Kaina = vaistas.Kaina,
						FkDarbuotojas = vaistas.FkDarbuotojas,
						FkGyvunas = GyvunasEVM.Gyvunas.ID
					});
				}
			}
			if(m != null)
			{
				GyvunasEVM.Vaistai.Add(m);
			}
			foreach(var vakcinacija in vakcinacijos)
			{
				if(vakcinacija.FkGyvunas == GyvunasEVM.Gyvunas.ID && !GyvunasEVM.VakcinacijaRemoveValues.Contains(vakcinacija.ID)
				&& GyvunasEVM.Vakcinacijos.Where(it => it.ID == vakcinacija.ID).ToList().Count == 0)
				{
					GyvunasEVM.Vakcinacijos.Add(new VakcinacijaEditVM.VakcinacijaM {
						ID = vakcinacija.ID,
						Pavadinimas = vakcinacija.Pavadinimas,
						NustatymoData = vakcinacija.NustatymoData,
						Kaina = vakcinacija.Kaina,
						FkDarbuotojas = vakcinacija.FkDarbuotojas,
						FkGyvunas = GyvunasEVM.Gyvunas.ID
					});
				}
			}
			if(v != null)
			{
				GyvunasEVM.Vakcinacijos.Add(v);
			}
		}
	}
}
