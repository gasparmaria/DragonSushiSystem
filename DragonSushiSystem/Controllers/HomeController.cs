using DragonSushiSystem.Models;
using DragonSushiSystem.ViewModels;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Produto produto = new Produto();
            var listaProduto = produto.listarTodosProdutos();
            return View(listaProduto);
        }

        public ActionResult Pagamento()
        {
            return View();
        }
    }
}