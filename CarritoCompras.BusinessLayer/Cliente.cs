using System.Collections.Generic;

namespace CarritoCompras.BusinessLayer
{
    public class Cliente
    {
        DataLayer.Cliente dCliente = new DataLayer.Cliente();

        public int Registrar(EntityLayer.Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                Mensaje = "El nombre del cliente no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                Mensaje = "El apellido del cliente no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje = "El correo del usuario no puede ser vacío";

            if (string.IsNullOrEmpty(Mensaje))
            {
                obj.Clave = Recursos.ConvertirSHA256(obj.Clave);
                return dCliente.Registrar(obj, out Mensaje);
            }
            else
                return 0;
        }

        public List<EntityLayer.Cliente> Listar()
        {
            return dCliente.Listar();
        }

        public bool CambiarClave(int IdCliente, string NuevaClave, out string mensaje)
        {
            return dCliente.CambiarClave(IdCliente, NuevaClave, out mensaje);
        }

        public bool RestablecerClave(int IdCliente, string Correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = Recursos.GenerarClave();
            bool resultado = dCliente.RestablecerClave(IdCliente, Recursos.ConvertirSHA256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña reestablecida";
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