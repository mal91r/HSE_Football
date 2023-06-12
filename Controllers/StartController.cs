using Microsoft.AspNetCore.Mvc;

namespace HseFootball.Controllers
{
    public class StartController : Controller
    {
        // GET: StartController
        public ActionResult Index()
        {
            return View("/Views/StartPageView.cshtml");
        }
    }
}
