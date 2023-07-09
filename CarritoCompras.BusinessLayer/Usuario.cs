using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoCompras.EntityLayer;

namespace CarritoCompras.BusinessLayer
{
    public class Usuario
    {
        Recursos oRecursos = new Recursos();
        DataLayer.Usuario dUsuario = new DataLayer.Usuario();

        public List<EntityLayer.Usuario> Listar()
        {
            return dUsuario.Listar();
        }

        public int Registrar(EntityLayer.Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                Mensaje = "El nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                Mensaje = "El apellido del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje = "El correo del usuario no puede ser vacío";

            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = Recursos.GenerarClave();
                string asunto = "Creación de Cuenta";
                string bmensaje = "<h3>Su cuenta fue creada corectamente</h3></br><p>Su contraseña de acceso es: ~clave~</p>";
                bmensaje = bmensaje.Replace("~clave~", clave);

                bool respuesta = Recursos.EnviarCorreo(obj.Correo, asunto, bmensaje);
                if (respuesta)
                {
                    obj.Clave = Recursos.ConvertirSHA256(clave);
                    Recursos.ConvertirSHA256(obj.Clave);
                    return dUsuario.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(EntityLayer.Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                mensaje = "El nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                mensaje = "El apellido del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                mensaje = "El correo del usuario no puede ser vacío";

            if (string.IsNullOrEmpty(mensaje))
                return dUsuario.Editar(obj, out mensaje);
            else
                return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return dUsuario.Eliminar(id, out mensaje);
        }

        public bool CambiarClave(int IdUsuario, string NuevaClave, out string mensaje)
        {
            return dUsuario.CambiarClave(IdUsuario, NuevaClave, out mensaje);
        }

        public bool RestablecerClave(int IdUsuario, string Correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = Recursos.GenerarClave();
            bool resultado = dUsuario.RestablecerClave(IdUsuario, nuevaclave, out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña restablecida";
                string bmensaje = "<h3>Su cuenta ha sido restablecida corectamente</h3></br><p>Su contraseña para acceder ahora es: ~clave~</p>";
                bmensaje = bmensaje.Replace("~clave~", nuevaclave);

                bool respuesta = Recursos.EnviarCorreo(Correo, asunto, bmensaje);

                if (respuesta)
                    return true;
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo restablecer la contraseña";
                return false;
            }
        }
    }
}