using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.BusinessLayer
{
    public class Marca
    {
        DataLayer.Marca dMarca = new DataLayer.Marca();

        public List<EntityLayer.Marca> Listar()
        {
            return dMarca.Listar();
        }

        public int Registrar(EntityLayer.Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                Mensaje = "La descripción de la marca no puede ser vacía";

            if (string.IsNullOrEmpty(Mensaje))
                return dMarca.Registrar(obj, out Mensaje);
            else
                return 0;
        }

        public bool Editar(EntityLayer.Marca obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                mensaje = "La descripción de la marca no puede ser vacía";

            if (string.IsNullOrEmpty(mensaje))
                return dMarca.Editar(obj, out mensaje);
            else
                return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return dMarca.Eliminar(id, out mensaje);
        }
    }
}
