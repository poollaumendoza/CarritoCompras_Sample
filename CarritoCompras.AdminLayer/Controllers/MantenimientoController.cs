using CarritoCompras.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace CarritoCompras.AdminLayer.Controllers
{
    [Authorize]
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
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;
            int idproductogenerado = 0;

            EntityLayer.Producto oProducto = new EntityLayer.Producto();
            oProducto = JsonConvert.DeserializeObject<EntityLayer.Producto>(objeto);

            decimal precio;
            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
                oProducto.Precio = precio;
            else
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);

            if (oProducto.IdProducto == 0) {
                idproductogenerado = new Producto().Registrar(oProducto, out mensaje);
                if (idproductogenerado != 0)
                    oProducto.IdProducto = idproductogenerado;
                else
                    operacion_exitosa = false;
                if(operacion_exitosa)
                    if(archivoImagen != null)
                    {
                        string rutaImagenes = ConfigurationManager.AppSettings["ServidorFotos"];
                        string extension = Path.GetExtension(archivoImagen.FileName);
                        string nombreImagen = string.Concat(oProducto.IdProducto.ToString(), extension);
                        try
                        {
                            archivoImagen.SaveAs(Path.Combine(rutaImagenes, nombreImagen));
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                            guardar_imagen_exito = false;
                        }
                        if (guardar_imagen_exito)
                        {
                            oProducto.RutaImagen = rutaImagenes;
                            oProducto.NombreImagen = nombreImagen;
                            bool rpta = new BusinessLayer.Producto().GuardarDatosImagen(oProducto, out mensaje);
                        }
                    }
            }
            else
                resultado = new Producto().Editar(oProducto, out mensaje);

            return Json(
                new { 
                    operacionExitosa = operacion_exitosa, 
                    idGenerado = oProducto.IdProducto, 
                    mensaje = mensaje }, 
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            EntityLayer.Producto oProducto = new Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();
            string textoBase64 = Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);

            return Json(
                new { 
                    conversion = conversion, 
                    textobase64 = textoBase64, 
                    extension = Path.GetExtension(oProducto.NombreImagen) }, 
                JsonRequestBehavior.AllowGet);
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