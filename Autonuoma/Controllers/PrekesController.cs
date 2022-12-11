using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;
using System.Runtime.Serialization.Formatters.Binary;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers
{
	/// <summary>
	/// Controller for working with 'Modelis' entity.
	/// </summary>
	public class PrekesController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			TempData.Clear();
			var prekes = PrekesRepo.List();
			return View(prekes);
		}

		
		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			var PrekesEVM = new PrekesEditVM();
			PopulateLists(PrekesEVM);
			return View(PrekesEVM);
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
			PrekesEditVM PrekesEVM)
		{
			
			if(save != null)
			{

				var duplicate = PrekesRepo.Exists(PrekesEVM.Prekes.ID);
				if(duplicate)
				{
					ModelState.First(m => m.Key == "Prekes.ID").Value.Errors.Add("Duplikatas");
				}
				else
				{
					TryValidateModel(PrekesEVM);
					//form field validation passed?
					if( ModelState.IsValid )
					{

						PrekesRepo.Insert(PrekesEVM);
						//save success, go back to the entity list
						return RedirectToAction("Index");
					}
				}
			}
			var errors =ModelState.Select(x=>x.Value.Errors).Where(y=>y.Count>0).ToList();
			PopulateLists(PrekesEVM);
			return View(PrekesEVM);
		}

		
		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		public ActionResult Edit(string id)
		{
			var PrekesEVM = PrekesRepo.Find(id);
			PopulateLists(PrekesEVM);
			return View(PrekesEVM);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>
		/// <param name="autoEvm">Entity model filled with latest data.</param>
		/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(
			string id,
			int? save, 
			PrekesEditVM PrekesEVM)
		{
			if (save != null) {
				if( ModelState.IsValid )
				{
					PrekesRepo.Update(PrekesEVM);
					//save success, go back to the entity list
					PopulateLists(PrekesEVM);
					return RedirectToAction("Index");
				}
			}
			var errors =ModelState.Select(x=>x.Value.Errors).Where(y=>y.Count>0).ToList();
			PopulateLists(PrekesEVM);
			return View(PrekesEVM);
		}

				/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		public ActionResult Delete(string id)
		{
			var PrekesEVM = PrekesRepo.FindForDeletion(id);
			return View(PrekesEVM);
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
				PrekesRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch( MySql.Data.MySqlClient.MySqlException )
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var PrekesEVM = PrekesRepo.FindForDeletion(id);

				return View("Delete", PrekesEVM);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
		public void PopulateLists(
			PrekesEditVM PrekesEVM,
			bool save = false)
		{
			// //load entities for the select lists
			var dydziai = RubuDydziaiRepo.List();
            //var sutartys = 
            var kategorijos = PrekiuKategorijosRepo.List();
			var gamybosVietos = GamybosVietosRepo.List();

			//build select lists
			PrekesEVM.Lists.RubuDydziai =
				dydziai.Select(it => {
					return
						new SelectListItem() {
							Value = it.pk_Id.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

            PrekesEVM.Lists.Kategorijos =
				kategorijos.Select(it => {
					return
						new SelectListItem() {
							Value = it.pk_SutrumpintasPavadinimas.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

            PrekesEVM.Lists.GamybosVietos =
				gamybosVietos.Select(it => {
					return
						new SelectListItem() {
							Value = it.pk_Id.ToString(),
							Text = it.Adresas
						};
				})
				.ToList();			
		}

		[HttpPost]
		public ActionResult Process(int quantity, int id, string pav)
		{
			HttpContext.Session.SetInt32("uzsakymopreke" + id.ToString(), quantity);
			if(HttpContext.Session.GetString("cart1") == null)
			{
				HttpContext.Session.SetString("cart1", id.ToString() + '\n');
				HttpContext.Session.SetString("cart2", quantity.ToString() + '\n');
				HttpContext.Session.SetString("cart3", pav + '\n');
			}
			else
			{
				var curr1 = HttpContext.Session.GetString("cart1");
				var curr2 = HttpContext.Session.GetString("cart2");
				var curr3 = HttpContext.Session.GetString("cart3");
				if(curr1.Contains(id.ToString()+'\n'))
				{
					var arr1 = curr1.Split('\n');
					var pos = Array.IndexOf(arr1, id.ToString());

					var arr2 = curr2.Split('\n');
					var arr3 = curr3.Split('\n');
					arr2[pos] = quantity.ToString();
					var newstring1 = "";
					var newstring2 = "";
					var newstring3 = "";
					for(int i = 0; i < arr1.Length; i++)
					{
						if(arr2[i] != "0")
						{
							newstring1 += arr1[i] + '\n';
							newstring2 += arr2[i] + '\n';
							newstring3 += arr3[i] + '\n';
						}
					}
					curr1 = newstring1;
					curr2 = newstring2;
					curr3 = newstring3;
				}
				else
				{
					curr1 += id.ToString() + '\n';
					curr2 += quantity.ToString() + '\n';
					curr3 += pav + '\n';
				}
				HttpContext.Session.SetString("cart1", curr1);
				HttpContext.Session.SetString("cart2", curr2);
				HttpContext.Session.SetString("cart3", curr3);
			}
			return RedirectToAction("Index");
		}
	}
}
