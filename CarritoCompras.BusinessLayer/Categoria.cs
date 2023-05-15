using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoCompras.EntityLayer;
using CarritoCompras.DataLayer;

namespace CarritoCompras.BusinessLayer
{
    public class Categoria
    {
        DataLayer.Categoria dCategoria = new DataLayer.Categoria();

        public List<EntityLayer.Categoria> Listar()
        {
            return dCategoria.Listar();
        }

        public int Registrar(EntityLayer.Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                Mensaje = "La descripción de la categoría no puede ser vacía";

            if (string.IsNullOrEmpty(Mensaje))
                return dCategoria.Registrar(obj, out Mensaje);
            else
                return 0;
        }

        public bool Editar(EntityLayer.Categoria obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                mensaje = "La descripción de la Categoria no puede ser vacía";

            if (string.IsNullOrEmpty(mensaje))
                return dCategoria.Editar(obj, out mensaje);
            else
                return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return dCategoria.Eliminar(id, out mensaje);
        }
    }
}