using HLKDotNetCore.PizzaApi.DataBase;
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
        public PizzaController()
        {
            _context = new AppDbContext();
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
        [HttpPost("Order")]
        public async Task<IActionResult> GetOrder(OrderRequest request)
        {
            var pizza = _context.Pizza.FirstOrDefault(x => x.PizzaID == request.PizzaID)!;
            var total = pizza.Price;
            if(request.Extras.Length > 0) 
            {
                var lstExtra =await _context.PizzaExtra.Where(x => request.Extras.Contains(x.PizzaExtraID)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrder pizzaOrder = new PizzaOrder()
            {
                PizzaID = request.PizzaID,
                PizzaOrderInvoiceNo = invoiceNo,
                Total = total
            };
            List<PizzaOrderDetail> pizzaOrderDetail = request.Extras.Select(x => new PizzaOrderDetail() 
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
