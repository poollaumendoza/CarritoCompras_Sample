using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.BusinessLayer
{
    public class Producto
    {
        DataLayer.Producto dProducto = new DataLayer.Producto();

        public List<EntityLayer.Producto> Listar()
        {
            return dProducto.Listar();
        }

        public int Registrar(EntityLayer.Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                Mensaje = "El nombre del Producto no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                Mensaje = "La descripción del Producto no puede ser vacío";
            else if (obj.oMarca.IdMarca == 0)
                Mensaje = "Debe seleccionar una marca";
            else if (obj.oCategoria.IdCategoria == 0)
                Mensaje = "Debe seleccionar una categoría";
            else if (obj.Precio == 0)
                Mensaje = "Debe ingresar el precio del producto";
            else if (obj.Stock == 0)
                Mensaje = "Debe ingresar el stock del producto";

            if (string.IsNullOrEmpty(Mensaje))
                return dProducto.Registrar(obj, out Mensaje);
            else
                return 0;
        }

        public bool GuardarDatosImagen(EntityLayer.Producto oProducto, out string Mensaje)
        {
            return dProducto.GuardarDatosImagen(oProducto, out Mensaje);
        }    

        public bool Editar(EntityLayer.Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                Mensaje = "El nombre del Producto no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                Mensaje = "La descripción del Producto no puede ser vacío";
            else if (obj.oMarca.IdMarca == 0)
                Mensaje = "Debe seleccionar una marca";
            else if (obj.oCategoria.IdCategoria == 0)
                Mensaje = "Debe seleccionar una categoría";
            else if (obj.Precio == 0)
                Mensaje = "Debe ingresar el precio del producto";
            else if (obj.Stock == 0)
                Mensaje = "Debe ingresar el stock del producto";

            if (string.IsNullOrEmpty(Mensaje))
                return dProducto.Editar(obj, out Mensaje);
            else
                return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return dProducto.Eliminar(id, out mensaje);
        }
    }
}