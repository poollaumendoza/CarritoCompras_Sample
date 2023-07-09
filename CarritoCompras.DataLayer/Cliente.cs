using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.DataLayer
{
    public class Cliente
    {
        public int Registrar(EntityLayer.Cliente obj, out string Mensaje)
        {
            int IdAutogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_RegistrarCliente", Cnx);
                    Cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    Cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    Cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    Cmd.Parameters.AddWithValue("Clave", obj.Clave);
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

        public List<EntityLayer.Cliente> Listar()
        {
            List<EntityLayer.Cliente> lista = new List<EntityLayer.Cliente>();

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "Select IdCliente, Nombres, Apellidos, Correo, Clave, Reestablecer from Cliente";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);

                    Cnx.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while (Dr.Read())
                        {
                            lista.Add(new EntityLayer.Cliente()
                            {
                                IdCliente = Convert.ToInt32(Dr["IdCliente"]),
                                Nombres = Dr["Nombres"].ToString(),
                                Apellidos = Dr["Apellidos"].ToString(),
                                Correo = Dr["Correo"].ToString(),
                                Clave = Dr["Clave"].ToString(),
                                Restablecer = Convert.ToBoolean(Dr["Reestablecer"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EntityLayer.Cliente>();
            }

            return lista;
        }

        

        public bool Editar(EntityLayer.Cliente obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_EditarCliente", Cnx);
                    Cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    Cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    Cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    Cmd.Parameters.AddWithValue("Correo", obj.Correo);
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
                    SqlCommand Cmd = new SqlCommand("Delete Cliente where IdCliente = @IdCliente", Cnx);
                    Cmd.Parameters.AddWithValue("@IdCliente", id);
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

        public bool CambiarClave(int IdCliente, string NuevaClave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "Update Cliente set Clave = @NuevaClave, Restablecer = 0 where IdCliente = @IdCliente";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);
                    Cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                    Cmd.Parameters.AddWithValue("@NuevaClave", NuevaClave);
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

        public bool RestablecerClave(int IdCliente, string Clave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "Update Cliente set Clave = @Clave, Restablecer = 1 where IdCliente = @IdCliente";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);
                    Cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                    Cmd.Parameters.AddWithValue("@Clave", Clave);
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