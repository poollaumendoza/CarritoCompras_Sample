using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CarritoCompras.DataLayer
{
    public class Producto
    {
        public List<EntityLayer.Producto> Listar()
        {
            List<EntityLayer.Producto> lista = new List<EntityLayer.Producto>();

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "Select p.IdProducto, p.Nombre, p.Descripcion, m.IdMarca, m.Descripcion[DesMarca], c.IdCategoria, " +
                        "c.Descripcion[DesCategoria], p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo from Producto p " +
                        "join Marca m on m.IdMarca = p.IdMarca join Categoria c on c.IdCategoria = p.IdCategoria";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);

                    Cnx.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while (Dr.Read())
                        {
                            lista.Add(new EntityLayer.Producto()
                            {
                                IdProducto = Convert.ToInt32(Dr["IdProducto"]),
                                Nombre = Dr["Nombre"].ToString(),
                                Descripcion = Dr["Descripcion"].ToString(),
                                oMarca = new EntityLayer.Marca()
                                {
                                    IdMarca = Convert.ToInt32(Dr["IdMarca"]), 
                                    Descripcion = Dr["DesMarca"].ToString()
                                },
                                oCategoria = new EntityLayer.Categoria()
                                { 
                                    IdCategoria = Convert.ToInt32(Dr["IdMarca"]), 
                                    Descripcion = Dr["DesCategoria"].ToString()
                                },
                                Precio = Convert.ToDecimal(Dr["Precio"], new CultureInfo("es-PE")),
                                Stock = Convert.ToInt32(Dr["Stock"]),
                                RutaImagen = Dr["RutaImagen"].ToString(),
                                NombreImagen = Dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(Dr["Activo"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EntityLayer.Producto>();
            }

            return lista;
        }

        public int Registrar(EntityLayer.Producto obj, out string Mensaje)
        {
            int IdAutogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_RegistrarProducto", Cnx);
                    Cmd.Parameters.AddWithValue("Nombre", obj.Descripcion);
                    Cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    Cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    Cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    Cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    Cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    Cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Cnx.Open();
                    Cmd.ExecuteNonQuery();

                    IdAutogenerado = Convert.ToInt32(Cmd.Parameters["Resultado"].Value);
                    Mensaje = Cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                IdAutogenerado = 0;
                Mensaje = ex.Message;
            }
            return IdAutogenerado;
        }

        public bool Editar(EntityLayer.Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_EditarProducto", Cnx);
                    Cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    Cmd.Parameters.AddWithValue("Nombre", obj.Descripcion);
                    Cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    Cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    Cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    Cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    Cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    Cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Cnx.Open();
                    Cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(Cmd.Parameters["Resultado"].Value);
                    mensaje = Cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }

        public bool GuardarDatosImagen(EntityLayer.Producto oProducto, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("Update Producto set RutaImagen = @RutaImagen, NombreImagen = @NombreImagen where IdProducto = @IdProducto", Cnx);
                    Cmd.Parameters.AddWithValue("@IdProducto", oProducto.IdProducto);
                    Cmd.Parameters.AddWithValue("@RutaImagen", oProducto.RutaImagen);
                    Cmd.Parameters.AddWithValue("@NombreImagen", oProducto.NombreImagen);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.Text;

                    Cnx.Open();
                    ;

                    if (Cmd.ExecuteNonQuery() > 0)
                        resultado = true;
                    else
                        Mensaje = "No se pudo actualizar imagen";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_EliminarProducto", Cnx);
                    Cmd.Parameters.AddWithValue("@IdProducto", id);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.Text;
                    
                    Cnx.Open();
                    Cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(Cmd.Parameters["Resultado"].Value);
                    mensaje = Cmd.Parameters["Mensaje"].ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }
    }
}