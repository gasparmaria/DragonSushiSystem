using DragonSushiSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class PratoController : Controller
    {


        // GET: Prato
        public ActionResult ListarPrato()
        {
            Prato prato = new Prato();
            var listaPratos = prato.listarTodosPratos();
            return View(listaPratos);
        }

        public ActionResult CadastrarPrato()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CadastrarPrato(Prato prato)
        {
            try
            {
                prato.cadastrarPrato(prato);
                var pratos = prato.listarTodosPratos();
                return RedirectToAction("Listarprato", pratos);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditarPrato(int id)
        {
            Prato prato = new Prato();
            var pratoSelecionado = prato.listarPratoPorID(id);
            return View(pratoSelecionado);
        }

        [HttpPost]
        public ActionResult EditarPrato(Prato prato)
        {
            try
            {
                prato.editarPrato(prato);
                var pratos = prato.listarTodosPratos();
                return RedirectToAction("Listarprato", pratos);
            }
            catch
            {
                return View();
            }
        }

    }
}