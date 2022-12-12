using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;
using System.Net.Mail;
using System.Net;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers
{
	/// <summary>
	/// Controller for working with 'Modelis' entity.
	/// </summary>
	public class RegistracijaController : Controller
	{

		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Create()
		{
			var naudotojasEVM = new NaudotojoEditVM();
			PopulateLists(naudotojasEVM);
			return View(naudotojasEVM);
		}

        // http://localhost:5000/Registracija/Confirm?pk_Id=16
		// Naudoti toki formata nuorodos
        public ActionResult Confirm(int pk_id)
        {
			var naudotojasEVM = NaudotojoRepo.Find(pk_id.ToString());
			if (naudotojasEVM != null) 
			{
				naudotojasEVM.Naudotojas.Tipas = 1;
				NaudotojoRepo.Update(naudotojasEVM);
			}
            return View();
        }

        [HttpPost]
		public ActionResult Create(
			int? save,
			int? aadd, int? aremove,
			int? madd, int? mremove,
			int? vadd, int? vremove,
			NaudotojoEditVM naudotojasEVM)
		{

			if (save != null)
			{

				var duplicate = NaudotojoRepo.Exists(naudotojasEVM.Naudotojas.pk_Id);
				if (duplicate)
				{
					ModelState.First(m => m.Key == "Naudotojas.pk_Id").Value.Errors.Add("Duplikatas");
				}
				else
				{
					TryValidateModel(naudotojasEVM);
					//form field validation passed?
					if (ModelState.IsValid)
					{
						NaudotojoRepo.Insert(naudotojasEVM);
						var naudotojas = NaudotojoRepo.FindByPhone(naudotojasEVM.Naudotojas.TelNr);
                        //save success, go back to the entity list
                        SmtpClient client = new SmtpClient();
						client.DeliveryMethod = SmtpDeliveryMethod.Network;
						client.UseDefaultCredentials = false;
						client.EnableSsl = true;
						client.Host = "smtp.gmail.com";
						client.Port = 587;


						client.Credentials = new NetworkCredential("ispprojektas1@gmail.com", "zdhjidlkmookrvty");
						MailMessage message = new MailMessage();
						message.From = new MailAddress("ispprojektas1@gmail.com", "ISP projektas");
						message.To.Add(new MailAddress(naudotojasEVM.Naudotojas.ElPastas));
						message.Subject = "Elektroninio pašto patvirtinimas";
						message.Body = $"Ačiū jog užsiregistravote mūsų puslapyje. Prašome paspausti nuorodą, jog patvirtintumėte savo elektroninį paštą. Patvirtinkite čia: http://localhost:5000/Registracija/Confirm?pk_Id={naudotojas.Naudotojas.pk_Id}";
						message.IsBodyHtml = true;
						client.Send(message);


						return RedirectToAction("Index");
					}
				}
			}
			var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
			PopulateLists(naudotojasEVM);
			return View(naudotojasEVM);
		}

		public void PopulateLists(
		   NaudotojoEditVM naudotojasEVM,
		   bool save = false)
		{
			// //load entities for the select lists
			var privilegijos = NaudotojoPrivilegijosRepo.List();
			//var sutartys = 
			var tipai = KlientoTipaiRepo.List();

			//build select lists
			naudotojasEVM.Lists.KlientoTipai =
				tipai.Where(it => it.Pavadinimas == "Nepatvirtintas").Select(it => {
					return
						new SelectListItem()
						{
							Value = it.pk_Id.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();

			naudotojasEVM.Lists.NaudotojoPrivilegijos =
				privilegijos.Where(it => it.Pavadinimas == "Klientas").Select(it => {
					return
						new SelectListItem()
						{
							Value = it.pk_id.ToString(),
							Text = it.Pavadinimas
						};
				})
				.ToList();
		}
	}
}
