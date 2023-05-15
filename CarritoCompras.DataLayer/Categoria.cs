using CarritoCompras.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.DataLayer
{
    public class Categoria
    {
        public List<EntityLayer.Categoria> Listar()
        {
            List<EntityLayer.Categoria> lista = new List<EntityLayer.Categoria>();

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "Select IdCategoria, Descripcion, Activo from Categoria";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);

                    Cnx.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while (Dr.Read())
                        {
                            lista.Add(new EntityLayer.Categoria()
                            {
                                IdCategoria = Convert.ToInt32(Dr["IdCategoria"]),
                                Descripcion = Dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(Dr["Activo"]),

                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EntityLayer.Categoria>();
            }

            return lista;
        }

        public int Registrar(EntityLayer.Categoria obj, out string Mensaje)
        {
            int IdAutogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_RegistrarCategoria", Cnx);
                    Cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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

        public bool Editar(EntityLayer.Categoria obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_EditarCategoria", Cnx);
                    Cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    Cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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

        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("Delete Categoria where IdCategoria = @IdCategoria", Cnx);
                    Cmd.Parameters.AddWithValue("@IdCategoria", id);
                    Cmd.CommandType = CommandType.Text;
                    Cnx.Open();
                    resultado = Cmd.ExecuteNonQuery() > 0 ? true : false;
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