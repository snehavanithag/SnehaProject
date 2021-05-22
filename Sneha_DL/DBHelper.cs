using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sneha_DL
{
    public class DBHelper
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();

        public DataSet GetData(string storedprocedure, Dictionary<string, dynamic> parameters = null)
        {

            DataSet dataset=new DataSet();
            using (SqlConnection connection = new SqlConnection(@ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedprocedure;

                if(parameters != null)
                {
                    foreach (KeyValuePair<string, dynamic> item in parameters)
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataset);
            }
            return dataset;
        }

        public void  UpdateData(string storedprocedure, Dictionary<string,dynamic> parameters)
        {

            using (SqlConnection connection = new SqlConnection(@ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedprocedure;

                if(parameters != null)
                {
                    foreach(KeyValuePair<string,dynamic> item in parameters)
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
           
        }
    }
}
