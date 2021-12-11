using DragonSushiSystem.Models;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult ListarProduto()
        {
            Produto produto = new Produto();
            var listaprodutos = produto.listarTodosProdutos();

            return View(listaprodutos);
        }
        
        public ActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            try
            {
                produto.cadastrarProduto(produto);
                var produtos = produto.listarTodosProdutos();
                return RedirectToAction("ListarProduto", produtos);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditarProduto(int id)
        {
            Produto produto = new Produto();
            var produtoselecionado = produto.listarProdutoPorID(id);
            return View(produtoselecionado);
        }

        [HttpPost]
        public ActionResult EditarProduto(Produto produto)
        {
            try
            {
                produto.editarProduto(produto);
                var produtos = produto.listarTodosProdutos();
                return RedirectToAction("ListarProduto", produtos);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeletarProduto(int id)
        {
            Produto produto = new Produto();
            var produtoselecionado = produto.listarProdutoPorID(id);
            return View(produtoselecionado);
        }

        [HttpPost]
        public ActionResult DeletarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produto.deletarProduto(produto);
                    var produtos = produto.listarTodosProdutos();
                    return RedirectToAction("ListarProduto", produtos);
                }
                return View(produto);
            }
            catch
            {
                return View();
            }
        }        
    }
}