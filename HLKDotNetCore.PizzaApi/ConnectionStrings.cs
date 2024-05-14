using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLKDotNetCore.PizzaApi
{
    internal static class ConnectionStrings
    {
        public static readonly SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-2A3IE1L\\SQLEXPRESS",
            InitialCatalog = "DotNetTraing",
            UserID = "sa",
            Password = "sasa",
            TrustServerCertificate = true
        };
    }
}
