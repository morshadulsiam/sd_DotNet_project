using System.Diagnostics;
using Crud.web.Data;
using Crud.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var allPets = await dbContext.Pets.Join(dbContext.Users,
              pet => pet.UserId,
              user => user.Id,
              (pet, user) => new PetViewModel {
                  Id = pet.Id,
                  PetCatagory = pet.PetCatagory,
                  PetAge = pet.PetAge,
                  Description = pet.Description,
                  Phone = pet.Phone,
                  ImagePath = pet.ImagePath,
                  UserFullName = user.FullName, // Get FullName instead of UserId
                  UserEmail = user.Email

              }).ToListAsync();


            return View(allPets);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var pets = await dbContext.Pets
                                      .Where(p => p.PetCatagory.Contains(query))
                                      .ToListAsync();
            return View("Search", pets); // Render Search.cshtml with the filtered pets
        }

        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
