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
        DataLayer.Usuarios dUsuario = new DataLayer.Usuarios();

        public List<EntityLayer.Usuario> Listar()
        {
            return dUsuario.Listar();
        }

        public int Registrar(EntityLayer.Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                mensaje = "El nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                mensaje = "El nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                mensaje = "El nombre del usuario no puede ser vacío";

            if (string.IsNullOrEmpty(mensaje)) {
                Recursos.ConvertirSHA256("test123");
                return dUsuario.Registrar(obj, out mensaje);
            }
            else
                return 0;
        }

        public bool Editar(EntityLayer.Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                mensaje = "El nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                mensaje = "El nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                mensaje = "El nombre del usuario no puede ser vacío";

            if (string.IsNullOrEmpty(mensaje))
                return dUsuario.Editar(obj, out mensaje);
            else
                return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return dUsuario.Eliminar(id, out mensaje);
        }
    }
}