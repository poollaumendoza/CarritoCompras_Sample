using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoCompras.EntityLayer;
using System.Data;
using System.Data.SqlClient;

namespace CarritoCompras.DataLayer
{
    public class Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    string query = "Select IdUsuario, Nombres, Apellidos, Correo, Clave, Restablecer, Activo from Usuario";
                    SqlCommand Cmd = new SqlCommand(query, Cnx);

                    Cnx.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while(Dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(Dr["IdUsuario"]),
                                Nombres = Dr["Nombres"].ToString(),
                                Apellidos = Dr["Apellidos"].ToString(),
                                Correo = Dr["Correo"].ToString(),
                                Clave = Dr["Clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(Dr["Restablecer"]),
                                Activo = Convert.ToBoolean(Dr["Activo"]),

                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuario>();
            }

            return lista;
        }

        public int Registrar(Usuario obj, out string mensaje)
        {
            int IdAutogenerado = 0;

            mensaje = string.Empty;
            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_RegistrarUsuario", Cnx);
                    Cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    Cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    Cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    Cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    Cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Cnx.Open();

                    IdAutogenerado = Convert.ToInt32(Cmd.Parameters["Resultado"].Value);
                    mensaje = Cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdAutogenerado = 0;
                mensaje = ex.Message;
            }
            return IdAutogenerado;
        }

        public bool Editar(Usuario obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection Cnx = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_EditarUsuario", Cnx);
                    Cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    Cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    Cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    Cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    Cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Cnx.Open();

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
                    SqlCommand Cmd = new SqlCommand("Delete Usuario where IdUsuario = @IdUsuario", Cnx);
                    Cmd.Parameters.AddWithValue("@IdUsuario", id);
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