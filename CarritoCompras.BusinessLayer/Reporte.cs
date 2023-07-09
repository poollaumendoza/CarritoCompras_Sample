using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.BusinessLayer
{
    public class Reporte
    {
        DataLayer.Reporte dReporte = new DataLayer.Reporte();

        public List<EntityLayer.Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return dReporte.Ventas(fechainicio, fechafin, idtransaccion);
        }

        public DataTable ListaVenta(string fechainicio, string fechafin, string idtransaccion)
        {
            return dReporte.ListaVenta(fechainicio, fechafin, idtransaccion);
        }
    }
}