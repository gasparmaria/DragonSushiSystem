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
        public ActionResult CadastrarPrato()
        {
            return View();
        }
    }
}