using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.DataLayer
{
    public class Conexion
    {
        public static string Cn = ConfigurationManager.ConnectionStrings["CadenaConexion"].ToString();
    }
}