using DragonSushiSystem.Models;
using DragonSushiSystem.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ViewModelComandaController : Controller
    {
        // GET: ViewModelComanda
        public ActionResult ListarComanda()
        {
            ViewModelComanda vmComanda = new ViewModelComanda();
            var listaComandasAbertas = vmComanda.listarTodosComandasAbertas();
           
            return View(listaComandasAbertas);
        }


        // GET: ViewModelComanda/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ViewModelComanda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewModelComanda/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewModelComanda/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ViewModelComanda/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewModelComanda/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViewModelComanda/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
