using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace HLKDotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly int _maxColumnWidth = 20;
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-2A3IE1L\\SQLEXPRESS",
            InitialCatalog = "DotNetTraing",
            UserID = "sa",
            Password = "sasa"
        };
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open.");

            string query = "select * from tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            Console.WriteLine("Connection Close.");

            #region Tabular Form
            Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |", "ID", "Title", "Author", "Content");
            Console.WriteLine(new string('-', (_maxColumnWidth + 4) * 4));
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |",
                        row[0].ToString().Substring(0, Math.Min(_maxColumnWidth, row[0].ToString().Length)),
                        row[1].ToString().Substring(0, Math.Min(_maxColumnWidth, row[1].ToString().Length)),
                        row[2].ToString().Substring(0, Math.Min(_maxColumnWidth, row[2].ToString().Length)),
                        row[3].ToString().Substring(0, Math.Min(_maxColumnWidth, row[3].ToString().Length)));
            }
            #endregion

        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle,
           @BlogAuthor,
           @BlogContent)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            string message = result > 0 ? "Insert Successful" : "Insert Failed";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[tbl_Blog]
                SET [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
                WHERE BlogID = @BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            Console.WriteLine(message);
        }
        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[tbl_Blog]
                            WHERE BlogID = @BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            Console.WriteLine(message);
        }
        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open.");

            string query = @"select * from tbl_Blog where BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            connection.Close();
            DataRow row= dt.Rows[0];
            Console.WriteLine("Connection Close.");

            #region Tabular Form
            Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |", "ID", "Title", "Author", "Content");
            Console.WriteLine(new string('-', (_maxColumnWidth + 4) * 4));
            Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |",
                        row[0].ToString().Substring(0, Math.Min(_maxColumnWidth, row[0].ToString().Length)),
                        row[1].ToString().Substring(0, Math.Min(_maxColumnWidth, row[1].ToString().Length)),
                        row[2].ToString().Substring(0, Math.Min(_maxColumnWidth, row[2].ToString().Length)),
                        row[3].ToString().Substring(0, Math.Min(_maxColumnWidth, row[3].ToString().Length)));
            #endregion
        }
    }
}
