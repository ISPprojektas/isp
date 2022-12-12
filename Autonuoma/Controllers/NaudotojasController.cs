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
    public class NaudotojasController : Controller
    {

        /// <summary>
        /// This is invoked when either 'Index' action is requested or no action is provided.
        /// </summary>
        /// <returns>Entity list view.</returns>
        public ActionResult Index()
        {
            TempData.Clear();
            var naudotojai = NaudotojoRepo.List();
            return View(naudotojai);
        }


        /// <summary>
        /// This is invoked when creation form is first opened in browser.
        /// </summary>
        /// <returns>Creation form view.</returns>
        public ActionResult Create()
        {
            var naudotojasEVM = new NaudotojoEditVM();
            PopulateLists(naudotojasEVM);
            return View(naudotojasEVM);
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
                        //save success, go back to the entity list
                        return RedirectToAction("Index");
                    }
                }
            }
            var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            PopulateLists(naudotojasEVM);
            return View(naudotojasEVM);
        }


        /// <summary>
        /// This is invoked when editing form is first opened in browser.
        /// </summary>
        /// <param name="id">ID of the entity to edit.</param>
        /// <returns>Editing form view.</returns>
        public ActionResult Edit(string id)
        {
            var naudotojasEVM = NaudotojoRepo.Find(id);
            PopulateLists(naudotojasEVM);
            return View(naudotojasEVM);
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
            NaudotojoEditVM naudotojasEVM)
        {
            if (save != null)
            {
                if (ModelState.IsValid)
                {
                    NaudotojoRepo.Update(naudotojasEVM);
                    //save success, go back to the entity list
                    PopulateLists(naudotojasEVM);
                    return RedirectToAction("Index");
                }
            }
            var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            PopulateLists(naudotojasEVM);
            return View(naudotojasEVM);
        }

        /// </summary>
        /// <param name="id">ID of the entity to delete.</param>
        /// <returns>Deletion form view.</returns>
        public ActionResult Delete(string id)
        {
            var naudotojasEVM = NaudotojoRepo.FindForDeletion(id);
            return View(naudotojasEVM);
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
                NaudotojoRepo.Delete(id);

                //deletion success, redired to list form
                return RedirectToAction("Index");
            }
            //entity in use, deletion not permitted
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                //enable explanatory message and show delete form
                ViewData["deletionNotPermitted"] = true;

                var naudotojasEVM = NaudotojoRepo.FindForDeletion(id);

                return View("Delete", naudotojasEVM);
            }
        }

        /// <summary>
        /// Populates select lists used to render drop down controls.
        /// </summary>
        /// <param name="modelisEvm">'Automobilis' view model to append to.</param>
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
                tipai.Select(it => {
                    return
                        new SelectListItem()
                        {
                            Value = it.pk_Id.ToString(),
                            Text = it.Pavadinimas
                        };
                })
                .ToList();

            naudotojasEVM.Lists.NaudotojoPrivilegijos =
                privilegijos.Select(it => {
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
