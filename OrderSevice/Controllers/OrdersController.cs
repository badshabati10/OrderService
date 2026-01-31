using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSevice.Data.Repositories;
using OrderSevice.DTOs;
using OrderSevice.Models;

namespace OrderSevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repo;

        public OrdersController(IOrderRepository repo)
        {
            _repo = repo;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _repo.GetAllAsync();
            return Ok(orders);
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto dto)
        {
            var order = new Order
            {
                OrderNumber = dto.OrderNumber,
                TotalAmount = dto.TotalAmount,
                Status = dto.Status,
                OrderDate = DateTime.UtcNow
            };

            await _repo.AddAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDto dto)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return NotFound();

            order.OrderNumber = dto.OrderNumber;
            order.TotalAmount = dto.TotalAmount;
            order.Status = dto.Status;

            await _repo.UpdateAsync(order);
            return NoContent();
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
