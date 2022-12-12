using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers
{
	/// <summary>
	/// Controller for working with 'Modelis' entity.
	/// </summary>
	public class PrisijungimasController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult Login(string email, string password)
        {
            var user = NaudotojoRepo.FindByEmail(email);
            Console.WriteLine("email: " + email + " password: " + password + " encrypted: " + user.Naudotojas.Slaptazodis);
            HttpContext.Session.SetInt32("id", user.Naudotojas.pk_Id);
            HttpContext.Session.SetInt32("privilegijos", user.Naudotojas.Privilegijos);
            if (user.Naudotojas.VerifyPassword(password,user.Naudotojas.Slaptazodis) == true)
            {
                return RedirectToAction("Index", "Prekes");
            }
            else
            {
                return Content("Incorrect email or password");
            }
        }
    }
}
