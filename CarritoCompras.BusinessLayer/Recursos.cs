using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;
using MimeKit;

namespace CarritoCompras.BusinessLayer
{
    public class Recursos
    {
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }

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

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("plmconsultores2013@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("plmconsultores2013@gmail.com", "hyhwsvqgwwjwidzr"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                //smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }
    }
}