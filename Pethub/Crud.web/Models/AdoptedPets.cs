namespace Crud.web.Models
{
    public class AdoptedPets
    {
        public Guid Id { get; set; }
        public string PetCatagory { get; set; }
        public string PetAge { get; set; }
        public string Description { get; set; }
        public int Phone { get; set; }

        public IFormFile? ImageFile { get; set; } // For image upload
        public string? ImagePath { get; set; }
    }
}
