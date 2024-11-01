using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRM392_BookSoccerYard.API.DTO.Order;
using PRM392_BookSoccerYard.API.Models;

namespace PRM392_BookSoccerYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PRM392_BookSoccerYardContext _context;
        private readonly IMapper _mapper;
        public OrdersController(PRM392_BookSoccerYardContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var result = await _context.Orders.ToListAsync();
            return _mapper.Map<List<OrderDTO>>(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(CreatedOrder orderDTO)
        {
            var payment = _mapper.Map<Payment>(orderDTO.payment);
            var order = _mapper.Map<Order>(orderDTO);
            var slot = await _context.Slots.Where(x=>x.Id== order.SlotId).FirstOrDefaultAsync();
            if (slot == null)
            {
                throw new Exception("Not found Slots");
            }
            else
            {
                order.StartTime = slot.StartTime;
                order.EndTime = slot.EndTime;
            }
            order.CreateDate = DateTime.Now;
            if (payment.Status == "Coc")
            {
                order.Status = StatusOrder.DaCoc.ToString();
            }
            else
            if (payment.Status == "Tong")
            {
                order.Status = StatusOrder.DaThanhToan.ToString();
            }
            else
            {
                order.Status = StatusOrder.Fail.ToString();
            }
            order.Payments.Add(payment);
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, _mapper.Map<OrderDTO>(order));
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
        [HttpGet("dashboard/price")]
        public async Task<IActionResult> GetDashboardPrice()
        {
            var list = await _context.Orders.Where(x=>x.Status==StatusOrder.DaThanhToan.ToString())
                .GroupBy(e => e.CreateDate.Value.Date)
                .Select(x => new DashboardDTO
                {
                    Day = x.Key.Date,
                    Quantity = x.Sum(x => x.TotalPrice.Value)
                })
                .ToListAsync();
            return Ok(list);
        }
        [HttpGet("dashboard/quantity")]
        public async Task<IActionResult> GetDashboardQuantity()
        {
            var list = await _context.Orders.Where(x => x.Status == StatusOrder.DaThanhToan.ToString())
                .GroupBy(e => e.CreateDate.Value.Date)
                .Select(x => new DashboardDTO
                {
                    Day = x.Key.Date,
                    Quantity = x.Count()
                })
                .ToListAsync();
            return Ok(list);
        }
    }
}
