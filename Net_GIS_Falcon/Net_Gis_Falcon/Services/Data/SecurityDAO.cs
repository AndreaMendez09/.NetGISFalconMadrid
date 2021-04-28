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
                if (dt.Rows.Count > 0)
                {
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
    }
}
