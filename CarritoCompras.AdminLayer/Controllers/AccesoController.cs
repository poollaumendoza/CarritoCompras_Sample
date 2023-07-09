using CarritoCompras.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarritoCompras.AdminLayer.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult RestablecerContraseña()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Correo, string Clave)
        {
            EntityLayer.Usuario oUsuario = new EntityLayer.Usuario();
            oUsuario = new BusinessLayer.Usuario().Listar().Where(u => u.Correo == Correo && u.Clave == Recursos.ConvertirSHA256(Clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo y/o contraseña incorrectos";
                return View();
            }
            else
            {
                if (oUsuario.Reestablecer)
                {
                    TempData["IdUsuario"] = oUsuario.IdUsuario;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false);

                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string IdUsuario, string ClaveActual, string NuevaClave, string ConfirmarNuevaClave)
        {
            EntityLayer.Usuario oUsuario = new EntityLayer.Usuario();
            oUsuario = new BusinessLayer.Usuario().Listar().Where(u => u.IdUsuario == int.Parse(IdUsuario)).FirstOrDefault();

            if (oUsuario.Clave != Recursos.ConvertirSHA256(ClaveActual))
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewData["vClave"] = string.Empty;
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (NuevaClave != ConfirmarNuevaClave)
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewData["vClave"] = ClaveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["vClave"] = string.Empty;
            NuevaClave = Recursos.ConvertirSHA256(NuevaClave);
            string Mensaje = string.Empty;

            bool respuesta = new BusinessLayer.Usuario().CambiarClave(int.Parse(IdUsuario), NuevaClave, out Mensaje);

            if (respuesta)
                return RedirectToAction("Index");
            else
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewBag.Error = Mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Restablecer(string Correo)
        {
            EntityLayer.Usuario oUsuario = new EntityLayer.Usuario();
            oUsuario = new BusinessLayer.Usuario().Listar().Where(item => item.Correo == Correo).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "No se encontró un usuario relacionado a este correo";
                return View();
            }
            string Mensaje = string.Empty;

            bool respuesta = new BusinessLayer.Usuario().RestablecerClave(oUsuario.IdUsuario, Correo, out Mensaje);

            if (respuesta)
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

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}