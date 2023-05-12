using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.BusinessLayer
{
    public class Recursos
    {
        public static string ConvertirSHA256(string texto)
        {
            StringBuilder oStringBuilder = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] resultado = hash.ComputeHash(enc.GetBytes(texto));

                foreach (var item in resultado)
                    oStringBuilder.Append(item.ToString("x2"));
            }
            return oStringBuilder.ToString();
        }
    }
}