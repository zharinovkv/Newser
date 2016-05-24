using System.Web.Mvc;

namespace Newser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AjaxTest() { return View(); }

        public PartialViewResult AjaxAction(int x, int y) {
            ViewBag.x = x;
            ViewBag.y = y;
            return PartialView();
        }
    }
}