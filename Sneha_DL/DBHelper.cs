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

        public Dictionary<string, dynamic> AddData(string storedprocedure, Dictionary<string,dynamic> parameters)
        {
            Dictionary<string, dynamic> OutPutList = new Dictionary<string, dynamic>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedprocedure;
                

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, dynamic> item in parameters)
                    {
                        if(Convert.ToString(item.Value) != "Output")
                             command.Parameters.AddWithValue(item.Key, item.Value);

                        else
                        {
                            SqlParameter outputParam = new SqlParameter();
                            outputParam.ParameterName = item.Key;
                            var value = item.Value;

                            if(value is int)
                                 outputParam.SqlDbType = SqlDbType.Int;

                            if (value is string)
                            {
                                outputParam.SqlDbType = SqlDbType.VarChar;
                                outputParam.Size = 20;
                            }

                            outputParam.Direction = ParameterDirection.Output;
                            command.Parameters.Add(outputParam);
                        }
                    }
                }

                connection.Open();
                command.ExecuteNonQuery();

                var OutPutKeys = parameters.Where(x => Convert.ToString(x.Value) == "Output").ToList();

                foreach(var item in OutPutKeys)
                {
                    OutPutList.Add(item.Key,command.Parameters[item.Key].Value);
                }
                return OutPutList;
            }
        }
    }
}
