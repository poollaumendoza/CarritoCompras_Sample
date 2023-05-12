using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarritoCompras.BusinessLayer;

namespace CarritoCompras.AdminLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<EntityLayer.Usuario> oLista = new List<EntityLayer.Usuario>();
            oLista = new Usuario().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
    }
}