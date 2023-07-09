using CarritoCompras.BusinessLayer;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace CarritoCompras.AdminLayer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult VistaDashBoard()
        {
            EntityLayer.DashBoard objeto = new DashBoard().VerDashBoard();

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListaReporte(string fechainicio, string fechafin, string idtransaccion)
        {
            List<EntityLayer.Reporte> oLista = new List<EntityLayer.Reporte>();
            oLista = new Reporte().Ventas(fechainicio, fechafin, idtransaccion);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportarVenta(string fechainicio, string fechafin, string idtransaccion)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(new Reporte().ListaVenta(fechainicio, fechafin, idtransaccion));
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta_" + DateTime.Now.ToUniversalTime() + ".xlsx");
                }
            }
        }
    }
}