using FrothyFrolics_server_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FrothyFrolics_server_.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public IActionResult FetchAll()
        {
            
            var foodItems = _foodRepository.GetAll();
            var json = JsonSerializer.Serialize(foodItems);
            
            return Json(foodItems);
        }
        
        public IActionResult GetByCategory([FromQuery] String category)
        {
            

            
            var foodItems = _foodRepository.GetByCategory(category);

            if (foodItems == null || !foodItems.Any())
            {
                
                return NotFound(); // Or return an empty JSON array
            }

            
            return Json(foodItems);
        }


        public IActionResult Details(int id)
        {
            var foodItem = _foodRepository.GetById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return View(foodItem);
        }

        [HttpPost]
        public IActionResult AddFoodItems([FromBody] List<FoodItem> foodItems)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }

            try
            {
                foreach (var item in foodItems)
                {

                    _foodRepository.Add(item);
                }
                return Ok(foodItems);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }




        [HttpPost]
        public IActionResult Create(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                _foodRepository.Add(foodItem);
                return RedirectToAction(nameof(Index));
            }
            return View(foodItem);
        }

        [HttpPost]
        public IActionResult Edit(int id, FoodItem foodItem)
        {
            if (id != foodItem.quantity)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _foodRepository.Update(foodItem);
                return RedirectToAction(nameof(Index));
            }
            return View(foodItem);
        }

        public IActionResult GetByTitle([FromQuery] string title)
        {
            Console.WriteLine($"---{title}---");
            
            var foodItem = _foodRepository.GetByTitle(title);
            Console.WriteLine(foodItem.imageResId);
            if (foodItem == null)
            {
                return NotFound();
            }
            
            return Json(foodItem);
        }

        public IActionResult Delete(int id)
        {
            var foodItem = _foodRepository.GetById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            _foodRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}