using Crud.web.Data;
using Crud.web.Models;
using Crud.web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Crud.web.Controllers
{
    [Authorize] // Ensures only logged-in users can access this controller
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<Users> userManager;

        public PetsController(ApplicationDbContext dbContext, UserManager<Users> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Login", "Account");

                if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewModel.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ImageFile.CopyToAsync(fileStream);
                    }

                    viewModel.ImagePath = "/uploads/" + uniqueFileName;
                }

                var pet = new Pet
                {
                    Id = Guid.NewGuid(),
                    PetCatagory = viewModel.PetCatagory,
                    PetAge = viewModel.PetAge,
                    Description = viewModel.Description,
                    Phone = viewModel.Phone,
                    ImagePath = viewModel.ImagePath,
                    UserId = user.Id // Associate pet with the logged-in user
                };

                await dbContext.Pets.AddAsync(pet);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Show only pets added by the logged-in user
            var userPets = await dbContext.Pets
                                          .Where(p => p.UserId == user.Id)
                                          .ToListAsync();

            return View(userPets);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await userManager.GetUserAsync(User);
            var pet = await dbContext.Pets.FindAsync(id);

            if (pet == null || pet.UserId != user.Id) return NotFound();

            return View(pet);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Pet viewModel)
        {
            var user = await userManager.GetUserAsync(User);
            var pet = await dbContext.Pets.FindAsync(viewModel.Id);

            if (pet == null || pet.UserId != user.Id) return NotFound();

            pet.PetCatagory = viewModel.PetCatagory;
            pet.PetAge = viewModel.PetAge;
            pet.Phone = viewModel.Phone;
            pet.Description = viewModel.Description;

            if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewModel.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.ImageFile.CopyToAsync(fileStream);
                }

                pet.ImagePath = "/uploads/" + uniqueFileName;
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.GetUserAsync(User);
            var pet = await dbContext.Pets.FindAsync(id);

            if (pet == null || pet.UserId != user.Id) return NotFound();

            dbContext.Pets.Remove(pet);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }


      
        [HttpPost]
        public async Task<IActionResult> Adopt(Guid petId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var pet = await dbContext.Pets.FindAsync(petId);
            if (pet == null)
            {
                return NotFound("Pet not found.");
            }

            if (pet.IsAdopted)
            {
                return BadRequest("Pet is already adopted.");
            }

            
            Console.WriteLine($"Before Update - Pet ID: {pet.Id}, IsAdopted: {pet.IsAdopted}, AdoptedByUserId: {pet.AdoptedByUserId}");

            pet.IsAdopted = true;
            pet.AdoptedByUserId = user.Id; 
            
            dbContext.Pets.Update(pet);  
            await dbContext.SaveChangesAsync();

            
            Console.WriteLine($"After Update - Pet ID: {pet.Id}, IsAdopted: {pet.IsAdopted}, AdoptedByUserId: {pet.AdoptedByUserId}");

            return RedirectToAction("AdoptedPets");
        }


        [HttpGet]
        public IActionResult AdoptedPets()
        {
            var adoptedPets = dbContext.Pets.Where(p => p.IsAdopted).ToList();

       
            var petViewModels = adoptedPets.Select(pet => new PetViewModel
            {
                Id = pet.Id,
                PetCatagory = pet.PetCatagory,
                PetAge = pet.PetAge,
                Description = pet.Description,
                Phone = pet.Phone,
                ImagePath = pet.ImagePath
            }).ToList();

            return View("AdoptedPets", petViewModels);  
        }

    }
}


