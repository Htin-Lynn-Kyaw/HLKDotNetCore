using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLKDotNetCore.PizzaApi.DataBase
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<PizzaModel> Pizza { get; set; }
        public DbSet<PizzaExtraModel> PizzaExtra { get; set; }
        public DbSet<PizzaOrder> PizzaOrder { get; set; }
        public DbSet<PizzaOrderDetail> PizzaOrderDetail { get; set; }
    }

    [Table("Tbl_Pizza")]
    public class PizzaModel
    {
        [Key]
        public int PizzaID { get; set; }
        public string? Pizza { get; set; }
        public decimal Price { get; set; }
    }

    [Table("Tbl_PizzaExtra")]
    public class PizzaExtraModel
    {
        [Key]
        public int PizzaExtraID { get; set; }
        public string? PizzaExtra { get; set; }
        public decimal Price { get; set; }
    }
    [Table("Tbl_PizzaOrder")]
    public class PizzaOrder
    {
        [Key]
        public int PizzaOrderID { get; set; }
        public string? PizzaOrderInvoiceNo { get; set; }
        public int PizzaID { get; set; }
        public decimal Total { get; set; }
    }
    [Table("Tbl_PizzaOrderDetail")]
    public class PizzaOrderDetail
    {
        [Key]
        public int PizzaOrderDetailID { get; set; }
        public string? PizzaOrderInvoiceNo { get; set; }
        public int PizzaExtraID { get; set; }
    }
    public class OrderRequest
    {
        public int PizzaID { get; set; }
        public int[]? Extras { get; set; }
    }
    public class OrderResponse
    {
        public string? InvoiceNo { get; set; }
        public string? Message { get; set; }
        public decimal Total { get; set; }
    }
    public class PizzaOrderModel
    {
        public int PizzaOrderID { get; set; }
        public int PizzaID { get; set; }
        public string? PizzaOrderInvoiceNo { get; set; }
        public string? Pizza { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
    public class PizzaOrderDetailModel
    {
        public int PizzaOrderDetailID { get; set; }
        public string? PizzaOrderInvoiceNo { get; set; }
        public int PizzaExtraID { get; set; }
        public string? PizzaExtra { get; set; }
        public decimal Price { get; set; }
    }
    public class PizzaOrderHeaderAndDetail
    {
        public PizzaOrderModel? pom { get; set; }
        public List<PizzaOrderDetailModel>? podm { get; set; }
    }
}
