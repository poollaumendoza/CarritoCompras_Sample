using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.DataLayer
{
    public class Marca
    {
        public List<EntityLayer.Marca> Listar()
        {
            List<EntityLayer.Marca> lista = new List<EntityLayer.Marca>();

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "Select IdMarca, Descripcion, Activo from Marca";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);

                    Cnx.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while (Dr.Read())
                        {
                            lista.Add(new EntityLayer.Marca()
                            {
                                IdMarca = Convert.ToInt32(Dr["IdMarca"]),
                                Descripcion = Dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(Dr["Activo"]),

                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EntityLayer.Marca>();
            }

            return lista;
        }

        public int Registrar(EntityLayer.Marca obj, out string Mensaje)
        {
            int IdAutogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_RegistrarMarca", Cnx);
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

        public bool Editar(EntityLayer.Marca obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_EditarMarca", Cnx);
                    Cmd.Parameters.AddWithValue("IdMarca", obj.IdMarca);
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
                    SqlCommand Cmd = new SqlCommand("Delete Marca where IdMarca = @IdMarca", Cnx);
                    Cmd.Parameters.AddWithValue("@IdMarca", id);
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