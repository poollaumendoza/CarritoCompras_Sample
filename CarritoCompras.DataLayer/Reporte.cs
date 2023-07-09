using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.DataLayer
{
    public class Reporte
    {
        public List<EntityLayer.Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<EntityLayer.Reporte> lista = new List<EntityLayer.Reporte>();

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_ReporteVentas", Cnx);
                    Cmd.Parameters.AddWithValue("FechaInicio", fechainicio);
                    Cmd.Parameters.AddWithValue("FechaFin", fechafin);
                    Cmd.Parameters.AddWithValue("IdTransaccion", idtransaccion);

                    Cnx.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while (Dr.Read())
                        {
                            lista.Add(new EntityLayer.Reporte()
                            {
                                FechaVenta = Dr["FechaVenta"].ToString(),
                                Cliente = Dr["Cliente"].ToString(),
                                Producto = Dr["Producto"].ToString(),
                                Precio = Convert.ToDecimal(Dr["Precio"], new CultureInfo("es-PE")),
                                Cantidad = Convert.ToInt32(Dr["Cantidad"]),
                                Total = Convert.ToDecimal(Dr["Total"], new CultureInfo("es-PE")),
                                IdTransaccion = Dr["IdTransaccion"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EntityLayer.Reporte>();
            }

            return lista;
        }

        public DataTable ListaVenta(string fechainicio, string fechafin, string idtransaccion)
        {
            List<EntityLayer.Reporte> oLista = new List<EntityLayer.Reporte>();
            oLista = Ventas(fechainicio, fechafin, idtransaccion);

            DataTable Dt = new DataTable();
            Dt.Locale = new System.Globalization.CultureInfo("es-PE");
            Dt.Columns.Add("Fecha Venta", typeof(string));
            Dt.Columns.Add("Cliente", typeof(string));
            Dt.Columns.Add("Producto", typeof(string));
            Dt.Columns.Add("Precio", typeof(decimal));
            Dt.Columns.Add("Cantidad", typeof(int));
            Dt.Columns.Add("Total", typeof(decimal));
            Dt.Columns.Add("IdTransaccion", typeof(string));

            foreach (EntityLayer.Reporte item in oLista)
            {
                Dt.Rows.Add(
                    new object[]
                    {
                        item.FechaVenta,
                        item.Cliente,
                        item.Producto,
                        item.Precio,
                        item.Cantidad,
                        item.Total,
                        item.IdTransaccion
                    });
            }

            Dt.TableName = "Datos";

            return Dt;
        }
    }
}