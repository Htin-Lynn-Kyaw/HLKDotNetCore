using HLKDotNetCore.NLayer.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace HLKDotNetCore.NLayer.DataAccess.DataBase
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
