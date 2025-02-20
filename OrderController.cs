using FrothyFrolics_server_.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FrothyFrolics_server_.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //[HttpGet("getbyemail")]
        public IActionResult GetByEmail([FromQuery] string emailId)
        {
            var orders = _orderRepository.GetByEmail(emailId);
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Json(orders);
        }

        //[HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Json(order);
        }

        //[HttpPost("addorder")]
        public IActionResult AddOrder([FromBody] Order order)
        {

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _orderRepository.Add(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return Ok(order);
        }

        //[HttpPut("updateorder")]
        public IActionResult UpdateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _orderRepository.Update(order);
            return Ok(order);
        }

        //[HttpDelete("deleteorder/{id}")]
        [HttpPost]
        public IActionResult DeleteOrderByEmail([FromBody] string email)
        {
            
            try
            {
                _orderRepository.Delete(email);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return Ok(email);
        }
    }
}
