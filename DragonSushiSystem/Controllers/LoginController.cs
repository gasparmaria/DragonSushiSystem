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
                Session["FuncionarioCPF"] = funcionario.FuncionarioCPF.ToString();
                Session["FuncionarioSenha"] = funcionario.FuncionarioSenha.ToString();

                return RedirectToAction("Index", "ViewModelHome");
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
            return View();
        }

        [HttpPost]
        public ActionResult Seguranca(Funcionario funcionario)
        {
            Funcionario mudarsenha = new Funcionario();
            mudarsenha.MudarSenha(funcionario);
            return RedirectToAction("Perfil", "Funcionario");
        }
    }
}