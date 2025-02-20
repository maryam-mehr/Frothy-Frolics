using FrothyFrolics_server_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections.Generic;
using System;
using System.Linq;

namespace FrothyFrolics_server_.Controllers
{
    //[Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartItemRepository;

        public CartController(ICartRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }


        //[HttpGet("getbyemail")]
        public IActionResult GetByEmail([FromQuery] string emailId)
        {
            var cartItems = _cartItemRepository.GetByEmail(emailId);
            

            if (cartItems == null || !cartItems.Any())
            {

                return Json(new List<CartItem>());
                
            }

            
            return Json(cartItems);
        }

        public IActionResult Details(int id)
        {
            var cartItem = _cartItemRepository.GetById(id);
            if (cartItem == null)
            {

                return Json(new CartItem());
            }
            return View(cartItem);
        }

        [HttpPost]
        public IActionResult AddCartItem([FromBody] CartItem cartItem)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {    
                var existingCartItem = _cartItemRepository.GetByTitle(cartItem.title,cartItem.email);
                if (existingCartItem != null)
                {
                    existingCartItem.quantity += 1;
                    _cartItemRepository.Update(existingCartItem);
                }
                else
                {  
                    _cartItemRepository.Add(cartItem);
                }
            }
                catch (Exception ex)
                {
                return StatusCode(500, "Internal server error");
            }
            return Ok(cartItem);
        }

        

        [HttpPost]
        public IActionResult Create(CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _cartItemRepository.Add(cartItem);
            }
            return View(cartItem);
        }

        [HttpPost]
        public IActionResult updateCartItem([FromBody] CartItem cartItem)
        {
            _cartItemRepository.Update(cartItem);
            return Ok(cartItem);
        }

        public IActionResult deleteCartItem([FromBody] CartItem cartItem)
        {
            _cartItemRepository.Delete(cartItem.title,cartItem.email);
            return Ok(cartItem);
        }

        
        public IActionResult deleteCartItemByEmail([FromBody] string email)
        {
            
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email cannot be null or empty.");
            }

            try
            {
                _cartItemRepository.DeleteCartItemByEmail(email);
                return Ok($"All cart items for email {email} have been deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
