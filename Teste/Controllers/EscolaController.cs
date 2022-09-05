using Microsoft.AspNetCore.Mvc;

namespace Teste.Controllers
{
    public class EscolaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult ExcluirConfirmacao()
        {
            return View();
        }

    }
}
