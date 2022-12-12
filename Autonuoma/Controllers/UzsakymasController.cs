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
			int id = -1;
			if (HttpContext.Session.GetInt32("id") != null)
				id = (int)HttpContext.Session.GetInt32("id");
			var uzsakymai = UzsakymaiRepo.List(id);
			return View(uzsakymai);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Create()
		{
			Uzsakymai uzsakymas = new Uzsakymai();
			PopulateLists(uzsakymas);
			return View(uzsakymas);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="modelisEvm">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(Uzsakymai Uzsakymas, int? save)
		{
			if(save != null)
			{
					if( ModelState.IsValid )
					{
						int id = (int)HttpContext.Session.GetInt32("id");
						var curr1 = HttpContext.Session.GetString("cart1");
						var curr2 = HttpContext.Session.GetString("cart2");
						var curr3 = HttpContext.Session.GetString("cart3");
						UzsakymaiRepo.Insert(Uzsakymas, curr1, curr2, curr3, id);
						//save success, go back to the entity list
						return RedirectToAction("Index");
					}
			}
			
			var errors =ModelState.Select(x=>x.Value.Errors).Where(y=>y.Count>0).ToList();
			PopulateLists(Uzsakymas);
			return View(Uzsakymas);
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

		public void PopulateShops(UzsakymaiBuyVM uzsakymai)
		{
			var parduotuves = ParduotuveRepo.List();

			uzsakymai.Lists.Parduotuves =
				parduotuves.Select(it => {
					return
						new SelectListItem() {
							Value = it.ID.ToString(),
							Text = it.Adresas
						};
				}).ToList();
		}

		/// <summary>
		/// This is invoked when buying form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		public ActionResult Buy(int id)
		{
			var UM = UzsakymaiRepo.Find(id);
			var UzsakymasBVM = new UzsakymaiBuyVM();
			UzsakymasBVM.Uzsakymai.Busena = UM.Busena;
			UzsakymasBVM.Uzsakymai.pk_Id = UM.pk_Id;			
			UzsakymasBVM.Uzsakymai.payment_method_selected = false;		
			UzsakymasBVM.Uzsakymai.fk_Parduotuve = UM.fk_Parduotuve;	
			return View(UzsakymasBVM);
		}

		/// <summary>
		/// This is invoked when buying form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		[HttpPost]
		public ActionResult Buy(UzsakymaiBuyVM UzsakymasBVM, int id, int? saveMethod, int? save)
		{
			if (save != null) {
				UzsakymaiRepo.UpdatePurchase(UzsakymasBVM);
				return RedirectToAction("Index");
			} else {
				PopulateShops(UzsakymasBVM);
				UzsakymasBVM.Uzsakymai.payment_method_selected = saveMethod != null ? true : false;
			}
			return View(UzsakymasBVM);
		}
	}
}
