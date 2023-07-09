using CarritoCompras.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarritoCompras.ClientLayer.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(EntityLayer.Cliente obj)
        {
            int resultado;
            string Mensaje = string.Empty;

            ViewData["Nombres"] = string.IsNullOrEmpty(obj.Nombres) ? string.Empty : obj.Nombres;
            ViewData["Apellidos"] = string.IsNullOrEmpty(obj.Apellidos) ? string.Empty : obj.Apellidos;
            ViewData["Correo"] = string.IsNullOrEmpty(obj.Correo) ? string.Empty : obj.Correo;
            
            if(obj.Clave != obj.ConfirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            resultado = new Cliente().Registrar(obj, out Mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = Mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string Correo, string Clave)
        {
            EntityLayer.Cliente oCliente = null;
            oCliente = new Cliente().Listar().Where(item => item.Correo == Correo && item.Clave == Recursos.ConvertirSHA256(Clave)).FirstOrDefault();

            if(oCliente == null)
            {
                ViewBag.Error = "Correo y/o contraseña no válidos";
                return View();
            }
            else
            {
                if (oCliente.Restablecer)
                {
                    TempData["IdCliente"] = oCliente.IdCliente;
                    return RedirectToAction("CambiarClave");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.Correo, false);
                    Session["Cliente"] = oCliente;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(Cliente obj)
        {


            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(string IdUsuario, string Clave, string NuevaClave)
        {
            return View();
        }
    }
}