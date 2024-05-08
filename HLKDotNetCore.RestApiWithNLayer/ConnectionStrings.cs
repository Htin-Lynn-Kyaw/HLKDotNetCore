using System.Data.SqlClient;

namespace HLKDotNetCore.RestApiWithNLayer
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
