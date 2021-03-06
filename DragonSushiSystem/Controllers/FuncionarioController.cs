using DragonSushiSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DragonSushiSystem.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Funcionario funcionario)
        {
            Funcionario testar = new Funcionario();
            testar.TestarUsuario(funcionario);

            if (funcionario.FuncionarioCPF != null && funcionario.FuncionarioSenha != null)
            {
                FormsAuthentication.SetAuthCookie(funcionario.FuncionarioCPF, false);
                var funcionarioLogado = funcionario.listarFuncionarioPorID(funcionario.FuncionarioID);
                Session["Funcionario"] = funcionarioLogado;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Funcionario");
            }
        }

        public ActionResult Logout()
        {
            Session["FuncionarioCPF"] = null;
            return RedirectToAction("Login", "Funcionario");
        }

        public ActionResult Perfil()
        {
            Funcionario funcionario = (Funcionario)Session["Funcionario"];
            return View(funcionario);
        }
        public ActionResult Seguranca()
        {
            Funcionario funcionario = (Funcionario) Session["Funcionario"];            
            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seguranca(Funcionario funcionario, FormCollection formCollection)
        {
            if (formCollection["txtNovaSenha"] == formCollection["txtConfirmarSenha"])
            {          
                funcionario.mudarSenha(funcionario, formCollection["txtNovaSenha"]);
                return RedirectToAction("Perfil", "Funcionario");
            }
            
            return Seguranca();
        }
    }
}