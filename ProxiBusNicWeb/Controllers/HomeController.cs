using ProxiBusNicWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProxiBusNicWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ProxiBusNicEntityContainer db = new ProxiBusNicEntityContainer();
        public ActionResult Index()
        {
            ViewBag.contarBus = db.Buses.Count();
            ViewBag.contarParadas = db.Paradas.Count();
            ViewBag.contarBusParadas = db.BusParadas.Count();
            ViewBag.contarSugerencias = db.Sugerencias.Count();
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
    }
}