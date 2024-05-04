using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace HLKDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, AdoDotNetParameter[]? parameters = null) //params (Cannot assign default value)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Key, item.Value)).ToArray());
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            connection.Close();
            string json = JsonConvert.SerializeObject(dt); // c# to json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; // jason to c#

            return lst;

        }
        public T QueryFirstOrDefult<T>(string query, AdoDotNetParameter[]? parameters = null) //params (Cannot assign default value)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Key, item.Value)).ToArray());
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            connection.Close();
            string json = JsonConvert.SerializeObject(dt); // c# to json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; // jason to c#

            return lst[0];

        }
        public int Execute(string query, AdoDotNetParameter[]? parameters = null) //params (Cannot assign default value)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Key, item.Value)).ToArray());
            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            

            return result;

        }
    }
    public class AdoDotNetParameter
    {
        public AdoDotNetParameter()
        {
            
        }
        public AdoDotNetParameter(string key, object value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
