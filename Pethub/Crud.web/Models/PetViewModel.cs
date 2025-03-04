

namespace Crud.web.Models
{
    public class PetViewModel
    {
        public Guid Id { get; set; }
        public string PetCatagory { get; set; }
        public string PetAge { get; set; }
        public string Description { get; set; }
        public int Phone { get; set; }

        public IFormFile? ImageFile { get; set; } // For image upload
        public string? ImagePath { get; set; } // To store file path in database

        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }

        public bool IsAdopted { get; set; }
    }
}

