using HLKDotNetCore.RestApiWithNLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace HLKDotNetCore.RestApiWithNLayer.DataBase
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
