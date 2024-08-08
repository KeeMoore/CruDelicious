using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CruDelicious.Models;
using CruDelicious.Context;

namespace CruDelicious.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        private void AddNewDish()
        {
            var NewDish = new Dish()
            {
                Name = "Vegan Pizza",
                ChefName = "Kermit The Frog",
                Tastiness = 5,
                Calories = 65,
                Description = "Best plant based pizza you'll ever taste!"
            };
            _context.Dishes.Add(NewDish);
            _context.SaveChanges();
        }

        [HttpGet]
        public IActionResult NewDish()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDish(Dish dish)
        {
            if (ModelState.IsValid)
            {
                // Save the dish to the database
                _context.Dishes.Add(dish);
                _context.SaveChanges();

                // Redirect to the Index action
                return RedirectToAction("Index");
            }

                 // If the model state is not valid, return the same view with validation errors
                return View("NewDish", dish);
        }

        // Index
        [HttpGet("")]
        public IActionResult Index()
        {
            // AddNewDish();
            var dishes = _context.Dishes.ToList();
            List<Dish> AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
            return View("Index", AllDishes);
        }

        [HttpGet("Dishes/new")]
        public ViewResult CreateNewDish()
        {
            return View("NewDish");
        }

        // New Dish
        [HttpGet("Dishes/Create")]
        public IActionResult Dish()
        {
            return View("NewDish");
        }

        // Create Dish
        [HttpPost("Dishes/Create")]
        public IActionResult Create(Dish dish)
        {
            Console.WriteLine("~~~~~~~~~~~~~TEST1~~~~~~~~~");
            if (ModelState.IsValid)
            {
                Console.WriteLine("~~~~~~~~~~~~~TEST2~~~~~~~~~");
                _context.Add(dish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("~~~~~~~~~~~~~TEST3~~~~~~~~~");
                return View("NewDish");
            }
        }

        // View Dish Details
        [HttpGet("Dishes/{id}")]
        public IActionResult ViewDish(int id)
        {
            Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
            if (dish == null)
            {
                return RedirectToAction("Index");
            }
            return View("DishDetail", dish);
        }

        // Edit Dish
        [HttpGet("Dishes/Edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dish == null)
            {
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        [HttpPost("Dishes/{dishId}/Update")]
        public IActionResult UpdateDish(int dishId, Dish updatedDish)
        {
            Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dish == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                dish.Name = updatedDish.Name;
                dish.ChefName = updatedDish.ChefName;
                dish.Tastiness = updatedDish.Tastiness;
                dish.Calories = updatedDish.Calories;
                dish.Description = updatedDish.Description;
                dish.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditDish", dish);
            }
        }

        // Delete Dish
        [HttpPost("Dishes/{dishId}/Delete")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dish != null)
            {
                   Console.WriteLine("~~~~~~~~~~~~~TESTA~~~~~~~~~");
                _context.Dishes.Remove(dish);
                _context.SaveChanges();
            }
               Console.WriteLine("~~~~~~~~~~~~~TESTB~~~~~~~~~");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
