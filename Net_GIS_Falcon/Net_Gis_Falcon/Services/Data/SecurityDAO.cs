using Microsoft.Data.SqlClient;
using Net_Gis_Falcon.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Net_Gis_Falcon.Services.Data
{
    public class SecurityDAO
    {
        internal bool FindByUser(Usuario user)
        {
            bool success = false;
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = BdConnection.connectionString;
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.Contraseña));

                //get hash result after compute it  
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilder.Append(result[i].ToString("x2"));
                }

                user.Contraseña = strBuilder.ToString();
                string query = "SELECT * FROM usuarios where email='" + user.Email.ToString() + "' AND contraseña='" + user.Contraseña.ToString() + "'";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Console.WriteLine(cmd.ExecuteNonQuery());
                Console.WriteLine(cmd.CommandText.ToString());
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //NpgsqlDataReader rd = cmd.ExecuteReader();
                da.Fill(dt);
                Console.WriteLine(dt.Rows[0][0].ToString());
                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine(dt);
                    user.IdPersona = int.Parse(dt.Rows[0][0].ToString());
                    Console.WriteLine(user.IdPersona);
                    success = true;

                }
                else
                {
                    success = false;
                }


                cmd.Dispose();
                connection.Close();
            }

            return success;
        }

        internal bool FindById(Usuario user)
        {
            bool success = false;
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = BdConnection.connectionString;
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                
                string query = "SELECT * FROM usuarios where id_persona='" + user.IdPersona + "'";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Console.WriteLine(cmd.ExecuteNonQuery());
                Console.WriteLine(cmd.CommandText.ToString());
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //NpgsqlDataReader rd = cmd.ExecuteReader();
                da.Fill(dt);
                Console.WriteLine(dt.Rows[0][0].ToString());
                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine(dt);
                    user.Nombre = dt.Rows[0][1].ToString();
                    user.Apellido = dt.Rows[0][2].ToString();
                    user.Email = dt.Rows[0][3].ToString();
                    user.Genero = dt.Rows[0][4].ToString();
                    user.Idioma = dt.Rows[0][5].ToString();
                    user.Foto = dt.Rows[0][7].ToString();
                    user.Municipio = dt.Rows[0][8].ToString();
                    user.FechaNacimiento = DateTime.Parse(dt.Rows[0][9].ToString());
                    success = true;

                }
                else
                {
                    success = false;
                }


                cmd.Dispose();
                connection.Close();
            }

            return success;
        }

        internal bool FindByEmail(Usuario user)
        {
            bool success = false;
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = BdConnection.connectionString;
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;

                string query = "SELECT * FROM usuarios where email='" + user.Email + "'";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Console.WriteLine(cmd.ExecuteNonQuery());
                Console.WriteLine(cmd.CommandText.ToString());
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //NpgsqlDataReader rd = cmd.ExecuteReader();
                da.Fill(dt);
                Console.WriteLine(dt.Rows[0][0].ToString());
                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine(dt);
                    user.IdPersona = int.Parse(dt.Rows[0][0].ToString());
                    user.Nombre = dt.Rows[0][1].ToString();
                    user.Apellido = dt.Rows[0][2].ToString();
                    user.Genero = dt.Rows[0][4].ToString();
                    user.Idioma = dt.Rows[0][5].ToString();
                    user.Foto = dt.Rows[0][7].ToString();
                    user.Municipio = dt.Rows[0][8].ToString();
                    user.FechaNacimiento = DateTime.Parse(dt.Rows[0][9].ToString());
                    success = true;

                }
                else
                {
                    success = false;
                }


                cmd.Dispose();
                connection.Close();
            }

            return success;
        }

        internal bool InsertAndCreateCookie(Usuario user)
        {
            try
            {
                /* Insertion After Validations*/
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = BdConnection.connectionString;
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "Insert into usuarios(nombre,apellido,email,genero,idioma,contraseña,foto,municipio,fecha_nacimiento) values(@nombre,@apellido,@email,@genero,@idioma,@contraseña,@foto,@municipio,@fecha_nacimiento)";
                    cmd.CommandType = CommandType.Text;

                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.Contraseña));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilder.Append(result[i].ToString("x2"));
                    }

                    user.Contraseña = strBuilder.ToString();

                    cmd.Parameters.Add(new NpgsqlParameter("@nombre", user.Nombre.ToString()));
                    cmd.Parameters.Add(new NpgsqlParameter("@apellido", user.Apellido.ToString()));
                    cmd.Parameters.Add(new NpgsqlParameter("@email", user.Email.ToString()));
                    cmd.Parameters.Add(new NpgsqlParameter("@genero", user.Genero.ToString()));
                    cmd.Parameters.Add(new NpgsqlParameter("@idioma", user.Idioma.ToString()));
                    cmd.Parameters.Add(new NpgsqlParameter("@contraseña", user.Contraseña.ToString()));
                    
                    if (user.Foto != null)
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("@foto", user.Foto.ToString()));
                    }
                    else
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("@foto", ""));
                    }
                    
                    cmd.Parameters.Add(new NpgsqlParameter("@municipio", user.Municipio.ToString()));
                    cmd.Parameters.Add(new NpgsqlParameter("@fecha_nacimiento", user.FechaNacimiento));
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                return false;
            }
        }
    }
}
