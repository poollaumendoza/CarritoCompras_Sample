using CarritoCompras.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace CarritoCompras.AdminLayer.Controllers
{
    public class MantenimientoController : Controller
    {
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<EntityLayer.Categoria> oLista = new List<EntityLayer.Categoria>();
            oLista = new Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(EntityLayer.Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
                resultado = new Categoria().Registrar(objeto, out mensaje);
            else
                resultado = new Categoria().Editar(objeto, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<EntityLayer.Marca> oLista = new List<EntityLayer.Marca>();
            oLista = new Marca().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca(EntityLayer.Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
                resultado = new Marca().Registrar(objeto, out mensaje);
            else
                resultado = new Marca().Editar(objeto, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new Marca().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<EntityLayer.Producto> oLista = new List<EntityLayer.Producto>();
            oLista = new Producto().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProducto(EntityLayer.Producto objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdProducto == 0)
                resultado = new Producto().Registrar(objeto, out mensaje);
            else
                resultado = new Producto().Editar(objeto, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}