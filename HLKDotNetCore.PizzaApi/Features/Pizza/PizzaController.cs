using HLKDotNetCore.PizzaApi.DataBase;
using HLKDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HLKDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly DapperService _dapper;
        public PizzaController()
        {
            _context = new AppDbContext();
            _dapper = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet("GetPizza")]
        public async Task<IActionResult> GetPizza()
        {
            var item = await _context.Pizza.ToListAsync();
            return Ok(item);
        }
        [HttpGet("GetPizzaExtra")]
        public async Task<IActionResult> GetPizzaExtra()
        {
            var item = await _context.PizzaExtra.ToListAsync();
            return Ok(item);
        }

        [HttpGet("GetOrderDapper/{invoiceNo}")]
        public IActionResult GetOrderDapper(string invoiceNo)
        {
            PizzaOrderModel order = _dapper.QueryFirstOrDefault<PizzaOrderModel>(
                        PizzaQueries.pizzaOrderConstant,
                        new PizzaOrderModel { PizzaOrderInvoiceNo = invoiceNo });

            List<PizzaOrderDetailModel> orderDetail = _dapper.Query<PizzaOrderDetailModel>(
                        PizzaQueries.pizzaOrderDetailConstant,
                        new PizzaOrderDetailModel { PizzaOrderInvoiceNo = invoiceNo });

            return Ok(
                      new PizzaOrderHeaderAndDetail
                      {
                          pom = order,
                          podm = orderDetail
                      }
                    );
        }

        [HttpGet("GetOrderEFCore/{invoiceNo}")]
        public IActionResult GetOrderEFCore(string invoiceNo)
        {
            //var item = await _context.PizzaOrder.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();
            PizzaOrderModel Order = (from po in _context.PizzaOrder
                                     join p in _context.Pizza on po.PizzaID equals p.PizzaID
                                     where po.PizzaOrderInvoiceNo == invoiceNo
                                     select new PizzaOrderModel()
                                     {
                                         PizzaID = p.PizzaID,
                                         PizzaOrderID = po.PizzaOrderID,
                                         PizzaOrderInvoiceNo = po.PizzaOrderInvoiceNo,
                                         Price = p.Price,
                                         Total = po.Total,
                                         Pizza = p.Pizza
                                     }).FirstOrDefault()!;

            List<PizzaOrderDetailModel> OrderDetail = (from pod in _context.PizzaOrderDetail
                                    join pe in _context.PizzaExtra on pod.PizzaExtraID equals pe.PizzaExtraID
                                    where pod.PizzaOrderInvoiceNo == invoiceNo
                                    select new PizzaOrderDetailModel()
                                    {
                                        PizzaExtra = pe.PizzaExtra,
                                        PizzaExtraID = pe.PizzaExtraID,
                                        PizzaOrderDetailID = pod.PizzaOrderDetailID,
                                        PizzaOrderInvoiceNo = pod.PizzaOrderInvoiceNo,
                                        Price = pe.Price
                                    }).ToList();

            return Ok(
                    new PizzaOrderHeaderAndDetail
                    {
                        pom = Order,
                        podm = OrderDetail,
                    }
                    );
        }
        [HttpPost("Order")]
        public async Task<IActionResult> GetOrder(OrderRequest orderRequest)
        {
            var pizza = _context.Pizza.FirstOrDefault(x => x.PizzaID == orderRequest.PizzaID)!;
            var total = pizza.Price;
            if (orderRequest.Extras is not null && orderRequest.Extras.Length > 0)
            {
                var lstExtra = await _context.PizzaExtra.Where(x => orderRequest.Extras.Contains(x.PizzaExtraID)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }
            var invoiceNo = "INV_" + DateTime.Now.ToString("ssmmHHddMMyyyy");
            PizzaOrder pizzaOrder = new PizzaOrder()
            {
                PizzaID = orderRequest.PizzaID,
                PizzaOrderInvoiceNo = invoiceNo,
                Total = total
            };
            List<PizzaOrderDetail> pizzaOrderDetail = orderRequest.Extras!.Select(x => new PizzaOrderDetail()
            {
                PizzaExtraID = x,
                PizzaOrderInvoiceNo = invoiceNo
            }).ToList();

            await _context.PizzaOrder.AddAsync(pizzaOrder);
            await _context.PizzaOrderDetail.AddRangeAsync(pizzaOrderDetail);
            await _context.SaveChangesAsync();

            OrderResponse orderResponse = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for your order! Enjoy!",
                Total = total
            };

            return Ok(orderResponse);
        }
    }
}
