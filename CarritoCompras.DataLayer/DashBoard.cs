using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.DataLayer
{
    public class DashBoard
    {
        public EntityLayer.DashBoard VerDashBoard()
        {
            EntityLayer.DashBoard objeto = new EntityLayer.DashBoard();

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "sp_ReporteDashBoard";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);

                    Cnx.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while (Dr.Read())
                        {
                            objeto = new EntityLayer.DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(Dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(Dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(Dr["TotalProducto"])
                            };
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                string mensaje = ex.Message;
                objeto = new EntityLayer.DashBoard();
            }

            return objeto;
        }
    }
}